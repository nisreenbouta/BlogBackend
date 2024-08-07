using BlogApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;

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

        public List<Blog> Get() => _blogs.Find(blog => true).ToList();

        public Blog Get(string id) => _blogs.Find<Blog>(blog => blog.Id == id).FirstOrDefault();

        public Blog Create(Blog blog)
        {
            _blogs.InsertOne(blog);
            return blog;
        }

        public void Update(string id, Blog blogIn) => _blogs.ReplaceOne(blog => blog.Id == id, blogIn);

        public void Remove(string id) => _blogs.DeleteOne(blog => blog.Id == id);
    }
}
