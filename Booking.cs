﻿using System.ComponentModel.DataAnnotations;

namespace Jwt.Model
{
    public class Booking
    {

            public Guid Id { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; }

            [Required]
            public string Genre { get; set; }

            [Required]
            public string Cast { get; set; }
        


    }
}
