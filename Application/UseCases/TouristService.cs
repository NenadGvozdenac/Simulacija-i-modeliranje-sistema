using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TouristService
    {
        private ITouristRepository _touristRepository;

        public TouristService(ITouristRepository touristRepository)
        {
            _touristRepository = touristRepository;
        }

        public static TouristService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TouristService>();
        }

        public List<Tourist> GetAll()
        {
            return _touristRepository.GetAll();
        }

        public Tourist GetById(int id)
        {
            return _touristRepository.GetById(id);
        }

        public Tourist GetByName(string name)
        {
            return _touristRepository.GetByName(name);
        }

        public void Add(Tourist tourist)
        {
            _touristRepository.Add(tourist);
        }

        public int NextId()
        {
           return _touristRepository.NextId();
        }
        public void Update(Tourist tourist)
        {
            _touristRepository.Update(tourist);
        }

        public void Delete(int id)
        {
           _touristRepository.Delete(id);
        }


    }
}
