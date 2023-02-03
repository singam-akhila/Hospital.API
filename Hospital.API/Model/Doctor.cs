using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.API.Model
{
    public class Doctor
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public int Experience { get; set; }
        public string Qualification { get; set; }
       
        
        public Specialization Specialization { get; set; }


        public int SpecializationId { get; set; }

    }
}
