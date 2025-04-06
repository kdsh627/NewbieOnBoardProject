using UnityEngine;
using UnityEngine.UI;

namespace Manager.UI
{
    public partial class UIManager : MonoBehaviour
    {

        [SerializeField] private GameObject _exchangeUIPanel;
        [SerializeField] private GameObject _buyItemUI;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _uploadButton;
        [SerializeField] private Button _withdrawButton;

        public GameObject BuyItemUI => _buyItemUI;

        public void ToggleExchangeUI(bool isOpen)
        {
            _exchangeUIPanel.SetActive(isOpen);
        }
        
        public void ToggleBuyItemUI(bool show)
        {
            _buyItemUI.SetActive(show);
        }

        public bool BuyItemUIIsActive(){
            return _buyItemUI.activeSelf;
        }
    }
}
