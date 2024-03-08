using BookingApp.Model.MutualModels;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.MutualRepositories
{
    internal class TourRepository
    {
        private const string FilePath = "../../../Resources/Data/Tours.csv";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
        }


        public Tour GetByName(string name)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FirstOrDefault(t => t.Name == name);
        }

        /// <summary>
        /// Adds a tour to the repository
        /// </summary>
        /// <param name="tour"></param>
        public void Add(Tour tour)
        {
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
        }

        /// <summary>
        /// Gets the next id for the tour
        /// </summary>
        /// <returns></returns>
        public int NextId()
        {
            _tours = _serializer.FromCSV(FilePath);
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(c => c.Id) + 1;
        }

    }
}
