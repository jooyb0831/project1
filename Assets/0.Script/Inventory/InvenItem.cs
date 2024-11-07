using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InvenItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] Image itemBG;
    [SerializeField] Image itemIcon;
    [SerializeField] GameObject cntBG;
    [SerializeField] TMP_Text cntTxt;
    public GameObject invenOption = null;
    public GameObject itemOptionWindow;
    public GameObject itemSellWindow;
    public GameObject itemBuyWindow;
    private Inventory inventory;
   

    public InvenData data;
    public void SetData(InvenData data)
    {
        this.data = data;
        itemIcon.sprite = data.iconSprite;
        itemBG.sprite = data.bgSprite;
        cntTxt.text = $"{data.count}";
        cntBG.SetActive(data.count <= 1 ? false : true);
    }



    public void SetInventory(Inventory inven)
    {
        inventory = inven;
    }

    public void ItemCntChange(InvenData data)
    {
        cntTxt.text = $"{data.count}";
        cntBG.SetActive(data.count <= 1 ? false : true);
        if(data.inQuickSlot)
        {
            data.qItem.ItemCntChange(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(transform.parent.GetComponent<Slots>().isMerchantInven || transform.transform.parent.GetComponent<Slots>().isSellInven)
            {
                return;
            }
            if(invenOption == null)
            {
                invenOption = Instantiate(itemOptionWindow, transform);
                //아이템 종류에 따라서 목록 다르게 수정
                invenOption.GetComponent<InvenItemOption>().item = this;

                for (int i = 0; i < invenOption.transform.GetChild(1).childCount; i++)
                {
                    invenOption.transform.GetChild(1).GetChild(i).gameObject.SetActive(true);
                }
                if (invenOption.GetComponent<InvenItemOption>().item.data.type.Equals(ItemType. Gem))
                {
                    invenOption.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                    invenOption.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                }

                invenOption.transform.SetParent(transform.parent.parent.parent);
                invenOption.transform.SetAsLastSibling();
            }

            else if(invenOption!=null)
            {
                Destroy(invenOption);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (transform.parent.GetComponent<Slots>().isMerchantInven)
            {
                GameObject window = Instantiate(itemSellWindow, transform.parent.parent.parent);
                window.transform.SetAsLastSibling();
                window.GetComponent<ItemSellWindow>().SetItem(this);
            }

            if (transform.parent.GetComponent<Slots>().isSellInven)
            {
                GameObject window = Instantiate(itemBuyWindow, transform.parent.parent.parent);
                window.transform.SetAsLastSibling();
                window.GetComponent<ItemBuyWindow>().SetItem(this);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
