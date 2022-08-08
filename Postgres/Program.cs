using Postgres;

class Program
{
    public static void Main(string[] args)
    {
        using (var context = new PostContext()) {
            
            var newPost = new Post
            {
                Id = 1,
                Title = "پست جدید",
                AuthorName = "رضا ادیبی",
                Created = DateTime.UtcNow,
                Content = "محتوا",
                Likes = 20
            };
            
            context.Posts.Add(newPost);
            context.SaveChanges();
        }
    }
}