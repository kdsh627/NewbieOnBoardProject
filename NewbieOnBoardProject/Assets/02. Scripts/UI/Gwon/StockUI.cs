using Manager.Inventory;
using Manager.Stock;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Stock
{
    public class StockUI : MonoBehaviour
    {
        [SerializeField] private int _index;
        [SerializeField] private Button _buyButton;
        [SerializeField] private InputField _buyInputField;
        [SerializeField] private Button _selIButton;
        [SerializeField] private InputField _sellInputField;

        private void Awake()
        {
            _buyButton.onClick.AddListener(BuyStock);
            _selIButton.onClick.AddListener(SellStock);
        }

        public void BuyStock()
        {
            int amount = int.Parse(_buyInputField.text);
            int price = amount * StockManager.Instance.Stocks[_index].Price;

            bool isBuy = InventoryManager.Instance.UpdateMoney(-price);

            if (isBuy)
            {
                StockManager.Instance.Stocks[_index].UpdateTotalBuyAmount(price);

                Item item = new Item();
                item.Data = StockManager.Instance.Stocks[_index].ItemData;
                item.Amount = amount;
                InventoryManager.Instance.AddItem(item, 0, AddType.Sorted);
            }
        }

        public void SellStock()
        {
            int amount = int.Parse(_sellInputField.text);
            int price = amount * StockManager.Instance.Stocks[_index].Price;

            bool isSell = InventoryManager.Instance.UpdateMoney(price);

            if (isSell)
            {
                StockManager.Instance.Stocks[_index].UpdateTotalSellAmount(price);
            }
        }
    }

}
