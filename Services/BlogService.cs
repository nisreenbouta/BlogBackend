using MongoDB.Driver;


namespace BlogApi.Services
{
    public class BlogService
    {
        private readonly IMongoCollection<Blog> _blogs;

        //This constructor  is used to create an instance of the BlogService class with the necessary database connection and collection.
        public BlogService(IMongoDatabase database) 
        {
            _blogs = database.GetCollection<Blog>("Blogs");
        }

        //get all blogs
       public async Task<List<Blog>> GetAllBlogsAsync()
        {
            return await _blogs.Find(blog => true).ToListAsync();
        }

        //get blog by id
        public async Task<Blog> GetBlogByIdAsync(string id) {
            return await _blogs.Find<Blog>(blog => blog.Id == id).FirstOrDefaultAsync();
        }

        //create blog
        public async Task<Blog> CreateBlogAsync(Blog blog)
        {
            await _blogs.InsertOneAsync(blog);
            return blog;
        }
        
        //update blog
        public async Task UpdateBlogAsync(string id, Blog blogIn) {

           await _blogs.ReplaceOneAsync(blog => blog.Id == id, blogIn);
        }
        //delete blog
        public async Task RemoveBlogAsync(string id) {
             await _blogs.DeleteOneAsync(blog => blog.Id == id);
        }
    }
}
