using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timer;
    [SerializeField] float time;

    public bool isRight = false;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRight == true)
        {
            transform.localScale = new Vector2(-5f, 5f);
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        else if (isRight == false)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        timer += Time.deltaTime;
        if (timer >= time)
        {
            Pooling.Instance.SetPool(DicKey.eBullet, gameObject);
            timer = 0;

        }
    }

    public void Initialize()
    {
        timer = 0;
        transform.localScale = new Vector2(3f, 3f);
        isRight = false;
    }
}
