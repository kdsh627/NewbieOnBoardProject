using TMPro;
using Manager.UI;

namespace Manager.Inventory
{
    public partial class InventoryManager
    {
        public void UpdateMoney(TMP_Text moneyText)
        {
            if (UIManager.Instance != null)
            {
                UIManager.Instance.UpdateMoneyUI(_money, moneyText);
            }
        }
    }
}