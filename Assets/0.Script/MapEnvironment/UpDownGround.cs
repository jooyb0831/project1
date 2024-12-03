using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownGround : MonoBehaviour
{
    public bool isUp;
    public float speed;
    [SerializeField] float top;
    [SerializeField] float btm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y = transform.localPosition.y;

        if (y <= btm)
        {
            isUp = true;
        }
        else if (y >= top)
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
