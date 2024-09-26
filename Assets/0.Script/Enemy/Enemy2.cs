using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    private bool isFind = false;
    [SerializeField] float fireTimer;
    [SerializeField] float fireDelayTime = 1f;
    [SerializeField] bool canFire = false;
    [SerializeField] EBullet eBullet;
    [SerializeField] Transform firePos;
    //[SerializeField] Player p;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        //p = GameManager.Instance.player;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (p == null)
        {
            p = GameManager.Instance.player;
        }
        float dist = Vector2.Distance(p.transform.position, transform.position);
        Debug.Log(dist);

        if (dist < 7f)
        {
            isFind = true;
        }

        if(isFind)
        {
            Vector2.MoveTowards(transform.position, p.transform.position, Time.deltaTime * data.Speed);

            if(transform.position.x>p.transform.position.x)
            {
                transform.localScale = new Vector3(4, 4, 4);
            }
            else if(transform.position.x<p.transform.position.x)
            {
                transform.localScale = new Vector3(-4, 4, 4);
            }


        }

        
        if (isFind)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer > fireDelayTime)
            {
                EBullet eb = Instantiate(eBullet, firePos);
                eb.transform.SetParent(null);
                fireTimer = 0;
            }
        }
        */

    }

    void Move()
    {

    }
    
    void Attack()
    {

    }

    public override void Init()
    {
        data.HP = 10;
        data.index = 1;
        data.Speed = 3f;
        data.AttackPower = 8;
        
        base.Init();
    }
}
