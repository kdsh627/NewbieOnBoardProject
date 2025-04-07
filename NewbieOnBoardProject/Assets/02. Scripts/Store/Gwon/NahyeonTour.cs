namespace Stock.NahyeonTour
{
    public class NahyeonTour : StockClass
    {
        private float _serverUserCountChangeRate; //서버 접속자 수 변화율
        private float _serverUserCountWeight; //서버 접속자 수 가중치
        private int _preServerUser; //이전 서버 유저
        private int _curServerUser; //현재 서버 유저

        private void Awake()
        {
            _preServerUser = 50;
        }

        public override void CalculateStockPrice()
        {
            SetRandomParameter();

            _serverUserCountChangeRate = CalculateParameter(_preServerUser, _curServerUser);
            _preServerUser = _curServerUser; //서버 접속자 수 변화율 계산 후 현재 서버 유저를 과거에 저장

            CalculateChangeRate();

            base.CalculateStockPrice();
        }

        protected override void CalculateChangeRate()
        {
            //여행 활동 변동률 = 서버 접속자 수 변화율 * 서버 접속자 수 가중치
            _changeRate = _serverUserCountChangeRate * _serverUserCountWeight;
        }
        protected override void SetRandomParameter()
        {
            _curServerUser = GetRandom(100);
            _serverUserCountWeight = GetRandom(1.0f);
        }
    }
}