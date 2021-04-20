using AutoMapper;
using Domain.DTO.ImageDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IMapper _mapper;


        public ImagesController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpPost("Upload")]
        public ActionResult<ImageUrlDto> UploadImage(IFormFile file)
        {
            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            var imageData = ms.ToArray();

            ms.Close();
            ms.Dispose();

            string imageBase64Data = Convert.ToBase64String(imageData);
            string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            ImageUrlDto imageObj = new ImageUrlDto() 
            { 
                ImageUrl = imageDataURL
            };
            return Ok(imageObj);
        }
    }
}
