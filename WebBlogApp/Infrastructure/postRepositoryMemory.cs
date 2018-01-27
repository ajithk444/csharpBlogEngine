using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Newtonsoft.Json;
using System.Linq;

namespace Infrastructure
{

    public class postRepositoryMemory : IBlogRepository
    {
        private static List<blogPost> _postList;
        private static int NextId = 1;

        public postRepositoryMemory()
        { 
            if (_postList == null)
            {
                _postList = new List<blogPost>();
            }
        }
        public void Add(blogPost newPost)
        {
            newPost.Id = NextId++;
            _postList.Add(newPost);
        }
        public void Delete(blogPost postToDelete)
        {
            // do we want to search for the id?
            var identifiedPost = GetById(postToDelete.Id);
            _postList.Remove(identifiedPost);
        }
        public void Edit(blogPost editedPost)
        {
            // var identifiedPostToUpdate = _postList.GetById(editedPost.Id);
            //    Find(d => d.Id == editedPost.Id);
            var identifiedPostToUpdate = GetById(editedPost.Id);

            identifiedPostToUpdate.AuthorLname = editedPost.AuthorLname;
            identifiedPostToUpdate.AuthorFname = editedPost.AuthorFname;
            identifiedPostToUpdate.Date = editedPost.Date;
            identifiedPostToUpdate.content = editedPost.content;
        }
        public blogPost GetById(int id)
        {
            var identifiedPost = _postList.Find(d => d.Id == id);
            return identifiedPost; 
        }
        public List<blogPost> ListAll()
        {
            return _postList;
        }
    }
}
