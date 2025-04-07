using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Manager.UI
{
    public partial class UIManager : MonoBehaviour
    {

        [SerializeField] private GameObject _exchangeUIPanel;
        [SerializeField] private GameObject _buyItemUI;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _uploadButton;
        [SerializeField] private Button _withdrawButton;

        public GameObject BuyItemUI => _buyItemUI;

        public void ToggleExchangeUI(bool isOpen)
        {
            _exchangeUIPanel.SetActive(isOpen);
        }

        public void ToggleBuyItemUI(bool show)
        {
            _buyItemUI.SetActive(show);
        }

        public bool BuyItemUIIsActive()
        {
            return _buyItemUI.activeSelf;
        }

        public void ToggleBuyItemTooltipUI(Vector3 position, OnSaleItem item, bool isOpen, GameObject itemTooltipUI)
        {
            if (item.Data == null) return;

            if (isOpen)
            {
                _itemTooltipText = itemTooltipUI.GetComponentsInChildren<TMP_Text>();

                itemTooltipUI.transform.position = position + new Vector3(80f, 50f, 0);

                TMP_Text name = _itemTooltipText[0];
                TMP_Text amount = _itemTooltipText[1];
                TMP_Text price = _itemTooltipText[2];


                name.text = item.Data.name;
                amount.text = "수량 : " + item.Amount;
                price.text = "가격 : " + item.Price;
            }

            itemTooltipUI.SetActive(isOpen);
        }
    }
}
