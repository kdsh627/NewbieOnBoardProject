using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Manager.Inventory
{
    public partial class InventoryManager : MonoBehaviour
    {
        public bool SellItem(ItemDataSO data, int Amount)
        {
            int totalAmount = 0;
            List<(int, Item)> findList = new List<(int, Item)>();

            for (int i = 0; i < _itemList.Count; ++i)
            {
                if (_itemList[i].Data == data)
                {
                    totalAmount += _itemList[i].Amount;
                    findList.Add((i, _itemList[i]));
                }
            }

            if (totalAmount < Amount) //팔 수 없는 양이면
            {
                return false;
            }


            foreach ((int index, Item itemData) item in findList)
            {
                if (item.itemData.Amount - Amount <= 0)
                {
                    Amount -= item.itemData.Amount;
                    RemoveItem(item.index);
                }
                else
                {
                    item.itemData.Amount -= Amount;
                }
            }
            return true;
        }
    }
}