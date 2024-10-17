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
    public int slotIdxNum;

}

public enum ItemType
{
    Gem,
    Potion
}

public class InventoryData
{
    public List<InvenItem> items = new List<InvenItem>();
}

public class Inventory : Singleton<Inventory>
{ 
    [SerializeField] InvenItem invenItem;
    public Transform[] invenSlots;

    public InventoryData inventoryData = new InventoryData();
    public List<InvenItem> invenItems = new List<InvenItem>(); // 인벤토리 데이터 받는 리스트
    private List<int> itemNumbers = new List<int>();
    public List<InvenData> invenDatas = new List<InvenData>();
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void GetItem(ItemData itemData)
    {
        if(itemNumbers.Contains(itemData.itemNumber))
        {
            ItemCheck(itemData);
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
        data.itemNumber = itemData.itemNumber;
        data.slotIdxNum = index;
        item.SetData(data);
        item.SetInventory(this);
        invenItems.Add(item);
        invenDatas.Add(item.data);
        inventoryData.items.Add(item);

    }


    int SlotCheck()
    {
        int number = 0;
        for(int i=0; i<invenSlots.Length; i++)
        {
            if(!invenSlots[i].GetComponent<Slots>().isFilled)
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
        invenItem.GetComponent<InvenItem>().AddItem(invenItem.data);
    }


    void ItemAdd(ItemData itemData)
    {
        //나중에 옆에 UI 추가되면 작업
    }
}
