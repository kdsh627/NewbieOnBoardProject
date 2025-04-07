using System;
using UnityEngine;

[Serializable]
public class OnSaleItem : Item
{
    public int Price;                

    public OnSaleItem(ItemDataSO data, int amount, int price, string sellerName)
    {
        this.Data = data;
        this.Amount = amount;
        this.Price = price;
        this.SellerName = sellerName;
    }
}