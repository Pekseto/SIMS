namespace Tourist_Project.Domain.Models;

public class AccommodationStatistics
{

    public int TotalReservations { get; set; }
    public int CancelledReservations { get; set; }
    public int RescheduledReservations { get; set; }
    public int TotalRenovationRecommendations { get; set; }
    public int MostOccupiedPeriod { get; set; }
    public AccommodationStatistics(int totalReservations, int cancelledReservations, int totalRenovationRecommendations)
    {
        TotalReservations = totalReservations;
        CancelledReservations = cancelledReservations;
        TotalRenovationRecommendations = totalRenovationRecommendations;
    }
}