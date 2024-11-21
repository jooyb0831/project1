using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
                tiles[idx].GetComponent<SpriteRenderer>().DOFade(0,0.2f);
            }
            else
            {
                tiles[idx].GetComponent<SpriteRenderer>().DOFade(1, 0.2f);
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
