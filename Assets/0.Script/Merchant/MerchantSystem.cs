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

    }

    void Init()
    {
        int index = inven.invenSlots.Length;
        for(int i =0; i<index; i++)
        {
            invenSlots.Add(inven.invenSlots[i]);
        }

    }

    /// <summary>
    /// 상점에서 내 인벤토리 세팅
    /// </summary>
    public void SetInven()
    {
        for(int i = 0; i<merchInvenSlots.Length; i++)
        {
            if(merchInvenSlots[i].transform.childCount>=1)
            {
                Destroy(merchInvenSlots[i].GetChild(0).gameObject);
            }
        }

        for (int i = 0; i < Inventory.Instance.invenSlots.Length; i++)
        {
            if(Inventory.Instance.invenSlots[i] == null)
            {
                return;
            }
            if (Inventory.Instance.invenSlots[i].GetComponent<Slots>().isFilled)
            {
                merchInvenUI = inven.invenSlots[i].GetChild(0).gameObject;
                Instantiate(merchInvenUI, merchInvenSlots[i]);
            }
        }
    }

    /// <summary>
    /// 아이템 판매 체크 함수
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public void SellItemCheck(InvenItem item, int count)
    {
        int index = item.transform.parent.GetSiblingIndex();
        Debug.Log($"{index}");

        //전체 판매할 경우
        if(merchInvenSlots[index].GetChild(0).GetComponent<InvenItem>().data.count==count)
        {
            //인벤토리에서 아이템 삭제
            Destroy(inven.invenSlots[index].GetChild(0).gameObject);
            //인벤토리 빈 것으로 체크
            inven.invenSlots[index].GetComponent<Slots>().isFilled = false;
        }

        //일부만 판매할 경우
        else if(merchInvenSlots[index].GetChild(0).GetComponent<InvenItem>().data.count > count)
        {
            //인벤토리에서 수량만 변경
            Inventory.Instance.ItemCount(inven.invenSlots[index].GetChild(0).GetComponent<InvenItem>(), count, true);
        }
    }

    public void BuyItemCheck(InvenItem item)
    {

    }

   public void OnExitBtn()
    {
        Time.timeScale = 1;
        window.SetActive(false);
    }
}
