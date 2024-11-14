using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int damage = 7;
    [SerializeField] Transform turnPoint;
    [SerializeField] float power;
    [SerializeField] bool start = false;
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!start)
        {
            rigid.AddForce(Vector3.up * power, ForceMode2D.Impulse);
            start = true;
        }

        float dist = Vector2.Distance(transform.position, turnPoint.position);

        if(dist<0.5f)
        {
            start = false;
        }
        
    }
}
