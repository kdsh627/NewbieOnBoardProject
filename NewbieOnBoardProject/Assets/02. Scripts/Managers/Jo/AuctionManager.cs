using Inventory.Space;
using Manager.Inventory;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Manager.Auction
{
    public class AuctionManager : MonoBehaviour
    {
        public static AuctionManager Instance { get; private set; }

        [SerializeField] private GameObject _itemTooltipUI;
        [SerializeField] private List<Item> _itemList = new();
        private bool _alreadyInitialized = false;
        public Item ItemData = null;
        public int Index = -1;
        public List<Item> AuctionItemList => _itemList;
        private TMP_Text[] _itemTooltipText;
        private void Awake()
        {
            Instance = this;
            _itemTooltipText = _itemTooltipUI.GetComponentsInChildren<TMP_Text>();
            DontDestroyOnLoad(gameObject);
            // 초기화는 AuctionUI에서 슬롯 개수 기반으로 호출
        }



        public void InitializeAuctionList(int count)
        {
            if (_alreadyInitialized) return;

            // null 검사 없이 그대로 확장
            while (_itemList.Count < count)
            {
                _itemList.Add(null); // 기존 값 유지, 부족한 부분만 채움
            }

            _alreadyInitialized = true;
            Debug.Log($"[AuctionManager] 초기화 완료 - 현재 슬롯 수: {_itemList.Count}");
        }

        

        public bool AddItem(Item data, int index, AddType type)
        {
            if (index == -1) return false;

            switch (type)
            {
                case AddType.Fixed:
                    _itemList[index] = data;
                    return true;
                case AddType.Sorted:
                    for (int i = 0; i < 54; i++)
                    {
                        if (_itemList[i] == null)
                        {
                            _itemList[i] = data;
                            return true;
                        }
                    }
                    break;
            }
            return false;
        }

        public Item GetAuctionItem(int index)
        {
            if (index >= 0 && index < _itemList.Count)
                return _itemList[index];
            return null;
        }

        public void RegisterAuctionItem(int index, Item item)
        {
            if (index >= 0 && index < _itemList.Count)
                _itemList[index] = item;
        }

        public void RemoveAuctionItem(int index)
        {
            if (index >= 0 && index < _itemList.Count)
                _itemList[index] = null;
        }

        public void SortAuctionItems() 
        {

            for (int i = 0; i < _itemList.Count; i++)
            {
                if(AuctionItemList[i].Data == null)
                {
                    Debug.Log("지움");
                    AuctionItemList.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// 아이템 툴팁을 표시하는 함수
        /// </summary>
        /// <param name="item"></param>
        /// <param name="position"></param>
        public void ToggleItemTooltipAuctionUI(Vector3 position, Item item, bool isOpen)
        {
            if (item.Data == false)
            {
                Debug.Log("[Info] not found item.Data");
                return;
            }


            if (isOpen)
            {
                _itemTooltipUI.transform.position = position + new Vector3(80f, 50f, 0);

                TMP_Text name = _itemTooltipText[0];
                TMP_Text amount = _itemTooltipText[1];
                TMP_Text seller = _itemTooltipText[2];
                TMP_Text startBid = _itemTooltipText[3];
                TMP_Text buyNowBid = _itemTooltipText[4];
                TMP_Text remainingTime = _itemTooltipText[5];


                name.text = item.Data.name;
                amount.text = "수량 : " + item.Amount;
                seller.text = item.SellerName;
                startBid.text = item.StartBid.ToString();
                buyNowBid.text = item.BuyNowPrice.ToString();
                remainingTime.text = item.RemainingTime.ToString();
            }

            _itemTooltipUI.SetActive(isOpen);
        }


    }
}
