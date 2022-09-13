
using System.ComponentModel.DataAnnotations;


namespace ProductManagement.Models
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome del prodotto è obbligatorio")]
        [StringLength(50,ErrorMessage = "Nome troppo lungo")]
        public string? ProductName { get; set; }
        
        [Required(ErrorMessage = "La descrizione del prodotto è obbligatoria")]
        [StringLength(300, ErrorMessage = "Descrizione troppo lunga")]
        public string? FullDescription { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Descrizione troppo lunga")]
        public string? ShortDescription { get; set; }
        
        
        [Required(ErrorMessage = "Il prezzo del prodotto è obbligatorio")]
        [Range(1,double.MaxValue,ErrorMessage = "Il prezzo del prodotto deve essere maggiore di 1")]
        public decimal Price { get; set; }

        public bool Published { get; set; }
    }
}
