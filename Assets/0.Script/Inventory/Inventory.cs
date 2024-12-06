using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class InvenData
{
    public Sprite iconSprite;
    public Sprite bgSprite;
    public int count;
    public int itemNumber;
    public string title;
    public ItemType type;
    public int price;
    public int usage;
    public int slotIdxNum;
    public bool inQuickSlot = false;
    public QuickInven qItem = null;
    public FieldItem fItem = null;
}

public enum ItemType
{
    Gem,
    Potion,
    Bomb,
    Missile,
    Etc
}

public class InventoryData
{
    public int curInvenNums = 5;
    public List<InvenItem> items = new List<InvenItem>();
    public bool InvenFull = false;
    
}

public class Inventory : Singleton<Inventory>
{
    private PlayerData pd;
    private Player p;
    [SerializeField] InvenItem invenItem;
    public Transform quickSlot;
    public Transform[] invenSlots;

    public InventoryData inventoryData = new InventoryData();
    public List<InvenItem> invenItems = new List<InvenItem>(); // 인벤토리 데이터 받는 리스트
    private List<int> itemNumbers = new List<int>();
    public List<InvenData> invenDatas = new List<InvenData>();
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        pd = GameManager.Instance.PlayerData;
    }

    public void GetItem(ItemData itemData)
    {
        if (itemNumbers.Contains(itemData.itemNumber))
        {
            ItemCheck(itemData);
            return;
        }

        bool isFull = EmptySlotCheck();
        if(isFull)
        {
            itemData.fieldItem.InvenFull();
            return;
        }
        
        itemNumbers.Add(itemData.itemNumber);
        int index = SlotCheck();
        InvenItem item = Instantiate(invenItem, invenSlots[index]);
        invenSlots[index].GetComponent<Slots>().isFilled = true;
        InvenData data = new InvenData();
        data.title = itemData.itemTitle;
        data.iconSprite = itemData.invenIcon;
        data.bgSprite = itemData.invenBGSprite;
        data.type = itemData.itemType;
        data.count = itemData.count;
        data.price = itemData.price;
        data.usage = itemData.usage;
        data.itemNumber = itemData.itemNumber;
        data.fItem = itemData.fieldItem;
        data.slotIdxNum = index;
        item.SetData(data);
        item.SetInventory(this);
        invenItems.Add(item);
        invenDatas.Add(item.data);
        inventoryData.items.Add(item);
    }

    public void GetItem(InvenItem invenItem)
    {
        if (itemNumbers.Contains(invenItem.data.itemNumber))
        {
            ItemCheck(invenItem);
            return;
        }

        itemNumbers.Add(invenItem.data.itemNumber);
        int index = SlotCheck();
        InvenItem item = Instantiate(invenItem, invenSlots[index]);
        invenSlots[index].GetComponent<Slots>().isFilled = true;
        item.data.slotIdxNum = index;
        item.SetInventory(this);
        invenItems.Add(item);
        invenDatas.Add(item.data);
        inventoryData.items.Add(item);
    }

    /*
    [SerializeField] InventoryUI invenUI;
    public void SetItem()
    {
        InventoryUI.Instance.Init();
        invenItems.Clear();
        
        Debug.Log("호출");
        Debug.Log($"{invenDatas.Count}");
        for(int i =0; i<invenDatas.Count; i++)
        {
            Debug.Log($"Hello{i}");
            
            InvenItem item = Instantiate(invenItem, invenSlots[invenDatas[i].slotIdxNum]);
            
            item.SetData(invenDatas[i]);
            item.transform.parent.GetComponent<Slots>().isFilled = true;
            invenItems.Add(item);
            if(invenDatas[i].inQuickSlot)
            {
                ItemEquip(item);
            }
            
        }
    }
    */
    int SlotCheck()
    {
        int number = -1;
        for (int i = 0; i < invenSlots.Length; i++)
        {
            if (!invenSlots[i].GetComponent<Slots>().isFilled 
                && !invenSlots[i].GetComponent<Slots>().isLocked)
            {
                number = i;
                break;
            }
        }
        return number;
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if(SceneChanger.Instance.sceneType.Equals("Ship")||SceneChanger.Instance.sceneType.Equals("Stage1"))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        */
    }

    void ItemCheck(ItemData itemData)
    {
        InvenItem invenItem = null;
        for(int i =0; i<invenItems.Count; i++)
        {
            if(invenItems[i].data.itemNumber == itemData.itemNumber)
            {
                invenItem = invenItems[i];
                break;
            }
        }
        invenItem.data.count += itemData.count;
        invenItem.GetComponent<InvenItem>().ItemCntChange(invenItem.data);
        if(invenItem.data.inQuickSlot)
        {
            invenItem.data.qItem.ItemCntChange(invenItem);
        }
    }

    void ItemCheck(InvenItem item)
    {
        InvenItem invenItem = null;
        for(int i = 0; i<invenItems.Count; i++)
        {
            if(invenItems[i].data.itemNumber == item.data.itemNumber)
            {
                invenItem = invenItems[i];
                break;
            }
        }
        invenItem.data.count += item.data.count;
        invenItem.GetComponent<InvenItem>().ItemCntChange(invenItem.data);
        if(invenItem.data.inQuickSlot)
        {
            invenItem.data.qItem.ItemCntChange(invenItem);
        }
    }

    public int ItemCheck(int itemNum)
    {
        int cnt = 0;
        InvenItem invenItem = null;
        if(invenItems.Count ==0)
        {
            return cnt;
        }
        else
        {
            for (int i = 0; i < invenItems.Count; i++)
            {
                if (invenItems[i].data.itemNumber == itemNum)
                {
                    invenItem = invenItems[i];
                    cnt = invenItem.data.count;
                    break;
                }
            }
            return cnt;
        }

    }

    public void Enchant(int itemNum, int useCnt)
    {
        InvenItem invenItem = null;
        for (int i = 0; i < invenItems.Count; i++)
        {
            if (invenItems[i].data.itemNumber == itemNum)
            {
                invenItem = invenItems[i];
                break;
            }
        }
        invenItem.data.count -= useCnt;
        invenItem.ItemCntChange(invenItem.data);
        if(invenItem.data.count<=0)
        {
            DeleteItem(invenItem);
            Destroy(invenItem.gameObject);
        }
    }

    void ItemAdd(ItemData itemData)
    {
        //나중에 옆에 UI 추가되면 작업
    }

    public void UseItem(InvenItem item)
    {
        ItemType type = item.data.type;

       if(!type.Equals(ItemType.Missile))
        {
            item.data.fItem.Using();
        }
        
        item.data.count--;
        item.ItemCntChange(item.data);
        if(item.data.count<=0)
        {
            DeleteItem(item);
            Destroy(item.gameObject);
        }
    }

    public void DeleteItem(InvenItem item)
    {
        item.transform.parent.GetComponent<Slots>().isFilled = false;
        item.invenOption = null;

        if (item.data.inQuickSlot)
        {
            item.data.qItem.transform.parent.GetComponent<QuickSlot>().isFilled = false;
            Destroy(item.data.qItem.gameObject);
        }
        int itemIdx = -1;
        for (int i = 0; i < invenItems.Count; i++)
        {
            if (invenItems[i].data.itemNumber == item.data.itemNumber)
            {
                itemIdx = i;
                break;
            }
        }
        itemNumbers.Remove(item.data.itemNumber);
        invenItems.RemoveAt(itemIdx);
        invenDatas.RemoveAt(itemIdx);
        Destroy(item);
    }

    [SerializeField] QuickInven quickItem;
    public void ItemEquip(InvenItem item)
    {
        if(quickSlot.GetComponent<QuickSlot>().isFilled)
        {
            Destroy(quickSlot.GetChild(0).gameObject);
        }
        QuickInven qItem = Instantiate(quickItem, quickSlot);
        item.data.inQuickSlot = true;
        item.data.qItem = qItem;
        qItem.SetData(item);
        qItem.SetInvenItem(item);
        quickSlot.GetComponent<QuickSlot>().isFilled = true;
    }

    public void ItemCount(InvenItem item, int count, bool min)
    {
        if(min == true)
        {
            int minus = item.data.count - count;

            if (minus <= 0)
            {
                DeleteItem(item);
                Destroy(item.gameObject);
            }
            else
            {
                item.data.count -= count;
                item.GetComponent<InvenItem>().ItemCntChange(item.data);
            }
        }
        else
        {
            if (itemNumbers.Contains(item.data.itemNumber))
            {
                InvenItem invenItem = null;
                for (int i = 0; i < invenItems.Count; i++)
                {
                    if (invenItems[i].data.itemNumber == item.data.itemNumber)
                    {
                        invenItem = invenItems[i];
                        break;
                    }
                }
                invenItem.data.count += count;
                invenItem.GetComponent<InvenItem>().ItemCntChange(invenItem.data);
                if (invenItem.data.inQuickSlot)
                {
                    invenItem.data.qItem.ItemCntChange(invenItem);
                }
            }
            else
            {
                GetItem(item);
            }

        }

    }

    public bool EmptySlotCheck()
    {
        bool isFull = false;
        for(int i=0; i<invenSlots.Length; i++)
        {
            if(invenSlots[i] == null)
            {
                isFull = true;
                break;
            }
            if (!invenSlots[i].GetComponent<Slots>().isFilled)
            {
                isFull = false;
                break;
            }
            else
            {
                isFull = true;
            }

        }
        return isFull;
    }

    


}
