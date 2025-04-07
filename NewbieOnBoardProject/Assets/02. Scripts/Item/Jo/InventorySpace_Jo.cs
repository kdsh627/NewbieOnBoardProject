using UnityEngine;
using UnityEngine.UI;
using Manager.UI;

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