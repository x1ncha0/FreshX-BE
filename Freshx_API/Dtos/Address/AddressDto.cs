namespace Freshx_API.Dtos
{
    public class ProvinceDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class DistrictDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ProvinceCode { get; set; }
    }

    public class WardDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string DistrictCode { get; set; }
    }

}
