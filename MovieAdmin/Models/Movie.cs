using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MovieAdmin.Models
{
    public class Movie
    {
        public int Id { get; set; }

        //Foreign Key
        public int GenreId { get; set; }

        // navigation property
       
        [ValidateNever]
        public Genre Genre { get; set; } = null!;

        public int AudienceRatingId { get; set; }

        [ValidateNever]
        public AudienceRating AudienceRating { get; set; } = null!;



        [Display(Name ="Title")]
        [MaxLength(500)]
        [Required]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        [Required]
        public string Synopsis { get; set; } = string.Empty;

        [Required]
        public int Runtime { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Released")]
        [Required]
        public DateTime ReleaseDate { get; set; }

        public string? ImageUrl { get; set; }

    }
}
