using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : EnemyBoss
{
    public Transform punchArea;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform fireArea;
    [SerializeField] float atkTimer = 0;
    [SerializeField] bool atk2 = false;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        data.HP = 10;
        data.Index = 1;
        data.Speed = 5f;
        data.Atk1Power = 20;
        data.Atk2Power = 25;
        data.EXP = 30;
        originSpeed = data.Speed;

        base.Init();
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            pd.StageCleared[1] = true;
            return;
        }

        if (p == null)
        {
            p = GameManager.Instance.Player;
        }

        if (state == BossState.Dead)
        {
            return;
        }

        float dist = Vector2.Distance(p.transform.position, transform.position);

        if (state == BossState.Back)
        {
            if (sk1isLeft)
            {
                transform.Translate(Vector2.left * Time.deltaTime * 6f);
            }
            else
            {
                transform.Translate(Vector2.right * Time.deltaTime * 6f);
            }
            SpriteCheck(state);
            return;
        }


        if (state == BossState.Hit)
        {
            SpriteCheck(state);
            return;
        }

        if(!atk2)
        {
            atkTimer += Time.deltaTime;
            if(atkTimer>=3f)
            {
                atkTimer = 0;
                atk2 = true;
            }
        }

        if (state == BossState.Back)
        {
            if (sk1isLeft)
            {
                transform.Translate(Vector2.left * Time.deltaTime * 6f);
            }
            else
            {
                transform.Translate(Vector2.right * Time.deltaTime * 6f);
            }
            SpriteCheck(state);
            return;
        }

        if (dist<15f)
        {
            state = BossState.Move;

            if(atk2)
            {
                state = BossState.Attack2;
                Attack2();
            }
            else
            {
                if(dist<3f)
                {
                    state = BossState.Attack1;
                    Attack1();
                }
            }
            Move();
            
        }
        else
        {
            state = BossState.Idle;
            Move();
        }

    }

    void Move()
    {

        if (transform.position.x > p.transform.position.x)
        {
            transform.localScale = new Vector3(-10, 10, 10);
        }
        else if (transform.position.x < p.transform.position.x)
        {
            transform.localScale = new Vector3(10, 10, 10);
        }
        if(state==BossState.Move)
        {
            transform.position = Vector2.MoveTowards(transform.position, p.transform.position, Time.deltaTime * data.Speed);
        }

        SpriteCheck(state);
    }


    void Attack1()
    {
        state = BossState.Attack1;
        SpriteCheck(state);
    }

    void Attack1Act()
    {
        punchArea.GetComponent<Boss2PunchArea>().isAttacked = true;
        punchArea.GetComponent<CircleCollider2D>().enabled = true;
    }

    void Attack2()
    {
        state = BossState.Attack2;
        SpriteCheck(state);
    }

    void Fire()
    {
        Vector2 dir = fireArea.rotation * new Vector2(2f, 0) * 5f;
        GameObject obj = Pooling.Instance.GetPool(DicKey.boss2Bullet, fireArea, fireArea.rotation).gameObject;
        obj.GetComponent<Boss2Bullet>().damage = data.Atk2Power;
        if(transform.localScale.x<0)
        {
            obj.GetComponent<Boss2Bullet>().Shoot(-dir);
        }
        else
        {
            obj.GetComponent<Boss2Bullet>().Shoot(dir);
        }
        atk2 = false;

    }
    void SpriteCheck(BossState state)
    {
        switch (state)
        {
            case BossState.Idle:
                {
                    sa.SetSprite(idleSprites, 0.2f);
                    break;
                }
            case BossState.Move:
                {
                    sa.SetSprite(walkSprites, 0.2f);
                    break;
                }
            case BossState.Hit:
                {
                    sa.SetSprite(hitSprites, 0.1f, false, Idle);
                    break;
                }
            case BossState.Attack1:
                {
                    sa.SetSprite(attack1Sprite, 0.2f, false, Attack1Act);
                    break;
                }
            case BossState.Attack2:
                {
                    sa.SetSprite(attack2Sprite, 0.1f, false, Fire);
                    break;
                }
            case BossState.Back:
                {
                    sa.SetSprite(idleSprites, 0.2f);
                    break;
                }
        }
    }
}