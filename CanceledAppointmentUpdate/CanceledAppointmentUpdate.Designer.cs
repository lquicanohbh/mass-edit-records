namespace CanceledAppointmentUpdate
{
    partial class CanceledAppointmentUpdate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uxSelectFileButton = new System.Windows.Forms.Button();
            this.uxFileNameBox = new System.Windows.Forms.TextBox();
            this.uxFileSelectedLabel = new System.Windows.Forms.Label();
            this.uxReadFileButton = new System.Windows.Forms.Button();
            this.uxCancelReadingFileButton = new System.Windows.Forms.Button();
            this.uxReadingFileLabel = new System.Windows.Forms.Label();
            this.uxReadingFileProgressBar = new System.Windows.Forms.ProgressBar();
            this.uxFileStatusLabel = new System.Windows.Forms.Label();
            this.readFileBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.processFileBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.uxSelectFileType = new System.Windows.Forms.ComboBox();
            this.uxResultTextBox = new System.Windows.Forms.TextBox();
            this.uxUpdateAddDeleteComboBox = new System.Windows.Forms.ComboBox();
            this.uxResetPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // uxSelectFileButton
            // 
            this.uxSelectFileButton.Location = new System.Drawing.Point(307, 12);
            this.uxSelectFileButton.Name = "uxSelectFileButton";
            this.uxSelectFileButton.Size = new System.Drawing.Size(291, 23);
            this.uxSelectFileButton.TabIndex = 0;
            this.uxSelectFileButton.Text = "Select File To Process";
            this.uxSelectFileButton.UseVisualStyleBackColor = true;
            this.uxSelectFileButton.Click += new System.EventHandler(this.uxSelectFileButton_Click);
            // 
            // uxFileNameBox
            // 
            this.uxFileNameBox.Location = new System.Drawing.Point(12, 104);
            this.uxFileNameBox.Name = "uxFileNameBox";
            this.uxFileNameBox.Size = new System.Drawing.Size(590, 20);
            this.uxFileNameBox.TabIndex = 1;
            // 
            // uxFileSelectedLabel
            // 
            this.uxFileSelectedLabel.AutoSize = true;
            this.uxFileSelectedLabel.Location = new System.Drawing.Point(12, 88);
            this.uxFileSelectedLabel.Name = "uxFileSelectedLabel";
            this.uxFileSelectedLabel.Size = new System.Drawing.Size(68, 13);
            this.uxFileSelectedLabel.TabIndex = 2;
            this.uxFileSelectedLabel.Text = "File Selected";
            // 
            // uxReadFileButton
            // 
            this.uxReadFileButton.Location = new System.Drawing.Point(12, 140);
            this.uxReadFileButton.Name = "uxReadFileButton";
            this.uxReadFileButton.Size = new System.Drawing.Size(267, 23);
            this.uxReadFileButton.TabIndex = 3;
            this.uxReadFileButton.Text = "Read File";
            this.uxReadFileButton.UseVisualStyleBackColor = true;
            this.uxReadFileButton.Click += new System.EventHandler(this.uxReadFileButton_Click);
            // 
            // uxCancelReadingFileButton
            // 
            this.uxCancelReadingFileButton.Location = new System.Drawing.Point(307, 140);
            this.uxCancelReadingFileButton.Name = "uxCancelReadingFileButton";
            this.uxCancelReadingFileButton.Size = new System.Drawing.Size(295, 23);
            this.uxCancelReadingFileButton.TabIndex = 4;
            this.uxCancelReadingFileButton.Text = "Cancel Reading File";
            this.uxCancelReadingFileButton.UseVisualStyleBackColor = true;
            this.uxCancelReadingFileButton.Click += new System.EventHandler(this.uxCancelReadingFile_Click);
            // 
            // uxReadingFileLabel
            // 
            this.uxReadingFileLabel.AutoSize = true;
            this.uxReadingFileLabel.Location = new System.Drawing.Point(12, 191);
            this.uxReadingFileLabel.Name = "uxReadingFileLabel";
            this.uxReadingFileLabel.Size = new System.Drawing.Size(66, 13);
            this.uxReadingFileLabel.TabIndex = 5;
            this.uxReadingFileLabel.Text = "Reading File";
            // 
            // uxReadingFileProgressBar
            // 
            this.uxReadingFileProgressBar.Location = new System.Drawing.Point(15, 207);
            this.uxReadingFileProgressBar.Name = "uxReadingFileProgressBar";
            this.uxReadingFileProgressBar.Size = new System.Drawing.Size(587, 23);
            this.uxReadingFileProgressBar.TabIndex = 6;
            // 
            // uxFileStatusLabel
            // 
            this.uxFileStatusLabel.AutoSize = true;
            this.uxFileStatusLabel.Location = new System.Drawing.Point(12, 233);
            this.uxFileStatusLabel.Name = "uxFileStatusLabel";
            this.uxFileStatusLabel.Size = new System.Drawing.Size(56, 13);
            this.uxFileStatusLabel.TabIndex = 11;
            this.uxFileStatusLabel.Text = "File Status";
            // 
            // readFileBackgroundWorker
            // 
            this.readFileBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.readFileBackgroundWorker_DoWork);
            // 
            // uxSelectFileType
            // 
            this.uxSelectFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uxSelectFileType.FormattingEnabled = true;
            this.uxSelectFileType.Items.AddRange(new object[] {
            "Appointments",
            "User Roles",
            "Practitioner/Staff"});
            this.uxSelectFileType.Location = new System.Drawing.Point(12, 14);
            this.uxSelectFileType.Name = "uxSelectFileType";
            this.uxSelectFileType.Size = new System.Drawing.Size(250, 21);
            this.uxSelectFileType.TabIndex = 14;
            this.uxSelectFileType.Tag = "";
            this.uxSelectFileType.SelectedIndexChanged += new System.EventHandler(this.uxSelectFileType_SelectedIndexChanged);
            // 
            // uxResultTextBox
            // 
            this.uxResultTextBox.Location = new System.Drawing.Point(608, 12);
            this.uxResultTextBox.Multiline = true;
            this.uxResultTextBox.Name = "uxResultTextBox";
            this.uxResultTextBox.Size = new System.Drawing.Size(277, 234);
            this.uxResultTextBox.TabIndex = 15;
            // 
            // uxUpdateAddDeleteComboBox
            // 
            this.uxUpdateAddDeleteComboBox.FormattingEnabled = true;
            this.uxUpdateAddDeleteComboBox.Items.AddRange(new object[] {
            "Update",
            "Add",
            "Delete"});
            this.uxUpdateAddDeleteComboBox.Location = new System.Drawing.Point(12, 50);
            this.uxUpdateAddDeleteComboBox.Name = "uxUpdateAddDeleteComboBox";
            this.uxUpdateAddDeleteComboBox.Size = new System.Drawing.Size(250, 21);
            this.uxUpdateAddDeleteComboBox.TabIndex = 16;
            // 
            // uxResetPasswordCheckBox
            // 
            this.uxResetPasswordCheckBox.AutoSize = true;
            this.uxResetPasswordCheckBox.Location = new System.Drawing.Point(307, 53);
            this.uxResetPasswordCheckBox.Name = "uxResetPasswordCheckBox";
            this.uxResetPasswordCheckBox.Size = new System.Drawing.Size(108, 17);
            this.uxResetPasswordCheckBox.TabIndex = 17;
            this.uxResetPasswordCheckBox.Text = "Reset Passwords";
            this.uxResetPasswordCheckBox.UseVisualStyleBackColor = true;
            // 
            // CanceledAppointmentUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 373);
            this.Controls.Add(this.uxResetPasswordCheckBox);
            this.Controls.Add(this.uxUpdateAddDeleteComboBox);
            this.Controls.Add(this.uxResultTextBox);
            this.Controls.Add(this.uxSelectFileType);
            this.Controls.Add(this.uxFileStatusLabel);
            this.Controls.Add(this.uxReadingFileProgressBar);
            this.Controls.Add(this.uxReadingFileLabel);
            this.Controls.Add(this.uxCancelReadingFileButton);
            this.Controls.Add(this.uxReadFileButton);
            this.Controls.Add(this.uxFileSelectedLabel);
            this.Controls.Add(this.uxFileNameBox);
            this.Controls.Add(this.uxSelectFileButton);
            this.Name = "CanceledAppointmentUpdate";
            this.Text = "CanceledAppointmentUpdate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uxSelectFileButton;
        private System.Windows.Forms.TextBox uxFileNameBox;
        private System.Windows.Forms.Label uxFileSelectedLabel;
        private System.Windows.Forms.Button uxReadFileButton;
        private System.Windows.Forms.Button uxCancelReadingFileButton;
        private System.Windows.Forms.Label uxReadingFileLabel;
        private System.Windows.Forms.ProgressBar uxReadingFileProgressBar;
        private System.Windows.Forms.Label uxFileStatusLabel;
        private System.ComponentModel.BackgroundWorker readFileBackgroundWorker;
        private System.ComponentModel.BackgroundWorker processFileBackgroundWorker;
        private System.Windows.Forms.ComboBox uxSelectFileType;
        private System.Windows.Forms.TextBox uxResultTextBox;
        private System.Windows.Forms.ComboBox uxUpdateAddDeleteComboBox;
        private System.Windows.Forms.CheckBox uxResetPasswordCheckBox;
    }
}