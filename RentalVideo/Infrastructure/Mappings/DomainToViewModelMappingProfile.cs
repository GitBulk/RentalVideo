using AutoMapper;
using RentalVideo.Entities;
using RentalVideo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalVideo.Infrastructure.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Movie, MovieViewModel>()
                .ForMember(vm => vm.Genre, opts => opts.MapFrom(m => m.Genre.Name))
                .ForMember(vm => vm.GenreId, opts => opts.MapFrom(m => m.Genre.Id))
                // we set if a Movie(ViewModel) is available or not by checking if any of its stocks is available
               .ForMember(vm => vm.IsAvailable, opts => opts.MapFrom(m => m.Stocks.Any(s => s.IsAvailable)))
                .ForMember(vm => vm.NumberOfStocks, opts => opts.MapFrom(m => m.Stocks.Count))
                .ForMember(vm => vm.Image, opts => opts.MapFrom(m => string.IsNullOrEmpty(m.Image) ? "unknown.jpg" : m.Image));

            CreateMap<Genre, GenreViewModel>()
                .ForMember(vm => vm.NumberOfMovies, opts => opts.MapFrom(m => m.Movies.Count));
        }

        public override string ProfileName
        {
            get
            {
                return "DomainToViewModelMappingProfile";
            }
        }

    }
}