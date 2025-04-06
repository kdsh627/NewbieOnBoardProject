using TMPro;
using UnityEngine;


namespace Manager.UI
{
    public partial class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private GameObject _inventoryUI;

        private TMP_Text[] _itemTooltipText;
        public GameObject InventoryUI => _inventoryUI;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _inventoryUI.SetActive(false);
        }

        /// <summary>
        /// 인벤토리 켜고 끄기
        /// </summary>
        public void ToggleInventoryUI()
        {
            bool isOpen = _inventoryUI.activeSelf;

            _inventoryUI.SetActive(!isOpen);
        }

        /// <summary>
        /// 돈 UI 텍스트 업데이트
        /// </summary>
        /// <param name="money"></param>
        public void UpdateMoneyUI(int money, TMP_Text moneyText)
        {
            Debug.Log(moneyText.text);
            moneyText.text = "" + money;
        }

        /// <summary>
        /// 아이템 툴팁을 표시하는 함수
        /// </summary>
        /// <param name="item"></param>
        /// <param name="position"></param>
        public void ToggleItemTooltipUI(Vector3 position, Item item, bool isOpen, GameObject itemTooltipUI)
        {
            if (item.Data == null) return;

            if (isOpen)
            {
                _itemTooltipText = itemTooltipUI.GetComponentsInChildren<TMP_Text>();

                itemTooltipUI.transform.position = position + new Vector3(80f, 50f, 0);

                TMP_Text name = _itemTooltipText[0];
                TMP_Text amount = _itemTooltipText[1];

                name.text = item.Data.name;
                amount.text = "수량 : " + item.Amount;
            }

            itemTooltipUI.SetActive(isOpen);
        }
    }

}