using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackChat.Models
{
    public class Sala
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre de la sala")]
        [Required(ErrorMessage = "El nombre de la sala es requerido(a)")]
        public string Name { get; set; }
 
        public int Status { get; set; }

        public Enrolado Enrolado { get; set; }

    }
}


