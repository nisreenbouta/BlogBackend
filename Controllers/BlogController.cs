using BlogApi.Services;
using Microsoft.AspNetCore.Mvc;


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
        public async Task<ActionResult<List<Blog>>> GetAllBlogsAsync()
        {
            return await _blogService.GetAllBlogsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlogByIdAsync(string id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return blog;
        }

        [HttpPost]
        public async Task<ActionResult<Blog>> CreateBlogAsync(Blog blog)
        {
            await _blogService.CreateBlogAsync(blog);
            return CreatedAtRoute(nameof(GetBlogByIdAsync), new { id = blog.Id }, blog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogAsync(string id, Blog blogIn)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            await _blogService.UpdateBlogAsync(id, blogIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogAsync(string id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            await _blogService.RemoveBlogAsync(id);

            return NoContent();
        }
    }
}
