using TicketHub.Domain.Enums;
using TicketHub.Domain.Exceptions;

namespace TicketHub.Domain.Entities;
public class Event
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Category Category { get; private set; }
    public DateTime StartDate { get; private set; } 
    public DateTime EndDate { get; private set; } 
    public int VenueId { get; private set; }
    public Venue? Venue { get; private set; }
    public EventStatus EventStatus { get; private set; }
    
    private Event(string name, DateTime startDate, DateTime endDate,
    int venueId, Category category, EventStatus eventStatus, string? description)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        VenueId = venueId;
        Category = category;
        EventStatus = eventStatus;
        Description = description ?? string.Empty;
    }

    public static Event CreateEvent(string name, DateTime startDate, DateTime endDate,
    Venue venue, Category category, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Name can not be empty.");
        }

        name = name.Trim();
        if (name.Length < 3 || name.Length > 200)
        {
            throw new DomainException("Name has to be between 3 and 200 characters.");
        }

        if(venue is null)
        {
            throw new DomainException("Venue is not valid");
        }

        EventTimeValidation(startDate, endDate, TimeZoneInfo.FindSystemTimeZoneById(venue.TimeZoneId));

        return new Event(name, startDate, endDate, venue.Id, category, EventStatus.Draft, description);
    }

    public void Publish()
    {
        if(EventStatus != EventStatus.Draft)
        {
            throw new DomainException("Event cannot be published");
        }

        EventStatus = EventStatus.Published;
    }

    public void Cancel()
    {
        if(EventStatus != EventStatus.Draft && EventStatus != EventStatus.Published)
        {
            throw new DomainException("Event cannot be canceled");
        }

        EventStatus = EventStatus.Cancelled;
    }

    private static void EventTimeValidation(DateTime startDate, DateTime endDate, TimeZoneInfo timeZoneInfo)
    {
        if(timeZoneInfo.IsInvalidTime(startDate) || timeZoneInfo.IsInvalidTime(endDate))
        {
            throw new DomainException("Date is not valid");
        }

        if (startDate.Kind != DateTimeKind.Unspecified || endDate.Kind != DateTimeKind.Unspecified)
        {
            throw new DomainException("Dates must be local venue time without a time zone.");
        }

        var startUtc = TimeZoneInfo.ConvertTimeToUtc(startDate, timeZoneInfo);
        if(startUtc <= DateTime.UtcNow)
        {
            throw new DomainException("Date has to be in the future");
        }

        var endUtc = TimeZoneInfo.ConvertTimeToUtc(endDate, timeZoneInfo);
        
        if(startUtc >= endUtc)
        {
            throw new DomainException("End date has to be after start date");
        }
    }
}