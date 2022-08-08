using SqlServer;

class Program
{
    public static void Main(string[] args)
    {
        using var context = new BlogContext();
        
        EditExistingEntity(context);
        
        UpdateExistingEntity(context);
        
        UpdateNewEntity(context);
        
        RemoveEntity(context);
        
        context.SaveChanges();
    }

    private static void EditExistingEntity(BlogContext context)
    {
        var post1 = context.Posts.Find(1);
        post1.Content = "محتوای جدید 1";
    }

    private static void UpdateExistingEntity(BlogContext context)
    {
        var newPost = new Post
        {
            Id = 3,
            Title = "پست 3",
            Content = "محتوای 3 تغییر کرده"
        };
        context.Update(newPost);
    }

    private static void UpdateNewEntity(BlogContext context)
    {
        var newPost = new Post
        {
            Title = "پست جدید",
            Created = new DateTime(2021, 1, 6),
            Content = "محتوای 5"
        };
        context.Update(newPost);
    }

    private static void RemoveEntity(BlogContext context)
    {
        var post = new Post
        {
            Id = 2
        };
        context.Remove(post);
    }

    private static void AddRecords()
    {
        using var context = new BlogContext();
        var posts = new List<Post>
        {
            new()
            {
                Title = "پست 1",
                Created = new DateTime(2020, 5, 6),
                Content = "محتوا 1",
            },
            new()
            {
                Title = "پست 2",
                Created = new DateTime(2020, 5, 6),
                Content = "محتوا 2",
            },
            new()
            {
                Title = "پست 3",
                Created = new DateTime(2020, 3, 10),
                Content = "محتوا 3",
            },
            new()
            {
                Title = "پست 4",
                Created = new DateTime(2020, 3, 10),
                Content = "محتوا 4",
            },
        };

        context.Posts.AddRange(posts);
        context.SaveChanges();
    }
    
    
}