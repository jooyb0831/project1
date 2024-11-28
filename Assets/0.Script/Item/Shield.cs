using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Player p;
    [SerializeField] float duration;
    [SerializeField] float timer;
    public bool isStart = false;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(gameObject.activeSelf)
        {
            isStart = true;
        }
        transform.position = p.transform.position;
        timer += Time.deltaTime;
        if(timer>=duration)
        {
            timer = 0;
            Pooling.Instance.SetPool(DicKey.shield, gameObject);
        }
        */
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EBullet>())
        {
            Pooling.Instance.SetPool(DicKey.eBullet, collision.GetComponent<EBullet>().gameObject);
        }

        if(collision.GetComponent<EBullet2>())
        {
            Pooling.Instance.SetPool(DicKey.eBullet2, collision.GetComponent<EBullet2>().gameObject);
        }

        if(collision.GetComponent<Boss2Bullet>())
        {
            Pooling.Instance.SetPool(DicKey.boss2Bullet, collision.GetComponent<Boss2Bullet>().gameObject);
        }

        if(collision.GetComponent<Boss3Bullet>())
        {
            Pooling.Instance.SetPool(DicKey.boss3Bullet, collision.GetComponent<Boss3Bullet>().gameObject);
        }

    }

    public void Initialize()
    {
        transform.position = Vector2.zero;
    }
}
