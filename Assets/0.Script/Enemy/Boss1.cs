using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss1 : EnemyBoss
{
    [SerializeField] bool isAttack2 = false;
    [SerializeField] bool posSet = false;
    [SerializeField] float atkTimer = 0;
    [SerializeField] bool atk2 = false;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }


    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            pd.StageCleared[0] = true;
        }
        if(Input.GetKeyDown(KeyCode.F7))
        {
            Debug.Log($"{data.HP}");
        }
        if(p == null)
        {
            p = GameManager.Instance.Player;
        }

        if (state == BossState.Dead)
        {
            return;
        }

        float dist = Vector2.Distance(p.transform.position, transform.position);

        if(!atk2)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer >= 10f)
            {
                atkTimer = 0;
                atk2 = true;
            }
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

                if(atk2)
                {
                    state = BossState.Attack2;
                    isAttack2 = true;
                }
                else
                {
                    if(dist<2f)
                    {
                        state = BossState.Attack1;
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
            posSet = false;
            return;
        }
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
                    sa.SetSprite(hitSprites, 0.1f, false, Idle);
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

    [SerializeField] Vector2 pos = new Vector2(0, 0);
    void Attack2()
    {
        state = BossState.Attack2;
        SpriteCheck(state);
        if(isAttack2)
        {
           
            if (!posSet)
            {
                
                if (transform.position.x < p.transform.position.x)
                {
                    pos = new Vector2(p.transform.position.x + 10f, p.transform.position.y);
                    posSet = true;
                }
                else if( (transform.position.x>p.transform.position.x))
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
                atk2 = false;
                Move();
            }
        }
        else
        {
            state = BossState.Idle;
            SpriteCheck(state);
        }
    }
    public override void Init()
    {
        data.HP =10;
        data.Index = 0;
        data.Speed = 5f;
        data.Atk1Power = 10;
        data.Atk2Power = 15;
        data.EXP = 20;
        originSpeed = data.Speed;
        base.Init();
    }
}
