/// <summary>
/// Represents a connection between two countries with a travel cost.
/// </summary>
public class Connection
{
    /// <summary>
    /// Gets the head country of the connection.
    /// </summary>
    public Country Head { get; init; }

    /// <summary>
    /// Gets the tail country of the connection.
    /// </summary>
    public Country Tail { get; init; }

    /// <summary>
    /// Gets or sets the cost of traveling from the head country to the tail country.
    /// </summary>
    public int Cost { get; set; }

    /// <summary>
    /// Creates a new connection between 2 countries with a travel cost.
    /// </summary>
    /// <param name="head">The head country of the connection.</param>
    /// <param name="tail">The tail country of the connection.</param>
    /// <param name="cost">The cost of traveling from the head country to the tail country or vice versa.</param>
    public Connection(Country head, Country tail, int cost)
    {
        Head = head;
        Tail = tail;
        Cost = cost;
    }
}
