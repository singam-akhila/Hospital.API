﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.API.Model
{
    public class Appointment
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public DateTime AppointmentDate { get; set; }

        public DateTime TimeSessions { get; set; }
        public string SpecializationOfDoctor {get;set;}

        public string DoctorName { get; set; }




    }
}
