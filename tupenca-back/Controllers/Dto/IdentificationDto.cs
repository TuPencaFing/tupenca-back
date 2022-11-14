using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class IdentificationDto
    {

        public string number { get; set; }

        public string type { get; set; }


    }
}

