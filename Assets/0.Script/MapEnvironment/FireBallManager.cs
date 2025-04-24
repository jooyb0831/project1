using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    [SerializeField] GameObject fireBall;
    [SerializeField] Transform firePos;
    [SerializeField] Transform fireballs;
    [SerializeField] float delay;
    [SerializeField] float timer;
    public bool isStart = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart)
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                timer = 0;
                GameObject obj = Pooling.Instance.GetPool(DicKey.fireball, firePos);
                obj.transform.position = firePos.position;
                obj.transform.SetParent(fireballs);
                isStart = false;
                
            }
        }

    }
}
