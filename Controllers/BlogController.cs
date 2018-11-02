
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
            // get posts in descending order to see the newest posts first
            var posts = _postContext.Posts.OrderByDescending(p => p.Id).ToList();
            var blogpostListModel = new BlogPostList();
            blogpostListModel.BlogPosts = posts;
            return View(blogpostListModel);
        }

        [Route("blog/create")]
        [HttpGet()]
        public IActionResult Create() {
            var blogpostWithAuthor = new PostWithAuthor();
            return View(blogpostWithAuthor);
        }

        [Route("blog/create")]
        [HttpPost]
        public IActionResult Create(PostWithAuthor item) {
            // Create new author
            var author = new BloggingEngine.DataAccess.Author() {
                FirstName = item.Author.FirstName,
                LastName = item.Author.LastName
            };
            // Add author to database
            _postContext.Authors.Add(author);
            _postContext.SaveChanges();
            // Get the id of the last added record
            var lastAddedAuthorId = author.Id;

            // Create new blog post
            var post = new BloggingEngine.DataAccess.Post() {
                PostTitle = item.Post.PostTitle,
                PostContent = item.Post.PostContent,
                AuthorId = lastAddedAuthorId,
                PostDate = item.Post.PostDate
            };
            // Add blog post to database
            _postContext.Posts.Add(post);
            _postContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [Route("blog/post/{id}")]
        [HttpGet()]
        public IActionResult Post([FromRoute]int id) {
            // get the post by id from query param
            var post = _postContext.Posts.Find(id);
            // send user back to index if post does not exist
            if (post == null)
            {
                return RedirectToAction("Index");
            }

            // get the author of the post
            var author = _postContext.Authors.Find(post.AuthorId);

            // get the comments of the post by post id
            var postComments = _postContext.Comments.Where(c => c.PostId == post.Id);
            var comments = postComments.ToList();

            // build author model
            var authorModel = new Author() {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };

            // build blog post model
            var blogpostModel = new BlogPostModel() {
                Id = post.Id,
                PostTitle = post.PostTitle,
                PostContent = post.PostContent,
                AuthorId = post.AuthorId,
                PostAuthor = authorModel,
                PostDate = post.PostDate,
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

        [Route("blog/post/{id}/comment")]
        [HttpPost]
        public IActionResult Comment([FromRoute] int id, BlogPostModel item) {
            var comment = new BloggingEngine.DataAccess.PostComment() {
                CommentAuthor = item.Comment.CommentAuthor,
                CommentContent = item.Comment.CommentContent,
                PostId = id
            };
            _postContext.Comments.Add(comment);
            _postContext.SaveChanges();

            return RedirectToAction("Post");
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
                PostTitle = post.PostTitle,
                PostContent = post.PostContent,
                AuthorId = post.AuthorId,
                PostDate = post.PostDate
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

            post.PostTitle = item.PostTitle;
            post.PostContent = item.PostContent;
            post.PostDate = item.PostDate;

            _postContext.Posts.Update(post);
            _postContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
