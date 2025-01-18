using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortenerService.Models.Repos;

namespace URLShortenerService.Interfaces
{
    public interface IMetroURL
    {
        IEnumerable<MetroURL> MetroURLs { get; }
        Task<IEnumerable<MetroURL>> GetAllMetroURL();
        Task<MetroURL> GetMetroURLById(int id);
        Task<MetroURL> AddMetroURL(MetroURL url);
        Task<MetroURL> UpdateMetroURL(MetroURL url);

    }
}