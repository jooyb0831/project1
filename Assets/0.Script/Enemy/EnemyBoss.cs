using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public class BossData
    {
        public int HP { get; set; }
        public int EXP { get; set; }
        public int Atk1Power { get; set; }
        public int Atk2Power { get; set; }
        public float Speed { get; set; }
        public int Index { get; set; }
    }

    public enum BossState
    {
        Idle,
        Move,
        Hit,
        Attack1,
        Attack2,
        Back,
        Dead
    }

    [SerializeField] protected Player p;
    SpriteRenderer sr;
    protected SpriteAnimation sa;
    public BossData data = new BossData();
    protected SkillSystem sksystem;

    protected List<Sprite> idleSprites;
    protected List<Sprite> hitSprites;
    protected List<Sprite> deadSprite;
    protected List<Sprite> attack1Sprite;
    protected List<Sprite> attack2Sprite;

    [SerializeField] public BossState state = BossState.Idle;
    protected bool isDead = false;
    private PlayerData pd;
    [SerializeField] GameObject dropItem;
    [SerializeField] protected bool ski1bulletDir = false;
    [SerializeField] protected bool sk1isLeft = false;

    protected SpriteManager.BossSprite bossSprites;

    [SerializeField] protected float originSpeed;

    [SerializeField] GameObject portal;
    [SerializeField] Transform portalPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public virtual void Init()
    {
        p = GameManager.Instance.Player;
        sr = GetComponent<SpriteRenderer>();
        sa = GetComponent<SpriteAnimation>();
        pd = GameManager.Instance.PlayerData;
        sksystem = GameManager.Instance.SkSystem;
        bossSprites = SpriteManager.Instance.bossSprite[data.Index];
        idleSprites = bossSprites.idleSprite;
        hitSprites = bossSprites.hitSprite;
        deadSprite = bossSprites.deadSprite;
        attack1Sprite = bossSprites.attack1Sprites;
        attack2Sprite = bossSprites.attack2Sprites;
        state = BossState.Idle;
        sa.SetSprite(idleSprites, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isDead)
        {
            return;
        }
        else
        {
            if (collision.gameObject.GetComponent<PBullet>())
            {
                TakeDamage(2);
                Pooling.Instance.SetPool(DicKey.pBullet, collision.GetComponent<PBullet>().gameObject);
                Debug.Log("hit");
            }

            if (collision.gameObject.GetComponent<Missile>())
            {
                TakeDamage(collision.GetComponent<Missile>().data.Damage);
            }

            if (collision.CompareTag("MissileArea"))
            {
                TakeDamage(collision.transform.parent.GetComponent<Missile>().data.Damage);
            }

            if (collision.GetComponent<Skill0Bullet>())
            {
                TakeDamage(collision.GetComponent<Skill0Bullet>().damage);
            }

            if (collision.GetComponent<Skill1Bullet>())
            {
                Skill1Damage(2, collision.gameObject);
            }
        }
        
    }

    public void TakeDamage(int damage)
    {
        originSpeed = data.Speed;
        data.HP -= damage;
        StartCoroutine(Hit());
        if (data.HP<=0)
        {
            isDead = true;
            state = BossState.Dead;
            Dead();
            return;
        }
    }

    IEnumerator Hit()
    {
        state = BossState.Hit;
        float originSpeed = data.Speed;
        data.Speed = 0;
        yield return new WaitForSeconds(0.5f);
        data.Speed = originSpeed;
    }

    public void Dead()
    {
        data.Speed = 0;
        GetComponent<CapsuleCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        sa.SetSprite(deadSprite, 0.2f, false, Clear);
        p.data.EXP += data.EXP;
    }

    void Clear()
    {
        GameObject obj = Instantiate(portal, portalPos);
        obj.transform.SetParent(null);
        Destroy(gameObject,0.6f);
    }

    public void Idle()
    {
        state = BossState.Idle;
        sa.SetSprite(idleSprites, 0.2f);
        data.Speed = originSpeed;
    }

    public void Skill1Damage(int damage, GameObject bullet)
    {
        data.HP -= damage;
        if (data.HP<=0)
        {
            isDead = true;
            state = BossState.Dead;
            sa.SetSprite(deadSprite, 0.2f, false);
            Dead();
            return;
        }
        Ski1SetDir(bullet);
        StartCoroutine(Back());
    }

    IEnumerator Back()
    {
        state = BossState.Back;
        sr.color = new Color32(255, 90, 90, 255);
        float originSpeed = data.Speed;
        data.Speed = 0f;
        yield return new WaitForSeconds(0.5f);
        data.Speed = originSpeed;
        sr.color = Color.white;
        state = BossState.Idle;
        ski1bulletDir = false;
    }

    public void Ski1SetDir(GameObject bullet)
    {
        if (!ski1bulletDir)
        {
            if (!bullet.GetComponent<Skill1Bullet>().isRight)
            {
                sk1isLeft = true;
            }
            else
            {
                sk1isLeft = false;
            }
        }
        ski1bulletDir = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
