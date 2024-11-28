using System.Collections.Generic;
using JetBrains.Annotations;

public class Item
{
    public string name;
    public int count;
    public string describe;
    public HashSet<string> tags;

    public Item(string name, int count, string describe, [CanBeNull] HashSet<string> tags = null)
    {
        this.name = name;
        this.count = count;
        this.describe = describe;
        this.tags = tags ?? new HashSet<string>();
    }

    public Item(string name, string describe) : this(name, 1, describe) {}
}
