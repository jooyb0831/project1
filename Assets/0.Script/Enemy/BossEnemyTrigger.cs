using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyTrigger : MonoBehaviour
{
    [SerializeField] GameObject bossMonster;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            bossMonster.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
