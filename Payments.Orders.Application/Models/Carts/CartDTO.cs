﻿namespace Payments.Orders.Application.Models.Carts;

public class CartDTO
{
    public long? Id { get; set; }
    public List<CartItemDTO> CartItems { get; set; } = null!;

}
