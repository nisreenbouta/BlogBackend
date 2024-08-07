using BlogApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Services
{
    public class BlogService
    {
        private readonly IMongoCollection<Blog> _blogs;

        public BlogService(IConfiguration config)
        {
            var client = new MongoClient(config.GetSection("MongoDBSettings:ConnectionString").Value);
            var database = client.GetDatabase(config.GetSection("MongoDBSettings:DatabaseName").Value);
            _blogs = database.GetCollection<Blog>("blogs");
        }

        public async Task<List<Blog>> GetAsync() => await _blogs.Find(blog => true).ToListAsync();

        public async Task<Blog> GetAsync(string id) => await _blogs.Find<Blog>(blog => blog.Id == id).FirstOrDefaultAsync();

        public async Task<Blog> CreateAsync(Blog blog)
        {
            await _blogs.InsertOneAsync(blog);
            return blog;
        }

        public async Task UpdateAsync(string id, Blog blogIn) => await _blogs.ReplaceOneAsync(blog => blog.Id == id, blogIn);

        public async Task RemoveAsync(string id) => await _blogs.DeleteOneAsync(blog => blog.Id == id);
    }
}
