using TicketHub.Domain.Exceptions;
using TicketHub.Domain.ValueObjects;
namespace TicketHub.Domain.Entities;
public class Venue
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public Address Address { get; private set; }
    public int Capacity { get; private set; }
    public string TimeZoneId { get; private set; }

    private Venue(string name, Address address, int capacity, string timeZoneId)
    {
        Name = name;
        Address = address;
        Capacity = capacity;
        TimeZoneId = timeZoneId;
    }

    public static Venue CreateVenue(string name, Address address, int capacity, string timeZoneId)
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

        if(address is null)
        {
            throw new DomainException("Address cannot be null");
        }

        if(capacity <= 0)
        {
            throw new DomainException("Capacity has to be greater then 0");
        }

        if (string.IsNullOrWhiteSpace(timeZoneId))
        {
            throw new DomainException("Time zone cannot be empty.");
        }

        return new Venue(name, address, capacity, timeZoneId);
    }
}