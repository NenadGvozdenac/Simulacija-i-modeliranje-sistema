using BookingApp.Application.Localization;
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
        List<Month> months = _monthRepository.GetAll();
        months.ForEach(m => m.Name = TranslationSource.Instance[m.Name]);
        return months;
    }

    public List<string> GetAllNames()
    {
        List<string> months = _monthRepository.GetAll().Select(m => m.Name).ToList();
        months.ForEach(m => m = TranslationSource.Instance[m]);
        return months;
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
