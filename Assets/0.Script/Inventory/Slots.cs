using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    public bool isFilled = false;
    public bool isMerchantInven = false;
    public bool isSellInven = false;
    public bool isLocked;
    [SerializeField] GameObject cover;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cover!=null)
        {
            if (!isLocked)
            {
                cover.SetActive(false);
            }
        }

    }
}
