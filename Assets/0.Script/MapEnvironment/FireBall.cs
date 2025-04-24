using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int damage = 7;
    [SerializeField] Transform turnPoint;
    [SerializeField] List<Sprite> fireBallSprites;
    [SerializeField] float power;
    [SerializeField] bool start;
    [SerializeField] bool fired;
    Rigidbody2D rigid;
    SpriteAnimation sa;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sa = GetComponent<SpriteAnimation>();
        fireBallSprites = SpriteManager.Instance.fireBallSprites;
        sa.SetSprite(fireBallSprites, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!start)
        {
            rigid.AddForce(Vector3.up * power, ForceMode2D.Impulse);
            start = true;
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FireBallTurnArea"))
        {
            transform.parent.GetComponent<FireBallManager>().isStart = true;
            Pooling.Instance.SetPool(DicKey.fireball,gameObject);
        }
    }

    public void Initialize()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        start = false;
        
    }

    
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FireBallTurnArea"))
        {
            
            start = false;
        }
    }
    
    public void Fire()
    {
        rigid.AddForce(Vector3.up * power, ForceMode2D.Impulse);
    }
    */
}
