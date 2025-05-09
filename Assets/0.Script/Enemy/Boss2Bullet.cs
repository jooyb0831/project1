using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Bullet : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteAnimation sa;
    public MissileData data = new MissileData();
    public bool isLeft = false;
    [SerializeField] List<Sprite> explosionSprites;
    [SerializeField] List<Sprite> fireSprites;
    [SerializeField] float bulletSpeed;
    [SerializeField] bool isEnd = false;
    public int damage;
    public Transform atkArea;
    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        sa.SetSprite(fireSprites, 0.2f);
        rigid = GetComponent<Rigidbody2D>();
        float x = transform.position.x;
    }

    public void Shoot(Vector2 dir)
    {
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody2D>();
        }
        rigid.velocity = dir;
    }

    private void FixedUpdate()
    {
        if (!isEnd)
        {
            float angle = Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
        else
        {
            return;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ground>())
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.3f);
            Attacked();
            atkArea.gameObject.SetActive(true);
        }

        if (collision.GetComponent<Player>())
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
        Pooling.Instance.SetPool(DicKey.boss2Bullet, gameObject);
    }

    public void Initialize()
    {
        isEnd = false;
        atkArea.gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().sprite = fireSprites[0];
        rigid.freezeRotation = false;
        rigid.constraints = RigidbodyConstraints2D.None;
        sa.SetSprite(fireSprites, 0.2f);
        GetComponent<CapsuleCollider2D>().isTrigger = true;
        transform.localRotation = Quaternion.identity;
    }
}
