using System.Collections.Generic;
using UnityEngine;
namespace Manager.Inventory
{
    public partial class InventoryManager : MonoBehaviour
    {
        [SerializeField] private List<OnSaleItem> _onSaleItemList;

        public List<OnSaleItem> OnSaleItemList => _onSaleItemList;

        public bool AddBuyItem(OnSaleItem data, int index, AddType type)
        {
            if (index == -1)
            {
                return false;
            }

            switch (type)
            {
                case AddType.Fixed: //정해진 자리에 데이터를 채워 넣음
                    _onSaleItemList[index] = data;
                    return true;
                case AddType.Sorted: //앞에서 부터 집어 넣음
                    for (int i = 0; i < _inventorySize; i++)
                    {
                        //리스트 순환하여 처음으로 데이터가 없는 곳에 삽입
                        if (_onSaleItemList[i] == null)
                        {
                            _onSaleItemList[i] = data;
                            return true;
                        }
                    }
                    break;
            }
            //끝까지 순회할 때까지 데이터 삽입이 없으면 꽉 찬 인벤토리 이므로 false반환
            return false;
        }
    }


}