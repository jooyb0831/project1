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
        Dead
    }

    [SerializeField] protected Player p;
    SpriteRenderer sr;
    protected SpriteAnimation sa;
    public EnemyData data = new EnemyData();
    

    protected List<Sprite> enemySprite;
    protected List<Sprite> deadSprite;
    protected List<Sprite> attackSprite;
    [SerializeField] protected EnemyState state = EnemyState.Idle;
    protected bool isDead = false;

    private PlayerData pd;
    [SerializeField] GameObject dropItem;
    [SerializeField] protected Transform eBulletParent;

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
        enemySprites = SpriteManager.Instance.enemySprite[data.index];
        enemySprite = enemySprites.idleSprite;
        deadSprite = enemySprites.deadSprite;
        attackSprite = enemySprites.attackSprite;
        state = EnemyState.Idle;
        sa.SetSprite(enemySprite, 0.2f);
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PBullet>() == true)
        {
            TakeDamage(2);
            Pooling.Instance.SetPool(DicKey.pBullet, collision.GetComponent<PBullet>().gameObject);
            Debug.Log("hit");
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


}
