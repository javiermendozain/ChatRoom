using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FrontChat.Models
{
    public class Enrolado
    {
        [Key]
        public int Id { get; set; }

        public int Status { get; set; }

        public int SalaId { get; set; }

        public Sala Sala { get; set; }

        public string UsuarioId { get; set; }
        
        public Usuario Usuario { get; set; }


    }
}


