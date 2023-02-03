using Hospital.API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.API.Repositories
{
    public class UserRepository : IURepository<User>
    {
        private readonly ApplicationDbContext _context;


        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(User obj)
        {
            if (obj != null)
            {
                _context.Users.Add(obj);
                await _context.SaveChangesAsync();
            }
        }

        public Task<Appointment> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            var users = _context.Users.Select(x => new User
            {
                Id=x.Id,
                Username = x.Username,
                EmailId = x.EmailId,
                Password = x.Password,
                FullName = x.FullName,
                Role = x.Role,
            }).ToList();
            return users;
        }

        public async Task<User> GetById(int id)
        {
            var users = await _context.Users.Select(x => new User
            {
                Id = x.Id,
                Username = x.Username,
                EmailId = x.EmailId,
                Password = x.Password,
                FullName = x.FullName,
                Role = x.Role,

            }).ToListAsync();

            var user = users.FirstOrDefault(X => X.Id == id);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        

        public Task<Appointment> Update(int id, User obj)
        {
            throw new NotImplementedException();
        }
    }
}
