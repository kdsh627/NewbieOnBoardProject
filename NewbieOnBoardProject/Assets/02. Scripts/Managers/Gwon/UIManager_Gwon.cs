using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Manager.UI
{
    public partial class UIManager : MonoBehaviour
    {
        [Serializable]
        private struct PriceField
        {
            public TMP_Text _curPrice;
            public TMP_Text _prePrice;
            public TMP_Text _highPrice;
            public TMP_Text _lowPrice;
        }

        [Header("---- 주식 종목 관련 UI ----")]
        [SerializeField] private GameObject _stockUI;
        [SerializeField] private List<TMP_Text> _stocksName;
        [SerializeField] private List<PriceField> _stockPriceFields;
        [SerializeField] private GameObject _stockNewsUI;

        [Header("---- 주식 사고 팔기 실패 시 UI ----")]
        [SerializeField] private GameObject _warningBuyUI;
        [SerializeField] private GameObject _warningInventoryUI;
        [SerializeField] private GameObject _warningSellUI;

        [Header("---- 주식 사고 팔기 성공 시 UI ----")]
        [SerializeField] private GameObject _successBuyUI;
        [SerializeField] private GameObject _successSellUI;


        public void ToggleStockUI(bool isOpen)
        {
            _stockUI.SetActive(isOpen);
        }

        public void SetStockName(int index, string name)
        {
            _stocksName[index].text = name;
        }

        public void SetPriceField(int index, int curPrice, int prePrice, int highPrice, int lowPrice, float changeRate)
        {
            _stockPriceFields[index]._curPrice.text = string.Format("현재 : {0} ({1:F1}%)", curPrice, changeRate);
            _stockPriceFields[index]._prePrice.text = "직전 : " + prePrice;
            _stockPriceFields[index]._highPrice.text = "고가 : " + highPrice;
            _stockPriceFields[index]._lowPrice.text = "저가 : " + lowPrice;
        }

        public void ShowSuccessBuyUI(string name, int Amount)
        {
            _successBuyUI.GetComponent<TMP_Text>().text = string.Format("{0} 주식을 {1}개 구매하였습니다.", name, Amount);
            _successBuyUI.SetActive(true);
        }
        public void ShowSuccessSellUI(string name, int Amount)
        {
            _successSellUI.GetComponent<TMP_Text>().text = string.Format("{0} 주식을 {1}개 판매하였습니다.", name, Amount);
            _successSellUI.SetActive(true);
        }

        public void ShowWarningBuyUI()
        {
            _warningBuyUI.SetActive(true);
        }

        public void ShowWarningSellUI()
        {
            _warningSellUI.SetActive(true);
        }

        public void ShowWarningInventoryUI(bool isBuy)
        {
            if (isBuy)
            {
                _warningInventoryUI.GetComponent<TMP_Text>().text = "아이템 칸이 부족합니다.";
            }
            else
            {
                _warningInventoryUI.GetComponent<TMP_Text>().text = "주식이 부족합니다.";
            }
            _warningInventoryUI.SetActive(true);
        }

        public void ShowStockNewsUI(string name, float changeRate)
        {
            if (changeRate >= 0)
            {
                _stockNewsUI.GetComponent<TMP_Text>().text = string.Format("{0} 주식이 {1:F1}% 상승하였습니다.", name, changeRate);
            }
            else
            {
                _stockNewsUI.GetComponent<TMP_Text>().text = string.Format("{0} 주식이 {1:F1}% 하락하였습니다.", name, changeRate);
            }
            _stockNewsUI.SetActive(true);
        }
    }
}
