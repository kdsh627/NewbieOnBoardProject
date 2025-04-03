namespace Stock.JuyeongSangjo
{
    public class JuyeongSangjo : StockClass
    {
        private float _deadChangeRate; //사망자 수 변화율
        private float _deadWeight; //사망자 가중치
        private int _preDeadAmount; //이전 기간 사망자 수
        private int _curDeadAmount; //현재 기간 사망자 수

        private void Awake()
        {
            _preDeadAmount = GetRandom(100);
        }

        public override void CalculateStockPrice()
        {
            SetRandomParameter();

            _deadChangeRate = CalculateParameter(_preDeadAmount, _curDeadAmount);
            _preDeadAmount = _curDeadAmount; //이전 사망자 수에 현재 사망자 수를 저장

            CalculateChangeRate();

            base.CalculateStockPrice();
        }

        protected override void CalculateChangeRate()
        {
            //장례 기업 변동률  = 사망자 수 변화율 * 사망자 가중치
            _changeRate = _deadChangeRate * _deadWeight;
        }
        protected override void SetRandomParameter()
        {
            _curDeadAmount = GetRandom(100);
            _deadWeight = GetRandom(1.0f);
        }
    }
}