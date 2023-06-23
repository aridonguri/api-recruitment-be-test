using System;

namespace ApiApplication.ViewModels.Models
{
    public class ImdbStatusModel
    {
        public bool Up { get; set; }
        public DateTime LastCall { get; set; }

        public ImdbStatusModel()
        {
            Up = false;
            LastCall = DateTime.MinValue;
        }
    }
}
