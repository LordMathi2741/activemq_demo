namespace Core.Entities;

public record Product
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}