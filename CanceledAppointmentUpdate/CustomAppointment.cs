using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CanceledAppointmentUpdate.AppointmentScheduling;

namespace CanceledAppointmentUpdate
{
    class CustomAppointment
    {
        public AppointmentSchedulingObject AppointmentSchedulingObj { get; set; }
        public string UniqueId { get; set; }
        public string StaffId { get; set; }
        public int RecordNum { get; set; }
        public TimeSpan Duration { get; set; }
        public int totalAppointments { get; set; }
        public CustomAppointment()
        {
            this.AppointmentSchedulingObj = new AppointmentSchedulingObject();
        }
    }
}
