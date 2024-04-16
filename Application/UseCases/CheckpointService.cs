using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.Views.TouristViews;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class CheckpointService
    {
        private ICheckpointRepository _checkpointRepository;


        public CheckpointService(ICheckpointRepository checkpointRepository)
        {
            _checkpointRepository = checkpointRepository;
        }

        public static CheckpointService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<CheckpointService>();
        }

        public List<Checkpoint> GetAll()
        {
            return _checkpointRepository.GetAll();
        }
        public void Add(Checkpoint checkpoint)
        {
            _checkpointRepository.Add(checkpoint);
        }

        public void Update(Checkpoint checkpoint)
        {
            _checkpointRepository?.Update(checkpoint);
        }

        public void Delete(int id)
        {
           _checkpointRepository.Delete(id);
        }

        public int NextId()
        {
           return _checkpointRepository.NextId();
        }



        public void RemoveByTourId(int tourId)
        {
            _checkpointRepository.RemoveByTourId(tourId);
        }

        public List<Checkpoint> GetCheckpointsByTourId(int id)
        {
            return _checkpointRepository.GetCheckpointsByTourId(id);
        }

        public void DeleteByAccommodationId(int id)
        {
           _checkpointRepository.DeleteByAccommodationId((int)id);
        }

    }
}
