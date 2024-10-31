using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenItemOption : MonoBehaviour
{
    public InvenItem item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
