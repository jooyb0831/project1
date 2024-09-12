using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float runSpeed = 7f;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] Transform firePos;
    [SerializeField] PBullet pBullet;
    public float jumpPower;
    public bool isJump = false;
    public bool isAttacking = false;
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
        Hit,
        Dead
    }

    private SpriteAnimation sa;

    private List<Sprite> idleSprites;
    private List<Sprite> walkSprites;
    private List<Sprite> jumpSprites;
    private List<Sprite> attackSprites;
    private List<Sprite> runAttackSprites;
    private List<Sprite> slideSprites;
    private List<Sprite> sitSprites;

    [SerializeField] State state = State.Idle;
    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        SpriteManager.PlayerSprite pSprite = SpriteManager.Instance.playerSprite[0];
        idleSprites = pSprite.idleSprites;
        walkSprites = pSprite.walkSprites;
        jumpSprites = pSprite.jumpSprites;
        attackSprites = pSprite.attackSprites;
        runAttackSprites = pSprite.runAttackSprites;
        slideSprites = pSprite.slideSprites;
        sitSprites = pSprite.sitSprites;

        sa.SetSprite(idleSprites, 0.2f);
        state = State.Idle;
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }
    
    /// <summary>
    /// 플레이어 움직임
    /// </summary>
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = transform.position.y;
        transform.Translate(new Vector2(x, 0) * Time.deltaTime * speed);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            state = State.Run;
            transform.Translate(new Vector2(x, 0) * Time.deltaTime * runSpeed);

        }
        if (Input.GetButtonDown("Jump"))
        {
            if (isJump == false)
            {
                isJump = true;
                state = State.Jump;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
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
        isAttacking = false;
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
            if (state != State.Idle && isAttacking == false)
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
}
