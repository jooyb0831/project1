using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] Transform[] slots;
    public Transform quickSlot;
    [SerializeField] Inventory inventory;
    [SerializeField] InvenItem sampleInvenitem;
    [SerializeField] PlayerData pData;
    [SerializeField] GameObject quickItem;

    void Start()
    {
        Init();
        SetInventory();
    }

    public void Init()
    {
        if (inventory == null)
        {
            inventory = GameManager.Instance.Inven;
        }

        if (pData == null)
        {
            pData = GameManager.Instance.PlayerData;
        }
        
        SetInvenSlot();
        InventoryCheck();
    }

    void SetInvenSlot()
    {
        int curSlotNum = inventory.inventoryData.curInvenNums;

        for (int i = 0; i < curSlotNum; i++)
        {
            inventory.invenSlots[i] = slots[i];
        }

        if (curSlotNum > 5)
        {
            int gap = curSlotNum - 5;

            for(int i =1; i<gap+1; i++)
            {
                slots[i + 4].GetComponent<Slots>().isLocked = false;
            }
        }
        inventory.quickSlot = quickSlot;
    }

    void SetInventory()
    {
        if (inventory == null)
        {
            inventory = GameManager.Instance.Inven;
        }
        inventory.invenItems.Clear();
        List<InvenData> invenData = inventory.invenDatas;
        if (invenData.Count == 0)
        {
            return;
        }
        for (int i = 0; i < invenData.Count; i++)
        {
            SetData(invenData[i]);
        }

        SceneType type = SceneChanger.Instance.sceneType;
        if (type.Equals(SceneType.Ship) || type.Equals(SceneType.Stage1) 
            || type.Equals(SceneType.Stage2) || type.Equals(SceneType.Stage3))
        {
            Inventory.Instance.quickSlot = quickSlot;
        }
        else
        {
            return;
        }
    }

    void SetData(InvenData data)
    {
        InvenItem item = null;
        item = Instantiate(sampleInvenitem, slots[data.slotIdxNum]);
        item.SetData(data);
        item.SetInventory(inventory);
        item.transform.parent.gameObject.GetComponent<Slots>().isFilled = true;
        inventory.invenItems.Add(item);

        if(data.inQuickSlot)
        {
            GameObject obj = Instantiate(quickItem, quickSlot);
            obj.GetComponent<QuickInven>().SetData(item);
            obj.GetComponent<QuickInven>().SetInvenItem(item);
            quickSlot.GetComponent<QuickSlot>().isFilled = true;
            data.qItem = obj.GetComponent<QuickInven>();
        }
    }


    void InventoryCheck()
    {
        foreach(var item in slots)
        {
            if(item.childCount>=1)
            {
                item.GetComponent<Slots>().isFilled = true;
            }
        }
    }




}
