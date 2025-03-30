using Manager.Inventory;
using Manager.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Inventory.Space
{
    public class InventorySpace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
        IDropHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public int Index = -1;
        public Item ItemData = null;
        public bool IsDrop = false;
        public bool IsEmpty = false;
        public GameObject _itemTooltipUI;

        [SerializeField] private RectTransform _itemRectTrans;
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private Image _itemImage;

        private Image[] _images;
        private GameObject _dragObject;

        private void Awake()
        {
            _images = GetComponentsInChildren<Image>();
        }

        //마우스 올라가 있을 때
        public void OnPointerEnter(PointerEventData eventData)
        {
            ToggleItemTooltip(true);
            ChangeImageAlpha(182);
        }

        //마우스 내려갈 때
        public void OnPointerExit(PointerEventData eventData)
        {
            ToggleItemTooltip(false);
            ChangeImageAlpha(255);
        }

        //드래그 시작 시
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("드래그 시작");
            IsEmpty = !_itemRectTrans.gameObject.activeSelf;

            if (!IsEmpty)
            {
                _itemImage.sprite = null;
                _itemRectTrans.gameObject.SetActive(false);
                _dragObject = Instantiate(_itemPrefab, transform.root);
                _dragObject.GetComponent<Image>().sprite = ItemData.Data.image;
                _dragObject.GetComponent<Image>().raycastTarget = false;
            }
        }

        //아이템 드래그
        public void OnDrag(PointerEventData eventData)
        {
            if(IsEmpty)
            {
                return;
            }

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                // 드래그한 오브젝트가 마우스를 따라 움직이게 설정
                Vector2 mousePos = Mouse.current.position.ReadValue();
                _dragObject.transform.position = mousePos;
            }
        }

        //드래그 종료 시
        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("드래그 종료");

            Destroy(_dragObject);

            if (IsDrop)
            {
                IsDrop = false;
                IsEmpty = false;
                return;
            }

            _itemRectTrans.gameObject.SetActive(true);
            _itemImage.sprite = ItemData.Data.image;
        }

        //해당 공간에 아이템을 내려놓을 때
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("인벤토리 드랍");
            InventorySpace other = eventData.pointerDrag.GetComponent<InventorySpace>();

            IsDrop = true;
            other.IsDrop = true;

            SwapItem(ref ItemData, ref other.ItemData);
            other.SetItem();
            SetItem();

            other.ToggleItemTooltip(false);
            ToggleItemTooltip(true);
        }

        /// <summary>
        /// 툴팁 열기 닫기 함수
        /// </summary>
        /// <param name="isOpen"></param>
        public void ToggleItemTooltip(bool isOpen)
        {
            UIManager.Instance.ToggleItemTooltipUI(_itemRectTrans.position, ItemData, isOpen, _itemTooltipUI);
        }

        //아이템을 서로 교환하는 함수
        private void SwapItem(ref Item data1, ref Item data2)
        {
            Item tmp = data1;
            data1 = data2;
            data2 = tmp;
        }

        /// <summary>
        /// 아이템 데이터에 따라 아이템을 바뀌주는 함수
        /// </summary>
        public void SetItem()
        {
            InventoryManager.Instance.AddItem(ItemData, Index, AddType.Fixed);

            if (ItemData.Data == null)
            {
                return;
            }

            _itemRectTrans.gameObject.SetActive(true);
            _itemImage.sprite = ItemData.Data.image;
        }

        //이미지의 알파값을 변경하는 함수
        private void ChangeImageAlpha(byte changeValue)
        {
            for(int i = 0; i < _images.Length; ++i)
            {
                Color32 color = _images[i].color;
                color.a = changeValue;
                _images[i].color = color;
            }
        }
    }
}
