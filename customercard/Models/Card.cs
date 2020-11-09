using System.ComponentModel.DataAnnotations;

namespace customercard.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }
        [Range(0, 9999999999999999, ErrorMessage = "Campo CustumerId tem que ter menos de 16 caracteres.")]
        public long CardNumber { get; set; }
        [Range(0, 99999, ErrorMessage = "Campo CVV tem que ter menos de 5 caracteres.")]
        public int CVV { get; set; }
        
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

    }
}