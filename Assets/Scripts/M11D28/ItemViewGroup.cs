using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.M11D28
{
    [ExecuteInEditMode]
    public class ItemViewGroup : MonoBehaviour
    {
        private GameObject itemPrefab;
        private ItemDatabase database;
        private List<ItemView> itemViews;
        private ItemInfo itemInfo;


        // Start is called before the first frame update
        void Start()
        {
            InitOther();
            InitDatabase();
        }

        void InitOther()
        {
            itemPrefab = Resources.Load<GameObject>("Prefabs/Item");
            database = new ItemDatabase();
            itemViews = new List<ItemView>(16);
            itemInfo = transform.root.GetComponentInChildren<ItemInfo>();
            itemInfo.OnUseBtnClickEvent = OnUseItem;
        }

        void InitDatabase()
        {
            database.AddItem(new Item("Energy", 2, "使用该物品，战斗力增加200"));
            database.AddItem(new Item("GemGreen", 1, "使用该物品，获得100金币"));
            database.AddItem(new Item("Heart", 1, "使用该物品，生命值增加100"));
            database.AddItem(new Item("Skull", 1, "使用该物品，防御力增加50"));
            database.AddItem(new Item("Star", 1, "使用该物品，幸运值增加20"));
        }
        

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Spawn();
            }
        }

        void Spawn()
        {
            Clear();

            for (int i = 0; i < database.items.Count; i++)
            {
                GameObject itemViewObj = Instantiate(itemPrefab, transform);
                ItemView itemView = itemViewObj.GetComponent<ItemView>();
                itemView.OnSelectedItemView = SelectedView;
                itemView.SetValue(database.items[i]);
                itemViews.Add(itemView);
            }
        }

        // 选中某个item后，将其展示在后侧面板
        void SelectedView(ItemView selectedItemView)
        {
            itemInfo.SetItemView(selectedItemView);
        }

        // <使用>按钮点击后的清理工作
        void OnUseItem(ItemView itemView)
        {
            database.RemoveItem(itemView.item);
            if (itemView.item.count == 0)
            {
                itemViews.Remove(itemView);
                Destroy(itemView.gameObject);
                itemInfo.Clear();
                return;
            }
            itemView.Refresh();
        }

        void Clear()
        {
            for (int i = 0; i < itemViews.Count; i++)
            {
                // 这里可以使用对象池
                Destroy(itemViews[i].gameObject);
            }
            itemViews.Clear();
        }
    }
}
