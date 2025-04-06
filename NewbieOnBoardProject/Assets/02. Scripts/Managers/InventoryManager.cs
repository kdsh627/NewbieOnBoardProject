using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum AddType
{
    Sorted,
    Fixed
}

namespace Manager.Inventory
{
    public partial class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        [SerializeField] private int _money;
        [SerializeField] private List<Item> _itemList;
        [SerializeField] private int _inventorySize = 27;

        public int Money => _money;
        public List<Item> ItemList => _itemList;

        private void Awake()
        {
            Instance = this;
        }

        public bool UpdateMoney(TMP_Text moneyText, int value = 0)
        {
            if (_money + value < 0)
                return false;

            _money += value;
            UI.UIManager.Instance.UpdateMoneyUI(_money, moneyText);
            return true;
        }

        public bool AddItem(Item data, int index, AddType type)
        {
            if (index == -1) return false;

            switch (type)
            {
                case AddType.Fixed:
                    _itemList[index] = data;
                    return true;
                case AddType.Sorted:
                    for (int i = 0; i < _inventorySize; i++)
                    {
                        if (_itemList[i] == null)
                        {
                            _itemList[i] = data;
                            return true;
                        }
                    }
                    break;
            }
            return false;
        }

        public void RemoveItem(int index)
        {
            if (index < 0 || index >= _itemList.Count)
            {
                Debug.LogError($"Invalid index: {index}. Cannot remove item.");
                return;
            }

            _itemList[index].Data = null;
        }

        public void ExchangeItem(int index1, int index2)
        {
            Item temp = _itemList[index1];
            _itemList[index1] = _itemList[index2];
            _itemList[index2] = temp;
        }
    }
}
