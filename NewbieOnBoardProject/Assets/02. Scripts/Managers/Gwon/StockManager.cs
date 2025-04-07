using System.Collections.Generic;
using System.Linq;
using Manager.UI;
using Stock;
using UI.Stock;
using UnityEngine;

namespace Manager.Stock
{
    public class StockManager : MonoBehaviour
    {
        public static StockManager Instance { get; private set; }

        [SerializeField] private GameObject _stockGroupUI;
        [SerializeField] private float _maxUpdateTime = 15.0f;

        private SortedList<float, string> changeRateList = new SortedList<float, string>();
        private List<StockClass> _stocks;
        private StockUI[] _stockUIs;
        private float _updateTime = float.Epsilon;

        public List<StockClass> Stocks => _stocks;

        void Awake()
        {
            Instance = this;
            _stockUIs = _stockGroupUI.GetComponentsInChildren<StockUI>();
            SetStockUIIndex();

            _stocks = GetComponents<StockClass>().ToList();
        }

        private void Start()
        {
            for (int i = 0; i < _stocks.Count; ++i)
            {
                UIManager.Instance.SetStockName(i, _stocks[i].Name);
            }
        }
        void Update()
        {
            UpdateStocks();
        }

        private void SetStockUIIndex()
        {
            for (int i = 0; i < _stockUIs.Length; ++i)
            {
                _stockUIs[i].Index = i;
            }
        }

        private void UpdateStocks()
        {
            if (_updateTime >= float.Epsilon)
            {
                _updateTime -= Time.deltaTime;
            }
            else
            {
                for (int i = 0; i < _stocks.Count; ++i)
                {
                    _stocks[i].CalculateStockPrice();

                    if (!changeRateList.ContainsKey(_stocks[i].ChangeRate))
                    {
                        changeRateList.Add(_stocks[i].ChangeRate, _stocks[i].Name);
                    }

                    _stockUIs[i].CreateGraphBar(_stocks[i].ChangeRate);
                    UIManager.Instance.SetPriceField(i, _stocks[i].Price, _stocks[i].PrePrice, _stocks[i].HighPrice, _stocks[i].LowPrice, _stocks[i].ChangeRate * 100);
                }

                (float changeRate, string name) maxChangeRateInfo = GetMaxChangeRate();
                UIManager.Instance.ShowStockNewsUI(maxChangeRateInfo.name, maxChangeRateInfo.changeRate * 100);
                changeRateList.Clear();
                _updateTime = _maxUpdateTime;
            }
        }


        //최대 최소 절대값을 비교하여 가장 큰 변화율 리턴
        private (float, string) GetMaxChangeRate()
        {
            float min = Mathf.Abs(changeRateList.First().Key);
            float max = Mathf.Abs(changeRateList.Last().Key);

            if (min < max)
            {
                return (changeRateList.Last().Key, changeRateList.Last().Value);
            }
            else
            {
                return (changeRateList.First().Key, changeRateList.First().Value);
            }
        }
    }

}
