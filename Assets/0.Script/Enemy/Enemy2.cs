using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    private bool isFind = false;
    [SerializeField] float fireTimer;
    [SerializeField] float fireDelayTime = 2f;
    [SerializeField] bool canFire = false;
    [SerializeField] EBullet2 eBullet;
    [SerializeField] Transform firePos;
    [SerializeField] float y;

    // Start is called before the first frame update
    void Start()
    {
        Init();


    }

    // Update is called once per frame
    void Update()
    {
        
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
        else
        {
            isFind = false;
        }

        if(isFind)
        {

            Move();
            //Attack();
        }
        else
        {
            return;
        }
        
        
        if (isFind)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer > fireDelayTime)
            {
                EBullet2 eb = Pooling.Instance.GetPool(DicKey.eBullet2, firePos).GetComponent<EBullet2>();
                if(transform.localScale.x<0)
                {
                    eb.isRight = true;
                }
                eb.damage = data.AttackPower;
                eb.transform.SetParent(eBulletParent);
                fireTimer = 0;
            }
        }
        

    }

    void Move()
    {
        Vector2 pos = Vector2.MoveTowards(transform.position, p.transform.position, Time.deltaTime * data.Speed);

        if (transform.position.x > p.transform.position.x)
        {
            transform.localScale = new Vector3(5, 5, 5);
        }
        else if (transform.position.x < p.transform.position.x)
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }

        transform.position = new Vector2(pos.x, y);
        
        
    }
    
    void Attack()
    {

    }

    public override void Init()
    {
        data.HP = 10;
        data.index = 1;
        data.Speed = 1f;
        data.AttackPower = 8;
        y = transform.position.y;

        base.Init();
    }

}
