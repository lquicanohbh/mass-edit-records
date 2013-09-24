using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CanceledAppointmentUpdate.AppointmentScheduling;
using CanceledAppointmentUpdate.UserManagement;
using System.Data.Odbc;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using CanceledAppointmentUpdate.PractitionerRegister;

namespace CanceledAppointmentUpdate
{
    public partial class CanceledAppointmentUpdate : Form
    {
        const string SystemCode = "LIVE";
        const string UserName = "ODBC";
        const string Password = "hotwire2011";
        List<FileType> FileTypeList = new List<FileType>();
        string selectedFileType = string.Empty;
        string update_Add_Delete = string.Empty;
        
        int count = 0;
        int total = 0;
       
        public CanceledAppointmentUpdate()
        {
            
            InitializeComponent();
            InitializeFileType();
            InitializeReadFileBackgroundWorker();
            uxReadFileButton.Enabled = false;
            uxCancelReadingFileButton.Enabled = false;
            uxFileNameBox.Enabled = false;
        }

        public void InitializeFileType()
        {
            FileTypeList.Add(new FileType { TypeName = "Appointments", NumberOfFields = 13, NumberOfFieldsRequired = 8 });
            FileTypeList.Add(new FileType { TypeName = "User Roles", NumberOfFields = 10, NumberOfFieldsRequired = 8 });
            FileTypeList.Add(new FileType { TypeName = "Practitioner/Staff", NumberOfFields = 9, NumberOfFieldsRequired = 9 });
        }

        private void InitializeReadFileBackgroundWorker()
        {
            readFileBackgroundWorker.WorkerReportsProgress = true;
            readFileBackgroundWorker.WorkerSupportsCancellation = true;
            readFileBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(readFileBackgroundWorker_RunWorkerCompleted);
            readFileBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(readFileBackgroundWorker_ProgressChanged);
            readFileBackgroundWorker.DoWork += new DoWorkEventHandler(readFileBackgroundWorker_DoWork);
        }

        private void readFileBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;
            switch (this.selectedFileType)
            {
                case "Appointments":
                    ProcessAppRecord((CustomAppointment)e.Argument, worker, e);
                    break;
                case "User Roles":
                    ProcessUserRecord((UserDefinitionObject)e.Argument, worker, e);
                    break;
                case "Practitioner/Staff":
                    ProcessPractitionerRecord((PractitionerRegistrationObject)e.Argument, worker, e);
                    break;
            }
        }

