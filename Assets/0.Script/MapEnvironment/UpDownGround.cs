using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownGround : MonoBehaviour
{
    public bool isUp;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y = transform.localPosition.y;

        if (y <= 0)
        {
            isUp = true;
        }
        else if (y >= 15)
        {
            isUp = false;
        }

        if (isUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);        
        }

        else
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }

        
    }
}
