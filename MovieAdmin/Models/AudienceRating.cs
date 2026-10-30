using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MovieAdmin.Models
{
    public class AudienceRating
    {
        // primary key
        public int Id { get; set; }

        // navigation property
        [ValidateNever]
        public List<Movie> Movies { get; set; } = new List<Movie>();

        [Required]
        public string Title { get; set; } = string.Empty;

    }
}