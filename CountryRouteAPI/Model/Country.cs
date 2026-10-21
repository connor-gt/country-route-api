/// <summary>
/// Represents a country with a unique code.
/// </summary>
public class Country
{   
    /// <summary>
    /// Gets the unique code of the country.
    /// </summary>
    public string Code { get; init; }

    /// <summary>
    /// Create a new country instance with a unique code.
    /// </summary>
    /// <param name="code">The country's code.</param>
    public Country(string code)
    {
        Code = code;
    }
}
