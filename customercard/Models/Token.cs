using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace customercard.Models
{
    public class Token
    {
        [Key]
        public int TokenId { get; set; }

        public long TokenValue { get; set; }
          [DataType(DataType.DateTime)]
        public DateTime DateToken { get; set; }

        [Range(0, 99999, ErrorMessage = "Campo CVV tem que ter menos de 5 caracteres.")]
        public int CVV { get; set; }
        public int CustomerId { get; set; }
        public int CardId { get; set; }
        public Customer Customer { get; set; }
        public Card Card { get; set; }

    }
}