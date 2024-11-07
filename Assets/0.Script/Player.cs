using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PlayerData data;
    public Inventory inventory;

    [SerializeField] private Rigidbody2D rigid;
    public Transform firePos;
    [SerializeField] PBullet pBullet;
    [SerializeField] GameObject rotateCore;
    [SerializeField] Transform missilePos;
    [SerializeField] Missile missile;
    [SerializeField] Transform foot;
    [SerializeField] float dist;
    [SerializeField] Transform pBulletParent;
    public bool isRun = false;
    public bool isJump = false;
    public bool isAttacking = false;
    public bool isHurt = false;
    public bool isLadder = false;
    [SerializeField] bool isBack = false;
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
        LadderMove,
        LadderIdle,
        BombThrow,
        BombThrowWalk,
        BombThrowRun,
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
    private List<Sprite> deadSprites;
    private List<Sprite> updownSprites;
    private List<Sprite> ladderIdleSprites;
    private List<Sprite> bombSprites;

    [SerializeField] bool isJumpRun = false;
    [SerializeField] State state = State.Idle;
    [SerializeField] Transform core;
    public Transform skill1;
    public Transform skill2;
    public GameObject skillPos;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameManager.Instance.Inven;
        data = GameManager.Instance.PlayerData;
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
        deadSprites = pSprite.deadSprites;
        updownSprites = pSprite.updownSprites;
        ladderIdleSprites = pSprite.ladderIdleSprites;
        bombSprites = pSprite.bombSprites;

        sa.SetSprite(idleSprites, 0.2f);
        state = State.Idle;
        rigid = GetComponent<Rigidbody2D>();
        speed = data.Speed;
    }

    [SerializeField] bool ladderAttach = false;
    [SerializeField] GameObject ladder = null;
    void LadderAttach()
    {
        if(isLadder)
        {
            if(ladder.GetComponent<LadderTop>())
            {
                if(Input.GetKeyDown(KeyCode.S))
                {
                    ladder.GetComponent<LadderTop>().LadderTopTrigger(true);
                    ladderAttach = true;
                    isJump = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                {
                    ladderAttach = true;
                    isJump = false;
                }
            }

        }
    }

    [SerializeField] bool ladderMoveTrue = false;
    // Update is called once per frame
    void Update()
    {
        if(inventory==null)
        {
            inventory = GameManager.Instance.Inven;
            return;
        }
        //치트
        if(Input.GetKeyDown(KeyCode.F4))
        {
            data.Level++;
        }

        Move();
        LadderAttach();
        RaycastHit2D hit = Physics2D.Raycast(foot.position, Vector3.down);
        Debug.DrawRay(foot.position, Vector3.down, Color.red);
        /*
        if(ladderAttach)
        {
            if(Input.GetAxisRaw("Vertical")!=0)
            {
                ladderMoveTrue = true;
            }
            else
            {
                ladderMoveTrue = false;
            }
        }
        if(isLadder)
        {
            if(Input.GetAxisRaw("Vertical")!=0)
            {
                ladderMoveTrue = true;
            }
            else
            {
                state = State.LadderIdle;
                ladderMoveTrue = false;
            }
        }
        */
        /*
        if(hit.collider.GetComponent<Ground>())
        {
            dist = hit.distance;
        }
        
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
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (speed == 0)
        {
            speed = data.Speed;
        }

        if (state == State.Hurt)
        {
            StateCheck(state);
            return;
        }

        if (state == State.Dead)
        {
            StateCheck(state);
            return;
        }

        if(ladderAttach)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (y!=0)
            {
                state = State.LadderMove;
                transform.Translate(new Vector2(0, y) * Time.deltaTime * speed);
            }
            else
            {
                state = State.LadderIdle;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (!isJump)
                {
                    state = State.Jump;

                    GetComponent<Rigidbody2D>().gravityScale = 1;
                    rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
                    transform.Translate(new Vector2(x, 0) * Time.deltaTime * speed);
                    isJump = true;
                    isLadder = false;
                    ladderAttach = false;
                }
                isRun = false;
            }
            /*
            if (!ladderMoveTrue)
            {
                state = State.LadderIdle;
            }
            else if(ladderMoveTrue)
            {
                if (y != 0)
                {
                    state = State.LadderMove;
                    transform.Translate(new Vector2(0, y) * Time.deltaTime * speed);
                }
                else
                {
                    state = State.LadderIdle;
                }
            }
            */

            StateCheck(state);
            return;
        }

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

        // 아이템 사용하는 코드
        if(inventory.quickSlot.GetComponent<QuickSlot>().isFilled)
        {
            InvenItem item = inventory.quickSlot.transform.GetChild(0).GetComponent<QuickInven>().invenItem;
            int itemNum = item.data.itemNumber;

            switch (itemNum) // 아이템 코드
            {
                case 2: // 아이템 넘버코드가 2번(미사일일 경우)
                    {
                        if (item.data.count > 0)
                        {
                            if (Input.GetKeyDown(KeyCode.O))
                            {
                                rotateCore.SetActive(true);
                            }


                            if (Input.GetKey(KeyCode.O))
                            {
                                if(x!=0)
                                {
                                    if(isRun)
                                    {
                                        state = State.BombThrowRun;
                                    }
                                    else
                                    {
                                        state = State.BombThrowWalk;
                                    }
                                }
                                else
                                {
                                    state = State.BombThrow;
                                }
                                if (transform.localScale.x < 0)
                                {
                                    if (core.rotation == Quaternion.Euler(new Vector3(0, 0, -90)))
                                    {
                                        isBack = false;
                                    }
                                    else if (core.rotation == Quaternion.Euler(new Vector3(0, 0, 0)))
                                    {
                                        isBack = true;
                                    }

                                    if (isBack == true)
                                    {
                                        core.Rotate(Vector3.forward * Time.deltaTime * 100f);
                                    }
                                    else
                                    {
                                        core.Rotate(Vector3.back * Time.deltaTime * 100f);
                                    }
                                }
                                else
                                {
                                    if (core.rotation == Quaternion.Euler(new Vector3(0, 0, 90)))
                                    {
                                        isBack = true;

                                    }
                                    else if (core.rotation.z < 0)
                                    {
                                        isBack = false;
                                    }

                                    if (isBack == true)
                                    {
                                        core.Rotate(Vector3.back * Time.deltaTime * 100f);
                                    }
                                    else
                                    {
                                        core.Rotate(Vector3.forward * Time.deltaTime * 100f);
                                    }

                                }

                            }
                            if (Input.GetKeyUp(KeyCode.O))
                            {
                                Inventory.Instance.UseItem(item);
                                state = State.Idle;
                                float bulletSpeed = 2f;
                                float power = 3;
                                Vector2 dir = rotateCore.transform.rotation * new Vector2(bulletSpeed, 0) * power;
                                GameObject obj = Pooling.Instance.GetPool(DicKey.pMissile, missilePos, rotateCore.transform.rotation).gameObject;
                                if (transform.localScale.x < 0)
                                {
                                    obj.GetComponent<Missile>().Shoot(-dir);
                                }
                                else
                                {
                                    obj.GetComponent<Missile>().Shoot(dir);
                                }

                                rotateCore.transform.rotation = Quaternion.identity;
                                rotateCore.SetActive(false);

                            }
                        }
                        break;
                    }
                case 3: // 아이템 넘버코드가 3번(회복약)
                    {
                        int plusHP = item.data.usage;
                        if(Input.GetKeyDown(KeyCode.O))
                        {
                            if(data.HP == data.MAXHP)
                            {
                                Debug.Log("이미 체력이 가득 찼습니다");
                                return;
                            }
                            else
                            {
                                if(data.MAXHP - data.HP >= plusHP)
                                {
                                    data.HP += plusHP;
                                }
                                else if(data.MAXHP - data.HP < plusHP)
                                {
                                    data.HP = data.MAXHP;
                                }
                            }
                            Debug.Log($"{data.HP}");
                            Inventory.Instance.UseItem(item);
                        }
                        break;
                    }
            }

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
            case State.BombThrow:
                {
                    sa.SetSprite(bombSprites, 0.2f);
                    break;
                }
            case State.BombThrowWalk:
                {
                    sa.SetSprite(runAttackSprites, 0.2f);
                }
                break;
            case State.BombThrowRun:
                {
                    sa.SetSprite(runAttackSprites, 0.1f);
                }
                break;
            case State.Dead:
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 90f);
                    sa.SetSprite(deadSprites, 0.2f);
                    break;
                }
            case State.LadderMove:
                {
                    sa.SetSprite(updownSprites, 0.2f);
                    break;
                }
            case State.LadderIdle:
                {
                    sa.SetSprite(ladderIdleSprites, 0.2f);
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
        if(collision.gameObject.GetComponent<Ground>())
        {
            isJump = false;
        }

        if(collision.gameObject.GetComponent<MoveGround>())
        {
            isJump = false;
            transform.SetParent(collision.transform);
            //transform.localScale = new Vector3(5, 5, 1);
        }

        if (collision.gameObject.GetComponent<LadderTop>())
        {
            if (ladder == null)
            {
                ladder = collision.gameObject;
                isLadder = true;
            }
            else if (!ladder.GetComponent<Ladder>())
            {
                ladder = collision.gameObject;
                isLadder = true;
            }
            else if (ladder.GetComponent<Ladder>())
            {
                ladder = collision.gameObject;
                isLadder = true;
                collision.gameObject.GetComponent<LadderTop>().LadderTopTrigger(true);
            }
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<MoveGround>())
        {
            transform.SetParent(null);
        }

        
    }

    [SerializeField] Transform ladderTop;
    [SerializeField] Transform ladderBtm;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DeadZone"))
        {
            Dead();
        }
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
                Dead();
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
                Dead();
            }
            Pooling.Instance.SetPool(DicKey.eBullet2, collision.GetComponent<EBullet2>().gameObject);
        }

        if(collision.gameObject.GetComponent<FallObject>())
        {
            if (!collision.GetComponent<FallObject>().isAttack)
            {
                if (data.HP > collision.GetComponent<FallObject>().damage)
                {
                    collision.GetComponent<FallObject>().isAttack = true;
                    isHurt = true;
                    data.HP -= collision.GetComponent<FallObject>().damage;
                    state = State.Hurt;
                }
                else if (data.HP <= collision.GetComponent<FallObject>().damage)
                {
                    Dead();
                }
            }
            else
            {
                return;
            }
        }

        if(collision.GetComponent<Ladder>())
        {
            isLadder = true;
            ladder = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladders"))
        {
            ladderMoveTrue = true;
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ladder>())
        {
            if(ladder.GetComponent<LadderTop>())
            {
                return;
            }
            else
            {
                isLadder = false;
                ladderMoveTrue = false;
                ladderAttach = false;
                ladder = null;
                state = State.Idle;
                GetComponent<Rigidbody2D>().gravityScale = 1;
            }

        }

        if(collision.GetComponent<LadderTop>())
        {
            collision.GetComponent<LadderTop>().LadderTopTrigger(false);
            if (ladder.GetComponent<LadderTop>())
            {
                isLadder = false;
                ladderMoveTrue = false;
                ladderAttach = false;
                state = State.Idle;
                GetComponent<Rigidbody2D>().gravityScale = 1;
                ladder = null;
            }
            else
            {
                return;
            }



        }

    }


    void Dead()
    {
        data.HP = 0;
        state = State.Dead;
        GameUI.Instance.gameOverUI.SetActive(true);
    }
}
