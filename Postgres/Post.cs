namespace Postgres;

public record Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string AuthorName { get; set; }
    public DateTime Created { get; set; }
    public string Content { get; set; }
    public int Likes { get; set; }
}