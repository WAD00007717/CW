using AutoMapper;
using Domain.DTO;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Interfaces;
using SocialMediaApi.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApi.Controllers
{
    // api/users
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _repository;
        public IConfiguration _configuration;


        public UsersController(IConfiguration config, IUserRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = config;
        }

        // GET api/users
        [HttpGet]
        public async Task<ActionResult<UsersWithCountDto>> GetAllUsersAsync([FromQuery] int? pageNumber)
        {
            var users = await _repository.GetAllUsersAsync(pageNumber);
            return Ok(users);
        }

        // GET api/users/{id}
        [HttpGet("{id}", Name = "GetUserByIdAsync")]
        public async Task<ActionResult<UserReadDto>> GetUserByIdAsync(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserReadDto>(user));
        }

        // POST api/users
        [HttpPost("Register")]
        public async Task<ActionResult<UserReadDto>> CreateUserAsync(UserCreateDto user)
        {
            // hash password for securoty reasons
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var userModel = _mapper.Map<User>(user);

            await _repository.CreateUserAsync(userModel);
            await _repository.SaveChangesAsync();

            var tokenString = JWTUtils.SignKey(_configuration, userModel);

            var loginDto = new LoginDto() { Username = userModel.Username, Id = userModel.Id.ToString(), Token = tokenString };

            return CreatedAtRoute(nameof(GetUserByIdAsync), new { Id = loginDto.Id }, loginDto);
        }

        // POST api/users/login
        [HttpPost("Login")]
        public async Task<ActionResult<LoginDto>> Login(UserCreateDto userData)
        {

            if (userData != null && userData.Username != null && userData.Password != null)
            {
                var user = await _repository.GetUserByUsernameAndPasswordAsync(userData.Username);
                if (user != null)
                {
                    // verify password in the database with coming in request body
                    bool verified = BCrypt.Net.BCrypt.Verify(userData.Password, user.Password);
                    if (!verified)
                    {
                        ModelState.AddModelError("message", "Invalid password");
                        return BadRequest(ModelState);
                    }
                    // sign JWT token if ok
                    var tokenString = JWTUtils.SignKey(_configuration, user);
                    
                    var loginDto = new LoginDto() { Username = user.Username, Token = tokenString, Id = user.Id.ToString() };

                    return Ok(loginDto);
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
