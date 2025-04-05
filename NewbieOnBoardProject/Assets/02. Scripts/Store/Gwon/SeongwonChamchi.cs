using UnityEngine;

namespace Stock.SeongwonChamchi
{
    public class SeongwonChamchi : StockClass
    {
        private class Fish
        {
            public float _changeRate; //변화율
            public float _weight; //가중치
            public int _curFishingAmount; //현재 어확량
            public int _preFishingAmount; //이전 어확량

            public static float operator +(Fish f1, Fish f2)
            {
                return f1._changeRate * f1._weight + f2._changeRate * f2._weight;
            }

            public static float operator +(float value, Fish f)
            {
                return value + f._changeRate * f._weight;
            }

            //현재 어획량을 과거로 옮김
            public void SetPreFishAmount()
            {
                _preFishingAmount = _curFishingAmount;
            }
        }

        private Fish _fish = new Fish();
        private Fish _salmon = new Fish();
        private Fish _blowFish = new Fish();
        private Fish _tropicalFish = new Fish();
        private Fish _calamari = new Fish();

        private void Awake()
        {
            _fish._preFishingAmount = 50;
            _salmon._preFishingAmount = 50;
            _blowFish._preFishingAmount = 50;
            _tropicalFish._preFishingAmount = 50;
            _calamari._preFishingAmount = 50;
        }

        public override void CalculateStockPrice()
        {
            SetRandomParameter();

            SetCropsChangeRate(_fish);
            SetCropsChangeRate(_salmon);
            SetCropsChangeRate(_blowFish);
            SetCropsChangeRate(_tropicalFish);
            SetCropsChangeRate(_calamari);

            _fish.SetPreFishAmount();
            _salmon.SetPreFishAmount();
            _blowFish.SetPreFishAmount();
            _tropicalFish.SetPreFishAmount();
            _calamari.SetPreFishAmount();

            CalculateChangeRate();

            base.CalculateStockPrice();
        }

        //광물의 변화율을 설정
        private void SetCropsChangeRate(Fish mineral)
        {
            mineral._changeRate = CalculateParameter(mineral._preFishingAmount, mineral._curFishingAmount);
        }

        protected override void CalculateChangeRate()
        {
            _changeRate = _fish + _salmon + _blowFish + _tropicalFish + _calamari;
        }

        protected override void SetRandomParameter()
        {
            _fish._weight = GetRandom(1.0f);
            _salmon._weight = GetRandom(1.0f);
            _blowFish._weight = GetRandom(1.0f);
            _tropicalFish._weight = GetRandom(1.0f);
            _calamari._weight = GetRandom(1.0f);

            _fish._curFishingAmount = GetRandom(100);
            _salmon._curFishingAmount = GetRandom(100);
            _blowFish._curFishingAmount = GetRandom(100);
            _tropicalFish._curFishingAmount = GetRandom(100);
            _calamari._curFishingAmount = GetRandom(100);
        }
    }
}