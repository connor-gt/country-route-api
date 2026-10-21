public class CountryService : ICountryService
{
    private readonly Graph graph = new();

    public CountryService()
    {
        graph.CreateConnection("CAN", "USA", 1);
        graph.CreateConnection("USA", "MEX", 1);
        graph.CreateConnection("MEX", "BLZ", 1);
        graph.CreateConnection("MEX", "GTM", 1);
        graph.CreateConnection("BLZ", "GTM", 1);
        graph.CreateConnection("GTM", "SLV", 1);
        graph.CreateConnection("GTM", "HND", 1);
        graph.CreateConnection("SLV", "HND", 1);
        graph.CreateConnection("HND", "NIC", 1);
        graph.CreateConnection("NIC", "CRI", 1);
        graph.CreateConnection("CRI", "PAN", 1);
    }

    public List<Country>? GetPath(string from, string to)
    {
        return graph.GetPath(from, to);
    }
}
