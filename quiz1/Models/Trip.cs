namespace quiz1.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string HotelName { get; set; }
        public decimal Rating { get; set; }
        public int ReviewCount { get; set; }
        public bool IsRefundable { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NightStay { get; set; }
        public int RoomCount { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public string ImageUrl { get; set; }
    }
}