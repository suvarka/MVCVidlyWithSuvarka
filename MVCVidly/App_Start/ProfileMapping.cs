using AutoMapper;
using MVCVidly.Dtos;
using MVCVidly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCVidly.App_Start
{
    public class ProfileMapping:Profile
    {
        public ProfileMapping()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>();
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MovieDto, Movie>();
        }
    }
}