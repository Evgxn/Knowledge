using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.ViewModel.User;

namespace Services
{
    public interface IUserService
    {
        Task AddUserAsync(AddUserViewModel vm);
    }

    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddUserAsync(AddUserViewModel vm)
        {
            var user = new User
            {
                FirstName = vm.FirstName,
                Surname = vm.Surname,
                Email = vm.Email,
                Password = vm.Password,
                Role = await _db.Roles.FirstOrDefaultAsync(r => r.RoleName == "Пользователь")
            };

            await _db.AddAsync(user);
            await _db.SaveChangesAsync();
        }
    }
}
