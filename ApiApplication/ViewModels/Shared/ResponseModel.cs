using System.Collections.Generic;

namespace ApiApplication.ViewModels.Shared
{
    public class ResponseModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public List<ValidationModel> Validations { get; set; }
    }

    public class ValidationModel
    {
        public string Attribute { get; set; }
        public string Message { get; set; }
    }
}
