using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1Bullet : MonoBehaviour
{
    private SpriteAnimation sa;
    private Player p;

    [SerializeField] float speed;
    [SerializeField] float timer;
    [SerializeField] float time;
    [SerializeField] List<Sprite> last;
    [SerializeField] List<Sprite> sk1BulletSprite;
     public bool isRight = true;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        sa = GetComponent<SpriteAnimation>();
        sa.SetSprite(last, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isRight)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        else if (!isRight)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        sa.SetSprite(sk1BulletSprite, 0.1f, false);

        timer += Time.deltaTime;
        if (timer > time)
        {
            Destroy(gameObject);
        }
    }
}
