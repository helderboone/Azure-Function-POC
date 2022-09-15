using System;

namespace AzureFunctionPOC.Messages;

public class RemoveShoppingCartMessage
{
    public Guid CustomerId { get; set; }
}
