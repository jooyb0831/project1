using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornFloor : MonoBehaviour
{
    public int Damage { get; set; } = 5;
    [SerializeField] bool isUp;
    [SerializeField] float speed;
    [SerializeField] float delay;
    [SerializeField] float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isUp)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
        else if(!isUp)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }

        float y = transform.localPosition.y;
        if(y<=-0.7f)
        {
            transform.localPosition = new Vector2(transform.localPosition.x,-0.7f);
            timer += Time.deltaTime;
            if(timer>delay)
            {
                timer = 0;
                isUp = true;
            }
        }

        if(y>=0)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, 0f);
            timer += Time.deltaTime;
            if(timer>=delay)
            {
                timer = 0;
                isUp = false;
            }
        }
    }
}
