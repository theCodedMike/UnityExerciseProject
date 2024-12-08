using System.Collections.Generic;

namespace M11D28
{
    public class ItemDatabase
    {
        public List<Item> items;

        public ItemDatabase()
        {
            items = new List<Item>(16);
        }

        /// <summary>
        /// 添加物品
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name == item.name)
                {
                    items[i].count++;
                    return;
                }
            }
            items.Add(item);
        }

        /// <summary>
        /// 移除物品
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(Item item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name == item.name)
                {
                    items[i].count--;
                    if (items[i].count == 0)
                    {
                        items.RemoveAt(i);
                        return;
                    }
                }
            }
        }
    }
}
