using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Service.Interfaces;

namespace eBookStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AuthorsController : ODataController
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            try
            {
                return Ok(_authorService.GetAuthors());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [EnableQuery]
        public IActionResult Get([FromODataUri] int key)
        {
            try
            {
                var author = _authorService.GetAuthorById(key);
                if (author == null)
                {
                    return NotFound();
                }
                return Ok(author);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Author author)
        {
            try
            {
                _authorService.AddAuthor(author);
                return Ok("Author added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromODataUri] int key, [FromBody] Author author)
        {
            try
            {
                if (key != author.AuthorId)
                {
                    return BadRequest();
                }

                _authorService.UpdateAuthor(author);
                return Ok("Author updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromODataUri] int key)
        {
            try
            {
                var author = _authorService.GetAuthorById(key);
                if (author == null)
                {
                    return NotFound();
                }

                _authorService.RemoveAuthor(key);
                return Ok("Author deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}