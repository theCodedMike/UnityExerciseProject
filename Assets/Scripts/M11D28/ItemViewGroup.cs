using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.M11D28
{
    public class ItemViewGroup : MonoBehaviour
    {
        private GameObject itemPrefab;
        private ItemDatabase database;
        private List<ItemView> itemViews;
        private ItemInfo itemInfo;

        private ItemView lastSelected; // 上一个被选中的Item
        private ItemView currSelected; // 当前选中的Item

        private Predicate<Item> filterStrategy;

        // Start is called before the first frame update
        void Start()
        {
            InitOther();
            InitDatabase();
            Spawn();
        }

        void InitOther()
        {
            itemPrefab = Resources.Load<GameObject>("Prefabs/Item");
            database = new ItemDatabase();
            itemViews = new List<ItemView>(16);
            itemInfo = transform.root.GetComponentInChildren<ItemInfo>();
            itemInfo.OnUseBtnClickEvent = OnUseItem;
            filterStrategy = (Item item) => true;
        }

        void InitDatabase()
        {
            database.AddItem(new Item("Energy", 2, "使用该物品，战斗力增加200", new HashSet<string>() { "道具" }));
            database.AddItem(new Item("GemGreen", 1, "使用该物品，获得100金币", new HashSet<string>() { "道具" }));
            database.AddItem(new Item("Heart", 1, "使用该物品，生命值增加100", new HashSet<string>() { "道具" }));
            database.AddItem(new Item("Skull", 1, "使用该物品，防御力增加50", new HashSet<string>() { "道具" }));
            database.AddItem(new Item("Star", 1, "使用该物品，幸运值增加20", new HashSet<string>() { "道具" }));
            database.AddItem(new Item("TabFlagBlue", 5, "使用该物品，战斗力增加200", new HashSet<string>() { "礼包" }));
            database.AddItem(new Item("TabFlagGreen", 3, "使用该物品，获得100金币", new HashSet<string>() { "礼包" }));
            database.AddItem(new Item("TabFlagPink", 77, "使用该物品，生命值增加100", new HashSet<string>() { "礼包" }));
            database.AddItem(new Item("TabFlagRed", 10, "使用该物品，防御力增加50", new HashSet<string>() { "礼包" }));
            database.AddItem(new Item("VictoryHornsL", 3, "使用该物品，幸运值增加20", new HashSet<string>() { "铭文" }));
            database.AddItem(new Item("VictoryHornsR", 9, "使用该物品，幸运值增加20", new HashSet<string>() { "铭文" }));
            database.AddItem(new Item("VictoryStar", 11, "使用该物品，幸运值增加20", new HashSet<string>() { "铭文" }));
        }


        // Update is called once per frame
        void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                Spawn();
            }*/
        }

        void Spawn()
        {
            Clear();

            List<Item> result = database.items.FindAll(filterStrategy).ToList();

            for (int i = 0; i < result.Count; i++)
            {
                GameObject itemViewObj = Instantiate(itemPrefab, transform);
                ItemView itemView = itemViewObj.GetComponent<ItemView>();
                itemView.OnSelectedItemView = SelectedView;
                itemView.SetValue(result[i]);
                itemViews.Add(itemView);
            }

            if (itemViews.Count > 0)
            {
                SelectedView(itemViews[0]);
            }
        }

        // 选中某个item后，将其展示在右侧面板
        void SelectedView(ItemView selectedItemView)
        {
            // 处理选中Item的背景
            if (currSelected == selectedItemView)
                return;
            if (lastSelected != null)
                lastSelected.RestoreBackgroundWhenClick();
            currSelected = selectedItemView;
            currSelected.ChangeBackgroundWhenClick();
            lastSelected = currSelected;

            // 处理Item的信息展示
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


        public void OnLabelTagChange(LabelTag labelTag)
        {
            string labelName = labelTag.GetText();
            
            filterStrategy = labelName switch
            {
                "道具" => (Item item) => item.tags.Contains("道具"),
                "礼包" => (Item item) => item.tags.Contains("礼包"),
                "铭文" => (Item item) => item.tags.Contains("铭文"),
                _ => (Item item) => true,
            };

            Spawn();
        }

        void OnDisable()
        {
            Clear();
        }
    }
}