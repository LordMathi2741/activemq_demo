using Core.Entities;
using Core.Response;

namespace Core.Helper;

public class MockHelper
{
    public static Response<Deliver> CreateMockDeliver()
    {
        var products = new List<Product>();
        var product1 = new Product
        {
            Name = "Tv 4k",
            Price = 1000,
            Quantity = 3
        };
        products.Add(product1);
        var product2 = new Product
        {
            Name = "PS5",
            Price = 300,
            Quantity = 1
        };
        products.Add(product2);
        var deliver = new Deliver(
            products,
            "johndoe@seller.com",
            "johndoe@buyer.com",
            "Av Nekolas 123"
            );
        var response = Response<Deliver>.Success(deliver);
        return response;
    }
}