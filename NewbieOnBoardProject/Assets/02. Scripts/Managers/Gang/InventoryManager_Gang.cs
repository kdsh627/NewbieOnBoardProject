using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Manager.Inventory
{
    public partial class InventoryManager : MonoBehaviour
    {
        [SerializeField] private List<OnSaleItem> _onSaleItemList;

        public List<OnSaleItem> OnSaleItemList => _onSaleItemList;
    }
}