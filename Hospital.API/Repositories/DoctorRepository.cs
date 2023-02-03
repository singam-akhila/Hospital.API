using Hospital.API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.API.Repositories
{
    public class DoctorRepository : IRepository<Doctor>, IDoctorRepository,IGetRepository<DoctorDto>
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(Doctor obj)
        {
            if (obj != null)
            {
                _context.Doctors.Add(obj);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Doctor> Delete(int id)
        {
            var HospitalDb1 = await _context.Doctors.FindAsync(id);
            if (HospitalDb1 != null)
            {
                _context.Doctors.Remove(HospitalDb1);
                await _context.SaveChangesAsync();
                return HospitalDb1;
            }
            return null;
        }

        public  IEnumerable<DoctorDto> GetAll()
        {
          var doctors =  _context.Doctors.Include(m => m.Specialization).Select(x=> new DoctorDto {
              Id = x.Id,
              DoctorName =  x.DoctorName,
              Experience = x.Experience,
              Qualification = x.Qualification,
              SpecializationId = x.SpecializationId,
              SpecializationName = x.Specialization.SpecializationName
               
          
          
          }).ToList();
            return doctors;
        }

        public async Task<DoctorDto> GetById(int id)
        {
            var doctors =await _context.Doctors.Include(x => x.Specialization).Select(x => new DoctorDto
            {
                Id = x.Id,
                DoctorName = x.DoctorName,
                Experience = x.Experience,
                Qualification = x.Qualification,
                SpecializationId = x.SpecializationId,
                SpecializationName = x.Specialization.SpecializationName

            }).ToListAsync();

            var doctor = doctors.FirstOrDefault(x=>x.Id == id);

            

            if (doctor != null)
            {
                return doctor;
            }
            return null;
        }



        public async Task<Doctor> Update(int id, Doctor obj)
        {
            var HospitalDb1 = await _context.Doctors.FindAsync(id);
            if (HospitalDb1 != null)
            {
                HospitalDb1.DoctorName = obj.DoctorName;
                HospitalDb1.Experience = obj.Experience;
                HospitalDb1.Qualification = obj.Qualification;
                HospitalDb1.SpecializationId = obj.SpecializationId;
                HospitalDb1.Specialization = obj.Specialization;
                _context.Doctors.Update(HospitalDb1);
                await _context.SaveChangesAsync();
                return HospitalDb1;
            }
            return null;
        }

        public async Task<IEnumerable<DoctorDto>> SearchBySpecialization(string specializationName)

        {
            if (!string.IsNullOrWhiteSpace(specializationName))
            {
                var doctorsWithSpecialization = await _context.Doctors.Include(x => x.Specialization).Select(x => new DoctorDto
                {
                    Id = x.Id,
                    DoctorName = x.DoctorName,
                    Experience = x.Experience,
                    Qualification = x.Qualification,
                    SpecializationId = x.SpecializationId,
                    SpecializationName = x.Specialization.SpecializationName

                }).ToListAsync();
                var doctors = doctorsWithSpecialization.Where(x => x.SpecializationName.Contains(specializationName)).ToList();
                return doctors;

            }
            return null;
        }



        //Get Specialization

        public async Task<IEnumerable<Specialization>> GetSpecializations()
        {
            var specializations = await _context.Specializations.ToListAsync();
            return specializations;
        }







    }
}
