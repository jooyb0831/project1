using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Bullet : MonoBehaviour
{
    public int damage;
    [SerializeField] float speed;
    [SerializeField] float duration = 7f;
    [SerializeField] float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);

        timer += Time.deltaTime;
        if(timer>=duration)
        { 
            Pooling.Instance.SetPool(DicKey.boss3Bullet, gameObject);
        }
    }

    public void Initialize()
    {
        timer = 0;
        transform.position = Vector3.zero;
    }
}
