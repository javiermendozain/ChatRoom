using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontChat.Models
{
    public class Usuario
    {
        [Key]
        public string UID { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre del usuario es requerido(a)")]
        public string Name { get; set; }

        public int Status { get; set; }

        public Enrolado Enrolado { get; set; }


    }
}


