using Hospital.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.API.Repositories
{
    public interface IGetRepository<T> where T :class
    {
        IEnumerable<T> GetAll();

        Task<T> GetById(int id);

    }
    public interface IRepository<T> where T : class
    {
       

        Task Create(T obj);

        Task<Doctor> Update(int id, T obj);

        Task<Doctor> Delete(int id);
    }

    public interface IDoctorRepository
    {
        Task<IEnumerable<DoctorDto>> SearchBySpecialization(string SpecializationName);

        Task<IEnumerable<Specialization>> GetSpecializations();

    }
    


    public interface IARepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        Task<T> GetById(int id);

        Task Create(T obj);

        Task<Appointment> Update(int id, T obj);

        Task<Appointment> Delete(int id);

    }
    public interface ISRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        Task Create(T obj); Task<Specialization> Delete(int id);
    }
}


public interface IURepository<T> where T : class
{
    IEnumerable<T> GetAll();

    Task<T> GetById(int id);

    Task Create(T obj);

    Task<Appointment> Update(int id, T obj);

    Task<Appointment> Delete(int id);

}