using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakFloor : MonoBehaviour
{
    [SerializeField] GameObject[] tiles;
    [SerializeField] float delay;
    [SerializeField] float timer;
    [SerializeField] int idx = 0;
    [SerializeField] bool isVanish;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>delay)
        {
            timer = 0;
            if(isVanish)
            {
                tiles[idx].SetActive(false);
            }
            else
            {
                tiles[idx].SetActive(true);
            }
            
            idx++;

            if(idx>=tiles.Length)
            {
                idx = 0;
                if(isVanish)
                {
                    isVanish = false;
                }
                else
                {
                    isVanish = true;
                }
            }
        }
    }
}
