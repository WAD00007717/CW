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
    // api/comments
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepo _repository;

        public CommentsController(ICommentRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/comments
        [HttpGet]
        public async Task<ActionResult<CommentsGetAllDto>> GetAllComments([FromQuery] int? pageNumber, int? userId, int? postId)
        {
            var comments = await _repository.GetAllCommentsAsync(pageNumber, userId, postId);
            return Ok(_mapper.Map<CommentsGetAllDto>(comments));
        }

        // GET api/comments/{id}
        [HttpGet("{id}", Name = "GetCommentByIdAsync")]
        public async Task<ActionResult<CommentReadDto>> GetCommentByIdAsync(int id)
        {
            var comment = await _repository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CommentReadDto>(comment));
        }

        // POST api/comments
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<CommentCreateDto>> CreateCommentAsync(CommentCreateDto comment)
        {
            //try
            //{
                var commentModel = _mapper.Map<Comment>(comment);
                await _repository.CreateCommentAsync(commentModel);
                await _repository.SaveChangesAsync();

                var commentReadDto = _mapper.Map<CommentReadDto>(commentModel);

                return CreatedAtRoute(nameof(GetCommentByIdAsync), new { Id = commentReadDto.Id }, commentReadDto);
            //}
            //catch (Exception err)
            //{

            //     return StatusCode(500, err.InnerException.Message);
            //}
            
        }

        // PUT api/comments/{1}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateComment(int id, CommentUpdateDto comment)
        {
            var commentModelFromRepo = await _repository.GetCommentByIdAsync(id);
            if (commentModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(comment, commentModelFromRepo);

            _repository.UpdateComment(commentModelFromRepo);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/comments/{1}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            var commentModelFromRepo = await _repository.GetCommentByIdAsync(id);
            if (commentModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteComment(commentModelFromRepo);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
