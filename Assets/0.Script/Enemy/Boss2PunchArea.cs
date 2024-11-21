using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2PunchArea : MonoBehaviour
{
    public bool isAttacked = false;
    [SerializeField] float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isAttacked)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                timer = 0;
                GetComponent<CircleCollider2D>().enabled = false;
                isAttacked = false;
            }
        }

    }
}
