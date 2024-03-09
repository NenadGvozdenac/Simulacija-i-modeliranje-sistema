﻿using BookingApp.Model.MutualModels;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.MutualRepositories
{
    internal class LanguageRepository
    {
        private const string FilePath = "../../../Resources/Data/languages.csv";

        private readonly Serializer<Language> _serializer;

        private List<Language> _languages;

        public LanguageRepository()
        {
            _serializer = new Serializer<Language>();
            _languages = _serializer.FromCSV(FilePath);
        }

        public void Add(Language language)
        {
            _languages.Add(language);
            _serializer.ToCSV(FilePath, _languages);
        }

        public void Update(Language language)
        {
            var existingLanguage= _languages.FirstOrDefault(a => a.Name == language.Name);
            if (existingLanguage != null)
            {
                existingLanguage.Name = language.Name;
                _serializer.ToCSV(FilePath, _languages);
            }
        }

        public void Delete(String name)
        {
            var existingLanguage = _languages.FirstOrDefault(a => a.Name == name);
            if (existingLanguage != null)
            {
                _languages.Remove(existingLanguage);
                _serializer.ToCSV(FilePath, _languages);
            }
        }








    }
}