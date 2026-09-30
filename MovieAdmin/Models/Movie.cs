using System.ComponentModel.DataAnnotations;

namespace MovieAdmin.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Display(Name ="Title")]
        [MaxLength(500)]
        [Required]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        [Required]
        public string Synopsis { get; set; } = string.Empty;

        [Required]
        public string Genre { get; set; } = string.Empty;

        [Required]
        public string Rating { get; set; } = string.Empty;

        [Required]
        public int Runtime { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]

        [Display(Name = "Released")]
        [Required]
        public DateTime ReleaseDate { get; set; }


    }
}
