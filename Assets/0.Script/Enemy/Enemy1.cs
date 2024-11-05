using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    private bool isLeft = true;
    [SerializeField] float timer = 0;
    [SerializeField] float time = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Update()
    {
        if (!isDead)
        {
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
            }
            else
            {
                Move();
            }

        }
        else
        {
            return;
        }
    }

    void Move()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            timer = 0;
            if (isLeft == true)
            {
                isLeft = false;
            }
            else if (isLeft == false)
            {
                isLeft = true;
            }

        }
        if (isLeft == true)
        {
            transform.localScale = new Vector3(4, 4, 4);
            transform.Translate(Vector2.left * Time.deltaTime * data.Speed);
        }
        else if (isLeft == false)
        {
            transform.localScale = new Vector3(-4, 4, 4);
            transform.Translate(Vector2.right * Time.deltaTime * data.Speed);
        }

    }



    public override void Init()
    {
        data.HP = 6;
        data.index = 0;
        data.Speed = 2f;
        data.AttackPower = 5;
        base.Init();
    }
}