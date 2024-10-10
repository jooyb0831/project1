using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private PlayerData pd;
    private SpriteAnimation sa;
    private List<Sprite> coinSprites;

    
    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.PlayerData;
        sa = GetComponent<SpriteAnimation>();
        coinSprites = SpriteManager.Instance.coinSprites;
        sa.SetSprite(coinSprites, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {
            if(pd==null)
            {
                pd = GameManager.Instance.PlayerData;
            }
            pd.Coin += 1;
            Destroy(gameObject);
            
        }
    }
}
