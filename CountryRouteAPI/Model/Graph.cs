using System.Diagnostics.CodeAnalysis;

public class Graph
{
    private readonly List<Country> countries = new();
    private readonly List<Connection> connections = new();

    public Connection CreateConnection(string from, string to, int cost)
    {
        Country head = GetOrAddCountry(from);
        Country tail = GetOrAddCountry(to);

        Connection connection = new(head, tail, cost);
        connections.Add(connection);
        return connection;
    }

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

    public Country? GetCountry(string code)
    {
        Country? country = countries.FirstOrDefault(c => string.Equals(c.Code, code, StringComparison.OrdinalIgnoreCase));
        return country;
    }

    public bool RemoveCountry(string code)
    {
        // TODO: handle removing connections as well
        return countries.RemoveAll(c => string.Equals(c.Code, code, StringComparison.OrdinalIgnoreCase)) > 0;
    }

    public bool RemoveConnection(string from, string to)
    {
        return connections.RemoveAll(c => 
            (string.Equals(c.Head.Code, from, StringComparison.OrdinalIgnoreCase)
            && string.Equals(c.Tail.Code, to, StringComparison.OrdinalIgnoreCase))
            || (string.Equals(c.Head.Code, to, StringComparison.OrdinalIgnoreCase)
            && string.Equals(c.Tail.Code, from, StringComparison.OrdinalIgnoreCase))) > 0;
    }

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

        PriorityQueue<(Country country, List<Country> path), int> frontier = new();
        Dictionary<Country, int> costSoFar = new();

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
                    List<Country> newPath = new List<Country>(path) { nextCountry };
                    frontier.Enqueue((nextCountry, newPath), newCost);
                }
            }
        }

        // No path found (probably because the destination country and starting country aren't connected)
        return null;
    }
}
