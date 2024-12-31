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
    public List<InvenItem> items = new();
    public bool InvenFull = false;
    
}

public class Inventory : Singleton<Inventory>
{
    private PlayerData pd;
    private Player p;
    [SerializeField] InvenItem invenItem;
    public Transform quickSlot;
    public Transform[] invenSlots;

    public InventoryData inventoryData = new();
    public List<InvenItem> invenItems = new(); // �κ��丮 ������ �޴� ����Ʈ
    private List<int> itemNumbers = new();
    public List<InvenData> invenDatas = new();
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        pd = GameManager.Instance.PlayerData;
    }

    /// <summary>
    /// �κ��丮�� ������ �߰��ϴ� �Լ�(ItemData �μ�)
    /// </summary>
    /// <param name="itemData"></param>
    public void GetItem(ItemData itemData)
    {
        if (itemNumbers.Contains(itemData.itemNumber))
        {
            ItemCheck(itemData);
            Destroy(itemData.obj);
            return;
        }

        bool isFull = EmptySlotCheck();
        if(isFull)
        {
            itemData.fieldItem.InvenFull(isFull);
            return;
        }
        itemData.fieldItem.InvenFull(isFull);

        itemNumbers.Add(itemData.itemNumber);
        int index = SlotCheck();
        InvenItem item = Instantiate(invenItem, invenSlots[index]);
        invenSlots[index].GetComponent<Slots>().isFilled = true;

        InvenData data = new InvenData
        {
            title = itemData.itemTitle,
            iconSprite = itemData.invenIcon,
            bgSprite = itemData.invenBGSprite,
            type = itemData.itemType,
            count = itemData.count,
            price = itemData.price,
            usage = itemData.usage,
            itemNumber = itemData.itemNumber,
            fItem = itemData.fieldItem,
            slotIdxNum = index
        };
        item.SetData(data);
        item.SetInventory(this);
        invenItems.Add(item);
        invenDatas.Add(item.data);
        inventoryData.items.Add(item);
        Destroy(itemData.obj);
    }

    /// <summary>
    /// �κ��丮�� ������ �߰�(InvenItem �μ�)
    /// </summary>
    /// <param name="invenItem"></param>
    public void GetItem(InvenItem invenItem)
    {
        if (itemNumbers.Contains(invenItem.data.itemNumber))
        {
            ItemCheck(invenItem);
            return;
        }

        bool isFull = EmptySlotCheck();
        if(isFull)
        {
            GameUI.Instance.fullInvenObj.SetActive(true);
            GameUI.Instance.fullInvenObj.GetComponent<FullInvenObj>().Act(0);
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

    /// <summary>
    /// �� ���� üũ
    /// </summary>
    /// <returns></returns>
    public bool EmptySlotCheck()
    {
        bool isFull = false;
        for (int i = 0; i < invenSlots.Length; i++)
        {
            if (invenSlots[i] == null)
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

    /// <summary>
    /// ���� �ε��� �ο�
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// �ߺ������� üũ(ItemData)
    /// </summary>
    /// <param name="itemData"></param>
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

    /// <summary>
    /// �ߺ������� üũ(InvenItem)
    /// </summary>
    /// <param name="item"></param>
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

    /// <summary>
    /// �ߺ������� üũ->������ �ڵ��ȣ ��ȯ
    /// </summary>
    /// <param name="itemNum"></param>
    /// <returns></returns>
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

    /// <summary>
    /// ��ȭ�� ������ ã�� �Լ�
    /// </summary>
    /// <param name="itemNum"></param>
    /// <param name="useCnt"></param>
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
        //���߿� ���� UI �߰��Ǹ� �۾�
    }

    /// <summary>
    /// ������ ���
    /// </summary>
    /// <param name="item"></param>
    public void UseItem(InvenItem item)
    {
        ItemType type = item.data.type;

       if(!type.Equals(ItemType.Missile))
       {
           item.data.fItem.Using();
       }
        
       if(type.Equals(ItemType.Potion))
        {
            if(pd.HP==pd.MAXHP)
            {
                GameUI.Instance.fullInvenObj.SetActive(true);
                GameUI.Instance.fullInvenObj.GetComponent<FullInvenObj>().Act(1);
                return;
            }
        }

        item.data.count--;
        item.ItemCntChange(item.data);
        if(item.data.count<=0)
        {
            DeleteItem(item);
            Destroy(item.gameObject);
        }
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    /// <param name="item"></param>
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
    /// <summary>
    /// ������ ����(������)
    /// </summary>
    /// <param name="item"></param>
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

    /// <summary>
    /// ������ ���� ���� �ڵ�
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    /// <param name="min"></param>
    public void ItemCount(InvenItem item, int count, bool min)
    {
        //������ ���̳ʽ� �� ��
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
                item.ItemCntChange(item.data);
            }
        }

        //������ �÷��� �� ��
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
                invenItem.ItemCntChange(invenItem.data);

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


    


}
