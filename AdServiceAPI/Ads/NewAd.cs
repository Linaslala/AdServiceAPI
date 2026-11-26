namespace AdServiceAPI.Ads
{
    public class NewAd
    {
        public string Title { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
