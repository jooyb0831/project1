using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public class Data
    {
        public int HP { get; set; }
        public int Index { get; set; }
        public int AttackPower { get; set; }
        public float Speed { get; set; }
        public int EXP { get; set; }
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


    protected Player p;
    SpriteRenderer sr;
    protected SpriteAnimation sa;
    public Data data = new();
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

    public virtual void Init()
    {
        p = GameManager.Instance.Player;
        sr = GetComponent<SpriteRenderer>();
        sa = GetComponent<SpriteAnimation>();
        pd = GameManager.Instance.PlayerData;
        sksystem = GameManager.Instance.SkSystem;
        enemySprites = SpriteManager.Instance.enemySprite[data.Index];
        enemySprite = enemySprites.idleSprite;
        moveSprite = enemySprites.moveSprite;
        deadSprite = enemySprites.deadSprite;
        attackSprite = enemySprites.attackSprite;
        state = EnemyState.Idle;
        sa.SetSprite(enemySprite, 0.2f);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PBullet pBullet = collision.GetComponent<PBullet>();
        if (pBullet)
        {
            TakeDamage(pd.AttackDamage);
            Pooling.Instance.SetPool(DicKey.pBullet, pBullet.gameObject);
            Debug.Log("hit");
        }

        Missile missile = collision.GetComponent<Missile>();
        if(missile)
        {
            TakeDamage(collision.GetComponent<Missile>().data.Damage);
        }

        if(collision.CompareTag("MissileArea"))
        {
            TakeDamage(collision.transform.parent.GetComponent<Missile>().data.Damage);
        }

        Skill0Bullet skill0Bullet = collision.GetComponent<Skill0Bullet>();
        if(skill0Bullet)
        {
            TakeDamage(skill0Bullet.damage);
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
        float originSpeed = data.Speed;
        sr.color = new Color32(255, 90, 90, 255);
        data.Speed = 0f;
        yield return new WaitForSeconds(0.2f);
        data.Speed = originSpeed;
        sr.color = Color.white;
    }

    /// <summary>
    /// 적 사망시 호출
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Dead()
    {
        if(pd == null)
        {
            pd = GameManager.Instance.PlayerData;
            StopCoroutine("Dead");
            StartCoroutine("Dead");
            yield return null;
        }

        //경험치 추가
        pd.EXP += data.EXP;
        
        //퀘스트 진행 체크
        QuestManager.Instance.Check(data.Index);

        //Sprite 세팅
        sa.SetSprite(deadSprite, 0.2f);

        yield return new WaitForSeconds(0.8f);

        //드랍아이템 생성
        GameObject item = Instantiate(dropItem, transform);
        item.transform.SetParent(null);

        //적 오브젝트 삭제
        Destroy(gameObject);
    }

    IEnumerator Back()
    {
        float originSpeed = data.Speed;
        state = EnemyState.Back;
        sr.color = new Color32(255, 90, 90, 255);
        data.Speed = 0f;

        yield return new WaitForSeconds(0.5f);
        data.Speed = originSpeed;
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
