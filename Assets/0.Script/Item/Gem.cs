using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] Player p;
    SpriteAnimation sa;
    public bool isFind = false;
    [SerializeField] List<Sprite> blueGemSprite;
    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        p = GameManager.Instance.player;
        sa.SetSprite(blueGemSprite, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {

        if(isFind == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, p.transform.position, Time.deltaTime * 5f);

            float dist = Vector2.Distance(p.transform.position, transform.position);
            if(dist<0.2f)
            {
                Destroy(gameObject);
            }
        }

        else
        {
            float dist = Vector2.Distance(p.transform.position, transform.position);
            if(dist<=1f)
            {
                isFind = true;
            }
        }
    }
}
