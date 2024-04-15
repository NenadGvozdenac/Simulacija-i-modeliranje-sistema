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
    public class TourService
    {
        private ITourRepository _tourRepository; 

        public TourService(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }

        public static TourService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TourService>();
        }

        public List<Tour> GetAll()
        {
            return _tourRepository.GetAll();
        }

        public Tour GetById(int id)
        {
           return _tourRepository.GetById(id);
        }

        public List<Tour> GetByOwnerId(int ownerId)
        {
            return _tourRepository.GetByOwnerId(ownerId);
        }
        public Tour GetByName(string name)
        {
            return _tourRepository.GetByName(name); 
        }

        
        public void Add(Tour tour)
        {
            _tourRepository.Add(tour);
        }

       
        public int NextId()
        {
           return _tourRepository.NextId();
        }

        public void Update(Tour tour)
        {
            _tourRepository.Update(tour);  
        }

        public void Delete(int id)
        {
            _tourRepository.Delete(id);
        }



    }
}
