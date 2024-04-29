using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases;

public class MonthService
{
    public IMonthRepository _monthRepository;

    public MonthService(IMonthRepository monthRepository)
    {
        _monthRepository = monthRepository;
    }

    public static MonthService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<MonthService>();
    }

    public List<Month> GetAll()
    {
        return _monthRepository.GetAll();
    }

    public List<string> GetAllNames()
    {
        return _monthRepository.GetAll().Select(m => m.Name).ToList();
    }

    public Month GetById(int id)
    {
        return _monthRepository.GetById(id);
    }

    public Month GetMonthByName(string name)
    {
        return _monthRepository.GetMonthByName(name);
    }

    public Month GetMonthByNumber(int number)
    {
        return _monthRepository.GetMonthByNumber(number);
    }
}
