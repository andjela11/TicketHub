using TicketHub.Domain.Entities;
using TicketHub.Domain.Exceptions;
using TicketHub.Domain.ValueObjects;

namespace TicketHub.Domain.Tests;
public class VenueTests
{
    [Fact]
    public void CreateVenue_WithValidProperties_ShouldCreateValidVenue()
    {
        //Arange
        string name = "Comique";
        Address address = Address.CreateAddress("Paris", "France", "75002", "1 Place Boieldieu");
        int capacity = 300;
        string timeZoneId = "Europe/Paris";

        //Act
        var venue = Venue.CreateVenue(name, address, capacity, timeZoneId);

        //Assert
        Assert.Equal(name, venue.Name);
        Assert.Equal(capacity, venue.Capacity);
        Assert.Equal(address.City, venue.Address.City);
    }

    [Fact]
    public void CreateVenue_WithInvalidCapacity_ShouldThrowDomainException()
    {
        //Arange
        string name = "Comique";
        Address address = Address.CreateAddress("Paris", "France", "75002", "1 Place Boieldieu");
        int capacity = -6;
        string timeZoneId = "Europe/Paris";

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => 
        Venue.CreateVenue(name, address, capacity, timeZoneId));
        Assert.Equal("Capacity has to be greater then 0", exception.Message);
        
    }

    [Fact]
    public void CreateVenue_WithShortName_ShouldThrowDomainException()
    {
        //Arange
        string name = "Co";
        Address address = Address.CreateAddress("Paris", "France", "75002", "1 Place Boieldieu");
        int capacity = 300;
        string timeZoneId = "Europe/Paris";

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => 
        Venue.CreateVenue(name, address, capacity, timeZoneId));
        Assert.Equal("Name has to be between 3 and 200 characters.", exception.Message);
        
    }

    [Fact]
    public void CreateVenue_WithEmptyTimeZone_ShouldThrowDomainException()
    {
        //Arange
        string name = "Comique";
        Address address = Address.CreateAddress("Paris", "France", "75002", "1 Place Boieldieu");
        int capacity = 300;
        string timeZoneId = "";

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => 
        Venue.CreateVenue(name, address, capacity, timeZoneId));
        Assert.Equal("Time zone cannot be empty.", exception.Message);
        
    }
    [Fact]
    public void CreateVenue_WithInvalidAddress_ShouldThrowDomainException()
    {
        //Arange
        string name = "Comique";
        Address address = null;
        int capacity = 300;
        string timeZoneId = "Europe/Paris";

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => 
        Venue.CreateVenue(name, address, capacity, timeZoneId));
        Assert.Equal("Address cannot be null", exception.Message);
        
    }
}