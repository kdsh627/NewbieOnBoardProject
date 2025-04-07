using System.Collections.Generic;
using System.Linq;
using Inventory.Space;
using Manager.Inventory;

namespace Inventory
{

    public class BuyItemInventory : Inventory
    {
        private List<BuyInventorySpace> _buyInventory = new List<BuyInventorySpace>();

        private void Awake()
        {
            _buyInventory = GetComponentsInChildren<BuyInventorySpace>().ToList();
        }
        void OnEnable()
        {
            InventoryManager.Instance.UpdateMoney(_moneyText);
            for (int i = 0; i < _buyInventory.Count; i++)
            {
                _buyInventory[i].Index = i;
                _buyInventory[i].ItemData = InventoryManager.Instance.OnSaleItemList[i];
                _buyInventory[i].SetItem();
                _buyInventory[i]._itemTooltipUI = _ItemToolTipUI;
            }
        }
        new public void UpdateInventory()
        {
            for (int i = 0; i < _buyInventory.Count; i++)
            {
                _buyInventory[i].Index = i;
                _buyInventory[i].ItemData = InventoryManager.Instance.OnSaleItemList[i];
                _buyInventory[i].SetItem();
            }
        }
    }
}


