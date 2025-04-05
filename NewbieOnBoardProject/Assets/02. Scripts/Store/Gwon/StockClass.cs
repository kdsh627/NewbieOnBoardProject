using System.Collections.Generic;
using UnityEngine;

namespace Stock
{
    public abstract class StockClass : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private ItemDataSO _itemData;
        [SerializeField] protected float _maxStockPriceRange = 0.3f; //최대 주가 변동폭
        [SerializeField] protected float _stockPriceRange = 0.000001f; // 주가 거래 변동폭
        [SerializeField] protected int _price = 1000; //현재 가격

        private int _prePrice = 500; //직전가

        protected int _totalBuyAmount = 0; //총 매수 금액
        protected int _totalSellAmount = 0; //총 매도 금액
        protected float _basicChangeRate = 0.0f; //기본 변동률
        protected float _changeRate = 0.0f; //해당 종목의 변동률

        private int _highPrice = 1000; //고가
        private int _lowPrice = 500; //저가
        private SortedSet<int> _sortedData = new SortedSet<int>();
        private Queue<int> _priceData = new Queue<int>();

        public int Price => _price;
        public int PrePrice => _prePrice;
        public int HighPrice => _highPrice;
        public int LowPrice => _lowPrice;
        public float ChangeRate => _changeRate;
        public ItemDataSO ItemData => _itemData;
        public string Name => _name;

        /// <summary>
        /// 총 매수 금액 업데이트
        /// </summary>
        public void UpdateTotalBuyAmount(int value)
        {
            _totalBuyAmount += value;
        }


        /// <summary>
        /// 총 매도 금액 업데이트
        /// </summary>
        public void UpdateTotalSellAmount(int value)
        {
            _totalBuyAmount -= value;
        }


        /// <summary>
        /// 주가계산
        /// </summary>
        public virtual void CalculateStockPrice()
        {
            CalculateBasicChangeRate();

            _prePrice = _price;

            _changeRate = Mathf.Clamp(_basicChangeRate + _changeRate, -_maxStockPriceRange, _maxStockPriceRange);


            //최소값 1 보장
            //주가 = 기존 주가 + 기존 주가 * (기본 변동률 + 여행활동 변동률)
            _price = Mathf.Max(_price + (int)(_price * _changeRate), 1);

            if(_priceData.Count < 20)
            {
                _priceData.Enqueue(_price);
                _sortedData.Add(_price);
            }
            else
            {
                _sortedData.Remove(_priceData.Dequeue());
                _priceData.Enqueue(_price);
                _sortedData.Add(_price);
            }

            //10개 중 고점 저점 업데이트
            _highPrice = _sortedData.Max;
            _lowPrice = _sortedData.Min;
        }

        /// <summary>
        /// 기본 변동률 계산
        /// </summary>
        protected void CalculateBasicChangeRate()
        {
            _basicChangeRate = (_totalBuyAmount - _totalSellAmount) * _stockPriceRange;
        }

        /// <summary>
        /// int값을 랜덤하게 반환
        /// </summary>
        protected int GetRandom(int max)
        {
            return Random.Range(0, max);
        }

        /// <summary>
        /// float값을 랜덤하게 반환
        /// </summary>
        protected float GetRandom(float max)
        {
            return Random.Range(0.0f, max);
        }

        /// <summary>
        /// 해당 종목에서 변동률 계산
        /// </summary>
        /// <param name="pre"></param>
        /// <param name="cur"></param>
        abstract protected void CalculateChangeRate();

        /// <summary>
        /// 해당 종목에서 변화율 계산
        /// </summary>
        /// <param name="pre"></param>
        /// <param name="cur"></param>
        /// <returns></returns>
        protected float CalculateParameter(int pre, int cur)
        {
            return (float)(cur - pre) / (pre + 1);
        }


        /// <summary>
        /// 랜덤하게 설정되는 변수들 설정
        /// </summary>
        abstract protected void SetRandomParameter();
    }

}