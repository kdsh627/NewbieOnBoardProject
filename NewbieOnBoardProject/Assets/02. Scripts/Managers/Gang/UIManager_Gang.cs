using UnityEngine;
using UnityEngine.UI;

namespace Manager.UI
{
    public partial class UIManager : MonoBehaviour
    {

        [SerializeField] private GameObject _exchangeUIPanel;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _uploadButton;
        [SerializeField] private Button _withdrawButton;


        public void ToggleExchangeUI(bool isOpen)
        {
            _exchangeUIPanel.SetActive(isOpen);
            Debug.Log(isOpen);
        }
    }
}
