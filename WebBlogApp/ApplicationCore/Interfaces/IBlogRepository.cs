using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IBlogRepository
    {
        List<blogPost> ListAll();
        blogPost GetById(int id);
        void Add(blogPost newPost);
        void Edit(blogPost editedPost);
        void Delete(blogPost postToDelete);
    }
}
