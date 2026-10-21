/// <summary>
/// Uses the <see cref="Graph"/> class.
/// </summary>
public class CountryService : ICountryService
{
    private readonly Graph graph = new();

    public CountryService()
    {
        // Costs can be updated to simulate shortest distance paths not always actually costing the least, but
        // for the demo I'm leaving them all as 1 (so it costs 1 to go from one country to one of its bordering
        // countries)
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
