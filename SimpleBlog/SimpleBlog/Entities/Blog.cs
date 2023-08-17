namespace SimpleBlog.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; } = "Anonymus";
        public int SubscriberAmount { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
