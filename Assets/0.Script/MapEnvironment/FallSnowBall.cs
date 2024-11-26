using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FallSnowBall : MonoBehaviour
{
    public int damage;
    public bool isEnd = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SnowBallEndZone"))
        {
            isEnd = true;
            StartCoroutine(Shrink());
        }
    }

    IEnumerator Shrink()
    {
        yield return new WaitForSeconds(1f);

        transform.DOScale(new Vector3(1, 1, 1), 2f)
            .OnComplete(() =>
            {
                Pooling.Instance.SetPool(DicKey.fallSnowBall,gameObject);
            });
    }

    public void Initialize()
    {
        isEnd = false;
        transform.localScale = new Vector3(3.5f, 3.5f, 1f);
        transform.position = Vector2.zero;
    }
}
