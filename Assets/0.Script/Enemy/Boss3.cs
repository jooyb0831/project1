using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : EnemyBoss
{
    [SerializeField] float moveCoolTime;
    [SerializeField] float moveTimer;

    [SerializeField] float attackCoolTime;
    [SerializeField] float attackTimer;

    [SerializeField] float fireDelay;
    [SerializeField] float fireTimer;

    [SerializeField] bool isMove = false;
    [SerializeField] bool isAttack = false;
    [SerializeField] float speed;
    [SerializeField] bool isUp = false;
    [SerializeField] Boss3Bullet bullet1;
    [SerializeField] Transform firePos;
    int patternNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        StartCoroutine(FirstMove());
    }

    public override void Init()
    {
        data.MAXHP = 10;
        data.CURHP = data.MAXHP;
        data.Index = 2;
        data.Speed = 5f;
        data.Atk1Power = 10;
        data.Atk2Power = 15;
        data.EXP = 20;
        data.BossName = "º¸½º 3";
        originSpeed = data.Speed;
        BossUI.Instance.boss = this;
        BossUI.Instance.SetUI();
        base.Init();
        
    }

    IEnumerator FirstMove()
    {
        yield return new WaitForSeconds(2f);
        isMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            pd.StageCleared[2] = true;
            return;
        }

        if(state == BossState.Hit)
        {
            if (isMove)
            {
                Move();
            }
            else if (isAttack)
            {
                Attack1();
            }
        }

        if(state != BossState.Hit)
        {
            if (isMove)
            {
                isAttack = false;
                moveTimer += Time.deltaTime;
                if (moveTimer >= moveCoolTime)
                {
                    isUp = true;
                    moveTimer = 0;
                }
                Move();
            }

            if (state == BossState.Attack1)
            {
                isMove = false;
                isAttack = true;
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackCoolTime)
                {
                    attackTimer = 0;
                    state = BossState.Move;
                    isMove = true;
                    return;
                }
                Attack1();
            }
        }
        

    }

    void Move()
    {
        if (isUp)
        {
            float y = transform.position.y;
            if (y >= -3.5f)
            {
                isUp = false;
                state = BossState.Attack1;
                return;
            }
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
        else
        {
            float y = transform.position.y;
            if (y <= -10.35f)
            {
                isUp = true;
                state = BossState.Attack1;
                return;
            }
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }
    }


    void Attack1()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireDelay)
        {
            fireTimer = 0;
            GameObject obj = Instantiate(bullet1, firePos).gameObject;
            obj.transform.SetParent(null);
        }
        
    }


    IEnumerator BossUpdate()
    {
        yield return new WaitForSeconds(3f);
        while(true)
        {
            if(data.CURHP<=0)
            {
                yield break;
            }

            if(patternNum>=2)
            {
                patternNum = 0;
            }

            string patternName = $"Pattern{patternNum + 1}";
            yield return StartCoroutine(patternName);
            yield return new WaitForSeconds(1.5f);

            patternNum++;
          
        }
    }

}
