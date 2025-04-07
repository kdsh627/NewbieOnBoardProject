namespace Stock.GyeongsuSaneopGaebal
{
    public class GyeongsuSaneopGaebal : StockClass
    {
        private float _productionChangeRate; //제작량 변화율
        private float _manufacturingWeight; //제조 기업 민감도 계수
        private int _preProduction; //이전 기간 제작량
        private int _curProduction; //현재 기간 건축 면적

        private void Awake()
        {
            _preProduction = 50;
        }

        public override void CalculateStockPrice()
        {
            SetRandomParameter();

            _productionChangeRate = CalculateParameter(_preProduction, _curProduction);
            _preProduction = _curProduction; //이전 제작량에 현재 제작량을 저장

            CalculateChangeRate();

            base.CalculateStockPrice();
        }

        protected override void CalculateChangeRate()
        {
            //제조 기업 변동률 = (제작량 변화율) * (제조 기업 민감도 계수)
            _changeRate = _productionChangeRate * _manufacturingWeight;
        }
        protected override void SetRandomParameter()
        {
            _curProduction = GetRandom(100);
            _manufacturingWeight = GetRandom(1.0f);
        }
    }

}