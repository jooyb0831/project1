using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Scene : MonoBehaviour
{
    private Inventory inven;
    // Start is called before the first frame update
    void Start()
    {
        inven = GameManager.Instance.Inven;
        //Inventory.Instance.SetItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
