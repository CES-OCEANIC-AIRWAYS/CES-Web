using Netcompany.Net.DomainDrivenDesign.Models;
using RoutePlanning.Domain.Enums;
using RoutePlanning.Domain.Locations;
using RoutePlanning.Domain.Users;

namespace RoutePlanning.Domain.Orders;
public class Order : AggregateRoot<Order>
{
    public Order(
        string trackingNumber, 
        Location start, 
        Location end, 
        long cost, 
        DateTime timestamp, 
        long journeyTime, 
        Status status, 
        string currentCarrier)
    {
        TrackingNumber = trackingNumber;
        Start = start;
        End = end;
        Cost = cost;
        Timestamp = timestamp;
        JourneyTime = journeyTime;
        Status = status;
        CurrentCarrier = currentCarrier;
    }

    private Order() : base()
    {
        TrackingNumber = null!;
        Start = null!;
        End = null!;
        CurrentCarrier = null!;
    }

    public string TrackingNumber { get; set; } = String.Empty; //12

    public long? Width { get; set; }

    public long? Height { get; set; }

    public long? Weight { get; set; }

    public long? Length { get; set; }

    public Location Start { get; set; }

    public long StartId { get; set; }

    public Location End { get; set; }

    public long EndId { get; set; }

    public long Cost { get; set; }

    public User? User { get; set; }

    public long? UserId { get; set; }

    public DateTime Timestamp { get; set; }

    public long JourneyTime { get; set; }

    public Status Status { get; set; }

    public string CurrentCarrier { get; set; }
}
