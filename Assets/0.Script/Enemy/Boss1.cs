using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss1 : EnemyBoss
{
    [SerializeField] bool isAttack2 = false;

    [SerializeField] GameObject portal;
    [SerializeField] Transform portalPos;
    [SerializeField] bool atk2Left = false;
    [SerializeField] float atkTimer = 0;
    [SerializeField] int prob = 0;
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

        atkTimer += Time.deltaTime;
        if(atkTimer>=5f)
        {
            atkTimer = 0;
            prob = Random.Range(0, 100);
        }
        
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
            if(dist<=15f)
            {
                state = BossState.Move;

                if(dist<10f)
                {
                    state = BossState.Attack2;
                    
                    isAttack2 = true;
                }
                
                /*
                if (dist < 2f)
                {
                    state = BossState.Attack1;
                }
                */
                Move();
            }
            else
            {
                state = BossState.Idle;
                Move();
            }
        }
    }

    [SerializeField] bool isAtk2DirSet = false;
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
            if (isAttack2)
            {
                Attack2();
            }
            SpriteCheck(state);
            return;
        }
        
        if (state == BossState.Idle)
        {
            SpriteCheck(state);
            isAtk2DirSet = false;
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
                    isAtk2DirSet = false;
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
                    sa.SetSprite(attack1Sprite, 0.2f, false, Idle);
                    break;
                }
            case BossState.Attack2:
                {
                    sa.SetSprite(attack2Sprite, 0.1f, false, Idle);
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
        SpriteCheck(state);
    }

    [SerializeField] float timer = 0;
    void Attack2()
    {
        state = BossState.Attack2;
        SpriteCheck(state);
        bool posSet = false;
        if(isAttack2)
        {
            Vector2 pos = new Vector2(0,0);
            if(transform.position.x<p.transform.position.x)
            {
                if (!posSet)
                {
                    pos = new Vector2(p.transform.position.x + 10f, p.transform.position.y);
                    posSet = true;
                }
            }
            else
            {
                if(!posSet)
                {
                    pos = new Vector2(p.transform.position.x - 10f, p.transform.position.y);
                    posSet = true;
                }
                
            }
            float dist = Vector2.Distance(transform.position, pos);
            transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * 20f);
            if (dist<=0.5f)
            {
                isAttack2 = false;
                state = BossState.Idle;
                posSet = false;
                Move();
            }
        }
        else
        {
            state = BossState.Idle;
            SpriteCheck(state);
        }
        if (timer >= 1.5)
        {
            isAttack2 = false;
            isAtk2DirSet = false;
            state = BossState.Idle;
            timer = 0;
        }
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
        Destroy(gameObject, 0.9f);
    }
    public override void Init()
    {
        data.HP =10;
        data.Index = 0;
        data.Speed = 5f;
        data.Atk1Power = 10;
        data.Atk2Power = 15;
        data.EXP = 20;
        base.Init();
    }
}
