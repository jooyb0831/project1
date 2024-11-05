using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill0Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Ground>())
        {
            Destroy(gameObject);
        }
    }
}
