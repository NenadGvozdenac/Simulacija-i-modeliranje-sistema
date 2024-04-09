using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ILanguageRepository
    {
        void Add(Language language);
        void Delete(string name);
        List<Language> GetAll();
        Language GetById(int id);
        Language GetLanguageByName(string name);
        List<string> GetLanguages();
        void Update(Language language);
    }
}