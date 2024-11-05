using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantSystem : MonoBehaviour
{
    [SerializeField] Player p;
    [SerializeField] PlayerData pd;
    [SerializeField] Inventory inven;

    [SerializeField] GameObject myInven;
    [SerializeField] List<Transform> slots;
    [SerializeField] GameObject merchInvenUI;

    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        inven = GameManager.Instance.Inven;
        SetInven();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetInven()
    {
        int index = inven.invenSlots.Length;
        for(int i =0; i<index; i++)
        {
            slots.Add(inven.invenSlots[i]);
        }
        
    }
}
