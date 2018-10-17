using System;
using System.Collections.Generic;

public class BlogPostModel
{  
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Author_id { get; set; }
    public string Post_date { get; set; }
}

public class BlogPostList {
    public List<BlogPostModel> BlogPosts { get; set; }
}