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

    public class postRepositoryFs : IBlogRepository
    {
        private static List<blogPost> _postList;
        private static int NextId = 1;
        private const string PATHNAME = "data";
        private const string FILENAME = "blogData.json";
        private readonly string _fullFilePath = Path.Combine(PATHNAME, FILENAME);


        public postRepositoryFs()
        { 
            if (_postList == null)
            {
                _postList = ReadList();

                try
                {
                    int topDogId = _postList.Max(d => d.Id);
                    NextId = ++topDogId;
                }
                catch (Exception)
                {
                    NextId = NextId++;

                }

            }
        }
        public void Add(blogPost newPost)
        {
            newPost.Id = NextId++;
            _postList.Add(newPost);
            SaveList();
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
        private void SaveList()
        {
            try
            {
                var listStr = JsonConvert.SerializeObject(_postList);
                if (!Directory.Exists(PATHNAME))
                {
                    Directory.CreateDirectory(PATHNAME);
                }
                File.WriteAllText(_fullFilePath, listStr);
            }
            catch (Exception)
            {
                // log error
            }
        }
        private List<blogPost> ReadList()
        {
            try
            {
                var listStr = File.ReadAllText(_fullFilePath);
                var rawList = JsonConvert.DeserializeObject<List<blogPost>>(listStr);  // complicated line
                if (rawList.Count > 0)
                {
                    return rawList;
                    // _dogs = rawList;
                }

            }
            catch (Exception ex)
            {
                // log error
            }
            return new List<blogPost>();
        }
        public List<blogPost> ListAll()
        {
            return _postList;
        }
    }
}
