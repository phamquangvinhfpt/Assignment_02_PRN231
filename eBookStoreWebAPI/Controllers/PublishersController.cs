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
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PublishersController : ODataController
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            try
            {
                return Ok(_publisherService.GetPublishers());
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
                var publisher = _publisherService.GetPublisherById(key);
                if (publisher == null)
                {
                    return NotFound();
                }
                return Ok(publisher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Publisher publisher)
        {
            try
            {
                _publisherService.AddPublisher(publisher);
                return Ok("Publisher added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromODataUri] int key, [FromBody] Publisher publisher)
        {
            try
            {
                if (key != publisher.PubId)
                {
                    return BadRequest("Invalid publisher id");
                }
                _publisherService.UpdatePublisher(publisher);
                return Ok("Publisher updated successfully");
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
                _publisherService.RemovePublisher(key);
                return Ok("Publisher deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}