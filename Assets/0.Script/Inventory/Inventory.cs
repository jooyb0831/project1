using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvenData
{
    public Sprite iconSprite;
    public Sprite bgSprite;
    public int count;
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
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void GetItem(ItemData itemData)
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
