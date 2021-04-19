using AutoMapper;
using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    // api/posts
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostRepo _repository;

        public PostsController(IPostRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/posts
        [HttpGet]
        public async Task<ActionResult<PostsWithCountDto>> GetAllPosts([FromQuery] int? pageNumber, int? userId)
        {
            var posts = await _repository.GetAllPostsAsync(pageNumber, userId);
            return Ok(posts);
        }

        // GET api/posts/{id}
        [Authorize]
        [HttpGet("{id}", Name = "GetPostByIdAsync")]
        public async Task<ActionResult<PostReadDto>> GetPostByIdAsync(int id)
        {
            var post = await _repository.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PostReadDto>(post));
            
        }

        // POST api/posts
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PostReadDto>> CreatePostAsync(PostCreateDto post)
        {
            var postModel = _mapper.Map<Post>(post);
            await _repository.CreatePostAsync(postModel);
            await _repository.SaveChangesAsync();

            var postReadDto = _mapper.Map<PostReadDto>(postModel);

            return CreatedAtRoute(nameof(GetPostByIdAsync), new { Id = postReadDto.Id }, postReadDto);
            // return Ok(postReadDto);
        }

        // PUT api/posts/{1}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePost(int id, PostUpdateDto post)
        {
            var postModelFromRepo = await _repository.GetPostByIdAsync(id);
            if (postModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(post, postModelFromRepo);

            _repository.UpdatePost(postModelFromRepo);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/posts/{1}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var postModelFromRepo = await _repository.GetPostByIdAsync(id);
            if (postModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeletePost(postModelFromRepo);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
