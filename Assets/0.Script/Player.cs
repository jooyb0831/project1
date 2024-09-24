using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PlayerData data;


    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] Transform firePos;
    [SerializeField] PBullet pBullet;

    public bool isRun = false;
    public bool isJump = false;
    public bool isAttacking = false;
    public bool isHurt = false;

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
        if (isJump)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if(Input.GetAxisRaw("Horizontal")!=0 && !isRun)
                {
                    Move();
                }
                return;
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



        /*
        fireTimer += Time.deltaTime;
        if(fireTimer>fireDelayTime)
        {
            fireTimer = 0;
            canFire = true;
        }
        */


    }

    /// <summary>
    /// �÷��̾� ������
    /// </summary>
    void Move()
    {
        if (state == State.Hurt)
        {
            StateCheck(state);
            return;
        }    
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(x, 0) * Time.deltaTime * data.Speed);

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

        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                isJump = true;
                state = State.Jump;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            }

        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRun = true;
            state = State.Run;
            transform.Translate(new Vector2(x, 0) * Time.deltaTime * data.RunSpeed);
            if (Input.GetKey(KeyCode.P))
            {
                state = State.RunAttack;
            }
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRun = false;
        }

        if (Input.GetKey(KeyCode.P))
        {
            if (state != State.Attack && state != State.WalkAttack)
            {
                if (x != 0)
                {
                    state = State.WalkAttack;
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

        if(isJump)
        {
            state = State.Jump;

        }
        
        StateCheck(state);
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
                    sa.SetSprite(attackSprites, 0.1f, false, Bullet);
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
                    sa.SetSprite(runAttackSprites, 0.2f, false, Bullet);
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
        PBullet pbullet = Instantiate(pBullet, firePos);
        if (transform.localScale.x < 0)
        {
            pbullet.isRight = false;
        }
        pbullet.transform.SetParent(null);
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
        //�����̰� ���� ��
        if(x!=0)
        {
            // ĳ������ ����
           
        }
        else
        {

        }

        /*
        // �������� �ʴ� ���
        else if(x==0)
        {
            // �����ϰ� ���� ��
            if(y!=0 && isJump == true)
            {
                sa.SetSprite(jumpSprites, 0.3f);
            }
            // �������� ��
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

        if(collision.gameObject.GetComponent<Jump>()&&rigid.velocity.y<0)
        {
            rigid.AddForce(new Vector2(0, 17), ForceMode2D.Impulse);
        }
    }
}
