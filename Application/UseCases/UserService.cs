using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class UserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public static UserService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<UserService>();
        }

        public User GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }

        public User GetById(int id)
        {
           return _userRepository.GetById(id);
        }

    }
}
