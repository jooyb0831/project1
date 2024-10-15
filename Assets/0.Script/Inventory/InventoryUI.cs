using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] Transform[] slots;
    [SerializeField] Inventory inventory;
    [SerializeField] InvenItem sampleInvenitem;
    [SerializeField] PlayerData pData;

    private void Start()
    {
        if (inventory == null)
        {
            inventory = GameManager.Instance.Inventory;
        }
        pData = GameManager.Instance.PlayerData;

    }

    void SetInvenSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            inventory.invenSlots[i] = slots[i];
        }
    }

    void SetInventory()
    {
        if (inventory == null)
        {
            inventory = GameManager.Instance.Inventory;
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
    }

    void SetData(InvenData data)
    {
        InvenItem item = null;
        item = Instantiate(sampleInvenitem, slots[data.slotIdxNum]);
        item.SetData(data);
        item.SetInventory(inventory);
        inventory.invenItems.Add(item);
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
