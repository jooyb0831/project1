using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        data.HP = JsonData.Instance.enemyData.eData[3].hp;
        data.Index = JsonData.Instance.enemyData.eData[3].index;
        data.Speed = JsonData.Instance.enemyData.eData[3].speed;
        data.AttackPower = JsonData.Instance.enemyData.eData[3].atkPower;
        data.EXP = JsonData.Instance.enemyData.eData[3].exp;
        base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(p == null)
        {
            p = GameManager.Instance.Player;
        }

        float dist = Vector2.Distance(p.transform.position, transform.position);

        if(state == EnemyState.Dead)
        {
            return;
        }

        if(state == EnemyState.Back)
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
            if(dist<7f && dist>0.8f)
            {
                state = EnemyState.Run;
                Move();
            }
            else
            {
                state = EnemyState.Idle;
                SpriteCheck(state);
            }
        }
    }

    void Move()
    {
        transform.localScale = transform.position.x > p.transform.position.x ? Vector3.one * 5f : new Vector3(-5f, 5f, 5f);
        transform.position = Vector2.MoveTowards(transform.position, p.transform.position, Time.deltaTime * data.Speed);
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
                    sa.SetSprite(moveSprite, 0.2f);
                    break;
                }
            case EnemyState.Attack:
                {
                    sa.SetSprite(attackSprite, 0.3f);
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

}
