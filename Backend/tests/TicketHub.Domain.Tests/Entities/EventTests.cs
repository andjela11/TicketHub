using TicketHub.Domain.Entities;
using TicketHub.Domain.Enums;
using TicketHub.Domain.Exceptions;
using TicketHub.Domain.ValueObjects;

namespace TicketHub.Domain.Tests;

public class EventTests
{
    [Fact]
    public void CreateEvent_WithValidProperties_ShouldCreateEvent()
    {
        //Arrange
        string name = "Moulin Rouge";
        DateTime start = new DateTime(2036, 12, 25, 18, 0, 0);
        DateTime end = new DateTime(2036, 12, 25, 21, 0, 0);
        string venueName = "Comique";
        Address address = Address.CreateAddress("1 Place Boieldieu", "Paris", "75002", "France");
        int capacity = 300;
        string timeZoneId = "Europe/Paris";
        var venue = Venue.CreateVenue(venueName, address, capacity, timeZoneId);
        Category category = Category.Theatre;
        string description = "Some description";

        //Act
        var newEvent = Event.CreateEvent(name, start, end, venue, category, description);

        //Assert
        Assert.Equal(name, newEvent.Name);
        Assert.Equal(category, newEvent.Category);
        Assert.Equal(start, newEvent.StartDate);
        Assert.Equal(EventStatus.Draft, newEvent.EventStatus);
        Assert.Equal(venue.Id, newEvent.VenueId);
    }

