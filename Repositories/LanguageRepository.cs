using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class LanguageRepository : BaseRepository<Language>, ILanguageRepository
{
    public LanguageRepository() : base("../../../Resources/Data/languages.csv") {}

    public Language GetLanguageByName(string v)
    {
        return GetAll().FirstOrDefault(l => l.Name == v);
    }

    public List<string> GetLanguages()
    {
        return GetAll().Select(l => l.Name).ToList();
    }
    public void Delete(string name)
    {
        DeleteRange(GetAll().Where(l => l.Name == name).ToList());
    }
}