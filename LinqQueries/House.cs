namespace LinqQueries;

public record House
{
    public int Id { get; set; }
    public string Address { get; set; }
    public int Area { get; set; }
    public Person Owner { get; set; }
}