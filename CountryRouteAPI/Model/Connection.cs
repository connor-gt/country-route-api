public class Connection
{
    public Country Head { get; init; }
    public Country Tail { get; init; }
    public int Cost { get; set; }

    public Connection(Country head, Country tail, int cost)
    {
        Head = head;
        Tail = tail;
        Cost = cost;
    }
}
