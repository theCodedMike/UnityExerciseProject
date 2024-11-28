public class Item
{
    public string name;
    public int count;
    public string describe;

    public Item(string name, int count, string describe)
    {
        this.name = name;
        this.count = count;
        this.describe = describe;
    }

    public Item(string name, string describe) : this(name, 1, describe) {}
}
