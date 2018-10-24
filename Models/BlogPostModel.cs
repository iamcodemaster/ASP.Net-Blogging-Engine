using System;
using System.Collections.Generic;
using BloggingEngine.DataAccess;

public class BlogPostModel
{  
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
    public string Post_date { get; set; }
    public List<PostComment> Comments { get; set; }
}

public class Author
  {  
    public int Id { get; set; }
    public string First_name { get; set; }
    public string Last_name { get; set; }
  }

public class BlogPostList {
    public List<Post> BlogPosts { get; set; }
}

public class Comment {
    public int Id { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }

}
