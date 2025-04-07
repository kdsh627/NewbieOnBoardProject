using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory.Space;
using Manager.Auction;
using Manager.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Auction.UI;

namespace Bidding.UI
{
    public class BiddingUI : MonoBehaviour
    {
        public static BiddingUI Instance { get; private set; }

        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private TMP_Text _itemNameText;
        [SerializeField] private TMP_Text _noticeText;
        [SerializeField] private TMP_InputField _bidInput;
        [SerializeField] private TMP_InputField _buyerInput;
        [SerializeField] private Button _registerButton;
        

        [SerializeField] private Item _biddingItem; //TODO: 인스턴스로 선택된 아이템 넘겨받기
        [SerializeField] private int _biddingindex; //TODO: 인스턴스로 선택된 아이템 넘겨받기
        [SerializeField] private int _bidPrice;
        [SerializeField] private string _buyer;
        private void OnEnable()
        {
            _noticeText.text = "";
            InventoryManager.Instance.UpdateMoney(_moneyText);

        }

        private void Awake()
        {
            Instance = this;

        }

        private void Start()
        {

        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _moneyText.text = "";
                _biddingItem = null;
                _biddingindex = -1;
                _bidInput.text = null;
                _biddingItem = null;
                _biddingindex = -1;
                _buyer = null;
                gameObject.SetActive(false);
            }
        }

        public void SetBuyer()
        {
            _buyer = _buyerInput.text;
        }

        public void SetBidItem(Item selected, int index)
        {
            _noticeText.text = "";
            _biddingItem = selected;
            _biddingindex = index;
            Debug.Log("registerItem: " + _biddingItem.Data.Name);
            _itemNameText.text = _biddingItem.Data.Name + " x " + _biddingItem.Amount + "현재가: " + _biddingItem.StartBid;



        }

        public void Bidding()
        {
            
            int.TryParse(_bidInput.text, out _bidPrice);

        }


        public void EnterBid()
        {
            if(_biddingItem.StartBid < _bidPrice)
            {
                

                Debug.Log("4");
                if (_biddingItem.BuyPlayer.Count == 0)
                {
                    // 첫 입찰
                    _biddingItem.BuyPlayer.Add(_buyer);
                    _biddingItem.BuyPrice.Add(_bidPrice);
                    AuctionManager.Instance.AuctionItemList[_biddingindex].StartBid = _bidPrice;
                    InventoryManager.Instance.UpdateMoney(_moneyText, -_bidPrice);
                }
                for (int i = 0; i < _biddingItem.BuyPlayer.Count; i++)
                {
                    Debug.Log("3");
                    if (_biddingItem.BuyPlayer[i] == null) //첫구매
                    {
                        Debug.Log("2");
                        
                        _biddingItem.BuyPlayer[_biddingItem.BuyPrice.Count + 1] = _buyer;
                        _biddingItem.BuyPrice[_biddingItem.BuyPrice.Count + 1] = _biddingItem.StartBid;
                        AuctionManager.Instance.AuctionItemList[_biddingindex].StartBid = _biddingItem.StartBid;
                        InventoryManager.Instance.UpdateMoney(_moneyText, -(_biddingItem.StartBid));
                        break;
                    }
                    else if(_biddingItem.BuyPlayer[i] == _buyer) //이전에 입찰 했었음
                    {
                        Debug.Log("1");
                        
                        _biddingItem.BuyPrice[i] = _biddingItem.StartBid;
                        AuctionManager.Instance.AuctionItemList[_biddingindex].StartBid = _biddingItem.StartBid;
                        InventoryManager.Instance.UpdateMoney(_moneyText, -(_biddingItem.StartBid - _biddingItem.BuyPrice[i]));

                        break;
                    }
                }
                
                gameObject.SetActive(false);
            }
            else
            {
                if(InventoryManager.Instance.Money < _bidPrice)
                {
                    _noticeText.text = "입찰할 돈이 부족합니다.";
                }
                else
                {
                    _noticeText.text = "입찰가는 기존 가격보다 높아야 합니다.";
                }
                
            }
        }
        

    }
}

