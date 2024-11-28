using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PlayerData data;
    public Inventory inventory;

    [SerializeField] private Rigidbody2D rigid;
    public Transform firePos;
    public Transform sitFirePos;
    [SerializeField] PBullet pBullet;
    [SerializeField] GameObject rotateCore;
    [SerializeField] Transform missilePos;
    [SerializeField] Missile missile;
    [SerializeField] Transform foot;
    [SerializeField] Transform bombPos;
    [SerializeField] float dist;
    [SerializeField] Transform pBulletParent;
    public bool isRun = false;
    public bool isJump = false;
    public bool isAttacking = false;
    public bool isHurt = false;
    public bool isLadder = false;
    // bool값 빼고 state로 다 처리
    [SerializeField] bool isBack = false;
    [SerializeField] float speed;
    public float jumpPower;
    [SerializeField] bool canFire = false;
    [SerializeField] float fireDelayTime = 0.8f;
    [SerializeField] float fireTimer;

    [SerializeField] bool onMoveFloor = false;
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
        SitAttack,
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
    private List<Sprite> sitAttackSprites;
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

    [SerializeField] float originSpeed;
    [SerializeField] float originRunSpeed;
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
        sitAttackSprites = pSprite.sitAttackSprites;
        deadSprites = pSprite.deadSprites;
        updownSprites = pSprite.updownSprites;
        ladderIdleSprites = pSprite.ladderIdleSprites;
        bombSprites = pSprite.bombSprites;

        sa.SetSprite(idleSprites, 0.2f);
        state = State.Idle;
        rigid = GetComponent<Rigidbody2D>();
        speed = data.Speed;
        
     

        originOffset = GetComponent<CapsuleCollider2D>().offset;
        originSize = GetComponent<CapsuleCollider2D>().size;
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
        RaycastHit2D hit = Physics2D.Raycast(foot.position, Vector3.down*0.2f);
        Debug.DrawRay(foot.position, Vector3.down*0.2f, Color.red);

        if(hit.collider != null)
        {
            //점프 안되게
        }

        if(Input.GetKeyUp(KeyCode.C))
        {
            GetComponent<CapsuleCollider2D>().offset = originOffset;
            GetComponent<CapsuleCollider2D>().size = originSize;
        }

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

        switch(state)
        {
            case State.Hurt:
            case State.Dead:
                {
                    StateCheck(state);
                }
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
            StateCheck(state);
            return;
        }

        if (x != 0)
        {
            // 변수 = 조건 ? 참 : 거짓

            transform.localScale = x > 0 ? Vector3.one * 5f : new Vector3(-5f, 5f, 5f);
            state = State.Walk;
        }
        else
        {
            state = State.Idle;
        }

        #region 키 이벤트
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

        if (Input.GetKey(KeyCode.C))
        {
            state = State.Sit;
            Sit();
            if (Input.GetKey(KeyCode.P))
            {
                state = State.SitAttack;
                Fire();
            }
        }

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
        
        // 총알 발사
        if (Input.GetKey(KeyCode.P))
        {
            if (state != State.Attack && state != State.WalkAttack && state!=State.SitAttack)
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

        }
        #endregion

        // 아이템 사용하는 코드
        if (inventory.quickSlot.GetComponent<QuickSlot>().isFilled)
        {
            InvenItem item = inventory.quickSlot.transform.GetChild(0).GetComponent<QuickInven>().invenItem;
            int itemNum = item.data.itemNumber;
            // itemNum 대신 enum값으로 변경
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
                                // 미사일 쏠 때 sin cos 사용해서 바꿔보기..
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
                                float power = 5;
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
                case 5: // 아이템 넘버코드 5번(폭탄)
                    {
                        if(Input.GetKeyDown(KeyCode.O))
                        {
                            if(item.data.count>0)
                            {
                                Inventory.Instance.UseItem(item);
                                GameObject obj = Pooling.Instance.GetPool(DicKey.bomb, bombPos);
                                obj.transform.SetParent(null);
                            }
                        }
                    }
                    break;
            }

        }
        

        if (isJump)
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
            case State.Sit:
                {
                    sa.SetSprite(sitSprites, 0.1f, false);
                    break;
                }
            case State.SitAttack:
                {
                    sa.SetSprite(sitAttackSprites, 0.1f);
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
    void IdleSprite()
    {
        sa.SetSprite(idleSprites, 0.2f);
        state = State.Idle;
        if (isAttacking == true)
        {
            isAttacking = false;
        }
        if (isHurt == true)
        {
            isHurt = false;
        }
    }

    void Bullet()
    {
        PBullet pbullet = Pooling.Instance.GetPool(DicKey.pBullet, firePos).GetComponent<PBullet>();
        if (pbullet == null)
        {
            return;
        }
        if (transform.localScale.x < 0)
        {
            pbullet.isRight = false;
        }
        pbullet.transform.SetParent(pBulletParent);
    }

    
    void Bullet(State state)
    {
        PBullet pbullet = null;
        Transform pos = (state.Equals(State.WalkAttack) || state.Equals(State.RunAttack)) ? firePos :
                        (state.Equals(State.SitAttack)) ? sitFirePos : null;

        if(pos!=null)
        {
            pbullet = Pooling.Instance.GetPool(DicKey.pBullet, pos).GetComponent<PBullet>();
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
            if(!canFire)
            {
                return;
            }
            Bullet();
            fireTimer = 0;
            canFire = false;
        }

        
        if(state == State.SitAttack)
        {
            if(canFire == false)
            {
                return;
            }
            Bullet(state);
            fireTimer = 0;
            canFire = false;
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>()) // 태그로 처리
        {
            isJump = false;
        }

        if(collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
        
        if (collision.gameObject.GetComponent<MoveGround>())
        {
            isJump = false;
            onMoveFloor = true;
            transform.SetParent(collision.transform);
            //transform.localScale = new Vector3(5, 5, 1);
        }

        if (collision.gameObject.GetComponent<UpDownGround>())
        {
            isJump = false;
            onMoveFloor = true;
            transform.SetParent(collision.transform);
            // y값 따라가게 하고
            //transform.localScale = new Vector3(5, 5, 1);
        }

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        
        if (enemy)
        {
            if (data.HP > enemy.data.AttackPower)
            {
                isHurt = true;
                data.HP -= enemy.data.AttackPower;
                Debug.Log($"{data.HP}");
                state = State.Hurt;

            }
            else if (data.HP <= enemy.data.AttackPower)
            {
                Dead();
            }

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

        if(collision.gameObject.GetComponent<FallSnowBall>())
        {
            if (!collision.gameObject.GetComponent<FallSnowBall>().isEnd && state!=State.Hurt)
            {
                if(data.HP>collision.gameObject.GetComponent<FallSnowBall>().damage)
                {
                    isHurt = true;
                    data.HP -= collision.gameObject.GetComponent<FallSnowBall>().damage;
                    state = State.Hurt;
                }
                else if(data.HP<=collision.gameObject.GetComponent<FallSnowBall>().damage)
                {
                    Dead();
                }
            }
            else
            {
                return;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MoveGround>())
        {
            if(Input.GetButtonDown("Jump"))
            {
                //rigid.velocity = Vector2.zero;
                transform.SetParent(null);
            }
        }

        if (collision.gameObject.GetComponent<UpDownGround>())
        {
            /*
            Vector3 pos = collision.gameObject.GetComponent<UpDownGround>().transform.position;
            pos.y -= 3;
            transform.position = pos;
            */
            if (Input.GetButtonDown("Jump"))
            {
                transform.SetParent(null);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<MoveGround>())
        {
            onMoveFloor = false;
            transform.SetParent(null);
        }

        if (collision.gameObject.GetComponent<UpDownGround>())
        {
            onMoveFloor = false;
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

        if(collision.CompareTag("Lava"))
        {
            Dead();
        }
        

        if (collision.gameObject.GetComponent<Jump>() && rigid.velocity.y < 0)
        {
            rigid.AddForce(new Vector2(0, 17), ForceMode2D.Impulse);
        }

        if(collision.gameObject.GetComponent<FireButton>() && rigid.velocity.y <0)
        {
            if(collision.gameObject.GetComponent<FireButton>().isOn)
            {
                return;
            }
            collision.gameObject.GetComponent<FireButton>().isOn = true;
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

        if (collision.gameObject.GetComponent<EBullet>())
        {
            if (data.HP > collision.GetComponent<EBullet>().damage)
            {
                isHurt = true;
                data.HP -= collision.GetComponent<EBullet>().damage;
                state = State.Hurt;
            }
            else if (data.HP <= collision.GetComponent<EBullet>().damage)
            {
                Dead();
            }
            Pooling.Instance.SetPool(DicKey.eBullet, collision.GetComponent<EBullet>().gameObject);
        }

        if (collision.gameObject.GetComponent<Boss2Bullet>())
        {
            if (data.HP > collision.GetComponent<Boss2Bullet>().damage)
            {
                isHurt = true;
                data.HP -= collision.GetComponent<Boss2Bullet>().damage;
                state = State.Hurt;
            }
            else if (data.HP <= collision.GetComponent<Boss2Bullet>().damage)
            {
                Dead();
            }
            Pooling.Instance.SetPool(DicKey.boss2Bullet, collision.GetComponent<Boss2Bullet>().gameObject);
        }


        if (collision.gameObject.GetComponent<Boss3Bullet>())
        {
            if (data.HP > collision.GetComponent<Boss3Bullet>().damage)
            {
                isHurt = true;
                data.HP -= collision.GetComponent<Boss3Bullet>().damage;
                state = State.Hurt;
            }
            else if (data.HP <= collision.GetComponent<Boss3Bullet>().damage)
            {
                Dead();
            }
            Pooling.Instance.SetPool(DicKey.boss3Bullet, collision.GetComponent<Boss3Bullet>().gameObject);
        }

        if (collision.gameObject.GetComponent<Boss3Bullet2>())
        {
            if (data.HP > collision.GetComponent<Boss3Bullet2>().damage)
            {
                isHurt = true;
                data.HP -= collision.GetComponent<Boss3Bullet2>().damage;
                state = State.Hurt;
            }
            else if (data.HP <= collision.GetComponent<Boss3Bullet2>().damage)
            {
                Dead();
            }
            //Pooling.Instance.SetPool(DicKey.boss3Bullet, collision.GetComponent<Boss3Bullet>().gameObject);
        }

        if (collision.gameObject.GetComponent<FallObject>())
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

        if(collision.CompareTag("Boss2Punch"))
        {
            Debug.Log("인식");
            if(collision.transform.parent.GetComponent<Boss2>().state.Equals(EnemyBoss.BossState.Attack1))
            {
                isHurt = true;
                state = State.Hurt;
                data.HP -= collision.transform.parent.GetComponent<Boss2>().data.Atk1Power;
                if(data.HP<=0)
                {
                    Dead();
                }
            }
        }

        if(collision.gameObject.GetComponent<FireBall>())
        {
            if(data.HP>collision.GetComponent<FireBall>().damage)
            {
                isHurt = true;
                data.HP -= collision.GetComponent<FireBall>().damage;
                state = State.Hurt;
            }
            else if (data.HP <= collision.GetComponent<FireBall>().damage)
            {
                Dead();
            }
        }

        if(collision.GetComponent<Ladder>())
        {
            isLadder = true;
            ladder = collision.gameObject;
        }

        if (collision.CompareTag("Boss1Atk"))
        {
            if(collision.transform.parent.GetComponent<Boss1>().state == EnemyBoss.BossState.Attack1)
            {
                isHurt = true;
                state = State.Hurt;
                data.HP -= collision.transform.parent.GetComponent<EnemyBoss>().data.Atk1Power;
                if(data.HP<=0)
                {
                    Dead();
                }
            }

            else if (collision.transform.parent.GetComponent<Boss1>().state == EnemyBoss.BossState.Attack2)
            {
                isHurt = true;
                state = State.Hurt;
                data.HP -= collision.transform.parent.GetComponent<Boss1>().data.Atk2Power;
                if (data.HP <= 0)
                {
                    Dead();
                }
            }
            else
            {
                return;
            }
        }

        if(collision.GetComponent<ThornFloor>())
        {
            isHurt = true;
            state = State.Hurt;
            data.HP -= collision.GetComponent<ThornFloor>().Damage;
            if(data.HP<=0)
            {
                Dead();
            }
        }

        if(collision.CompareTag("BombArea"))
        {
            isHurt = true;
            state = State.Hurt;
            data.HP -= collision.transform.parent.GetComponent<Bomb>().damage;
            if(data.HP<=0)
            {
                Dead();
            }
        }

        if(collision.CompareTag("SnowArea"))
        {
            originSpeed = data.Speed;
            originRunSpeed = data.RunSpeed;

            speed /= 2;
            data.Speed /= 2;
            data.RunSpeed /= 2;
        }

        if(collision.GetComponent<FireBar>())
        {
            isHurt = true;
            state = State.Hurt;
            data.HP -= collision.GetComponent<FireBar>().damage;
            if(data.HP<=0)
            {
                Dead();
            }
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
            if(ladder == null)
            {
                return;
            }
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
        }

        if(collision.CompareTag("SnowArea"))
        {
            speed = originSpeed;
            data.Speed = originSpeed;
            data.RunSpeed = originRunSpeed;
        }

    }

    [SerializeField] Vector2 originOffset;
    [SerializeField] Vector2 originSize;
   
    void Sit()
    {
        GetComponent<CapsuleCollider2D>().offset = new Vector2(originOffset.x, -0.07884782f);
        GetComponent<CapsuleCollider2D>().size = new Vector2(originSize.x, 0.2223044f);
    }


    void Dead()
    {
        data.HP = 0;
        state = State.Dead;
        GameUI.Instance.gameOverUI.SetActive(true);
    }
}
