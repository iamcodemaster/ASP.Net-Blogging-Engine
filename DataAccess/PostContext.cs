using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BloggingEngine.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingEngine.DataAccess
{
  public class PostContext : DbContext
  {
    public PostContext(DbContextOptions<PostContext> config) : base(config)
    {

    }

    public DbSet<Post> Posts { get; internal set; }
    public DbSet<Author> Authors { get; internal set; }
    public DbSet<PostComment> Comments { get; internal set; }
  }

  [Table("Post")]
  public class Post
  {  
    [Key]
    public int Id { get; set; }
    public string PostTitle { get; set; }
    public string PostContent { get; set; }
    public int AuthorId { get; set; }
    public Author PostAuthor { get; set; }
    public string PostDate { get; set; }
    public List<PostComment> Comments { get; set; }
    public Comment Comment { get; set; }

  }

  [Table("Author")]
  public class Author
  {  
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }

  [Table("PostComment")]
  public class PostComment {
    [Key]
    public int Id { get; set; }
    public string CommentAuthor { get; set; }
    public string CommentContent { get; set; }
    public int PostId { get; set; }
  }

}