using ApiApplication.Database.Entities;
using System.Collections.Generic;
using System;

namespace ApiApplication.ViewModels.Models
{
    public class ShowtimeModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<string> Schedule { get; set; }
        public MovieModel Movie { get; set; }
        public int AuditoriumId { get; set; }
    }
}
