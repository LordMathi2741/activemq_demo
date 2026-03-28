namespace Core.Entities;

public sealed class Deliver : BaseEntity
{
    private List<Product> Products { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string Address { get; set; }
    
    public decimal TotalPayment { get; set; }


    public Deliver(
        List<Product> products, 
        string from, 
        string to, 
        string address
        )
    {
        Products = products;
        From = from;
        To = to;
        Address = address;
        TotalPayment = Products.Sum( p => p.Price * p.Quantity);
    }
}