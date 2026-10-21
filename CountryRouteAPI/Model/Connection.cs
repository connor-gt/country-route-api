public class Connection
{
    public readonly Country Head;
    public readonly Country Tail;
    public int Cost { get; set; }

    public Connection(Country head, Country tail, int cost)
    {
        Head = head;
        Tail = tail;
        Cost = cost;
    }
}
