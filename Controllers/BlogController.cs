
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BloggingEngine.Models;
using BloggingEngine.DataAccess;

namespace BloggingEngine.Controllers
{
    public class BlogController : Controller
    {
        private PostContext _postContext;

        public BlogController(PostContext postContext)
        {
            _postContext = postContext;
        }

        [Route("blog")]
        [HttpGet()]
        public IActionResult Index() {
            var posts = _postContext.Posts.ToList();
            var blogpostListModel = new BlogPostList();
            blogpostListModel.BlogPosts = posts;
            return View(blogpostListModel);
        }

        [Route("blog/post/{id}")]
        [HttpGet()]
        public IActionResult Post([FromRoute]int id) {
            var post = _postContext.Posts.Find(id);
            var blogpostModel = new BlogPostModel() {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Author_id = post.Author_id,
                Post_date = post.Post_date
            };
            return View(blogpostModel);
        }

        [Route("blog/post/{id}")]
        [HttpPost]
        public IActionResult Delete([FromRoute] int id) {
            _postContext.Remove(_postContext.Posts.Find(id));
            _postContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("blog/edit/{id}")]
        [HttpGet()]
        public IActionResult Edit([FromRoute] int id) {
            var post = _postContext.Posts.Find(id);
            var blogpostModel = new BlogPostModel() {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Author_id = post.Author_id,
                Post_date = post.Post_date
            };
            return View(blogpostModel);
        }
    }
}
