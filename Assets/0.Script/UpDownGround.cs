using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownGround : MonoBehaviour
{
    [SerializeField] bool isUp;
    [SerializeField] float delay;
    [SerializeField] float timer;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);        
        }

        else
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }

        timer += Time.deltaTime;
        if(timer>=delay)
        {
            if(isUp)
            {
                isUp = false;
            }
            else
            {
                isUp = true;
            }
            timer = 0;
        }
    }
}
