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
    public Transform bombPos;
    [SerializeField] float dist;
    [SerializeField] Transform pBulletParent;
    public bool isLadder = false;
    // bool값 빼고 state로 다 처리
    [SerializeField] bool isBack = false;
    [SerializeField] float speed;
    public float jumpPower;
    [SerializeField] bool canFire = false;
    [SerializeField] float fireDelayTime = 0.8f;
    [SerializeField] float fireTimer;
    [SerializeField] UpDownGround moveFloor = null;
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
    public State state = State.Idle;
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
    
    /// <summary>
    /// 사다리에 붙음 처리 함수
    /// </summary>
    void LadderAttach()
    {
        if(isLadder)
        {
            if (ladder.GetComponent<LadderTop>())
            {
                if(Input.GetKeyDown(KeyCode.S))
                {
                    ladder.GetComponent<LadderTop>().LadderTopTrigger(true);
                    ladderAttach = true;
                    state = State.LadderIdle;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                {
                    ladderAttach = true;
                    state = State.LadderIdle;
                }
            }
            StateCheck(state);

        }
    }

    
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
            data.Coin += 10;
        }

        Move();
        LadderAttach();

        if (Input.GetKeyUp(KeyCode.C))
        {
            GetComponent<CapsuleCollider2D>().offset = originOffset;
            GetComponent<CapsuleCollider2D>().size = originSize;
        }

        // 총알 발사 시간 카운트
        fireTimer += Time.deltaTime;
        if (fireTimer > fireDelayTime)
        {
            fireTimer = 0;
            canFire = true;
        }

    }

    /// <summary>
    /// 플레이어 점프
    /// </summary>
    public void Jump()
    {
        if(GetComponent<Rigidbody2D>().gravityScale!=1)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        if(moveFloor!=null && moveFloor.isUp)
        {
            rigid.AddForce(Vector2.up * (jumpPower+moveFloor.speed), ForceMode2D.Impulse);
        }
        else
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
        }
        isLadder = false;
        ladderAttach = false;
    }

    
    /// <summary>
    /// 플레이어 움직임
    /// </summary>
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        switch (state)
        {
            case State.Hurt:
            case State.Dead:
                {
                    StateCheck(state);
                }
                return;
        }
        if (speed == 0)
        {
            speed = data.Speed;
        }
        #region 사다리 이동
        // 사다리에 붙은 상태
        if (ladderAttach)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            // 사다리에 매달려 있는 상태에서 점프키 누르면 점프로 변경
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            // y(종 이동)값이 0이 아닌 경우 : 위 아래로 이동중
            if (y != 0)
            {
                state = State.LadderMove;

                // 종으로 이동
                transform.Translate(new Vector2(0, y) * Time.deltaTime * speed);
            }
            else
            {
                state = State.LadderIdle;
            }
            StateCheck(state);
            return;
        }
        #endregion

        // 플레이어 기본 움직임(횡 이동)
        transform.Translate(new Vector2(x, 0) * Time.deltaTime * speed);

        #region 점프
        // 점프 Raycast
        RaycastHit2D hit = Physics2D.Raycast(foot.position, Vector3.down, 0.2f);
        Debug.DrawRay(foot.position, Vector3.down * 0.2f, Color.red);

        if(hit.collider == null)
        {
            state = State.Jump;
            if(x!=0)
            {
                transform.localScale = x > 0 ? Vector3.one * 5f : new Vector3(-5f, 5f, 5f);
            }
            StateCheck(state);
            return;
        }

        // Raycast의 반환(hit)이 Ground인 경우
        if(hit.collider.GetComponent<Ground>())
        {
            state = State.Idle;
            if(Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            state = State.Jump;
            StateCheck(state);
        }
        #endregion


        #region 횡 이동
        // x(횡 이동)값이 0이 아닌 경우 : 좌우로 이동할 때
        if (x != 0)
        {
            // 변수 = 조건 ? 참 : 거짓

            transform.localScale = x > 0 ? Vector3.one * 5f : new Vector3(-5f, 5f, 5f);
            
            if(state == State.Jump)
            {
                StateCheck(state);
                return;
            }
            
            // 플레이어 달리기
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = data.RunSpeed;
                state = State.Run;

                // 달리는 상태에서 총을 쏠 경우
                if (Input.GetKey(KeyCode.P))
                {
                    state = State.RunAttack;
                    Fire();
                }
            }
            else
            {
                state = State.Walk;
            }

            // 키에서 손을 때면 원래 속도로
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = data.Speed;
            }

        }
        else
        {
            if(state==State.Jump)
            {
                StateCheck(state);
                return;
            }
            state = State.Idle;
        }
        #endregion


        #region 키 이벤트
        // 앉기
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

            if (item.data.type.Equals(ItemType.Missile))
            {
                if(Input.GetKeyDown(KeyCode.O))
                {
                    item.data.fItem.Using();
                }
                if (Input.GetKey(KeyCode.O))
                {
                    item.data.fItem.GetComponent<MissileItem>().FireReady();
                }
                if(Input.GetKeyUp(KeyCode.O))
                {
                    item.data.fItem.GetComponent<MissileItem>().Fire();
                    Inventory.Instance.UseItem(item);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    Inventory.Instance.UseItem(item);
                }
            }


            /*
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
            */

        }
        

        StateCheck(state);
    }

    /// <summary>
    /// State에 따라 Sprite 바꾸는 함수
    /// </summary>
    /// <param name="state"></param>
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

    /// <summary>
    /// 특정 행동 후 Idle 상태로 바꾸는 함수
    /// </summary>
    void IdleSprite()
    {
        sa.SetSprite(idleSprites, 0.2f);
        state = State.Idle;
    }

    /// <summary>
    /// 총알(IdleState)
    /// </summary>
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

    /// <summary>
    /// 총알(움직일 경우)
    /// </summary>
    /// <param name="state"></param>
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
    
    /// <summary>
    /// 총알 발사
    /// </summary>
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

    /// <summary>
    /// 플레이어 피격 처리 함수
    /// </summary>
    /// <param name="damage"></param>
    void PlayerHit(int damage)
    {
        state = State.Hurt;
        data.HP -= damage;
        if (data.HP <= 0)
        {
            Dead();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<MoveGround>())
        {
            transform.SetParent(collision.transform);
            //transform.localScale = new Vector3(5, 5, 1);
        }

        if (collision.gameObject.GetComponent<UpDownGround>())
        {
            moveFloor = collision.gameObject.GetComponent<UpDownGround>();
            transform.SetParent(collision.transform);

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

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        
        if (enemy)
        {
            PlayerHit(enemy.data.AttackPower);
        }

        FallSnowBall snowBall = collision.gameObject.GetComponent<FallSnowBall>();

        if (snowBall)
        {
            if (!snowBall.isEnd && state!=State.Hurt)
            {
                PlayerHit(snowBall.damage);
            }
            else
            {
                return;
            }
        }
    }

    //https://blog.naver.com/yoohee2018/221187229189
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MoveGround>())
        {
            if(Input.GetButtonDown("Jump"))
            {
                
                transform.SetParent(null);
            }
        }

        if (collision.gameObject.GetComponent<UpDownGround>())
        {
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
            transform.SetParent(null);
        }

        if (collision.gameObject.GetComponent<UpDownGround>())
        {
            moveFloor = null;
            rigid.bodyType = RigidbodyType2D.Dynamic;
            transform.SetParent(null);
        }

        if(collision.gameObject.GetComponent<LadderTop>())
        {
            if (ladder == null)
            {
                return;
            }
            ladder = null;
            isLadder = false;
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

        if (collision.GetComponent<Ladder>())
        {
            isLadder = true;
            ladder = collision.gameObject;
        }

        EBullet bullet = collision.gameObject.GetComponent<EBullet>();
        if (bullet)
        {
            PlayerHit(bullet.damage);
            Pooling.Instance.SetPool(DicKey.eBullet, bullet.gameObject);
        }

        EBullet2 bullet2 = collision.gameObject.GetComponent<EBullet2>();
        if (bullet2)
        {
            PlayerHit(bullet2.damage);
            Pooling.Instance.SetPool(DicKey.eBullet2, bullet2.gameObject);
        }

        if (collision.CompareTag("Boss1Atk"))
        {
            Boss1 boss = collision.transform.parent.GetComponent<Boss1>();
            if (boss.state == EnemyBoss.BossState.Attack1)
            {
                PlayerHit(boss.data.Atk1Power);
            }
            else if (boss.state == EnemyBoss.BossState.Attack2)
            {
                PlayerHit(boss.data.Atk2Power);
            }
            else
            {
                return;
            }
        }

        Boss2Bullet boss2Bullet = collision.gameObject.GetComponent<Boss2Bullet>();
        if (boss2Bullet)
        {
            PlayerHit(boss2Bullet.damage);
            Pooling.Instance.SetPool(DicKey.boss2Bullet, boss2Bullet.gameObject);
        }

        Boss3Bullet boss3Bullet = collision.gameObject.GetComponent<Boss3Bullet>();
        if (boss3Bullet)
        {
            PlayerHit(boss3Bullet.damage);
            Pooling.Instance.SetPool(DicKey.boss3Bullet, boss3Bullet.gameObject);
        }

        Boss3Bullet2 boss3Bullet2 = collision.gameObject.GetComponent<Boss3Bullet2>();
        if (boss3Bullet2)
        {
            PlayerHit(boss3Bullet2.damage);
        }

        FallObject fallObj = collision.gameObject.GetComponent<FallObject>();
        if (fallObj)
        {
            if (!fallObj.isAttack)
            {
                fallObj.isAttack = true;
                PlayerHit(fallObj.damage);
            }
        }

        if(collision.CompareTag("Boss2Punch"))
        {
            Boss2 boss = collision.transform.parent.GetComponent<Boss2>();
            if(boss.state.Equals(EnemyBoss.BossState.Attack1))
            {
                PlayerHit(boss.data.Atk1Power);
            }
        }

        FireBall fireBall = collision.gameObject.GetComponent<FireBall>();
        if (fireBall)
        {
            PlayerHit(fireBall.damage);
        }

        if(collision.GetComponent<ThornFloor>())
        {
            ThornFloor thornFloor = collision.GetComponent<ThornFloor>();
            PlayerHit(thornFloor.Damage);
        }

        if(collision.CompareTag("BombArea"))
        {
            Bomb bomb = collision.transform.parent.GetComponent<Bomb>();
            PlayerHit(bomb.damage);
        }

        if(collision.CompareTag("SnowArea"))
        {
            originSpeed = data.Speed;
            originRunSpeed = data.RunSpeed;

            speed /= 2;
            data.Speed /= 2;
            data.RunSpeed /= 2;
        }

        FireBar fireBar = collision.GetComponent<FireBar>();
        if (fireBar)
        {
            PlayerHit(fireBar.damage);
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ladder>())
        {
            if (ladder == null)
            {
                return;
            }
            if (ladder.GetComponent<LadderTop>())
            {
                return;
            }
            isLadder = false;
            ladderAttach = false;
            ladder = null;
            state = State.Idle;
            GetComponent<Rigidbody2D>().gravityScale = 1;

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
        GameUI.Instance.GameOver();
    }


    float deg;
    float turretSpeed = 50f;
    float circleR = 0.4f;
    bool isUp = true;
    [SerializeField] GameObject turret;
    void Missile()
    {
        if (Input.GetKey(KeyCode.K))
        {
            if(isUp)
            {
                deg = deg + Time.deltaTime * turretSpeed;

                if(turret.transform.rotation == Quaternion.Euler(0,0,90f))
                {
                    isUp = false;
                }
            }
            else
            {
                deg = deg - Time.deltaTime * turretSpeed;
                if (turret.transform.rotation == Quaternion.Euler(0,0,0))
                {
                    isUp = true;
                }
            }
            
            float rad = deg * Mathf.Deg2Rad;
            float x = circleR * Mathf.Cos(rad);
            float y = circleR * Mathf.Sin(rad);
            turret.transform.localPosition = new Vector2(x, y);
            turret.transform.eulerAngles = new Vector3(0, 0, deg);

        }

    }


}