    [Fact]
    public void CreateEvent_WithIcorectDate_ShouldThrowDomainException()
    {
        //Arrange
        string name = "Moulin Rouge";
        DateTime start = new DateTime(2025, 12, 25, 18, 0, 0);
        DateTime end = new DateTime(2025, 12, 25, 21, 0, 0);
        string venueName = "Comique";
        Address address = Address.CreateAddress("1 Place Boieldieu", "Paris", "75002", "France");
        int capacity = 300;
        string timeZoneId = "Europe/Paris";
        var venue = Venue.CreateVenue(venueName, address, capacity, timeZoneId);
        Category category = Category.Theatre;
        string description = "Some description";

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => 
        Event.CreateEvent(name, start, end, venue, category, description));
        Assert.Equal("Date has to be in the future", exception.Message);
    }

    [Fact]
    public void CreateEvent_WithEndDateBeforeStart_ShouldThrowDomainException()
    {
        //Arrange
        string name = "Moulin Rouge";
        DateTime start = new DateTime(2036, 12, 25, 18, 0, 0);
        DateTime end = new DateTime(2036, 12, 23, 21, 0, 0);
        string venueName = "Comique";
        Address address = Address.CreateAddress("1 Place Boieldieu", "Paris", "75002", "France");
        int capacity = 300;
        string timeZoneId = "Europe/Paris";
        var venue = Venue.CreateVenue(venueName, address, capacity, timeZoneId);
        Category category = Category.Theatre;
        string description = "Some description";

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => 
        Event.CreateEvent(name, start, end, venue, category, description));
        Assert.Equal("End date has to be after start date", exception.Message);
    }

    [Fact]
    public void CreateEvent_WithVenueNull_ShouldThrowDomainException()
    {
        //Arrange
        string name = "Moulin Rouge";
        DateTime start = new DateTime(2035, 12, 25, 18, 0, 0);
        DateTime end = new DateTime(2035, 12, 25, 21, 0, 0);
        
        Category category = Category.Theatre;
        string description = "Some description";

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => 
        Event.CreateEvent(name, start, end, null!, category, description));
        Assert.Equal("Venue is not valid", exception.Message);
    }

    [Fact]
    public void CreateEvent_WithEmptyStringForName_ShouldThrowDomainException()
    {
        //Arrange
        string name = " ";
        DateTime start = new DateTime(2036, 12, 25, 18, 0, 0);
        DateTime end = new DateTime(2036, 12, 25, 21, 0, 0);
        string venueName = "Comique";
        Address address = Address.CreateAddress("1 Place Boieldieu", "Paris", "75002", "France");
        int capacity = 300;
        string timeZoneId = "Europe/Paris";
        var venue = Venue.CreateVenue(venueName, address, capacity, timeZoneId);
        Category category = Category.Theatre;
        string description = "Some description";

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => 
        Event.CreateEvent(name, start, end, venue, category, description));
        Assert.Equal("Name can not be empty.", exception.Message);
    }

    [Fact]
    public void CreateEvent_WithTooLongName_ShouldThrowDomainException()
    {
        //Arrange
        string name = "The European languages are members of the same family. Their separate existence is a myth. Europe uses the same vocabulary. The languages only differ in their grammar, their pronunciation and their most common words.";
        DateTime start = new DateTime(2036, 12, 25, 18, 0, 0);
        DateTime end = new DateTime(2036, 12, 25, 21, 0, 0);
        string venueName = "Comique";
        Address address = Address.CreateAddress("1 Place Boieldieu", "Paris", "75002", "France");
        int capacity = 300;
        string timeZoneId = "Europe/Paris";
        var venue = Venue.CreateVenue(venueName, address, capacity, timeZoneId);
        Category category = Category.Theatre;
        string description = "Some description";

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => 
        Event.CreateEvent(name, start, end, venue, category, description));
        Assert.Equal("Name has to be between 3 and 200 characters.", exception.Message);
    }

    [Fact]
    public void CreateEvent_WithTooShortName_ShouldThrowDomainException()
    {
        //Arrange
        string name = "a";
        DateTime start = new DateTime(2036, 12, 25, 18, 0, 0);
        DateTime end = new DateTime(2036, 12, 25, 21, 0, 0);
        string venueName = "Comique";
        Address address = Address.CreateAddress("1 Place Boieldieu", "Paris", "75002", "France");
        int capacity = 300;
        string timeZoneId = "Europe/Paris";
        var venue = Venue.CreateVenue(venueName, address, capacity, timeZoneId);
        Category category = Category.Theatre;
        string description = "Some description";

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => 
        Event.CreateEvent(name, start, end, venue, category, description));
        Assert.Equal("Name has to be between 3 and 200 characters.", exception.Message);
    }

    [Fact]
    public void CreateEvent_WithStartDateAndEndDateAreSame_ShouldThrowDomainException()
    {
        //Arrange
        string name = "Moulin Rouge";
        DateTime start = new DateTime(2036, 12, 25, 18, 0, 0);
        DateTime end = new DateTime(2036, 12, 25, 18, 0, 0);
        string venueName = "Comique";
        Address address = Address.CreateAddress("1 Place Boieldieu", "Paris", "75002", "France");
        int capacity = 300;
        string timeZoneId = "Europe/Paris";
        var venue = Venue.CreateVenue(venueName, address, capacity, timeZoneId);
        Category category = Category.Theatre;
        string description = "Some description";

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => 
        Event.CreateEvent(name, start, end, venue, category, description));
        Assert.Equal("End date has to be after start date", exception.Message);
    }

    [Fact]
    public void Publish_WhenEventStatusIsDraft_ShouldChangeEventStatusToPublished()
    {
        //Arrange
        string name = "Moulin Rouge";
        DateTime start = new DateTime(2036, 12, 25, 18, 0, 0);
        DateTime end = new DateTime(2036, 12, 25, 21, 0, 0);
        string venueName = "Comique";
        Address address = Address.CreateAddress("1 Place Boieldieu", "Paris", "75002", "France");
        int capacity = 300;
        string timeZoneId = "Europe/Paris";
        var venue = Venue.CreateVenue(venueName, address, capacity, timeZoneId);
        Category category = Category.Theatre;
        string description = "Some description";

        var newEvent = Event.CreateEvent(name, start, end, venue, category, description);

        //Act
        newEvent.Publish();

        //Assert
        Assert.Equal(EventStatus.Published, newEvent.EventStatus);
    }

    [Fact]
    public void Publish_WhenEventStatusIsPublished_ShouldThrowDomainException()
    {
        //Arrange
        string name = "Moulin Rouge";
        DateTime start = new DateTime(2036, 12, 25, 18, 0, 0);
        DateTime end = new DateTime(2036, 12, 25, 21, 0, 0);
        string venueName = "Comique";
        Address address = Address.CreateAddress("1 Place Boieldieu", "Paris", "75002", "France");
        int capacity = 300;
        string timeZoneId = "Europe/Paris";
        var venue = Venue.CreateVenue(venueName, address, capacity, timeZoneId);
        Category category = Category.Theatre;
        string description = "Some description";

        var newEvent = Event.CreateEvent(name, start, end, venue, category, description);
        newEvent.Publish();

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => newEvent.Publish());
        Assert.Equal("Event cannot be published", exception.Message);
    }

    [Fact]
    public void Publish_WhenEventStatusIsCanceled_ShouldThrowDomainException()
    {
        //Arrange
        string name = "Moulin Rouge";
        DateTime start = new DateTime(2036, 12, 25, 18, 0, 0);
        DateTime end = new DateTime(2036, 12, 25, 21, 0, 0);
        string venueName = "Comique";
        Address address = Address.CreateAddress("1 Place Boieldieu", "Paris", "75002", "France");
        int capacity = 300;
        string timeZoneId = "Europe/Paris";
        var venue = Venue.CreateVenue(venueName, address, capacity, timeZoneId);
        Category category = Category.Theatre;
        string description = "Some description";

        var newEvent = Event.CreateEvent(name, start, end, venue, category, description);
        newEvent.Publish();
        newEvent.Cancel();

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => newEvent.Publish());
        Assert.Equal("Event cannot be published", exception.Message);
    }

    [Fact]
    public void Cancel_WhenEventStatusIsDraft_ShouldChangeEventStatusToCanceled()
    {
        //Arrange
        string name = "Moulin Rouge";
        DateTime start = new DateTime(2036, 12, 25, 18, 0, 0);
        DateTime end = new DateTime(2036, 12, 25, 21, 0, 0);
        string venueName = "Comique";
        Address address = Address.CreateAddress("1 Place Boieldieu", "Paris", "75002", "France");
        int capacity = 300;
        string timeZoneId = "Europe/Paris";
        var venue = Venue.CreateVenue(venueName, address, capacity, timeZoneId);
        Category category = Category.Theatre;
        string description = "Some description";

        var newEvent = Event.CreateEvent(name, start, end, venue, category, description);

        //Act
        newEvent.Cancel();

        //Assert
        Assert.Equal(EventStatus.Cancelled, newEvent.EventStatus);
    }

    [Fact]
    public void Cancel_WhenEventIsPublished_ShouldChangeEventStatusToCanceled()
    {
        //Arrange
        string name = "Moulin Rouge";
        DateTime start = new DateTime(2036, 12, 25, 18, 0, 0);
        DateTime end = new DateTime(2036, 12, 25, 21, 0, 0);
        string venueName = "Comique";
        Address address = Address.CreateAddress("1 Place Boieldieu", "Paris", "75002", "France");
        int capacity = 300;
        string timeZoneId = "Europe/Paris";
        var venue = Venue.CreateVenue(venueName, address, capacity, timeZoneId);
        Category category = Category.Theatre;
        string description = "Some description";

        var newEvent = Event.CreateEvent(name, start, end, venue, category, description);
        newEvent.Publish();

        //Act
        newEvent.Cancel();

        //Assert
        Assert.Equal(EventStatus.Cancelled, newEvent.EventStatus);
    }

    [Fact]
    public void Cancel_WhenEventStatusIsCanceled_ShouldThrowDomainException()
    {
        //Arrange
        string name = "Moulin Rouge";
        DateTime start = new DateTime(2036, 12, 25, 18, 0, 0);
        DateTime end = new DateTime(2036, 12, 25, 21, 0, 0);
        string venueName = "Comique";
        Address address = Address.CreateAddress("1 Place Boieldieu", "Paris", "75002", "France");
        int capacity = 300;
        string timeZoneId = "Europe/Paris";
        var venue = Venue.CreateVenue(venueName, address, capacity, timeZoneId);
        Category category = Category.Theatre;
        string description = "Some description";

        var newEvent = Event.CreateEvent(name, start, end, venue, category, description);
        newEvent.Publish();
        newEvent.Cancel();

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => newEvent.Cancel());
        Assert.Equal("Event cannot be canceled", exception.Message);
    }
}