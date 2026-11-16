using TicketHub.Domain.Exceptions;

namespace TicketHub.Domain.ValueObjects;

public record Address
{
    public string StreetName { get; private init; }

    public string City { get; private init; }
    public string PostalCode { get; private init; }
    public string Country { get; private init; }
    
    // ostala polja isto

    private Address(string streetName, string city, string postalCode, string country)
    {
        StreetName = streetName;
        City = city;
        PostalCode = postalCode;
        Country = country;
    }

    public static Address CreateAddress(string streetName, string city, string postalCode, string country)
    {
        if (string.IsNullOrWhiteSpace(streetName))
        {
            throw new DomainException("Name can not be empty.");
        }

        streetName = streetName.Trim();
        if (streetName.Length < 3 || streetName.Length > 300)
        {
            throw new DomainException("Name has to be between 3 and 200 characters.");
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            throw new DomainException("Name can not be empty.");
        }

        city = city.Trim();
        if (city.Length < 3 || city.Length > 100)
        {
            throw new DomainException("Name has to be between 3 and 200 characters.");
        }

        if (string.IsNullOrWhiteSpace(postalCode))
        {
            throw new DomainException("Name can not be empty.");
        }

        postalCode = postalCode.Trim();
        if (postalCode.Length < 3 || postalCode.Length > 100)
        {
            throw new DomainException("Name has to be between 3 and 200 characters.");
        }

        if (string.IsNullOrWhiteSpace(country))
        {
            throw new DomainException("Name can not be empty.");
        }

        country = country.Trim();
        if (country.Length < 3 || country.Length > 50)
        {
            throw new DomainException("Name has to be between 3 and 200 characters.");
        }
        return new Address(streetName, city, postalCode, country);
    }
}