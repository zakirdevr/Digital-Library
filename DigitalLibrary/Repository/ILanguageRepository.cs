using DigitalLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalLibrary.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetAllLanguage();
    }
}