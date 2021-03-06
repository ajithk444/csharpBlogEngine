﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBlogApp.Models;

namespace WebBlogApp.Controllers
{
    public class HomeController : Controller
    {
        // how do I trigger this off of the startup file *****
        private readonly IBlogRepository _postRepo;
        // return to the below
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/using-constructors
        public HomeController(IBlogRepository postRepo)  
        {
            _postRepo = postRepo;  // this is the point at which an dependency has been injected from the outside world

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            var myPost = new blogPost
            {
                AuthorFname = "Bill",
                AuthorLname = "Chandler",
                Date = "29Jan2018",
                content = "some blog post somewhere at some time signifying something of scintillating substance"
            };

            _postRepo.Add(myPost);

            return View(myPost);


            return View();
        }
        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
