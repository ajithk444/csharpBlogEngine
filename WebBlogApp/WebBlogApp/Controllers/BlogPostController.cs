using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebBlogApp.Controllers
{
    public class BlogPostController : Controller
    {

        private readonly IBlogRepository _postRepo;


        public BlogPostController(IBlogRepository postRepo)  // ZZZ -- question for Jeff here; what exactly is this doing?
        {
            _postRepo = postRepo;
        }

        // GET: BlogPost
        public IActionResult Index()
        {
            return View(_postRepo.ListAll());
        }

        // GET: BlogPost/Details/5
        public IActionResult Details(int id)
        {
            return View(_postRepo.GetById(id));
        }

        // GET: BlogPost/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogPost/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(blogPost newBlog, IFormCollection collection)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _postRepo.Add(newBlog);
                    return RedirectToAction(nameof(Index));
                }

                return View(newBlog);
            }
            catch
            {
                return View(newBlog);
            }
        }
        // GET: BlogPost/Edit/5
        public IActionResult Edit(int id)
        {
            return View(_postRepo.GetById(id));
        }
        // POST: BlogPost/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(blogPost editedPost, int id, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _postRepo.Edit(editedPost);
                    return RedirectToAction(nameof(Index));
                }
                return View(editedPost);
            }
            catch
            {
                return View(editedPost);
            }
        }
        // GET: BlogPost/Delete/5
        public IActionResult Delete(int id)
        {
            return View(_postRepo.GetById(id));
        }
        // POST: BlogPost/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(blogPost postToDelete, int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                _postRepo.Delete(postToDelete);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }   
    }
}