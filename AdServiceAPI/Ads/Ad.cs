namespace AdServiceAPI.Ads
{
    public class Ad
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
