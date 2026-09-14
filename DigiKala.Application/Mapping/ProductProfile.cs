using AutoMapper;
using DigiKala.Application.Dtos;
using DigiKala.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Application.Mapping

    {
        public class ProductProfile : Profile
        {
            public ProductProfile()
            {
                CreateMap<Product, ProductDto>();
            }
        }
    }
