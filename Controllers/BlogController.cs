
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

        [Route("blog/create")]
        [HttpGet()]
        public IActionResult Create() {
            var blogpostModel = new BlogPostModel();
            return View(blogpostModel);
        }

        [Route("blog/create")]
        [HttpPost]
        public IActionResult Create(BlogPostModel item) {
            var post = new BloggingEngine.DataAccess.Post() {
                Id = item.Id,
                Title = item.Title,
                Content = item.Content,
                AuthorId = item.AuthorId,
                Post_date = item.Post_date
            };
            _postContext.Posts.Add(post);
            _postContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [Route("blog/post/{id}")]
        [HttpGet()]
        public IActionResult Post([FromRoute]int id) {
            var post = _postContext.Posts.Find(id);
            if (post == null)
            {
                return RedirectToAction("Index");
            }

            var author = _postContext.Authors.Find(post.AuthorId);

            var comments = _postContext.Comments.ToList();

            var authorModel = new Author() {
                Id = author.Id,
                First_name = author.First_name,
                Last_name = author.Last_name
            };

            var blogpostModel = new BlogPostModel() {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                AuthorId = post.AuthorId,
                Author = authorModel,
                Post_date = post.Post_date,
                Comments = comments
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
            if (post == null)
            {
                return RedirectToAction("Index");
            }
            var blogpostModel = new BlogPostModel() {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                AuthorId = post.AuthorId,
                Post_date = post.Post_date
            };
            return View(blogpostModel);
        }

        // The tutorial I followed to create this method:
        // https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api-mac?view=aspnetcore-2.1
        [Route("blog/edit/{id}")]
        [HttpPost()]
        public IActionResult Update([FromRoute] int id, BlogPostModel item) {
            var post = _postContext.Posts.Find(id);
            if (post == null)
            {
                return RedirectToAction("Index");
            }

            post.Title = item.Title;
            post.Content = item.Content;
            post.Post_date = item.Post_date;

            _postContext.Posts.Update(post);
            _postContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
