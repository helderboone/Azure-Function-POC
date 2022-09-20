namespace AzureFunctionPOC.Domain.ShoppingCart;

public class ShoppingCart
{
    public ShoppingCart(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
    }

    protected ShoppingCart() { }

    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Total { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();

    public bool HasVoucher { get; set; }
    public decimal Discount { get; set; }

    public Voucher Voucher { get; set; }
}