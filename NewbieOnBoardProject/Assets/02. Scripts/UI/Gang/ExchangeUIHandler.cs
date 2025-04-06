using UnityEngine;
using UnityEngine.UI;
using Manager.UI;

public class ExchangeUIHandler : MonoBehaviour
{
    [SerializeField] private Button _buyButton;

    private void Start()
    {   

        _buyButton.onClick.AddListener(OpenBuyUI);
    }

    private void Update(){
        if (UIManager.Instance.BuyItemUIIsActive() && Input.GetKeyDown(KeyCode.Escape)){
            ReturnToExchangeUI();
        }
    }

    private void ReturnToExchangeUI(){
        UIManager.Instance.ToggleBuyItemUI(false);
        UIManager.Instance.ToggleExchangeUI(true);  
    }

    public void OpenBuyUI()
    {
        UIManager.Instance.ToggleExchangeUI(false);   
        UIManager.Instance.ToggleBuyItemUI(true);        
        // BuyItemUIManager.Instance.PopulateBuyItems();     // Populate the UI
    }


}