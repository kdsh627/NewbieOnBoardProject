namespace Stock.HoeryunNaRock
{
    public class HoeryunNaRock : StockClass
    {
        private class Mineral
        {
            public float _changeRate; //변화율
            public float _weight; //가중치
            public int _curMiningAmount; //현재 광물 채굴량
            public int _preMiningAmount; //이전 광물 채굴량

            public static float operator +(Mineral m1, Mineral m2)
            {
                return m1._changeRate * m1._weight + m2._changeRate * m2._weight;
            }

            public static float operator +(float value, Mineral m)
            {
                return value + m._changeRate * m._weight;
            }

            //현재 광물 채굴량을 과거로 옮김
            public void SetPreMiningAmount()
            {
                _preMiningAmount = _curMiningAmount;
            }
        }

        private Mineral _coal = new Mineral();
        private Mineral _steel = new Mineral();
        private Mineral _gold = new Mineral();
        private Mineral _diamond = new Mineral();

        private void Awake()
        {
            _coal._preMiningAmount = 50;
            _steel._preMiningAmount = 50;
            _gold._preMiningAmount = 50;
            _diamond._preMiningAmount = 50;
        }

        public override void CalculateStockPrice()
        {
            SetRandomParameter();

            SetMineralChangeRate(_coal);
            SetMineralChangeRate(_steel);
            SetMineralChangeRate(_gold);
            SetMineralChangeRate(_diamond);

            _coal.SetPreMiningAmount();
            _steel.SetPreMiningAmount();
            _gold.SetPreMiningAmount();
            _diamond.SetPreMiningAmount();

            CalculateChangeRate();

            base.CalculateStockPrice();
        }

        //광물의 변화율을 설정
        private void SetMineralChangeRate(Mineral mineral)
        {
            mineral._changeRate = CalculateParameter(mineral._preMiningAmount, mineral._curMiningAmount);
        }

        protected override void CalculateChangeRate()
        {
            _changeRate = _coal + _steel + _gold + _diamond;
        }

        protected override void SetRandomParameter()
        {
            _coal._weight = GetRandom(1.0f);
            _steel._weight = GetRandom(1.0f);
            _gold._weight = GetRandom(1.0f);
            _diamond._weight = GetRandom(1.0f);

            _coal._curMiningAmount = GetRandom(100);
            _steel._curMiningAmount = GetRandom(100);
            _gold._curMiningAmount = GetRandom(100);
            _diamond._curMiningAmount = GetRandom(100);
        }
    }
}
