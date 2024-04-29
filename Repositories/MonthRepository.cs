using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class MonthRepository : BaseRepository<Month>, IMonthRepository
{
    public MonthRepository() : base("../../../Resources/Data/months.csv") { }

    public Month GetMonthByNumber(int monthNumber)
    {
        return GetAll().FirstOrDefault(m => m.Id == monthNumber);
    }

    public Month GetMonthByName(string monthName)
    {
        return GetAll().FirstOrDefault(m => m.Name == monthName);
    }
}
