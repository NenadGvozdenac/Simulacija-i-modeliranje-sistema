using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    internal class LanguageService
    {
        private ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public static LanguageService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<LanguageService>();
        }

        public Language GetLanguageByName(string v)
        {
            return _languageRepository.GetLanguageByName(v);
        }

        public List<string> GetLanguages()
        {
            return _languageRepository.GetLanguages();
        }
        public void Delete(string name)
        {
           _languageRepository.Delete(name);    
        }

        public Language GetById(int languageId)  //needs fix
        {
           return _languageRepository.GetById(languageId);
        }
    }
}
