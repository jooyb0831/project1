using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantSystem : Singleton<MerchantSystem>
{
    public GameObject window;
    [SerializeField] Player p;
    [SerializeField] PlayerData pd;
    [SerializeField] Inventory inven;

    [SerializeField] GameObject myInven;
    [SerializeField] List<Transform> invenSlots;
    [SerializeField] Transform[] merchInvenSlots;
    [SerializeField] GameObject merchInvenUI;

    [SerializeField] Transform[] merchSellSlots;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        inven = GameManager.Instance.Inven;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F6))
        {
            SetInven();
        }

    }

    void Init()
    {
        int index = inven.invenSlots.Length;
        for(int i =0; i<index; i++)
        {
            invenSlots.Add(inven.invenSlots[i]);
        }

    }

    public void SetInven()
    {
        Debug.Log("ÀÛµ¿");
        for(int i = 0; i<merchInvenSlots.Length; i++)
        {
            if(merchInvenSlots[i].transform.childCount>=1)
            {
                Destroy(merchInvenSlots[i].GetChild(0).gameObject);
            }
        }

        for (int i = 0; i < Inventory.Instance.invenSlots.Length; i++)
        {
            if (Inventory.Instance.invenSlots[i].GetComponent<Slots>().isFilled)
            {
                merchInvenUI = inven.invenSlots[i].GetChild(0).gameObject;
                Instantiate(merchInvenUI, merchInvenSlots[i]);
            }
        }
    }

    public void FindItem(InvenItem item, int count)
    {
        int index = item.transform.parent.GetSiblingIndex();
        Debug.Log($"{index}");

        if(merchInvenSlots[index].GetChild(0).GetComponent<InvenItem>().data.count==count)
        {
            Destroy(inven.invenSlots[index].GetChild(0).gameObject);
            inven.invenSlots[index].GetComponent<Slots>().isFilled = false;
        }
        else if(merchInvenSlots[index].GetChild(0).GetComponent<InvenItem>().data.count > count)
        {
            Inventory.Instance.ItemCount(inven.invenSlots[index].GetChild(0).GetComponent<InvenItem>(), count, true);
        }
    }

    /*
    public void BuyItems(InvenItem item, int count)
    {
        int index = transform.parent.GetSiblingIndex();

        
    }
    */
}
