public class Country
{
    public readonly string Code;
    private readonly List<Connection> connections;

    public Country(string code)
    {
        Code = code;
        connections = new List<Connection>();
    }

    public bool CreateConnection(string countryCode)
    {
        if (string.Equals(Code, countryCode, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return true;   
    }

    public List<Connection> GetConnections()
    {
        // I'm using ToList() so the list is a duplicate and makes sure this country's list can't be modified directly
        return connections.ToList();
    }
}
