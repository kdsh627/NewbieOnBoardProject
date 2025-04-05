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
        private Button _buyButton;
        private TMP_InputField _buyInputField;
        private Button _sellButton;
        private TMP_InputField _sellInputField;

        private void Awake()
        {
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
                if(isItemAdded)
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
    }

}
