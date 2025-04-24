using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour
{
    [SerializeField] FireBar fire;
    [SerializeField] Sprite onSprite;
    public bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn)
        {
            fire.gameObject.SetActive(true);
            GetComponent<SpriteRenderer>().sprite = onSprite;
        }
    }
}
