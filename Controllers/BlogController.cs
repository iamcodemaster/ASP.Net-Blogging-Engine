
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

        public IActionResult Delete( [FromRoute] int postId) {
            return View();
        }

        public IActionResult Edit( [FromRoute] int id) {
            var blogpost = new BlogPostModel() {
                Id = id,
                Title = "Blog Post Title 3", 
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla in dolor pharetra, ultrices ante vel, mattis justo. Nulla facilisi. Curabitur nec interdum ex. Phasellus posuere quam eget vulputate vulputate. Mauris maximus ex ac enim auctor, nec sagittis ipsum ultricies. Sed ac tellus nec est pellentesque aliquam ac sit amet arcu. Pellentesque pellentesque ipsum quis est cursus, sollicitudin blandit ex facilisis. Pellentesque malesuada mi ligula, vitae hendrerit mi finibus quis. Nullam velit neque, lobortis vitae tempor quis, rhoncus id arcu. Ut convallis ligula id vehicula iaculis. Integer non felis in ipsum viverra feugiat. Nunc fringilla, leo a dictum rhoncus, dui enim feugiat risus, vestibulum mollis ligula magna vel sapien. Maecenas a est neque. Aliquam iaculis feugiat ex, vitae porttitor magna luctus vitae. Sed quis convallis magna, vel cursus turpis. Aenean mattis, velit nec faucibus interdum, metus odio convallis dolor, vitae cursus nibh justo a dui. Aliquam erat volutpat. Pellentesque ut euismod neque. Proin lobortis magna in lacus sodales ornare. Aliquam id tempus nunc, quis feugiat mauris. Aliquam efficitur euismod mi, sed scelerisque lectus blandit quis. Mauris faucibus malesuada nulla eget consectetur. Ut mollis justo tempus odio mattis malesuada. Praesent aliquet magna at urna vehicula, interdum imperdiet diam accumsan. Nullam sodales consequat semper. Cras tincidunt, felis non blandit fermentum, quam ex pulvinar lacus, a fringilla ante lacus a velit. Fusce aliquet felis ut urna euismod semper ac ut massa. Nunc eget nunc dapibus, dapibus urna eu, bibendum ligula. Cras pretium ex et nulla ultrices rutrum. Aenean vestibulum varius diam, eget tempor sapien eleifend non. Duis erat urna, efficitur vel placerat ac, ultricies ac mi. Curabitur porta nunc vel tellus iaculis luctus. Proin malesuada ante vel massa pulvinar pretium. Curabitur tempor commodo condimentum. Suspendisse eu eros neque. Vestibulum id sem id arcu tincidunt euismod eu ut metus. Morbi elementum dolor ut cursus venenatis. Sed nec purus neque. Maecenas tincidunt tristique justo elementum ornare. Sed sagittis massa sed interdum mattis. Donec sed suscipit nisl. Donec maximus magna eu nibh condimentum congue. Ut nec mauris posuere, ullamcorper urna tincidunt, eleifend nibh. Proin laoreet, justo ut convallis rutrum, arcu libero pulvinar ligula, eget cursus orci ipsum a lectus. Vivamus a justo massa. Mauris tristique, sapien tempor porttitor semper, quam risus hendrerit sapien, at suscipit justo odio ut elit.", 
                Author_id = 2
            };
            return View(blogpost);
        }
    }
}
