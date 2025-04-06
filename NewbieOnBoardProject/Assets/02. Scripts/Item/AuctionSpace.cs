using Bidding.UI;
using Manager.Auction;
using Manager.UI;
using Register.UI;
using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.Space
{
    public class AuctionSpace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public static AuctionSpace Instance { get; private set; }

        public int Index = -1;
        public Item ItemData = null;
        public Item SelectedItem;

        [SerializeField] private RectTransform _itemRectTrans;
        [SerializeField] private Image _itemImage;
        [SerializeField] private GameObject _itemUI;
        [SerializeField] private GameObject _registerUI;
        [SerializeField] private GameObject _biddingUI;

        private Image[] _images;

        public Image ItemImage => _itemImage;
        public RectTransform ItemRectTrans => _itemRectTrans;

        private void Awake()
        {
            Instance = this;
            if (_itemUI == null)
            {
                Transform found = transform.Find("ItemUI");
                if (found != null)
                {
                    _itemUI = found.gameObject;
                }
                else
                {
                    Debug.LogWarning($"[AuctionSpace] {gameObject.name} - ItemUI 오브젝트가 없음");
                }
            }

            if (_itemImage == null && _itemUI != null)
            {
                _itemImage = _itemUI.GetComponentInChildren<Image>(true);
                if (_itemImage == null)
                    Debug.LogWarning($"[AuctionSpace] {gameObject.name} - ItemUI 하위에 Image 없음");
            }

            if (_itemRectTrans == null)
            {
                _itemRectTrans = GetComponent<RectTransform>();
            }

            _images = GetComponentsInChildren<Image>(true);
        }
        /*
        public void SetItem()
        {
            AuctionManager.Instance.AddItem(ItemData, Index, AddType.Fixed);

            if (ItemData.Data == null)
                return;

            AuctionSpace.Instance.ItemRectTrans.gameObject.SetActive(true);
            AuctionSpace.Instance.ItemImage.sprite = ItemData.Data.image;
        }
        */
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("[Info] PointerEnter");
            ChangeImageAlpha(182);
            if (ItemData != null && ItemData.Data != null)
            {
                ToggleItemTooltip(true);

            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("[Info] PointerExit");
            ChangeImageAlpha(255);
            if (ItemData != null && ItemData.Data != null)
            {
                ToggleItemTooltip(false);

            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {

            Debug.Log(Index + " 클릭했음");



            // AuctionItemList[Index]가 null이 아니고, 그 안에 Data가 null이 아니면
            if (AuctionManager.Instance.AuctionItemList != null &&
                Index >= 0 && Index < AuctionManager.Instance.AuctionItemList.Count &&
                AuctionManager.Instance.AuctionItemList[Index] != null &&
                AuctionManager.Instance.AuctionItemList[Index].Data != null)
            {
                // 먼저 두 UI를 비활성화
                if (_registerUI != null) _registerUI.SetActive(false);
                if (_biddingUI != null) _biddingUI.SetActive(false);

                if (Index >= 27)
                {
                    // 등록 함수
                    Debug.Log("아이템 있는곳을 클릭했음");
                    SelectedItem = AuctionManager.Instance.AuctionItemList[Index];
                    Debug.Log("SelectedItem: " + SelectedItem.Data.Name);

                    // _registerUI만 활성화
                    if (_registerUI != null)
                    {
                        _registerUI.SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning("[AuctionSpace] _registerUI가 null입니다.");
                    }

                    RegisterUI.Instance.SetRegisterItem(SelectedItem, Index);


                }
                else
                {
                    if (_biddingUI != null)
                    {
                        _biddingUI.SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning("[AuctionSpace] _biddingUI가 null입니다.");
                    }

                    // 입찰 함수
                    SelectedItem = AuctionManager.Instance.AuctionItemList[Index];
                    BiddingUI.Instance.SetBidItem(SelectedItem, Index);

                    // _biddingUI만 활성화
                    if (_biddingUI != null)
                    {
                        _biddingUI.SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning("[AuctionSpace] _biddingUI가 null입니다.");
                    }

                    Debug.Log("입찰 성공. 현재가: " + AuctionManager.Instance.AuctionItemList[Index].StartBid);
                }
            }
            else
            {
                Debug.LogWarning($"[AuctionSpace] AuctionItemList[Index] 또는 Data가 null입니다. Index: {Index}");
            }
        }


        public void RefrechAuction()
        {
            AuctionSpace.Instance.SetItem();
        }

        public void ToggleItemTooltip(bool isOpen)
        {
            UIManager.Instance.ToggleItemTooltipUI(_itemRectTrans.position, ItemData, isOpen);
        }


        public void SetItem()
        {
            if (_itemUI == null || _itemImage == null)
            {
                Debug.LogWarning($"[SetItem] {gameObject.name} - _itemUI 또는 _itemImage 없음");
                return;
            }

            if (ItemData == null || ItemData.Data == null)
            {
                // ItemData 또는 ItemData.Data가 null일 경우 UI를 비활성화
                _itemUI.SetActive(false);
                _itemImage.sprite = null;
                return;
            }

            _itemUI.SetActive(true);

            // 아이템 이미지가 null이 아닐 경우에만 스프라이트 설정
            if (ItemData.Data.image != null)
            {
                _itemImage.sprite = ItemData.Data.image;
            }
            else
            {
                Debug.LogWarning("[SetItem] 이미지가 없습니다.");
            }
        }


        private void ChangeImageAlpha(byte changeValue)
        {
            for (int i = 0; i < _images.Length; i++)
            {
                Color32 color = _images[i].color;
                color.a = changeValue;
                _images[i].color = color;
            }
        }
    }
}
