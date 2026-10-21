public class Graph
{
    private readonly List<Country> countries = new();
    private readonly List<Connection> connections = new();

    public Connection CreateConnection(string from, string to, int cost)
    {
        if (!HasCountry(from))
        {
            AddCountry(from);
        }

        if (!HasCountry(to))
        {
            AddCountry(to);
        }

        Connection connection = new(from, to, cost);
        connections.Add(connection);
        return connection;
    }

    public bool AddCountry(string code)
    {
        if (HasCountry(code))
        {
            return false;
        }

        Country country = new(code);
        countries.Add(country);
        return true;
    }

    public bool HasCountry(string code)
    {
        return countries.Any(c => string.Equals(c.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    public bool RemoveCountry(string code)
    {
        return countries.RemoveAll(c => string.Equals(c.Code, code, StringComparison.OrdinalIgnoreCase)) > 0;
    }

    public bool RemoveConnection(string from, string to)
    {
        return connections.RemoveAll(c => 
            (string.Equals(c.Head, from, StringComparison.OrdinalIgnoreCase)
            && string.Equals(c.Tail, to, StringComparison.OrdinalIgnoreCase))
            || (string.Equals(c.Head, to, StringComparison.OrdinalIgnoreCase)
            && string.Equals(c.Tail, from, StringComparison.OrdinalIgnoreCase))) > 0;
    }

    public List<Country> GetPath(string from, string to)
    {
        return null;
    }
}
