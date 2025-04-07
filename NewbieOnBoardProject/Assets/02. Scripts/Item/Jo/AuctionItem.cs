using System;

[Serializable]
public class AuctionItem
{
    public Item Item;               // 아이템 기본 정보 (아이템 종류 + 수량)
    public int RegisterPrice;       // 등록 가격 (시작가)
    public int CurrentBidPrice;     // 현재 입찰가
    public int InstantBuyPrice;     // 즉시 구매가
    public string SellerUID;        // 등록자 고유 식별자 (예: 유저 ID)

    public AuctionItem(Item item, int registerPrice, int instantBuyPrice, string sellerUID)
    {
        Item = item;
        RegisterPrice = registerPrice;
        CurrentBidPrice = registerPrice; // 시작가는 곧 최초 입찰가
        InstantBuyPrice = instantBuyPrice;
        SellerUID = sellerUID;
    }
}
