using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenItemOption : MonoBehaviour
{
    public InvenItem item;
    [SerializeField] DumpWindowUI dumpWindowUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
        if(hit.collider != gameObject)
        {
            if(Input.GetMouseButton(0))
            {
                Destroy(gameObject);
            }
            else
            {
                return;
            }
        }
        */
    }
    
    public void OnEquiqBtn()
    {
        Inventory.Instance.ItemEquip(item);
        transform.SetParent(item.transform);
        transform.SetAsLastSibling();
        gameObject.SetActive(false);
    }

    public void OnExitBtn()
    {
        transform.SetParent(item.transform);
        transform.SetAsLastSibling();
        gameObject.SetActive(false);
    }

    public void OnDeleteBtn()
    {
        DumpWindowUI dumpUI = Instantiate(dumpWindowUI, transform);
        dumpUI.SetItem(item);
        dumpUI.transform.SetParent(transform.parent);
        dumpUI.transform.localPosition = Vector2.zero;
        Destroy(gameObject);
    }
}
