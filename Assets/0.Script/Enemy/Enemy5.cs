using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : Enemy
{
    [SerializeField] float fireTimer;
    [SerializeField] float fireDelayTime = 2f;
    [SerializeField] bool canFire = false;
    [SerializeField] Transform firePos;
    [SerializeField] float y;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        data.HP = 25;
        data.index = 4;
        data.Speed = 1.8f;
        data.AttackPower = 20;
        y = transform.position.y;

        base.Init();
    }

    // Update is called once per frame
    void Update()
    {

        if (p == null)
        {
            p = GameManager.Instance.Player;
        }
        float dist = Vector2.Distance(p.transform.position, transform.position);
        //Debug.Log(dist);

        if (state == EnemyState.Dead)
        {
            return;
        }

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
            if (dist <= 12f)
            {
                state = EnemyState.Run;


                if (dist < 10)
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
        if (state == EnemyState.Idle)
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
        EBullet eb = Pooling.Instance.GetPool(DicKey.eBullet, firePos).GetComponent<EBullet>();
        if (transform.localScale.x < 0)
        {
            eb.isRight = true;
        }
        eb.damage = data.AttackPower;
        eb.transform.SetParent(eBulletParent);
        state = EnemyState.Idle;
    }
}
