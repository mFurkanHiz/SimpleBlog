using SimpleBlog.Entities;

namespace SimpleBlog.Models
{
    public class BlogModel
    {
        public BlogModel()
        {
            SelectedBlog = new Blog();
        }
        // Selected Blog, provdes us to hold the features of Blog in this variable.
        public Blog SelectedBlog { get; set; }
    }
}
