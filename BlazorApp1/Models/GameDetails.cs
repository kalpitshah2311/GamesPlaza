using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Models
{
    public class GameDetails
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required(ErrorMessage ="Genre field is required")]
        public string? GenreId { get; set; }

        [Required]
        [Range(1,100)]
        public decimal Price { get; set; }

        public DateOnly ReleaseDate { get; set; }

    }
}
