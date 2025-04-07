using System.Collections.Generic;
using Manager.Inventory;
using Manager.Stock;
using Manager.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Stock
{
    public class StockUI : MonoBehaviour
    {
        public int Index;
        [SerializeField] private GameObject _graphGroup;
        [SerializeField] private GameObject _graphBar;

        private Button _buyButton;
        private TMP_InputField _buyInputField;
        private Button _sellButton;
        private TMP_InputField _sellInputField;
        private float _width;
        private float _preHeight;
        private List<RectTransform> _graphBarsTrans = new List<RectTransform>();
        private Vector3 _nextPostion = Vector3.zero;
        private float _maxLine;
        private float _minLine;

        private void Awake()
        {
            _maxLine = 220.0f;
            _minLine = 0.0f;
            _width = 600.0f / 20;
            _nextPostion = new Vector2(0.0f, 220.0f / 2);

            Button[] buttons = GetComponentsInChildren<Button>();
            TMP_InputField[] inputFields = GetComponentsInChildren<TMP_InputField>();

            _buyButton = buttons[0];
            _buyInputField = inputFields[0];
            _sellButton = buttons[1];
            _sellInputField = inputFields[1];

            _buyButton.onClick.AddListener(BuyStock);
            _sellButton.onClick.AddListener(SellStock);
        }

        public void BuyStock()
        {
            int amount = int.Parse(_buyInputField.text);
            int price = amount * StockManager.Instance.Stocks[Index].Price;

            bool isBuy = InventoryManager.Instance.UpdateMoney(null, -price);

            if (isBuy)
            {
                StockManager.Instance.Stocks[Index].UpdateTotalBuyAmount(price);

                Item item = new Item();
                item.Data = StockManager.Instance.Stocks[Index].ItemData;
                item.Amount = amount;

                bool isItemAdded = InventoryManager.Instance.AddItem(item, 0, AddType.Sorted);

                //아이템 추가 성공
                if (isItemAdded)
                {
                    UIManager.Instance.ShowSuccessBuyUI(item.Data.Name, item.Amount);
                    StockManager.Instance.Stocks[Index].UpdateTotalBuyAmount(price);
                }
                else
                {
                    UIManager.Instance.ShowWarningInventoryUI(true);
                }
            }
            else //구매실패
            {
                UIManager.Instance.ShowWarningBuyUI();
            }
        }


        public void SellStock()
        {
            int amount = int.Parse(_sellInputField.text);
            int price = amount * StockManager.Instance.Stocks[Index].Price;

            ItemDataSO item = StockManager.Instance.Stocks[Index].ItemData;

            bool isSell = InventoryManager.Instance.SellItem(item, amount);

            if (isSell)
            {
                InventoryManager.Instance.UpdateMoney(null, price);
                UIManager.Instance.ShowSuccessSellUI(item.Name, amount);
                StockManager.Instance.Stocks[Index].UpdateTotalSellAmount(price);
            }
            else
            {
                UIManager.Instance.ShowWarningSellUI();
            }
        }

        public void CreateGraphBar(float changeRate)
        {
            RectTransform rect;

            Debug.Log(changeRate * 100);

            if (changeRate * 100 >= float.Epsilon)
            {
                _graphBar.GetComponent<Image>().color = Color.red;
                rect = Instantiate(_graphBar, _graphGroup.transform).GetComponent<RectTransform>();
                rect.position = new Vector2(rect.position.x, 220.0f / 2);
                rect.anchoredPosition = _nextPostion;
                _preHeight = changeRate * 100;
                _nextPostion = new Vector2(_nextPostion.x + rect.sizeDelta.x, _nextPostion.y + _preHeight);
            }
            else
            {
                _graphBar.GetComponent<Image>().color = Color.blue;
                rect = Instantiate(_graphBar, _graphGroup.transform).GetComponent<RectTransform>();
                rect.position = new Vector2(rect.position.x, 220.0f / 2);
                _preHeight = -changeRate * 100;
                rect.anchoredPosition = new Vector2(_nextPostion.x, _nextPostion.y - _preHeight);
                _nextPostion = new Vector2(_nextPostion.x + rect.sizeDelta.x, rect.anchoredPosition.y);
            }
            rect.sizeDelta = new Vector2(_width, _preHeight + 1.0f);

            if (_graphBarsTrans.Count < 20)
            {
                _graphBarsTrans.Add(rect);
            }
            else
            {
                MoveGraph();
            }
        }

        private void MoveGraph()
        {

        }
    }

}
