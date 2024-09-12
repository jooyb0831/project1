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

    Player p;
    SpriteRenderer sr;
    SpriteAnimation sa;
    public EnemyData data = new EnemyData();

    [SerializeField] List<Sprite> enemySprite;
    [SerializeField] List<Sprite> deadSprite;
    EnemyState state = EnemyState.Idle;
    protected bool isDead = false;


    [SerializeField] GameObject dropItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void Init()
    {
        //p = GameManager.Instance.player;
        sr = GetComponent<SpriteRenderer>();
        sa = GetComponent<SpriteAnimation>();
        SpriteManager.EnemySprite enemySprites = SpriteManager.Instance.enemySprite[data.index];
        enemySprite = enemySprites.idleSprite;
        deadSprite = enemySprites.deadSprite;
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
            Destroy(collision.gameObject);
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

    IEnumerator Dead()
    {
        sa.SetSprite(deadSprite, 0.2f);
        yield return new WaitForSeconds(0.8f);
        GameObject item = Instantiate(dropItem, transform);
        item.transform.SetParent(null);
        Destroy(gameObject);
    }
}
