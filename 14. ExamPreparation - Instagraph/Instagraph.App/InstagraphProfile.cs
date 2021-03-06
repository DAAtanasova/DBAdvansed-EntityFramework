﻿using AutoMapper;
using Instagraph.DataProcessor.Dtos;
using Instagraph.Models;

namespace Instagraph.App
{
    public class InstagraphProfile : Profile
    {
        public InstagraphProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(p=>p.ProfilePicture, pp=>pp.UseValue<Picture>(null));
            
        }
    }
}
