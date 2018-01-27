using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class blogPost
    {
        public int Id { get; set; }
        public string AuthorLname { get; set; }
        public string AuthorFname { get; set; }
        public string Date { get; set; }
        public string content { get; set; }
    }
}
