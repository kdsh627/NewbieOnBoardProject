using System;
using UnityEngine;

[Serializable]
public class OnSaleItem : Item
{
    public int Price;             
    public int Stock;         
    public string SellerName;    

    public OnSaleItem(ItemDataSO data, int amount, int price, int stock, string sellerName)
    {
        this.Data = data;
        this.Amount = amount;
        this.Price = price;
        this.Stock = stock;
        this.SellerName = sellerName;
    }
}