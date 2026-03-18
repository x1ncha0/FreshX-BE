namespace FreshX.Application.Dtos
{
    public class ProvinceDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class DistrictDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ProvinceCode { get; set; } = string.Empty;
    }

    public class WardDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string DistrictCode { get; set; } = string.Empty;
    }

}

