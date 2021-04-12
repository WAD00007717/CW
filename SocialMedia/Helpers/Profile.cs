using Domain.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace SocialMedia.Helpers
{
    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            CreateMap<Post, PostReadDto>();
            CreateMap<PostUpdateDto, Post>();
            CreateMap<PostCreateDto, Post>();
            CreateMap<Post, PostUpdateDto>();
        }
    }
}
