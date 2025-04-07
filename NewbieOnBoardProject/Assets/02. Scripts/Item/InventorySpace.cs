using Manager.Inventory;
using Manager.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Inventory.Space
{
    public partial class InventorySpace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
        IDropHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public static InventorySpace Instance { get; private set; }

        public int Index = -1;
        public Item ItemData = null;
        public bool IsDrop = false;
        public bool IsEmpty = false;

        [SerializeField] private RectTransform _itemRectTrans;
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private Image _itemImage;

        private Image[] _images;
        private GameObject _dragObject;

        public Image ItemImage => _itemImage;

        private void Awake()
        {
            Instance = this;
            _images = GetComponentsInChildren<Image>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ToggleItemTooltip(true);
            ChangeImageAlpha(182);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ToggleItemTooltip(false);
            ChangeImageAlpha(255);
        }

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

        public void OnDrag(PointerEventData eventData)
        {
            if (IsEmpty)
            {
                return;
            }
            
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Vector2 mousePos = Mouse.current.position.ReadValue();
                _dragObject.transform.position = mousePos;
            }
        }

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



        public void ToggleItemTooltip(bool isOpen)
        {
            UIManager.Instance.ToggleItemTooltipUI(_itemRectTrans.position, ItemData, isOpen);
        }

        private void SwapItem(ref Item data1, ref Item data2)
        {
            Item tmp = data1;
            data1 = data2;
            data2 = tmp;
        }

        public void SetItem()
        {
            InventoryManager.Instance.AddItem(ItemData, Index, AddType.Fixed);

            if (ItemData.Data == null)
                return;

            _itemRectTrans.gameObject.SetActive(true);
            _itemImage.sprite = ItemData.Data.image;
        }

        private void ChangeImageAlpha(byte changeValue)
        {
            for (int i = 0; i < _images.Length; ++i)
            {
                Color32 color = _images[i].color;
                color.a = changeValue;
                _images[i].color = color;
            }
        }
    }
}
