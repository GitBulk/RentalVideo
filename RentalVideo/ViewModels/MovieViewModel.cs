using RentalVideo.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentalVideo.ViewModels
{
    // excluded the Image property from MovieViewModel binding cause we will be using
    // a specific FileUpload action to upload images.
    [Bind(Exclude = "Image")]
    public class MovieViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Genre { get; set; }
        public int GenreId { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Producer { get; set; }
        public DateTime ReleaseDate { get; set; }
        public byte Rating { get; set; }
        public string TrailerURI { get; set; }
        public bool IsAvailable { get; set; }
        public int NumberOfStocks { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            var validator = new MovieViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}