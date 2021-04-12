using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Data;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    // api/posts
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepo _repository;

        public PostsController(IPostRepo repository)
        {
            _repository = repository;
        }
        // private readonly MockPostRepo _repository = new MockPostRepo();
        // GET api/posts
        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetAllPosts()
        {
            var posts = _repository.GetPosts();
            return Ok(posts);
        }

        // GET api/posts/{id}
        [HttpGet("{id}")]
        public ActionResult<Post> GetPostById(int id)
        {
            var post = _repository.GetPostById(id);
            return Ok(post);
        }
    }
}