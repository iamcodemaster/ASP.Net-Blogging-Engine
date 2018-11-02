using System;
using System.Collections.Generic;
using BloggingEngine.DataAccess;

public class BlogPostModel
{  
    public int Id { get; set; }
    public string PostTitle { get; set; }
    public string PostContent { get; set; }
    public int AuthorId { get; set; }
    public Author PostAuthor { get; set; }
    public string PostDate { get; set; }
    public List<PostComment> Comments { get; set; }
    public Comment Comment { get; set; }
}

// public class Author
//   {  
//     public int Id { get; set; }
//     public string FirstName { get; set; }
//     public string LastName { get; set; }
//   }

public class BlogPostList {
    public List<Post> BlogPosts { get; set; }
}

public class Comment {
    public int Id { get; set; }
    public string CommentAuthor { get; set; }
    public string CommentContent { get; set; }
    public int PostId { get; set; }

}

// To create a blog post and add a new author
public class PostWithAuthor {
    public Author Author { get; set; }
    public BlogPostModel Post { get; set; }
}
