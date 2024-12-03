using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : EnemyBoss
{
    [SerializeField] float moveCoolTime;
    [SerializeField] float moveTimer;

    [SerializeField] float attackCoolTime;
    [SerializeField] float attackTimer;

    [SerializeField] float attack2CoolTime;
    [SerializeField] float attack2Timer;

    [SerializeField] float fireDelay;
    [SerializeField] float fireTimer;

    [SerializeField] float atk2Duration;
    [SerializeField] float atk2DurationTimer;

    [SerializeField] bool isMove = false;
    [SerializeField] bool isAttack1 = false;
    [SerializeField] bool isAttack2 = false;

    [SerializeField] float speed;
    [SerializeField] bool isUp = false;
    [SerializeField] Boss3Bullet2 bullet2;
    [SerializeField] Boss3Bullet bullet1;
    [SerializeField] Transform firePos;

    [SerializeField] Transform bulletParent;
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
        data.Atk1Power = 15;
        data.Atk2Power = 30;
        data.EXP = 40;
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

        if(!isAttack2)
        {
            attack2Timer += Time.deltaTime;
            if (attack2Timer >= attack2CoolTime)
            {
                attack2Timer = 0;
                isMove = false;
                isAttack1 = false;
                isAttack2 = true;
            }
        }
        if(isAttack2)
        {
            Attack2();
        }

        if (isMove)
        {
            isAttack1 = false;
            moveTimer += Time.deltaTime;
            if (moveTimer >= moveCoolTime)
            {
                isUp = true;
                moveTimer = 0;
            }
            Move();
        }

        if (isAttack1)
        {
            isMove = false;
            state = BossState.Attack1;
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCoolTime)
            {
                attackTimer = 0;
                state = BossState.Move;
                isMove = true;
                isAttack1 = false;
                return;
            }
            Attack1();
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
                isAttack1 = true;
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
                isAttack1 = true;
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
            GameObject obj = Pooling.Instance.GetPool(DicKey.boss3Bullet, firePos);
            obj.GetComponent<Boss3Bullet>().damage = data.Atk1Power;
            obj.transform.SetParent(bulletParent);
        }
    }

    [SerializeField] bool isDown = false;
    void Attack2()
    {
        if(!bullet2.gameObject.activeSelf)
        {
            bullet2.gameObject.SetActive(true);
            bullet2.damage = data.Atk2Power;
        }
        atk2DurationTimer += Time.deltaTime;

        float y = transform.position.y;
        if(y>=-3.5f)
        {
            isDown = true;
        }
        else if(y<=-10.35f)
        {
            isDown = false;
        }

        if(isDown)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 10f);
        }
        else
        {
            transform.Translate(Vector2.up * Time.deltaTime * 10f);
        }

        if(atk2DurationTimer>=atk2Duration)
        {
            isMove = true;
            atk2DurationTimer = 0;
            isAttack2 = false;
            bullet2.gameObject.SetActive(false) ;
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

    public void BossHit()
    {
        StartCoroutine(BossHitSprite());
    }

    IEnumerator BossHitSprite()
    {
        GetComponent<SpriteRenderer>().color = new Color32(255,85,85,255);
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

}
