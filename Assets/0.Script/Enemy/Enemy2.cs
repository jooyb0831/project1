using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    

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

        if (dist <= 10f)
        {
            state = EnemyState.Run;
            

            if(dist<7f)
            {
                state = EnemyState.Attack;
            }
            Move();
        }

        else
        {
            state = EnemyState.Idle;
            Move();
        }
    }

    void Move()
    {
        if (transform.position.x > p.transform.position.x)
        {
            transform.localScale = new Vector3(5, 5, 5);
        }
        else if (transform.position.x < p.transform.position.x)
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }

        if (state == EnemyState.Attack)
        {
            SpriteCheck(state);
            return;
        }
        if(state == EnemyState.Idle)
        {
            SpriteCheck(state);
            return;
        }
        if(state == EnemyState.Dead)
        {
            SpriteCheck(state);
            return;
        }

        Vector2 pos = Vector2.MoveTowards(transform.position, p.transform.position, Time.deltaTime * data.Speed);

        transform.position = new Vector2(pos.x, y);

        SpriteCheck(state);

    }
    
    void SpriteCheck(EnemyState state)
    {
        switch(state)
        {
            case EnemyState.Idle:
                {
                    sa.SetSprite(enemySprite, 0.2f);
                    break;
                }
            case EnemyState.Run:
                {
                    sa.SetSprite(enemySprite, 0.2f);
                    break;
                }
            case EnemyState.Attack:
                {
                    sa.SetSprite(attackSprite, 0.3f, false, Attack);
                    break;
                }
            case EnemyState.Dead:
                {
                    sa.SetSprite(deadSprite, 0.2f);
                    break;
                }

        }
    }
    void Attack()
    {
        EBullet2 eb = Pooling.Instance.GetPool(DicKey.eBullet2, firePos).GetComponent<EBullet2>();
        if (transform.localScale.x < 0)
        {
            eb.isRight = true;
        }
        eb.damage = data.AttackPower;
        eb.transform.SetParent(eBulletParent);
        state = EnemyState.Idle;
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
