using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    [SerializeField] float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);

        /*
        if (transform.localPosition.x <= -14.5f)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        else if(transform.localPosition.x >= 0)
        {
            
        }
        */
    }
}
