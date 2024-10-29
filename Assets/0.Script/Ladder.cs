using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Transform bottom;
    public Transform up;
    public bool isTop = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LadderTrigger(bool ladderAttach)
    {
        if(ladderAttach)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        else if (!ladderAttach)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
