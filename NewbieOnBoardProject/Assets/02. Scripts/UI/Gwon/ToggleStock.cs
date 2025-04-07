using Manager.UI;
using UnityEngine;

namespace UI.Stock
{
    public class ToggleStock : MonoBehaviour
    {
        private bool _inPlayer = false;
        private bool _isOpen = false;

        private void Start()
        {
            UIManager.Instance.ToggleStockUI(_isOpen);
        }

        private void Update()
        {
            ToggleStockUI();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _inPlayer = true;
            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _inPlayer = false;
                _isOpen = false;
                UIManager.Instance.ToggleStockUI(_isOpen);
            }
        }

        private void ToggleStockUI()
        {
            if (Input.GetKeyDown(KeyCode.F) && _inPlayer)
            {
                _isOpen = !_isOpen;
                UIManager.Instance.ToggleStockUI(_isOpen);
            }
        }

    }
}