        void readFileBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            uxReadingFileProgressBar.Value = e.ProgressPercentage;
        }

        void readFileBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                uxFileStatusLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                uxFileStatusLabel.Text = "Error " + e.Error.Message;
            }
            else
            {
                if (e.Result != null)
                {
                    switch(this.selectedFileType)
                    {
                        case "Appointments":
                            var appresponse = (AppointmentScheduling.WebServiceResponse)e.Result;
                            uxResultTextBox.Text += appresponse.Message + "\r\n";
                            break;
                        case "User Roles":
                             var userresponse = (UserManagement.WebServiceResponse)e.Result;
                            uxResultTextBox.Text += userresponse.Message + "\r\n";
                            break;
                        case "Practitioner/Staff":
                            var practitionerResponse = (PractitionerRegister.WebServiceResponse)e.Result;
                            uxResultTextBox.Text += practitionerResponse.Message + "\r\n";
                            break;
                        default:
                            uxResultTextBox.Text += "Error: This Type is not supported.";
                            break;
                    }
                }
                else
                    uxResultTextBox.Text += "Record Failed! ";
            }
        }

        private void ProcessAppRecord(CustomAppointment appointment, BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                var response = FileAppointmentWebService(appointment);
                count++;
                int percent = (int)(Math.Ceiling(((double) count / total) * 100));
                worker.ReportProgress(percent, appointment);
                e.Result = response;
            }
            catch (Exception ex)
            {
                worker.CancelAsync();
            }
        }

        private void ProcessUserRecord(UserDefinitionObject user, BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                var response = FileUserWebService(user);
                count++;
                int percent = (int)(Math.Ceiling(((double)count / total) * 100));
                worker.ReportProgress(percent, user);
                e.Result = response;
            }
            catch (Exception ex)
            {
                worker.CancelAsync();
            }
        }

        private void ProcessPractitionerRecord(PractitionerRegistrationObject practitioner,BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                var response = FilePractitionerWebService(practitioner);
                count++;
                int percent = (int)(Math.Ceiling(((double)count / total) * 100));
                worker.ReportProgress(percent, practitioner);
                e.Result = response;
            }
            catch (Exception ex)
            {
                worker.CancelAsync();
            }
        }

        private List<CustomAppointment> ReadAppFile(string FileName)
        {
            try
            {
                string[] allText = File.ReadAllLines(FileName);
                return CreateAppointmentList(allText);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private List<UserDefinitionObject> ReadUserFile(string FileName)
        {
            try
            {
                string[] allText = File.ReadAllLines(FileName);
                return CreateUserList(allText);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private List<PractitionerRegistrationObject> ReadPractitionerFile(string FileName)
        {
            try
            {
                string[] allText = File.ReadAllLines(FileName);
                return CreatePractitionerList(allText);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private List<PractitionerRegistrationObject> CreatePractitionerList(string[] textFileLines)
        {
            FileType fileType = new FileType();
            fileType = FileTypeList.Find(f => f.TypeName == uxSelectFileType.Text.ToString());
            var practitionerList = new List<PractitionerRegistrationObject>();
            var worker = new BackgroundWorker();

            for (int i = 0; i < textFileLines.Length; i++)
            {
                var tempobj = textFileLines[i].Split('|');
                if (tempobj.Length == fileType.NumberOfFields)
                {
                    practitionerList.Add(new PractitionerRegistrationObject
                    {
                        Name = tempobj[0],
                        RegistrationDate = Convert.ToDateTime(tempobj[1]),
                        Discipline = tempobj[2],
                        CategoriesForCoverage = tempobj[3],
                        PractitionerCategory = tempobj[4],
                        OfficeAddressZipCode = tempobj[5],
                        OfficeAddressCity = tempobj[6],
                        OfficeAddressState = tempobj[7],
                        OfficeTelephone1 = tempobj[8]
                    });
                }
                else
                {
                    uxResultTextBox.Text += "Error in creating Practitioner List";
                }
            }
            total = practitionerList.Count();
            return practitionerList;
        }

        private List<CustomAppointment> CreateAppointmentList(string[] textFileLines)
        {
            FileType fileType = new FileType();
            fileType = FileTypeList.Find(f => f.TypeName == uxSelectFileType.Text.ToString());
            var appointmentList = new List<CustomAppointment>();
            var worker = new BackgroundWorker();
           
            CustomAppointment customApp = new CustomAppointment();
            AppointmentSchedulingObject appObj = new AppointmentSchedulingObject();
            appObj = customApp.AppointmentSchedulingObj;

            for (int i = 0; i < textFileLines.Length; i++)
            {
                var tempObj = textFileLines[i].Split('|');
                if (tempObj.Length == fileType.NumberOfFields)
                {
                    appointmentList.Add(new CustomAppointment
                    {
                        AppointmentSchedulingObj = new AppointmentSchedulingObject
                        {
                            ApptDate = DateTime.Parse(tempObj[0]),
                            ApptDateSpecified = true,
                            Site = tempObj[1],
                            ApptStartTime = tempObj[2],
                            ApptEndTime = tempObj[3],
                            ApptStatus = tempObj[4],
                            ServiceCode = tempObj[5],
                            ClientID = tempObj[6],
                            Program = tempObj[7],
                            NumberOfClients = tempObj[8],
                            Episode = Convert.ToInt64(tempObj[9]),
                            EpisodeSpecified = true,
                            ApptNotes = tempObj[10]
                        },
                        UniqueId = tempObj[11],
                        StaffId = tempObj[12]
                    });


                }
                else
                {
                    uxResultTextBox.Text = "Error in Creating Appointment List";
                }
            }
            total = appointmentList.Count();
            return appointmentList;
        }

        private List<UserDefinitionObject> CreateUserList(string[] textFileLines)
        {
            FileType fileType = new FileType();
            fileType = FileTypeList.Find(f => f.TypeName == uxSelectFileType.Text.ToString());
            var userList = new List<UserDefinitionObject>();
            var worker = new BackgroundWorker();

            for (int i = 0; i < textFileLines.Length; i++)
            {
                var tempObj = textFileLines[i].Split('|');
                if (tempObj.Length == fileType.NumberOfFields)
                {
                    userList.Add(new UserDefinitionObject
                    {
                        UserId = tempObj[0],
                        UserRoles = tempObj[1],
                        AllowUserRenewal = tempObj[2],
                        IsUserAStaffMember = tempObj[3],
                        IsUserAUnit = tempObj[4],
                        IsUserSystemAdministrator = tempObj[5],
                        StaffId = tempObj[6],
                        UserDescription = tempObj[7],
                        WarnNonCaseloadAccess = tempObj[8],
                        PasswordTermDurationDays = Convert.ToInt64(tempObj[9]),
                        PasswordTermDurationDaysSpecified = true
                    });
                }
                else
                {
                    uxResultTextBox.Text = "Error in Creating User List";
                }
            }
            total = userList.Count();
            return userList;
        }

        private AppointmentScheduling.WebServiceResponse FileAppointmentWebService(CustomAppointment appointment)
        {
            var fileWebService = new AppointmentScheduling.AppointmentScheduling();
            var response = fileWebService.AddAppointment(null, null, null, null, null);

            switch(this.update_Add_Delete)
            {
                case "Add":

                    var Addresponse = fileWebService.AddAppointment(SystemCode,
                        UserName,
                        Password,
                        appointment.AppointmentSchedulingObj,
                        appointment.StaffId);
                    fileWebService.Dispose();
                    return Addresponse;
                case "Update":
                    var Updateresponse = fileWebService.UpdateAppointment(SystemCode,
                        UserName,
                        Password,
                        appointment.AppointmentSchedulingObj,
                        appointment.StaffId,
                        appointment.UniqueId);
                    fileWebService.Dispose();
                    return Updateresponse;
                case "Delete":
                    var DeleteResponse = fileWebService.DeleteAppointment(SystemCode,
                        UserName,
                        Password,
                        appointment.UniqueId);
                    fileWebService.Dispose();
                    return DeleteResponse;
                default:
                    return response;
            }
        }

        private PractitionerRegister.WebServiceResponse FilePractitionerWebService(PractitionerRegistrationObject practitioner)
        {
            var fileWebService = new PractitionerRegister.WebServices();
            var response = fileWebService.PractitionerRegister(null, null, null, null, 0, false);

            switch (this.update_Add_Delete)
            {
                case "Update":
                    var UpdateResponse = fileWebService.EditPractitionerRegister(SystemCode,
                        UserName,
                        Password,
                        practitioner,
                        0,
                        false);
                    fileWebService.Dispose();
                    return UpdateResponse;
                case "Add":
                    var Addresponse = fileWebService.PractitionerRegister(SystemCode,
                        UserName,
                        Password,
                        practitioner,
                        0,
                        false);
                    fileWebService.Dispose();
                    return Addresponse;
                case "Delete":
                    throw new NotImplementedException("Cannot delete practitioners");
                default:
                    return response;
            }
        }

        private UserManagement.WebServiceResponse FileUserWebService(UserDefinitionObject user)
        {
            var fileWebService = new UserManagement.WebServices();
            var response = fileWebService.UpdateUser(null, null, null, null);

            if (!uxResetPasswordCheckBox.Checked)
            {
                switch (this.update_Add_Delete)
                {
                    case "Update":
                        var Updateresponse = fileWebService.UpdateUser(SystemCode,
                            UserName,
                            Password,
                            user);
                        fileWebService.Dispose();
                        return Updateresponse;
                    case "Add":
                        var Addresponse = fileWebService.CreateUser(SystemCode,
                            UserName,
                            Password,
                            user);
                        fileWebService.Dispose();
                        return Addresponse;
                    case "Delete":
                        var Deleteresponse = fileWebService.DeActivateUser(SystemCode,
                            UserName,
                            Password,
                            user.UserId);
                        fileWebService.Dispose();
                        return Deleteresponse;
                    default:
                        return response;
                }
            }
            else
            {
                var resetResponse = fileWebService.GeneratePassword(SystemCode,
                    UserName,
                    Password,
                    user.UserId);
                fileWebService.Dispose();

                /*add functionality to write to a file the user and their new password
                 * 
                 * 
                 * 
                 */

                return resetResponse;
            }
        }

        private void uxSelectFileButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                uxFileNameBox.Text = openFileDialog.FileName;
                uxReadFileButton.Enabled = true;
            }
            else
            {
                uxResultTextBox.Text = "Error: cannot read file, please try again";
            }
        }

        private void uxReadFileButton_Click(object sender, EventArgs e)
        {
            uxFileStatusLabel.Text = string.Empty;
            this.uxReadFileButton.Enabled = false;
            this.uxCancelReadingFileButton.Enabled = true;
            this.selectedFileType = uxSelectFileType.Text;
            this.update_Add_Delete = uxUpdateAddDeleteComboBox.Text;
            
            switch(this.selectedFileType)
            {

                case "Appointments":
                    var appointmentList = ReadAppFile(uxFileNameBox.Text);
                    BackgroundWorker Appbgw;

                    foreach (var appointment in appointmentList)
                    {
                        Appbgw = CreateBgw();
                        if (!Appbgw.IsBusy)
                        {
                            Appbgw.RunWorkerAsync(appointment);
                        }
                    }
                break;

                case "User Roles":
                    var userList = ReadUserFile(uxFileNameBox.Text);
                    BackgroundWorker userbgw;

                    foreach (var user in userList)
                    {
                        userbgw = CreateBgw();
                        if (!userbgw.IsBusy)
                        {
                            userbgw.RunWorkerAsync(user);
                        }
                    }
                break;
            }
            //uxResultTextBox.Text += "Loop finished";
        }

        private BackgroundWorker CreateBgw()
        {
            var bgw = new BackgroundWorker();
            bgw.DoWork += readFileBackgroundWorker_DoWork;
            bgw.ProgressChanged += readFileBackgroundWorker_ProgressChanged;
            bgw.RunWorkerCompleted += readFileBackgroundWorker_RunWorkerCompleted;
            bgw.WorkerSupportsCancellation = true;
            bgw.WorkerReportsProgress = true;
            return bgw;
        }

        private void uxCancelReadingFile_Click(object sender, EventArgs e)
        {
            this.uxCancelReadingFileButton.Enabled = false;
            this.uxReadFileButton.Enabled = true;
            if (readFileBackgroundWorker.WorkerSupportsCancellation == true)
            {
                readFileBackgroundWorker.CancelAsync();
            }
        }

        private void uxSelectFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uxSelectFileType.Text == "Appointments")
            {
                uxResetPasswordCheckBox.Enabled = false;
            }
            else if (uxSelectFileType.Text == "User Roles")
            {
                uxResetPasswordCheckBox.Enabled = true;
            }
        }
    }
}
