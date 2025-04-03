using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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


        [SerializeField] private GameObject _stockUI;
        [SerializeField] private List<TMP_Text> _stocksName;
        [SerializeField] private List<PriceField> _stockPriceFields;

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


    }
}
