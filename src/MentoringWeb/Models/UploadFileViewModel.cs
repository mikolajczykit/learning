using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;

namespace MentoringWeb.Models
{
    public class UploadFileViewModel
    {
        [DisplayName("Upload File")]
        public IFormFile File { get; set; }
    }
}
