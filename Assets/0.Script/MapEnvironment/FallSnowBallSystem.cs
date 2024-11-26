using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSnowBallSystem : Singleton<FallSnowBallSystem>
{
    [SerializeField] float timer;
    [SerializeField] float delay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=delay)
        {
            timer = 0;
            MakeBall();
        }
    }

    public void MakeBall()
    {
        GameObject obj = Pooling.Instance.GetPool(DicKey.fallSnowBall, transform).gameObject;
        obj.transform.SetParent(null);
    }    
}
