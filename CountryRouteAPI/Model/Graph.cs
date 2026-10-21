/// <summary>
/// Represents a graph of countries and connections between them.
/// </summary>
public class Graph
{
    private readonly List<Country> countries = new();
    private readonly List<Connection> connections = new(); // Connections are bidirectional

    /// <summary>
    /// Create a connection between 2 countries with a travel cost.
    /// <para>
    /// If either the from or to country does not exist in the graph, it will be created as a new country instance
    /// and added to the graph.
    /// </para>
    /// </summary>
    /// <param name="from">The code of the country from which to create the connection.</param>
    /// <param name="to">The code of the country to which to create the connection.</param>
    /// <param name="cost">The cost of traveling from the <see cref="from"/> country to the <see cref="to"/> country.</param>
    /// <returns>The created connection.</returns>
    public Connection CreateConnection(string from, string to, int cost)
    {
        Country head = GetOrAddCountry(from);
        Country tail = GetOrAddCountry(to);

        Connection connection = new(head, tail, cost);
        connections.Add(connection);
        return connection;
    }

    /// <summary>
    /// Gets an existing country from the graph by its code, or adds a new country if it does not exist.
    /// </summary>
    /// <param name="code">The code of the country to get or add (and then get).</param>
    /// <returns>The existing or newly added country.</returns>
    public Country GetOrAddCountry(string code)
    {
        Country? country = GetCountry(code);
        if (country == null)
        {
            country = new Country(code);
            countries.Add(country);
        }

        return country;
    }

    /// <summary>
    /// Get a country from the graph by its code.
    /// </summary>
    /// <param name="code">The country's code.</param>
    /// <returns>The country if found, otherwise null.</returns>
    public Country? GetCountry(string code)
    {
        Country? country = countries.FirstOrDefault(c => string.Equals(c.Code, code, StringComparison.OrdinalIgnoreCase));
        return country;
    }

    /// <summary>
    /// Finds the shortest path between 2 countries in the graph using Dijkstra's algorithm (find the lowest costing
    /// path).
    /// </summary>
    /// <param name="from">The code of the country from which to start the path.</param>
    /// <param name="to">The code of the country to which to end the path.</param>
    /// <returns>The list of countries representing the shortest path, or null if no path is found.</returns>
    /// <exception cref="ArgumentException">Thrown if either the starting or destination country are not found.</exception>
    public List<Country>? GetPath(string from, string to)
    {
        Country? startingCountry = GetCountry(from);
        if (startingCountry == null)
        {
            throw new ArgumentException($"Country with code '{from}' does not exist in the graph.");
        }

        Country? destinationCountry = GetCountry(to);
        if (destinationCountry == null)
        {
            throw new ArgumentException($"Country with code '{to}' does not exist in the graph.");
        }

        PriorityQueue<(Country country, List<Country> path), int> frontier = new(); // Acts as a min-heap
        Dictionary<Country, int> costSoFar = new(); // Keeps track of the lowest cost to reach each country

        frontier.Enqueue((startingCountry, new List<Country> { startingCountry }), 0);
        costSoFar[startingCountry] = 0;

        while (frontier.Count > 0)
        {
            var (currentCountry, path) = frontier.Dequeue();

            // Check if the destination country has been reached and if so return the current path
            if (currentCountry == destinationCountry)
            {
                return path;
            }

            foreach (var connection in connections.Where(c => c.Head == currentCountry || c.Tail == currentCountry))
            {
                Country nextCountry = connection.Head == currentCountry ? connection.Tail : connection.Head;
                int newCost = costSoFar[currentCountry] + connection.Cost;

                if (!costSoFar.ContainsKey(nextCountry) || newCost < costSoFar[nextCountry])
                {
                    costSoFar[nextCountry] = newCost;
                    List<Country> newPath = new(path) { nextCountry };
                    frontier.Enqueue((nextCountry, newPath), newCost);
                }
            }
        }

        // No path found (probably because the destination country and starting country aren't connected)
        return null;
    }
}
