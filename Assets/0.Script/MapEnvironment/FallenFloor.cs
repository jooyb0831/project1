using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FallenFloor : MonoBehaviour
{
    private Player p;
    [SerializeField] float delay;
    [SerializeField] float timer;
    public int idx;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        idx = transform.GetSiblingIndex();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {
            StartCoroutine(VanishFloor());
        }
    }

    IEnumerator VanishFloor()
    {
        yield return new WaitForSeconds(0.5f);
        
        GetComponent<SpriteRenderer>().DOFade(0,0.3f)
            .OnComplete(() =>
            {
                GetComponent<BoxCollider2D>().enabled = false;
            });
        
        yield return new WaitForSeconds(2f);
        GetComponent<SpriteRenderer>().DOFade(1, 0.3f)
           .OnComplete(() =>
           {
               GetComponent<BoxCollider2D>().enabled = true;
           });
    }
    
}
