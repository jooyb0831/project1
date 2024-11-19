using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
{
    [SerializeField] float fireTimer;
    [SerializeField] float fireDelayTime = 1f;
    [SerializeField] bool canFire = false;
    [SerializeField] EBullet2 eBullet;
    [SerializeField] Transform firePos;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        data.HP = 15;
        data.index = 2;
        data.Speed = 3f;
        data.AttackPower = 10;

        base.Init();
    }
    // Update is called once per frame
    void Update()
    {
        if(state == EnemyState.Dead)
        {
            return;
        }

        float dist = Vector2.Distance(p.transform.position, transform.position);
        if (state == EnemyState.Back)
        {
            if (sk1isLeft)
            {
                transform.Translate(Vector2.left * Time.deltaTime * 10f);
            }
            else
            {
                transform.Translate(Vector2.right * Time.deltaTime * 10f);
            }
            SpriteCheck(state);
            return;
        }

        else
        {
            if(dist<15f)
            {
                state = EnemyState.Run;

                if(dist<10f)
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
    }

    void Move()
    {
        if(transform.position.x > p.transform.position.x)
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
        if (state == EnemyState.Idle)
        {
            SpriteCheck(state);
            return;
        }

        Vector2 pos = Vector2.MoveTowards(transform.position, p.transform.position, Time.deltaTime * data.Speed);

        transform.position = new Vector2(pos.x, transform.position.y);

        SpriteCheck(state);
    }

    void SpriteCheck(EnemyState state)
    {
        switch (state)
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
            case EnemyState.Back:
                {
                    sa.SetSprite(enemySprite, 0.2f);
                    break;
                }
                /*
            case EnemyState.Dead:
                {
                    sa.SetSprite(deadSprite, 0.2f);
                    break;
                }
                */

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
}
