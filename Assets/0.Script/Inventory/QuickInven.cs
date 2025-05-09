using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuickInven : MonoBehaviour
{
    public Transform quickSlot;
    private Inventory inventory;
    public InvenItem invenItem;
    private Player p;
    public int cnt;
    [SerializeField] Image itemIcon;
    [SerializeField] GameObject cntBG;
    [SerializeField] TMP_Text cntTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(InvenItem item)
    {
        itemIcon.sprite = item.data.iconSprite;
        cnt = item.data.count;
        cntTxt.text = $"{cnt}";
        cntBG.SetActive(cnt <= 1 ? false : true);
    }
    public void ItemCntChange(InvenItem item)
    {
        cnt = item.data.count;
        cntTxt.text = $"{cnt}";
        cntBG.SetActive(cnt <= 1 ? false : true);
    }


    public void SetInvenItem(InvenItem item)
    {
        invenItem = item;
    }
}
