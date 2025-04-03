using System.Collections.Generic;
using System.Linq;
using Manager.UI;
using NUnit.Framework;
using Stock;
using UnityEngine;

namespace Manager.Stock
{
    public class StockManager : MonoBehaviour
    {
        public static StockManager Instance { get; private set; }

        [SerializeField] private GameObject StockUI;
        [SerializeField] private float _maxUpdateTime = 15.0f;

        private List<StockClass> _stocks;
        private float _updateTime = float.Epsilon;

        public List<StockClass> Stocks => _stocks;

        void Awake()
        {
            Instance = this;
            _stocks = GetComponents<StockClass>().ToList();
        }

        private void Start()
        {
            for (int i = 0; i < _stocks.Count; ++i)
            {
                UIManager.Instance.SetStockName(i, _stocks[i].Name);
            }
        }

        // Update is called once per frame
        void Update()
        {
            UpdateStocks();
        }

        private void UpdateStocks()
        {
            if(_updateTime >= float.Epsilon)
            {
                _updateTime -= Time.deltaTime;
            }
            else
            {
                for(int i = 0; i < _stocks.Count; ++i)
                {
                    _stocks[i].CalculateStockPrice();
                    UIManager.Instance.SetPriceField(i, _stocks[i].Price, _stocks[i].PrePrice, _stocks[i].HighPrice, _stocks[i].LowPrice, _stocks[i].ChangeRate*100);
                }
                _updateTime = _maxUpdateTime;
            }
        }
    }

}
