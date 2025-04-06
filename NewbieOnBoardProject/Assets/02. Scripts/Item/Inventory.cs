using System.Collections.Generic;
using System.Linq;
using Inventory.Space;
using Manager.Inventory;
using TMPro;
using UnityEngine;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] protected GameObject _ItemToolTipUI;
        [SerializeField] protected TMP_Text _moneyText;
        private List<InventorySpace> _inventory = new List<InventorySpace>();

        private void Awake()
        {
            _inventory = GetComponentsInChildren<InventorySpace>().ToList();
        }

        void OnEnable()
        {
            InventoryManager.Instance.UpdateMoney(_moneyText);
            for (int i = 0; i < _inventory.Count; i++)
            {
                _inventory[i].Index = i;
                _inventory[i].ItemData = InventoryManager.Instance.ItemList[i];
                _inventory[i].SetItem();
                _inventory[i]._itemTooltipUI = _ItemToolTipUI;
            }
        }

        public void UpdateInventory()
        {
            for (int i = 0; i < _inventory.Count; i++)
            {
                _inventory[i].Index = i;
                _inventory[i].ItemData = InventoryManager.Instance.ItemList[i];
                _inventory[i].SetItem();
            }
        }
    }
}
