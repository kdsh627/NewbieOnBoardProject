using Inventory.Space;
using Manager.Inventory;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Inventory.UI
{
    public class InventoryUI : MonoBehaviour
    {
        public static InventoryUI Instance { get; private set; }

        [SerializeField] private TMP_Text _moneyText;
        private List<InventorySpace> _inventory = new List<InventorySpace>();

        private void Awake()
        {
            Instance = this;
            _inventory = GetComponentsInChildren<InventorySpace>().ToList();
        }

        void OnEnable()
        {
            Debug.Log("인벤UI켜짐");
            InventoryManager.Instance.UpdateMoney(_moneyText);
            for (int i = 0; i < _inventory.Count; i++)
            {
                _inventory[i].Index = i;
                _inventory[i].ItemData = InventoryManager.Instance.ItemList[i]; _inventory[i].SetItem();
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
