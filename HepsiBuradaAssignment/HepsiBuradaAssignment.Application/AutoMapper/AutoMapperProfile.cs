using AutoMapper;
using HepsiBuradaAssignment.Application.Models;
using HepsiBuradaAssignment.Domain.Entities;

namespace HepsiBuradaAssignment.Application.AutoMapper
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDtoModel>();
            CreateMap<Campaign, CampaignDtoModel>();
        }
    }
}
