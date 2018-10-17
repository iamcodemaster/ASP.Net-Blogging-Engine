using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BloggingEngine.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingEngine.DataAccess
{
  public class PostContext : DbContext
  {
    public PostContext(DbContextOptions<PostContext> config) : base(config)
    {

    }

    public DbSet<Post> Posts { get; internal set; }
  }

  public class Post
  {  
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Author_id { get; set; }
    public string Post_date { get; set; }
  }
}