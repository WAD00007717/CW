using AutoMapper;
using Domain.DTO;
using Domain.Models;
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
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {
            var posts = await  _repository.GetAllPosts();
            return Ok(_mapper.Map<IEnumerable<PostReadDto>>(posts));
        }

        // GET api/posts/{id}
        [HttpGet("{id}", Name = "GetPostById")]
        public ActionResult<PostReadDto> GetPostById(int id)
        {
            var post = _repository.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PostReadDto>(post));
        }

        // POST api/posts
        [HttpPost]
        public ActionResult<PostReadDto> CreatePost(PostCreateDto post)
        {
            var postModel = _mapper.Map<Post>(post);
            _repository.CreatePost(postModel);
            _repository.SaveChanges();

            var postReadDto = _mapper.Map<PostReadDto>(postModel);

            return CreatedAtRoute(nameof(GetPostById), new { Id = postReadDto.Id }, postReadDto);
            // return Ok(postReadDto);
        }

        // PUT api/posts/{1}
        [HttpPut("{id}")]
        public ActionResult UpdatePost(int id, PostUpdateDto post)
        {
            var postModelFromRepo = _repository.GetPostById(id);
            if (postModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(post, postModelFromRepo);

            _repository.UpdatePost(postModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE api/posts/{1}
        [HttpDelete("{id}")]
        public ActionResult DeletePost(int id)
        {
            var postModelFromRepo = _repository.GetPostById(id);
            if (postModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeletePost(postModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
