using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VinylTapes.Shared.Models
{
   
        public class LoginDto
        {
            [Required]
            [EmailAddress(ErrorMessage = "Neteisingas el. pašto formatas")]
            [Display(Name = "Įveskite el. paštą")]
            public string ElPastas { get; set; } = "";
            [Required]
            [Display(Name = "Įveskite slaptažodį")]
            public string Slaptazodis { get; set; } = "";
        }

        public class RegisterDto
        {
            [Required]
            [Display(Name = "Įveskite vardą")]
             public string Vardas { get; set; }
            [Required]
            [EmailAddress(ErrorMessage = "Neteisingas el. pašto formatas")]
            [Display(Name = "Iveskite el. paštą")]
             public string ElPastas { get; set; }
            [Required]
            [Display(Name = "Įveskite slaptažodį")]
            public string Slaptazodis { get; set; } 
            [Required]
            [Display(Name = "Pakartokite slaptažodį")]
            [Compare(nameof(Slaptazodis), ErrorMessage = "slaptažodžiai nesutampa")]
            public string PakartotiSlaptazodi { get; set; } 
        }
    
}
