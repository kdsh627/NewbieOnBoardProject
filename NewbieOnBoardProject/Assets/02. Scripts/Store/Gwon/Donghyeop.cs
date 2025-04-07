namespace Stock.Donghyeop
{
    public class Donghyeop : StockClass
    {
        private class Crops
        {
            public float _changeRate; //변화율
            public float _weight; //가중치
            public int _curCropsAmount; //현재 수확량
            public int _preCropsAmount; //이전 수확량

            public static float operator +(Crops c1, Crops c2)
            {
                return c1._changeRate * c1._weight + c2._changeRate * c2._weight;
            }

            public static float operator +(float value, Crops c)
            {
                return value + c._changeRate * c._weight;
            }

            //현재 광물 채굴량을 과거로 옮김
            public void SetPreCropsAmount()
            {
                _preCropsAmount = _curCropsAmount;
            }
        }

        private Crops _wheat = new Crops();
        private Crops _carrot = new Crops();
        private Crops _potato = new Crops();
        private Crops _pumpkin = new Crops();
        private Crops _watermelon = new Crops();
        private Crops _sugarCane = new Crops();

        private void Awake()
        {
            _wheat._preCropsAmount = 50;
            _carrot._preCropsAmount = 50;
            _potato._preCropsAmount = 50;
            _pumpkin._preCropsAmount = 50;
            _watermelon._preCropsAmount = 50;
            _sugarCane._preCropsAmount = 50;
        }

        public override void CalculateStockPrice()
        {
            SetRandomParameter();

            SetCropsChangeRate(_wheat);
            SetCropsChangeRate(_carrot);
            SetCropsChangeRate(_potato);
            SetCropsChangeRate(_pumpkin);
            SetCropsChangeRate(_watermelon);
            SetCropsChangeRate(_sugarCane);

            _wheat.SetPreCropsAmount();
            _carrot.SetPreCropsAmount();
            _potato.SetPreCropsAmount();
            _pumpkin.SetPreCropsAmount();
            _watermelon.SetPreCropsAmount();
            _sugarCane.SetPreCropsAmount();

            CalculateChangeRate();

            base.CalculateStockPrice();
        }

        //광물의 변화율을 설정
        private void SetCropsChangeRate(Crops mineral)
        {
            mineral._changeRate = CalculateParameter(mineral._preCropsAmount, mineral._curCropsAmount);
        }

        protected override void CalculateChangeRate()
        {
            _changeRate = _wheat + _carrot + _potato + _pumpkin + _watermelon + _sugarCane;
        }

        protected override void SetRandomParameter()
        {
            _wheat._weight = GetRandom(1.0f);
            _carrot._weight = GetRandom(1.0f);
            _potato._weight = GetRandom(1.0f);
            _pumpkin._weight = GetRandom(1.0f);
            _watermelon._weight = GetRandom(1.0f);
            _sugarCane._weight = GetRandom(1.0f);

            _wheat._curCropsAmount = GetRandom(100);
            _carrot._curCropsAmount = GetRandom(100);
            _potato._curCropsAmount = GetRandom(100);
            _pumpkin._curCropsAmount = GetRandom(100);
            _watermelon._curCropsAmount = GetRandom(100);
            _sugarCane._curCropsAmount = GetRandom(100);
        }
    }
}