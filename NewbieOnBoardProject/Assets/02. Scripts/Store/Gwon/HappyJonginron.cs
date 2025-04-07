namespace Stock.HappyJonginron
{
    public class HappyJonginron : StockClass
    {
        private float _totalStockDropCompanyChangeRate; //전분기 주가 하락 기업 수 변화율
        private float _loanCompanyWeight; //대출 기업 가중치
        private int _preStockDropCompanyCount; //이전 분기 주가 하락 기업 수
        private int _curStockDropCompanyCount; //현재 분기 주가 하락 기업 수

        private void Awake()
        {
            _preStockDropCompanyCount = 50;
        }

        public override void CalculateStockPrice()
        {
            SetRandomParameter();

            _totalStockDropCompanyChangeRate = CalculateParameter(_preStockDropCompanyCount, _curStockDropCompanyCount);
            _preStockDropCompanyCount = _curStockDropCompanyCount; //이전 분기 주가 하락 기업 수에 현재 분기 주가 하락 기업 수를 저장

            CalculateChangeRate();

            base.CalculateStockPrice();
        }

        protected override void CalculateChangeRate()
        {
            //대출 기업 변동률 = (전분기 주가 하락 기업 수 변화율) * (대출 기업 가중치)
            _changeRate = _totalStockDropCompanyChangeRate * _loanCompanyWeight;
        }
        protected override void SetRandomParameter()
        {
            _curStockDropCompanyCount = GetRandom(100);
            _loanCompanyWeight = GetRandom(1.0f);
        }
    }
}