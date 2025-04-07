using Mono.Cecil;
using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public partial class Item
{
    public string SellerName;

    public ItemDataSO Data;
    public int Amount;

    public int StartBid;
    public int BuyNowPrice;
    public float RemainingTime;

    public List<string> BuyPlayer;
    public List<int> BuyPrice;

    public Item Clone()
    {
        return new Item
        {
            Data = this.Data, // ItemData는 그대로 공유해도 무방 (수정되지 않으니까)
            Amount = this.Amount,
            StartBid = this.StartBid,
            BuyNowPrice = this.BuyNowPrice,
            SellerName = this.SellerName,
            RemainingTime = this.RemainingTime
        };
    }
}