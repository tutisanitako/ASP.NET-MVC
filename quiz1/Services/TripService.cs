using quiz1.Models;

namespace quiz1.Services
{
    public class TripService : ITripService
    {
        private static List<Trip> _trips = new List<Trip>
        {
            new Trip
            {
                Id = 1,
                UserId = "user@example.com",
                HotelName = "Lakeside Motel Warefront",
                Rating = 4.5m,
                ReviewCount = 1200,
                IsRefundable = false,
                CheckInDate = new DateTime(2022, 3, 18),
                CheckOutDate = new DateTime(2022, 3, 20),
                NightStay = 2,
                RoomCount = 1,
                OriginalPrice = 150,
                CurrentPrice = 130,
                ImageUrl = "/images/hotel1.jpg"
            },
            new Trip
            {
                Id = 2,
                UserId = "user@example.com",
                HotelName = "Lakeside Motel Warefront",
                Rating = 4.5m,
                ReviewCount = 1200,
                IsRefundable = false,
                CheckInDate = new DateTime(2022, 3, 18),
                CheckOutDate = new DateTime(2022, 3, 20),
                NightStay = 2,
                RoomCount = 1,
                OriginalPrice = 150,
                CurrentPrice = 130,
                ImageUrl = "/images/hotel1.jpg"
            },
            new Trip
            {
                Id = 3,
                UserId = "user@example.com",
                HotelName = "Lakeside Motel Warefront",
                Rating = 4.5m,
                ReviewCount = 1200,
                IsRefundable = false,
                CheckInDate = new DateTime(2022, 3, 18),
                CheckOutDate = new DateTime(2022, 3, 20),
                NightStay = 2,
                RoomCount = 1,
                OriginalPrice = 150,
                CurrentPrice = 130,
                ImageUrl = "/images/hotel1.jpg"
            },

            new Trip
            {
                Id = 4,
                UserId = "test@test.com.com",
                HotelName = "Lakeside Motel Warefront",
                Rating = 4.5m,
                ReviewCount = 1200,
                IsRefundable = false,
                CheckInDate = new DateTime(2022, 3, 18),
                CheckOutDate = new DateTime(2022, 3, 20),
                NightStay = 2,
                RoomCount = 1,
                OriginalPrice = 150,
                CurrentPrice = 130,
                ImageUrl = "/images/hotel1.jpg"
            },

            new Trip
            {
                Id = 5,
                UserId = "test@test.com",
                HotelName = "Lakeside Motel Warefront",
                Rating = 4.5m,
                ReviewCount = 1200,
                IsRefundable = false,
                CheckInDate = new DateTime(2022, 3, 18),
                CheckOutDate = new DateTime(2022, 3, 20),
                NightStay = 2,
                RoomCount = 1,
                OriginalPrice = 150,
                CurrentPrice = 130,
                ImageUrl = "/images/hotel1.jpg"
            },

            new Trip
            {
                Id = 6,
                UserId = "admin@example.com",
                HotelName = "Lakeside Motel Warefront",
                Rating = 4.5m,
                ReviewCount = 1200,
                IsRefundable = false,
                CheckInDate = new DateTime(2022, 3, 18),
                CheckOutDate = new DateTime(2022, 3, 20),
                NightStay = 2,
                RoomCount = 1,
                OriginalPrice = 150,
                CurrentPrice = 130,
                ImageUrl = "/images/hotel1.jpg"
            },

            new Trip
            {
                Id = 7,
                UserId = "admin@example.com",
                HotelName = "Lakeside Motel Warefront",
                Rating = 4.5m,
                ReviewCount = 1200,
                IsRefundable = false,
                CheckInDate = new DateTime(2022, 3, 18),
                CheckOutDate = new DateTime(2022, 3, 20),
                NightStay = 2,
                RoomCount = 1,
                OriginalPrice = 150,
                CurrentPrice = 130,
                ImageUrl = "/images/hotel1.jpg"
            },


        };

        public List<Trip> GetUserTrips(string userEmail)
        {
            return _trips.Where(t => t.UserId.ToLower() == userEmail.ToLower()).ToList();
        }
    }
}