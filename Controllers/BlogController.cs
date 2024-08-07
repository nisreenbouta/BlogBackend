using BlogApi.Models;
using BlogApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Blog>>> GetAsync()
        {
            var blogs = await _blogService.GetAsync();
            return Ok(blogs);
        }

        [HttpGet("{id:length(24)}", Name = "GetBlog")]
        public async Task<ActionResult<Blog>> GetAsync(string id)
        {
            var blog = await _blogService.GetAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        [HttpPost]
        public async Task<ActionResult<Blog>> CreateAsync(Blog blog)
        {
            await _blogService.CreateAsync(blog);
            return CreatedAtRoute("GetBlog", new { id = blog.Id.ToString() }, blog);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateAsync(string id, Blog blogIn)
        {
            var blog = await _blogService.GetAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            await _blogService.UpdateAsync(id, blogIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var blog = await _blogService.GetAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            await _blogService.RemoveAsync(id);

            return NoContent();
        }
    }
}
