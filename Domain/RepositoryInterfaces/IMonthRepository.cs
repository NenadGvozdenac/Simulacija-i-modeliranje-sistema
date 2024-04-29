using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces;

public interface IMonthRepository
{
    List<Month> GetAll();
    Month GetById(int id);
    Month GetMonthByName(string monthName);
    Month GetMonthByNumber(int monthNumber);
}