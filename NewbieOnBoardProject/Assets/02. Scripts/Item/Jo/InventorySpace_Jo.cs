using Manager.UI;
using UnityEngine;

namespace Inventory.Space
{
    public partial class InventorySpace
    {
        public GameObject _itemTooltipUI;

        public void ToggleItemTooltipWithUI(bool isOpen)
        {
            if (UIManager.Instance != null)
            {
                UIManager.Instance.ToggleItemTooltipUI(_itemRectTrans.position, ItemData, isOpen);
            }
        }
    }
}