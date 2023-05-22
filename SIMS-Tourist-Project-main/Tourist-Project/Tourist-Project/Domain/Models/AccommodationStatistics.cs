namespace Tourist_Project.Domain.Models;

public class AccommodationStatistics
{

    public int TotalReservations { get; set; }
    public int CancelledReservations { get; set; }
    public int RescheduledReservations { get; set; }
    public int TotalRenovationRecommendations { get; set; }
    public int Occupancy { get; set; }
    public string Period { get; set; }
    public AccommodationStatistics(int totalReservations, int cancelledReservations, int rescheduledReservations, int totalRenovationRecommendations, int occupancy, string period)
    {
        TotalReservations = totalReservations;
        CancelledReservations = cancelledReservations;
        RescheduledReservations = rescheduledReservations;
        TotalRenovationRecommendations = totalRenovationRecommendations;
        Occupancy = occupancy;
        Period = period;
    }
}