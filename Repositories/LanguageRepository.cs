using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class LanguageRepository : ILanguageRepository
{
    private const string FilePath = "../../../Resources/Data/languages.csv";

    private readonly Serializer<Language> _serializer;

    private List<Language> _languages;

    public LanguageRepository()
    {
        _serializer = new Serializer<Language>();
        _languages = _serializer.FromCSV(FilePath);
        _languages.Sort((language1, language2) => string.Compare(language1.Name, language2.Name));
    }

    public List<Language> GetAll()
    {
        return _languages;
    }

    public Language GetById(int id)
    {
        return _languages.FirstOrDefault(a => a.Id == id);
    }

    public void Add(Language language)
    {
        _languages.Add(language);
        _serializer.ToCSV(FilePath, _languages);
    }

    public void Update(Language language)
    {
        var existingLanguage = _languages.FirstOrDefault(a => a.Name == language.Name);
        if (existingLanguage != null)
        {
            existingLanguage.Name = language.Name;
            _serializer.ToCSV(FilePath, _languages);
        }
    }

    public void Delete(string name)
    {
        var existingLanguage = _languages.FirstOrDefault(a => a.Name == name);
        if (existingLanguage != null)
        {
            _languages.Remove(existingLanguage);
            _serializer.ToCSV(FilePath, _languages);
        }
    }


    public List<string> GetLanguages()
    {
        return _languages.Select(l => l.Name).Distinct().ToList();
    }

    public Language GetLanguageByName(string name)
    {
        return _languages.FirstOrDefault(l => l.Name == name);
    }



}
