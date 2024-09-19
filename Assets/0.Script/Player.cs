using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PlayerData data;


    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] Transform firePos;
    [SerializeField] PBullet pBullet;

    public bool isJump = false;
    public bool isAttacking = false;
    public bool isHurt = false;
    [SerializeField] float fireDelayTime = 0.5f;
    float fireTimer;
    public enum State
    {
        Idle,
        Walk,
        Run,
        Jump,
        WalkAttack,
        Attack,
        Slide,
        Sit,
        Hurt,
        Dead
    }

    private SpriteAnimation sa;

    private List<Sprite> idleSprites;
    private List<Sprite> walkSprites;
    private List<Sprite> jumpSprites;
    private List<Sprite> attackSprites;
    private List<Sprite> runAttackSprites;
    private List<Sprite> hurtSprites;
    private List<Sprite> slideSprites;
    private List<Sprite> sitSprites;

    [SerializeField] State state = State.Idle;
    // Start is called before the first frame update
    void Start()
    {
        data = GameManager.Instance.playerData;
        sa = GetComponent<SpriteAnimation>();
        SpriteManager.PlayerSprite pSprite = SpriteManager.Instance.playerSprite[0];
        idleSprites = pSprite.idleSprites;
        walkSprites = pSprite.walkSprites;
        jumpSprites = pSprite.jumpSprites;
        attackSprites = pSprite.attackSprites;
        runAttackSprites = pSprite.runAttackSprites;
        hurtSprites = pSprite.hurtSprites;
        slideSprites = pSprite.slideSprites;
        sitSprites = pSprite.sitSprites;

        sa.SetSprite(idleSprites, 0.2f);
        state = State.Idle;
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(isHurt == true)
        {
            sa.SetSprite(hurtSprites, 0.2f);
            Invoke("IdleSprite", 0.5f);
        }
        else
        {
            Move();

        }
    }
    public float jumpTimer;
    public float jumpTime;
    public float jumpPower;
    public float jumptimeLimit;
    /// <summary>
    /// 플레이어 움직임
    /// </summary>
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Jump");
        transform.Translate(new Vector2(x, 0) * Time.deltaTime * data.Speed);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            state = State.Run;
            transform.Translate(new Vector2(x, 0) * Time.deltaTime * data.RunSpeed);

        }

        if (Input.GetButton("Jump"))
        {
            if (isJump == false)
            {
                isJump = true;
                state = State.Jump;
                jumpTimer += Time.deltaTime;
                if(jumpTimer>=jumpTime)
                {
                    return;
                }
                else
                {
                    rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
                }


            }
        }

        if (Input.GetMouseButton(0))
        {
            if(isAttacking == false)
            {
                isAttacking = true;
                if(x!=0)
                {
                    state = State.WalkAttack;
                }
                else
                {
                    state = State.Attack;
                }
            }
            Attack();
        }
        SpriteChange(x, y);

       
    }


    void JumpGuage()
    {
        if(Input.GetButton("Jump") && !isJump)
        {
            if(jumptimeLimit >jumpTime)
            {
                jumpTime += 0.1f;
            }
        }
    }
    void Attack()
    {
        state = State.Attack;
        Invoke("Bullet",0.3f);

    }

    void Bullet()
    {
        fireTimer += Time.deltaTime;
        if(fireTimer>=fireDelayTime)
        {
            fireTimer = 0;
            PBullet pbullet = Instantiate(pBullet, firePos);
            if(transform.localScale.x<0)
            {
                pbullet.isRight = false;
            }
            pbullet.transform.SetParent(null);
        }

    }
    void IdleSprite()
    {
        sa.SetSprite(idleSprites, 0.2f);
        state = State.Idle;
        if (isAttacking == true)
        {
            isAttacking = false;
        }
        if(isHurt == true)
        {
            isHurt = false;
        }
    }

    void SpriteChange(float x, float y)
    {
        if(x!=0)
        {
            if(x>0)
            {
                transform.localScale = new Vector3(5f, 5f, 5f);
            }
            else
            {
                transform.localScale = new Vector3(-5f, 5f, 5f);
            }
            if (state == State.Run)
            {
                sa.SetSprite(walkSprites, 0.1f);
            }
            else
            {
                sa.SetSprite(walkSprites, 0.2f);
                state = State.Walk;
            }
            if (isJump == false)
            {
                sa.SetSprite(walkSprites, 0.1f);
                state = State.Walk;
            }
            if(y != 0 && isJump == true)
            {
                sa.SetSprite(jumpSprites, 0.3f);
            }
            if(isAttacking == true)
            {
                sa.SetSprite(runAttackSprites, 0.2f);
                Invoke("IdleSprite", 0.6f);
            }
        }
        else if(x==0)
        {
            if(y!=0 && isJump == true)
            {
                sa.SetSprite(jumpSprites, 0.3f);
            }
            if (isAttacking == true)
            {
                sa.SetSprite(attackSprites, 0.2f);
                Invoke("IdleSprite", 0.6f);
            }
            if(state.Equals(State.Hurt))
            {
                sa.SetSprite(hurtSprites, 0.2f);
            }
            if (state != State.Idle && isAttacking == false && isJump == false)
            {
                sa.SetSprite(idleSprites, 0.2f);
                state = State.Idle;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Ground>()==true)
        {
            isJump = false;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() == true)
        {
            if (data.HP > collision.GetComponent<Enemy>().data.AttackPower)
            {
                isHurt = true;
                data.HP -= collision.GetComponent<Enemy>().data.AttackPower;
                Debug.Log($"{data.HP}");
                state = State.Hurt;

            }
            else if (data.HP <= collision.GetComponent<Enemy>().data.AttackPower)
            {
                Destroy(gameObject);
            }

        }

        if(collision.gameObject.GetComponent<Jump>()&&rigid.velocity.y<0)
        {
            rigid.AddForce(new Vector2(0, 17), ForceMode2D.Impulse);
        }
    }
}
