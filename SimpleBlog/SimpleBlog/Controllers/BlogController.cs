using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Data;
using SimpleBlog.Entities;
using SimpleBlog.Models;

namespace SimpleBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly Context _context;
        private readonly BlogModel model;

        public BlogController(Context context, BlogModel model)
        {
            _context = context;
            this.model = model;
        }
        // List all blogs
        public IActionResult List()
        {
            return View(_context.Blogs.ToList());
        }
        // Create Page
        public IActionResult Create()
        {
            return View();
        }
        // Creates (Publishes) a new blog post
        [HttpPost]
        public IActionResult Create(Blog post)
        {
            post.CreatedAt = DateTime.Now;
            _context.Blogs.Add(post);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
        // Edit page for the selected blog
        public IActionResult Edit(int Id)
        {
            model.SelectedBlog = _context.Blogs.Find(Id);
            return View("Edit", model);
        }
        // Edits the selected blog post
        [HttpPost]
        public IActionResult Edit(BlogModel model)
        {
            _context.Blogs.Update(model.SelectedBlog);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
        // Deletes the selected blog post
        public IActionResult Delete(int Id)
        {
            // Find the selected blog post
            var blog = _context.Blogs.Include(b => b.Comments).FirstOrDefault(b => b.Id == Id);
            model.SelectedBlog = blog;

            blog.Comments.Clear(); // Clear all comments of selected blog
            _context.Blogs.Remove(blog); // Remove the selected blog
            _context.SaveChanges(); // Save changes
            return RedirectToAction("List");
        }
        // See details about the selected blog post
        public IActionResult Details(int Id)
        {
            // Find the selected blog post
            var blog = _context.Blogs.Include(b => b.Comments).FirstOrDefault(b => b.Id == Id);
            if (blog == null) // If not exist, throw NotFound
            {
                return NotFound();
            }
            model.SelectedBlog = blog;
            return View("Details", model);
        }

        public IActionResult Comment(int Id)
        {
            // Find the selected blog post
            var blog = _context.Blogs.Include(b => b.Comments).FirstOrDefault(b => b.Id == Id);
            if (blog == null) // If not exist, throw NotFound
            {
                return NotFound();
            }
            // Create new comment for the selected blog
            var commentModel = new CommentModel
            {
                SelectedBlog = blog,
                SelectedComment = new Comment()
            };
            return View("Comment", commentModel);
        }

        [HttpPost]
        public IActionResult Comment(CommentModel model)
        {
            model.SelectedComment.CreatedAt = DateTime.Now;
            // Find the selected blog post
            var blog = _context.Blogs.Include(b => b.Comments).FirstOrDefault(b => b.Id == model.SelectedBlog.Id);
            if (blog == null) // If not exist, throw NotFound
            {
                return NotFound();
            }
            blog.Comments.Add(model.SelectedComment); // Add comment
            _context.SaveChanges(); // Save changes
            return RedirectToAction("List", model);
        }

        public IActionResult Subscribe(int Id)
        {
            model.SelectedBlog = _context.Blogs.Find(Id); // Find the selected blog post
            model.SelectedBlog.SubscriberAmount++; // Increment subscribe amount for every click
            _context.SaveChanges(); // Save changes
            return RedirectToAction("List");
        }
    }
}
