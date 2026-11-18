
using TicketHub.Domain.ValueObjects;

namespace TicketHub.Domain.Tests;

public class AddressTests
{
    [Fact]
    public void CreateAddress_WhenTwoSameAddresses_ShouldBeEqual()
    {
        Address address1 = Address.CreateAddress("1 Place Boieldieu", "Paris", "75002", "France");
        Address address2 = Address.CreateAddress("1 Place Boieldieu", "Paris", "75002", "France");

        var result = address1 == address2;

        Assert.True(result);
    }
}