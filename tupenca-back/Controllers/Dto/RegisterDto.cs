using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupenca_back.Controllers.Dto;

namespace tupenca_back.Model
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 40 character in length.")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 40 character in length.")]

        public string password { get; set; }

        public ImagenDto? image { get; set; }

    }
}
