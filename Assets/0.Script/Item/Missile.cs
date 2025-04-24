using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileData
{

    public int Damage { get; set; } = 10;

}
public class Missile : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteAnimation sa;
    public MissileData data = new MissileData();
    public bool isLeft = false;
    [SerializeField] List <Sprite> explosionSprites;
    [SerializeField] List<Sprite> missileSprite;
    [SerializeField] float bulletSpeed;
    [SerializeField] bool isEnd = false;
     public Transform atkArea;
    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        sa.SetSprite(missileSprite, 0.2f);
        rigid = GetComponent<Rigidbody2D>();
        float x = transform.position.x;

    }


    public void Shoot(Vector2 dir)
    {
        if(rigid == null)
        {
            rigid = GetComponent<Rigidbody2D>();
        }
        rigid.velocity = dir;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void FixedUpdate()
    {
        if(!isEnd)
        {
            float angle = Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle - 90);
        }
        else
        {
            return;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Ground>())
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.3f);
            Attacked();
            atkArea.gameObject.SetActive(true);
        }

        if(collision.GetComponent<Enemy>())
        {
            Attacked();
            atkArea.gameObject.SetActive(true);
        }

        if(collision.GetComponent<EnemyBoss>())
        {
            Attacked();
            atkArea.gameObject.SetActive(true);
        }
    }

    public void Attacked()
    {
        GetComponent<SpriteRenderer>().sprite = explosionSprites[0];
        isEnd = true;
        rigid.velocity = Vector2.zero;
        transform.rotation = Quaternion.identity;
        rigid.constraints = RigidbodyConstraints2D.FreezePosition;
        sa.SetSprite(explosionSprites, 0.2f, false, DestroyObject);
    }



    void DestroyObject()
    {
        Pooling.Instance.SetPool(DicKey.pMissile, gameObject);
    }

    public void Initialize()
    {
        isEnd = false;
        atkArea.gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().sprite = missileSprite[0];
        rigid.freezeRotation = false;
        rigid.constraints = RigidbodyConstraints2D.None;
        sa.SetSprite(missileSprite, 0.2f);
        GetComponent<CapsuleCollider2D>().isTrigger = true;
        transform.localRotation = Quaternion.identity;
    }
}
