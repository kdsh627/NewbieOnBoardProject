namespace Stock.HoegidongMakgeolri
{
    public class HoegidongMakgeolri : StockClass
    {
        private float _buildingAreaChangeRate; //건축 면적 변화율
        private float _buildingAreaWeight; //건축 면적 가중치
        private int _preBuildingArea; //이전 기간 건축 면적
        private int _curBuildingArea; //현재 기간 건축 면적

        private void Awake()
        {
            _preBuildingArea = GetRandom(100);
        }

        public override void CalculateStockPrice()
        {
            SetRandomParameter();

            _buildingAreaChangeRate = CalculateParameter(_preBuildingArea, _curBuildingArea);
            _preBuildingArea = _curBuildingArea; //이전 건축 면적에 현재 건축 면적을 저장

            CalculateChangeRate();

            base.CalculateStockPrice();
        }

        protected override void CalculateChangeRate()
        {
            //건축 면적 변동률  = 건축 면적 변화율 * 건축 면적 가중치
            _changeRate = _buildingAreaChangeRate * _buildingAreaWeight;
        }
        protected override void SetRandomParameter()
        {
            _curBuildingArea = GetRandom(100);
            _buildingAreaWeight = GetRandom(1.0f);
        }
    }

}