public class Connection
{
    public readonly string Head;
    public readonly string Tail;
    public int Cost { get; set; }

    public Connection(string head, string tail, int cost)
    {
        Head = head;
        Tail = tail;
        Cost = cost;
    }
}
