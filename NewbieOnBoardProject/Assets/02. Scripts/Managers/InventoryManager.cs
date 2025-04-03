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

        /// <summary>
        /// 돈을 업데이트하고 업데이트 가능여부 반환
        /// </summary>
        /// <param name="value"> 업데이트할 값</param>
        public bool UpdateMoney(TMP_Text moneyText, int value = 0)
        {
            if (_money + value < 0)
            {
                return false;
            }

            _money += value;

            if (moneyText != null)
            {
                UI.UIManager.Instance.UpdateMoneyUI(_money, moneyText);
            }
            return true;
        }


        /// <summary>
        /// 인벤토리에 아이템 추가, 추가 가능여부를 bool로 반환
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        public bool AddItem(Item data, int index, AddType type)
        {
            if (index == -1)
            {
                return false;
            }

            switch (type)
            {
                case AddType.Fixed: //정해진 자리에 데이터를 채워 넣음
                    _itemList[index] = data;
                    return true;
                case AddType.Sorted: //앞에서 부터 집어 넣음
                    for (int i = 0; i < _inventorySize; i++)
                    {
                        //리스트 순환하여 처음으로 데이터가 없는 곳에 삽입
                        if (_itemList[i] == null)
                        {
                            _itemList[i] = data;
                            return true;
                        }
                    }
                    break;
            }
            //끝까지 순회할 때까지 데이터 삽입이 없으면 꽉 찬 인벤토리 이므로 false반환
            return false;
        }

        /// <summary>
        /// 인벤토리에 아이템 제거
        /// </summary>
        /// <param name="index"></param>
        public void RemoveItem(int index)
        {
            _itemList[index].Data = null;
            _itemList[index].Amount = 0;
        }

        /// <summary>
        /// 인벤토리에 아이템 교환
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public void ExchangeItem(int index1, int index2)
        {
            Item temp = _itemList[index1];
            _itemList[index1] = _itemList[index2];
            _itemList[index2] = temp;
        }
    }
}