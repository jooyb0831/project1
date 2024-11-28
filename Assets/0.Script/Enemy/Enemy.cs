using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public class EnemyData
    {
        public int HP { get; set; }
        public int EXP { get; set; }
        public int AttackPower { get; set; }
        public int AttackDist { get; set; }
        public float Speed { get; set; }
        public float AttackSpeed { get; set; }
        public int index { get; set; }
    }

    public enum EnemyState
    {
        Idle,
        Run,
        Hit,
        Attack,
        Back,
        Dead
    }


    [SerializeField] protected Player p;
    SpriteRenderer sr;
    protected SpriteAnimation sa;
    public EnemyData data = new EnemyData();
    protected SkillSystem sksystem;

    protected List<Sprite> enemySprite;
    protected List<Sprite> moveSprite;
    protected List<Sprite> deadSprite;
    protected List<Sprite> attackSprite;
    [SerializeField] protected EnemyState state = EnemyState.Idle;
    protected bool isDead = false;

    private PlayerData pd;
    [SerializeField] GameObject dropItem;
    [SerializeField] protected Transform eBulletParent;
    [SerializeField] protected bool ski1bulletDir = false;
    [SerializeField] protected bool sk1isLeft = false;
    protected SpriteManager.EnemySprite enemySprites;
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
        enemySprites = SpriteManager.Instance.enemySprite[data.index];
        enemySprite = enemySprites.idleSprite;
        moveSprite = enemySprites.moveSprite;
        deadSprite = enemySprites.deadSprite;
        attackSprite = enemySprites.attackSprite;
        state = EnemyState.Idle;
        sa.SetSprite(enemySprite, 0.2f);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PBullet>() == true)
        {
            TakeDamage(pd.AttackDamage);
            Pooling.Instance.SetPool(DicKey.pBullet, collision.GetComponent<PBullet>().gameObject);
            Debug.Log("hit");
        }

        if(collision.gameObject.GetComponent<Missile>())
        {
            TakeDamage(collision.GetComponent<Missile>().data.Damage);
        }

        if(collision.CompareTag("MissileArea"))
        {
            TakeDamage(collision.transform.parent.GetComponent<Missile>().data.Damage);
        }

        if(collision.GetComponent<Skill0Bullet>())
        {
            TakeDamage(collision.GetComponent<Skill0Bullet>().damage);
        }

        if(collision.GetComponent<Skill1Bullet>())
        {
            Skill1Damage(2, collision.gameObject);
        }

        if(collision.CompareTag("BombArea"))
        {
            TakeDamage(collision.transform.parent.GetComponent<Bomb>().damage);
        }
    }


    public void TakeDamage(int damage)
    {
        if(data.HP<=0)
        {
            return;
        }
        data.HP -= damage;
        StartCoroutine("Hit");
        if(data.HP<=0)
        {
            isDead = true;
            state = EnemyState.Dead;
            StartCoroutine("Dead");
        }
    }

    public void Skill1Damage(int damage, GameObject bullet)
    {
        if (data.HP <= 0)
        {
            return;
        }
        data.HP -= damage;
        Ski1SetDir(bullet);
        StartCoroutine("Back");
        if (data.HP <= 0)
        {
            isDead = true;
            state = EnemyState.Dead;
            StartCoroutine("Dead");
        }
    }

    IEnumerator Hit()
    {
        sr.color = new Color32(255, 90, 90, 255);
        data.Speed = 0f;
        yield return new WaitForSeconds(0.2f);
        data.Speed = 2f;
        sr.color = Color.white;
    }

    protected IEnumerator Dead()
    {
        if(pd == null)
        {
            pd = GameManager.Instance.PlayerData;
        }

        pd.EXP += 10;
        QuestManager.Instance.Check(this);
        sa.SetSprite(deadSprite, 0.2f);
        yield return new WaitForSeconds(0.8f);
        GameObject item = Instantiate(dropItem, transform);
        
        item.transform.SetParent(null);
        item.transform.localScale = new Vector3(5f,5f,5f);
        Destroy(gameObject);
    }

    IEnumerator Back()
    {
        state = EnemyState.Back;
        sr.color = new Color32(255, 90, 90, 255);
        data.Speed = 0f;
        /*
        if (transform.localScale.x < 0)
        {
            transform.Translate(Vector2.left * Time.deltaTime * 10f);
        }
        else
        {
            Vector2 pos = new Vector2(transform.position.x + 10f, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * 5f);
        }
        */
        yield return new WaitForSeconds(0.5f);
        data.Speed = 2f;
        sr.color = Color.white;
        state = EnemyState.Idle;
        ski1bulletDir = false;
    }

    public void Ski1SetDir(GameObject bullet)
    {
        if(!ski1bulletDir)
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
}
