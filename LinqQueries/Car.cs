namespace LinqQueries;

public record Car
{
    public int Id { get; set; }
    public string CarName { get; set; }
    public int MadeYear { get; set; }
    public string OwnerNationalId { get; set; }
}