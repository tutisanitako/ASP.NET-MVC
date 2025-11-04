using quiz1.Models;

namespace quiz1.Services
{
    public interface ITripService
    {
        List<Trip> GetUserTrips(string userEmail);
    }
}