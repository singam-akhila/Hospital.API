using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.API.Model
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public int Experience { get; set; }
        public string Qualification { get; set; }


        public string SpecializationName { get; set; }


        public int SpecializationId { get; set; }
    }
}
