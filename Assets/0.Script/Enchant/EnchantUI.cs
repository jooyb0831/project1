using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantUI : Singleton<EnchantUI>
{
    public GameObject window;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(window.activeSelf)
            {
                window.SetActive(false);
            }
            else if(!window.activeSelf)
            {
                return;
            }
        }
    }

}
