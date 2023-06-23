using ApiApplication.APIClient.ImdbAPI;
using ApiApplication.Database.Entities;
using ApiApplication.Database.Query;
using ApiApplication.ViewModels.Models;
using ApiApplication.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Database
{
    public class ShowtimesRepository : IShowtimesRepository
    {
        private readonly CinemaContext _context;

        public ShowtimesRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<ShowtimeEntity> Add(ShowtimeModel showtimeEntity)
        {
            ImdbApiClient apiClient = new ImdbApiClient();

            string imdbId = showtimeEntity.Movie.ImdbId;

            MovieEntity movie = apiClient.GetMovieDetails(imdbId);

            if (movie != null)
            {
                var showTime = new ShowtimeEntity
                {
                    Id = showtimeEntity.Id,
                    Movie = movie,
                    EndDate = showtimeEntity.EndDate,
                    StartDate = showtimeEntity.StartDate,
                    Schedule = showtimeEntity.Schedule,
                    AuditoriumId = 1
                };

                await _context.Showtimes.AddAsync(showTime);

                await _context.SaveChangesAsync();

                return await GetByMovie(showTime.Id);
            }

            return null;
        }

        public async Task Delete(int id)
        {
            var showTime = await _context.Showtimes.FindAsync(id);

            if (showTime == null) return;

            _context.Showtimes.Remove(showTime);

            await _context.SaveChangesAsync();
        }

        public async Task<ShowtimeEntity> GetByMovie(int id)
        {
            var showTime = await _context.Showtimes.Include(x => x.Movie).Where(x => x.Id == id).FirstOrDefaultAsync();

            return new ShowtimeEntity
            {
                Id = showTime.Id,
                AuditoriumId = showTime.AuditoriumId,
                EndDate = showTime.EndDate,
                Movie = showTime.Movie,
                Schedule = showTime.Schedule,
                StartDate = showTime.StartDate
            };
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetCollection(GetCollectionQuery query)
        {
            var showtimes = await (from sht in _context.Showtimes
                                   join m in _context.Movies on sht.Id equals m.Id
                                   where (sht.StartDate <= ((query.Date != null ? (query.Date) : sht.StartDate))
                                   && sht.EndDate >= ((query.Date != null ? (query.Date) : sht.EndDate)))
                                   && m.Title == ((query.Title ?? m.Title))
                                   orderby m.ReleaseDate descending
                                   select new ShowtimeEntity
                                   {
                                       Id = sht.Id,
                                       StartDate = sht.StartDate,
                                       EndDate = sht.EndDate,
                                       Movie = sht.Movie,
                                       Schedule = sht.Schedule,
                                       AuditoriumId = sht.AuditoriumId
                                   }).ToListAsync();

            return showtimes;
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetCollection(Func<IQueryable<ShowtimeEntity>, bool> filter)
        {
            return await _context.Showtimes.Include(x => x.Movie).ToListAsync();
        }

        public async Task<ShowtimeEntity> Update(ShowtimeModel showtimeEntity)
        {
            ImdbApiClient apiClient = new ImdbApiClient();

            if (showtimeEntity.Movie == null)
                return null;

            string imdbId = showtimeEntity.Movie.ImdbId;

            MovieEntity movie = apiClient.GetMovieDetails(imdbId);

            if(movie != null)
            {
                var showTime = await _context.Showtimes.Include(x => x.Movie).Where(x => x.Id == showtimeEntity.Id).FirstOrDefaultAsync();

                showTime.Movie.Title = movie.Title;
                showTime.Movie.Stars = movie.Stars;
                showTime.Movie.ReleaseDate = movie.ReleaseDate;
                showTime.StartDate = showtimeEntity.StartDate;
                showTime.EndDate = showtimeEntity.EndDate;
                showTime.Schedule = showtimeEntity.Schedule;
                showTime.AuditoriumId = showtimeEntity.AuditoriumId;

                await _context.SaveChangesAsync();

                return await GetByMovie(showtimeEntity.Id);
            }

            return null;
        }
    }
}
