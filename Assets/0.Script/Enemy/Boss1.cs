using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss1 : EnemyBoss
{
    [SerializeField] bool isAttack2 = false;

    [SerializeField] GameObject portal;
    [SerializeField] Transform portalPos;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F7))
        {
            Debug.Log($"{data.HP}");
        }
        if(p == null)
        {
            p = GameManager.Instance.Player;
        }

        float dist = Vector2.Distance(p.transform.position, transform.position);
        
        if(state == BossState.Dead)
        {
            Dead();
            return;
        }

        if(state == BossState.Back)
        {
            if(sk1isLeft)
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

        if(state == BossState.Hit)
        {
            SpriteCheck(state);
            return;
        }

        else
        {
            if(dist<=10f)
            {
                state = BossState.Move;

                if (dist < 2f)
                {
                    state = BossState.Attack1;
                }
                Move();
            }
            else
            {
                state = BossState.Idle;
                Move();
            }
        }
    }


    void Move()
    {
        if(transform.position.x > p.transform.position.x)
        {
            transform.localScale = new Vector3(-10, 10, 10);
        }
        else if (transform.position.x<p.transform.position.x)
        {
            transform.localScale = new Vector3(10, 10, 10);
        }

        if(state == BossState.Attack1)
        {
            SpriteCheck(state);
            return;
        }

        if (state == BossState.Attack2)
        {
            SpriteCheck(state);
            if (isAttack2)
            {
                float timer = 0;
                timer += Time.deltaTime;
                if (transform.position.x > p.transform.position.x)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * 10f);
                }
                else
                {
                    transform.Translate(Vector3.right * Time.deltaTime * 10f);
                }
                if (timer > 1.5)
                {
                    isAttack2 = false;
                    timer = 0;
                }

            }
            if(!isAttack2)
            {

            }
            return;
        }

        if (state == BossState.Idle)
        {
            SpriteCheck(state);
            return;
        }

        //Vector2 pos = Vector2.MoveTowards(transform.position, p.transform.position, Time.deltaTime * data.Speed);

        transform.position = Vector2.MoveTowards(transform.position, p.transform.position, Time.deltaTime * data.Speed);
        SpriteCheck(state);
    }

    void SpriteCheck(BossState state)
    {
        switch(state)
        {
            case BossState.Idle:
                {
                    sa.SetSprite(idleSprites, 0.2f);
                    break;
                }
            case BossState.Move:
                {
                    sa.SetSprite(idleSprites, 0.2f);
                    break;
                }
            case BossState.Hit:
                {
                    sa.SetSprite(hitSprites, 0.2f, false, Idle);
                    break;
                }
            case BossState.Attack1:
                {
                    sa.SetSprite(attack1Sprite, 0.3f, false, Idle);
                    break;
                }
            case BossState.Attack2:
                {
                    sa.SetSprite(attack2Sprite, 0.3f, false, Idle);
                    break;
                }
            case BossState.Back:
                {
                    sa.SetSprite(idleSprites, 0.2f);
                    break;
                }
        }
    }

    void Attack1()
    {
        state = BossState.Attack1;
    }

    void Attack2()
    {
        state = BossState.Attack2;
    }

    void Dead()
    {
        data.Speed = 0;
        GetComponent<CapsuleCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        sa.SetSprite(deadSprite, 0.3f, false, Clear);
    }

    void Clear()
    {
        GameObject obj = Instantiate(portal, portalPos);
        obj.transform.SetParent(null);
        Destroy(gameObject, 1f);
    }
    public override void Init()
    {
        data.HP = 4;
        data.Index = 0;
        data.Speed = 5f;
        data.Atk1Power = 10;
        data.EXP = 20;
        base.Init();
    }

}
