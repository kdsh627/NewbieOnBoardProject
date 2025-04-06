using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory.UI;
using Inventory.Space;
using Manager.Auction;
using Manager.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Auction.UI;

namespace Register.UI
{
    public class RegisterUI : MonoBehaviour
    {
        public static RegisterUI Instance { get; private set; }

        [SerializeField] private TMP_Text _itemNameText;
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private TMP_Text _infoText;
        [SerializeField] private TMP_InputField _sellerName;
        [SerializeField] private TMP_InputField _startBidInput;
        [SerializeField] private TMP_InputField _buyNowInput;
        [SerializeField] private Button _registerButton;
        [SerializeField] private int _selectedIndex;
        [SerializeField] private GameObject _inventoryUI;
        [SerializeField] private GameObject _auctionUI;

        [SerializeField] private Item registerItem; //TODO: 인스턴스로 선택된 아이템 넘겨받기

        private void OnEnable()
        {
            _infoText.text = "";
            InventoryManager.Instance.UpdateMoney(_moneyText);

        }

        private void Awake()
        {
            Instance = this;

        }

        private void Start()
        {

        }

        public void SetRegisterItem(Item selected, int index)
        {
            registerItem = selected;
            Debug.Log("registerItem: " + registerItem.Data.Name);
            _itemNameText.text = registerItem.Data.Name + " x " + registerItem.Amount;
            Debug.Log("_selectedIndex: " + _selectedIndex);
            _selectedIndex = index;

        }

        public void SetSellerName()
        {
            registerItem.SellerName = _sellerName.text;
        }
        public void SetStartBid()
        {
            int.TryParse(_startBidInput.text, out registerItem.StartBid);
            Debug.Log("시작구매가 테스트: " + registerItem.StartBid);
        }

        public void SetBuyNowPrice()
        {
            int.TryParse(_buyNowInput.text, out registerItem.BuyNowPrice);
            Debug.Log("즉시구매가 테스트: " + registerItem.BuyNowPrice);
        }

        public void RegisterButtonDown()
        {
            if (registerItem.Data == null)
            {

            }
            // 금액이 충분한지 체크
            if (registerItem.StartBid / 10 <= InventoryManager.Instance.Money)
            {
                registerItem.RemainingTime = 86400f; // 1일로 설정

                // 경매장 빈자리 찾기
                for (int i = 0; i < AuctionManager.Instance.AuctionItemList.Count; i++)
                {
                    Debug.Log("경매장 빈자리 찾는중. 현재 인덱스: " + i);

                    // 빈 자리가 있는지 확인
                    if (AuctionManager.Instance.AuctionItemList[i] == null || AuctionManager.Instance.AuctionItemList[i].Data == null)
                    {
                        Debug.Log("빈자리 찾음. 현재 인덱스: " + i);

                        // 경매장에 아이템 추가
                        AuctionManager.Instance.AuctionItemList[i] = registerItem.Clone();
                        Debug.Log("인덱스 " + i + "에 물건 추가. AuctionItemList[i].Data.Name: " + AuctionManager.Instance.AuctionItemList[i].Data.Name);


                        // 선택된 아이템 초기화
                        AuctionSpace.Instance.SelectedItem.Data = null;
                        AuctionSpace.Instance.SelectedItem.Amount = 0;

                        // _selectedIndex - 27을 인덱스로 가지는 인벤토리 아이템 제거
                        int removeIndex = _selectedIndex - 27;
                        if (removeIndex >= 0 && removeIndex < InventoryManager.Instance.ItemList.Count)
                        {
                            InventoryManager.Instance.RemoveItem(removeIndex);
                            Debug.Log("인벤토리에서 아이템 제거됨: " + removeIndex);
                        }
                        else
                        {
                            Debug.LogError($"잘못된 인덱스: {removeIndex}. 인벤토리에서 아이템을 제거할 수 없습니다.");
                        }

                        // 경매 UI 업데이트
                        //AuctionUI.Instance.AuctionSlots[i].RefrechAuction();

                        _auctionUI.SetActive(true);
                        AuctionUI.Instance.InitializeAuctionUI();
                        AuctionSpace.Instance.SetItem();
                        AuctionUI.Instance.RebootAuctionUI();
                        // 인벤토리 UI 업데이트
                        _inventoryUI.SetActive(true);
                        InventorySpace.Instance.SetItem();
                        InventoryUI.Instance.UpdateInventory();
                        _inventoryUI.SetActive(false);
                        // 금액 업데이트
                        InventoryManager.Instance.UpdateMoney(_moneyText, -(registerItem.StartBid / 10));

                        // UI 비활성화

                        gameObject.SetActive(false);
                        break;
                    }
                }
            }
            else
            {
                _infoText.text = "수수료가 부족합니다.";
                Debug.Log("돈 부족함");
            }
        }

    }
}