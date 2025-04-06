using System.Collections.Generic;
using System.Linq;
using Inventory.Space;
using Manager.Inventory;
using TMPro;
using UnityEngine;

namespace Inventory{

    public class BuyItemInventory: Inventory
    {
        void OnEnable(){
            InventoryManager.Instance.UpdateMoney(_moneyText);
            for (int i = 0; i < _inventory.Count; i++)
            {
                _inventory[i].Index = i;
                _inventory[i].ItemData = InventoryManager.Instance.OnSaleItemList[i];
                _inventory[i].SetItem();
                _inventory[i]._itemTooltipUI = _ItemToolTipUI;
            }
        }
        new public void UpdateInventory()
        {
            for (int i = 0; i < _inventory.Count; i++)
            {
                _inventory[i].Index = i;
                _inventory[i].ItemData = InventoryManager.Instance.OnSaleItemList[i];
                _inventory[i].SetItem();
            }
        }
    }
}


