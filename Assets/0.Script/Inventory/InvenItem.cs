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

    public void AddItem(InvenData data)
    {
        cntTxt.text = $"{data.count}";
        cntBG.SetActive(data.count <= 1 ? false : true);
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
            if(invenOption == null)
            {
                invenOption = Instantiate(itemOptionWindow, transform);
                //아이템 종류에 따라서 목록 다르게 수정
                invenOption.GetComponent<InvenItemOption>().item = this;
                invenOption.transform.SetParent(transform.parent.parent.parent);
                invenOption.transform.SetAsLastSibling();
            }

            else if(invenOption!=null)
            {
                Destroy(invenOption);
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
