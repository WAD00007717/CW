using Domain.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.DTO.ImageDTOs;

namespace SocialMedia.Helpers
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Post, PostReadDto>();
            CreateMap<Post, PostsWithCountDto>();
            CreateMap<PostUpdateDto, Post>();
            CreateMap<PostCreateDto, Post>();
            CreateMap<Post, PostUpdateDto>();
            CreateMap<ICollection<Post>, ICollection<PostsWithoutImageDto>>();

            CreateMap<CommentsWithCountDto, CommentsGetAllDto>();
            CreateMap<Comment, CommentReadDto>();
            CreateMap<CommentUpdateDto, Comment>();
            CreateMap<CommentCreateDto, Comment>();
            CreateMap<Comment, CommentUpdateDto>();

            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<User, LoginDto>();

            CreateMap<string, ImageUrlDto>();


        }
    }
}
