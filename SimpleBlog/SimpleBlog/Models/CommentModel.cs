using SimpleBlog.Entities;

namespace SimpleBlog.Models
{
    public class CommentModel
    {
        // This model is used to hold features of the selected blog
        public CommentModel()
        {
            SelectedBlog = new Blog();
        }
        public Blog SelectedBlog { get; set; }
        public Comment SelectedComment { get; set; }
        public DateTime SelectedCreatedAt { get; set; }
    }
}
