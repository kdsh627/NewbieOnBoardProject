using System;
using System.Collections.Generic;
using UnityEngine;

public partial class Item
{
    public Item CloneWithAuctionInfo()
    {
        return new Item
        {
            Data = this.Data,
            Amount = this.Amount,
            StartBid = this.StartBid,
            BuyNowPrice = this.BuyNowPrice,
            SellerName = this.SellerName,
            RemainingTime = this.RemainingTime,
            BuyPlayer = new List<string>(this.BuyPlayer ?? new List<string>()),
            BuyPrice = new List<int>(this.BuyPrice ?? new List<int>())
        };
    }
}
