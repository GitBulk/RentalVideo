using FluentValidation;
using RentalVideo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalVideo.Infrastructure.Validators
{
    public class MovieViewModelValidator : AbstractValidator<MovieViewModel>
    {
        public MovieViewModelValidator()
        {
            RuleFor(m => m.GenreId).GreaterThan(0).WithMessage("Select a Genre");
            RuleFor(m => m.Director).NotEmpty().Length(1, 100)
                .WithMessage("Select a Director.");
            RuleFor(m => m.Writer).NotEmpty().Length(1, 50).WithMessage("Select a writer");
            RuleFor(m => m.Producer).NotEmpty().Length(1, 50).WithMessage("Select a producer");
            RuleFor(m => m.Description).NotEmpty().WithMessage("Select a description");
            RuleFor(m => m.Rating).InclusiveBetween((byte)0, (byte)5);
            RuleFor(m => m.TrailerURI).NotEmpty().Must(IsValidTrailerURI).WithMessage("Only Youtube trailers are supported");
        }

        private bool IsValidTrailerURI(string trailerURI)
        {
            return (!string.IsNullOrWhiteSpace(trailerURI) && trailerURI.ToLower().StartsWith("https://www.youtube.com/watch?"));
        }
    }

}