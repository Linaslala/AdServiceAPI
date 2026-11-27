namespace AdServiceAPI.Ads
{
    public class UpdateAd
    {
        public string Title { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
