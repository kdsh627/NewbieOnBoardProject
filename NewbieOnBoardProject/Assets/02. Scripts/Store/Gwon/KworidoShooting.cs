namespace Stock.KworidoShooting
{
    public class KworidoShooting : StockClass
    {
        private float _huntChangeRate; //몬스터 처치 변동률
        private float _huntWeight; //몬스터 처치 가중치
        private int _preHuntAmount; //이전 몬스터 처치 수
        private int _curHuntAmount; //현재 몬스터 처치 수

        private void Awake()
        {
            _preHuntAmount = 50;
        }

        public override void CalculateStockPrice()
        {
            SetRandomParameter();

            _huntChangeRate = CalculateParameter(_preHuntAmount, _curHuntAmount);
            _preHuntAmount = _curHuntAmount; //이전 몬스터처치 수에 현재 몬스터 처치 수를 넘김

            CalculateChangeRate();

            base.CalculateStockPrice();
        }

        protected override void CalculateChangeRate()
        {
            //몬스터 처치 변동률 = 몬스터 처치 수 변화율 * 몬스터 처치 가중치
            _changeRate = _huntChangeRate * _huntWeight;
        }
        protected override void SetRandomParameter()
        {
            _curHuntAmount = GetRandom(100);
            _huntWeight = GetRandom(1.0f);
        }
    }
}