using Sqlite;

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
                Created = DateTime.Now,
                Content = "محتوا",
                Likes = 20
            };
            
            context.Posts.Add(newPost);
            context.SaveChanges();
        }
    }
}