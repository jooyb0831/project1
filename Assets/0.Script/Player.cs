using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PlayerData data;


    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] Transform firePos;
    [SerializeField] PBullet pBullet;
    [SerializeField] Transform foot;
    [SerializeField] float dist;
    [SerializeField] Transform pBulletParent;
    public bool isRun = false;
    public bool isJump = false;
    public bool isAttacking = false;
    public bool isHurt = false;

    [SerializeField] float speed;
    public float jumpPower;
    [SerializeField] bool canFire = false;
    [SerializeField] float fireDelayTime = 0.8f;
    [SerializeField] float fireTimer;
    public enum State
    {
        Idle,
        Walk,
        Run,
        Jump,
        WalkAttack,
        RunAttack,
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

    [SerializeField] bool isJumpRun = false;
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
        speed = data.Speed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RaycastHit2D hit = Physics2D.Raycast(foot.position, Vector3.down);
        Debug.DrawRay(foot.position, Vector3.down, Color.red);

        if(hit.collider.GetComponent<Ground>())
        {
            dist = hit.distance;
        }
        /*
        if(isJump && Input.GetAxisRaw("Horizontal")!=0)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                isJumpRun = true;
                speed = data.Speed;
                Move();
            }
            
            else
            {
                Move();
            }
        }
        else
        {
            Move();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isJumpRun = false;
        }

        
        if (isJump)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    speed = data.Speed;
                }
                else
                {
                    Move();
                }
            }
            else
            {
                Move();
            }
        }
        else
        {
            Move();
        }
        */



        fireTimer += Time.deltaTime;
        if (fireTimer > fireDelayTime)
        {
            fireTimer = 0;
            canFire = true;
        }

    }

    /// <summary>
    /// 플레이어 움직임
    /// </summary>
    void Move()
    {
        if (speed == 0)
        {
            speed = data.Speed;
        }

        if (state == State.Hurt)
        {
            StateCheck(state);
            return;
        }
        
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0)
        {
            if (x > 0)
            {
                transform.localScale = new Vector3(5f, 5f, 5f);
            }
            else
            {
                transform.localScale = new Vector3(-5f, 5f, 5f);
            }
            state = State.Walk;
        }
        else
        {
            state = State.Idle;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {

            speed = data.RunSpeed;
            state = State.Run;
            if (Input.GetKey(KeyCode.P))
            {
                state = State.RunAttack;
                Fire();
            }
            isRun = true;
        }
        
        transform.Translate(new Vector2(x, 0) * Time.deltaTime * speed);

        

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(dist>0.1f)
            {
                return;
            }
            
        }

        

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = data.Speed;
            isRun = false;
        }



        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                isJump = true;
                state = State.Jump;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            }
            isRun = false;

        }

        // 점프중일 때 Shift 키 막기



        
        if (Input.GetKey(KeyCode.P))
        {
            if (state != State.Attack && state != State.WalkAttack)
            {
                if (x != 0)
                {
                    state = State.WalkAttack;
                    Fire();
                }
                else
                {
                    state = State.Attack;
                }
            }
            /*
            if (canFire == true)
            {
                
                canFire = false;
                //Invoke("Bullet", 0.3f);
            }
            else
            {
                return;
            }
            */
        }

        if (isJump)
        {
            state = State.Jump;

        }

        StateCheck(state);
    }

    void Run(float x)
    {
        transform.Translate(new Vector2(x, 0) * Time.deltaTime * speed);
    }

    void StateCheck(State state)
    {
        switch (state)
        {

            case State.Idle:
                {
                    sa.SetSprite(idleSprites, 0.2f);
                    break;
                }
            case State.Walk:
                {
                    sa.SetSprite(walkSprites, 0.2f);
                    break;
                }
            case State.Run:
                {
                    sa.SetSprite(walkSprites, 0.1f);
                    break;
                }
            case State.Attack:
                {
                    sa.SetSprite(attackSprites, 0.3f, false, Bullet);
                    //Invoke("IdleSprite", 0.6f);
                    break;
                }
            case State.RunAttack:
                {
                    sa.SetSprite(runAttackSprites, 0.1f);
                    break;
                }
            case State.WalkAttack:
                {
                    sa.SetSprite(runAttackSprites, 0.2f);

                    break;
                }
            case State.Jump:
                {
                    sa.SetSprite(jumpSprites, 0.2f);
                    break;
                }
            case State.Hurt:
                {
                    sa.SetSprite(hurtSprites, 0.2f);
                    Invoke("IdleSprite", 0.5f);
                    break;
                }

        }
    }

    void Bullet()
    {
        PBullet pbullet = Pooling.Instance.GetPool(DicKey.pBullet, firePos).GetComponent<PBullet>();
        if(pbullet == null)
        {
            return;
        }
        if (transform.localScale.x < 0)
        {
            pbullet.isRight = false;
        }
        pbullet.transform.SetParent(pBulletParent);

    }

    void Fire()
    {
        if(state == State.WalkAttack || state == State.RunAttack)
        {
            if(canFire == false)
            {
                return;
            }
            else if(canFire == true)
            {
                Bullet();
                fireTimer = 0;
                canFire = false;
            }
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
        //움직이고 있을 때
        if(x!=0)
        {
            // 캐릭터의 방향
           
        }
        else
        {

        }

        /*
        // 움직이지 않는 경우
        else if(x==0)
        {
            // 점프하고 있을 때
            if(y!=0 && isJump == true)
            {
                sa.SetSprite(jumpSprites, 0.3f);
            }
            // 공격중일 때
            if (state == State.Attack)
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
        */
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

        if (collision.gameObject.GetComponent<Jump>() && rigid.velocity.y < 0)
        {
            rigid.AddForce(new Vector2(0, 17), ForceMode2D.Impulse);
        }

        if (collision.gameObject.GetComponent<EBullet2>())
        {
            if(data.HP>collision.GetComponent<EBullet2>().damage)
            {
                isHurt = true;
                data.HP -= collision.GetComponent<EBullet2>().damage;
                state = State.Hurt;
            }
            else if (data.HP <= collision.GetComponent<EBullet2>().damage)
            {
                Destroy(gameObject);
            }
            Pooling.Instance.SetPool(DicKey.eBullet2, collision.GetComponent<EBullet2>().gameObject);
        }
    }
}
