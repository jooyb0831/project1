using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData data;
    public Inventory inventory;
    private Pooling pooling;
    private Rigidbody2D rigid;

    public Transform firePos;
    public Transform sitFirePos;
    public Transform bombPos;
    [SerializeField] Transform foot;

    [SerializeField] Transform pBulletParent;
   
    private float speed;
    public float jumpPower;
    float originSpeed;
    float originRunSpeed;

    public bool isLadder = false;
    bool ladderAttach = false;
    GameObject ladder = null;

    public Transform skill1;
    public Transform skill2;
    public GameObject skillPos;

    bool canFire = false;
    [SerializeField] float fireDelayTime = 0.8f;
    float fireTimer;
    private UpDownGround moveFloor = null;

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

    public State state = State.Idle;



    void Start()
    {
        inventory = GameManager.Instance.Inven;
        data = GameManager.Instance.PlayerData;
        pooling = GameManager.Instance.Pooling;

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

        if (hit.collider == null)
        {
            state = State.Jump;
            if (x != 0)
            {
                transform.localScale = x > 0 ? Vector3.one * 5f : new Vector3(-5f, 5f, 5f);
            }
            StateCheck(state);
            return;
        }

        // Raycast의 반환(hit)이 Ground인 경우
        if (hit.collider.GetComponent<Ground>())
        {
            state = State.Idle;
            if (Input.GetButtonDown("Jump"))
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

        // 기본공격(총 발사)
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

        #region 아이템
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
        }
        #endregion

        #endregion

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
        //Pool에서 총알 가져오기
        PBullet pbullet = pooling.GetPool(DicKey.pBullet, firePos).GetComponent<PBullet>();
        
        //총알이 없으면 리턴
        if (pbullet == null) return;
        //플레이어 방향에 따른 발사 방향 처리
        if (transform.localScale.x < 0)
        {
            pbullet.isRight = false;
        }
        pbullet.transform.SetParent(pBulletParent);
    }

    /// <summary>
    /// 총알
    /// </summary>
    /// <param name="state"></param>
    void Bullet(State state)
    {
        PBullet pbullet = null;
        //총알 발사 위치 Transform 받기
        //서서 공격 시 firePos, 앉아서 공격시 sitFirePos로 세팅
        Transform pos = (state.Equals(State.Attack) || state.Equals(State.WalkAttack) 
                        || state.Equals(State.RunAttack)) ? firePos :
                        (state.Equals(State.SitAttack)) ? sitFirePos : null;

        //발사 위치가 있다면
        if(pos != null)
        {
            //총알 Pool에서 꺼내서 발사처리
            pbullet = pooling.GetPool(DicKey.pBullet, pos).GetComponent<PBullet>();
        }
        //플레이어 방향에 따른 발사 방향 처리
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
        //발사할 수 없는 상태면 리턴 
        if (!canFire) return;
        //총알 처리
        Bullet(state);
        //발사시간 초기화 
        fireTimer = 0;
        //발사 불가능한 상태로 체크
        canFire = false;
        
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MoveGround>() || collision.gameObject.GetComponent<UpDownGround>())
        {
            if(Input.GetButtonDown("Jump"))
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
       
        if(collision.CompareTag("DeadZone") || collision.CompareTag("Lava"))
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
            pooling.SetPool(DicKey.eBullet, bullet.gameObject);
        }

        EBullet2 bullet2 = collision.gameObject.GetComponent<EBullet2>();
        if (bullet2)
        {
            PlayerHit(bullet2.damage);
            pooling.SetPool(DicKey.eBullet2, bullet2.gameObject);
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
            pooling.SetPool(DicKey.boss2Bullet, boss2Bullet.gameObject);
        }

        Boss3Bullet boss3Bullet = collision.gameObject.GetComponent<Boss3Bullet>();
        if (boss3Bullet)
        {
            PlayerHit(boss3Bullet.damage);
            pooling.SetPool(DicKey.boss3Bullet, boss3Bullet.gameObject);
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

    Vector2 originOffset;
    Vector2 originSize;
   
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
}
