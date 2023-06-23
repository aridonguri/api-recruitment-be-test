using ApiApplication.Database.Entities;
using ApiApplication.Database.Query;
using ApiApplication.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Database
{
    public interface IShowtimesRepository
    {
        Task<IEnumerable<ShowtimeEntity>> GetCollection(GetCollectionQuery query);
        Task<IEnumerable<ShowtimeEntity>> GetCollection(Func<IQueryable<ShowtimeEntity>, bool> filter);
        Task<ShowtimeEntity> GetByMovie(int id);
        Task<ShowtimeEntity> Add(ShowtimeModel showtimeEntity);
        Task<ShowtimeEntity> Update(ShowtimeModel showtimeEntity);
        Task Delete(int id);
    }
}
