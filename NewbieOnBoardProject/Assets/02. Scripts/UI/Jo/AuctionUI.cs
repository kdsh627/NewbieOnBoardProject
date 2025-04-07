using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory.Space;
using Manager.Auction;
using Manager.Inventory;
using TMPro;
using UnityEngine;

namespace Auction.UI
{
    public class AuctionUI : MonoBehaviour
    {
        public static AuctionUI Instance { get; private set; }
        [SerializeField] private TMP_Text _moneyText;
        private List<AuctionSpace> _auctionSlots = new();
        public List<AuctionSpace> AuctionSlots => _auctionSlots;

        private void OnEnable()
        {

            StartCoroutine(InitializeAuctionUI());
        }

        private void Awake()
        {
            Instance = this;
            AuctionManager.Instance.SortAuctionItems();
        }

        public void RebootAuctionUI()
        {
            StartCoroutine(InitializeAuctionUI());
        }


        public IEnumerator InitializeAuctionUI()
        {
            yield return null; // 한 프레임 대기하여 인스펙터 값 로드 보장

            _auctionSlots = GetComponentsInChildren<AuctionSpace>(true).ToList();
            int half = _auctionSlots.Count / 2;
            Debug.Log("_auctionslots.Count: " + _auctionSlots.Count);
            // AuctionManager 강제 초기화
            if (AuctionManager.Instance.AuctionItemList.Count < _auctionSlots.Count)
            {
                AuctionManager.Instance.InitializeAuctionList(_auctionSlots.Count);
            }

            // 위쪽 경매 슬롯
            for (int i = 0; i < half; i++)
            {
                _auctionSlots[i].Index = i;
                _auctionSlots[i].ItemData = AuctionManager.Instance.AuctionItemList.Count > i
                    ? AuctionManager.Instance.AuctionItemList[i]
                    : null;
                _auctionSlots[i].SetItem();
            }

            // 아래쪽 인벤토리 슬롯
            for (int i = half; i < _auctionSlots.Count; i++)
            {
                int inventoryIndex = i - half;
                _auctionSlots[i].Index = i;
                Debug.Log(_auctionSlots[i].Index + " 를 " + i + "로 만들었음.");
                if (InventoryManager.Instance.ItemList.Count > inventoryIndex)
                {
                    _auctionSlots[i].ItemData = InventoryManager.Instance.ItemList[inventoryIndex];
                    AuctionManager.Instance.AuctionItemList[i] = InventoryManager.Instance.ItemList[inventoryIndex];
                }
                else
                {
                    _auctionSlots[i].ItemData = null;
                }

                _auctionSlots[i].SetItem();
            }

            InventoryManager.Instance.UpdateMoney(_moneyText);
        }
    }
}