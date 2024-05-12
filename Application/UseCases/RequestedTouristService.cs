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
    public class RequestedTouristService
    {
        private IRequestedTouristRepository _requestedTouristRepository;

        public RequestedTouristService(IRequestedTouristRepository requestedTourRepository)
        {
            _requestedTouristRepository = requestedTourRepository;
        }

        public static RequestedTouristService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<RequestedTouristService>();
        }

        public List<RequestedTourist> GetAll()
        {
            return _requestedTouristRepository.GetAll();
        }

        public RequestedTourist GetById(int id)
        {
            return _requestedTouristRepository.GetById(id);
        }

        public RequestedTourist GetByName(string name)
        {
            return _requestedTouristRepository.GetByName(name);
        }

        public void Add(RequestedTourist tourist)
        {
            _requestedTouristRepository.Add(tourist);
        }

        public int NextId()
        {
            return _requestedTouristRepository.NextId();
        }
        public void Update(RequestedTourist tourist)
        {
            _requestedTouristRepository.Update(tourist);
        }

        public void Delete(int id)
        {
            _requestedTouristRepository.Delete(id);
        }
    }
}
