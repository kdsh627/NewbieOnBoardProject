using System;
using UnityEngine;

[Serializable]
public class OnSaleItem : Item
{
    public int Price;               
    public string SellerName;    

    public OnSaleItem(ItemDataSO data, int amount, int price, string sellerName)
    {
        this.Data = data;
        this.Amount = amount;
        this.Price = price;
        this.SellerName = sellerName;
    }
}