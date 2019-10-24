using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FrontChat.Models
{
    public class ChatTracebility
    {
        [Key]
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime AtDate { get; set; }

        public string UserName { get; set; }

        public string UserID { get; set; }

        public int enroladoId { get; set; }
        
        public Enrolado enrolado  { get; set; }

    }

}


