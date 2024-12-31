using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    SpriteAnimation sa;
    Player p;
    PlayerData pd;
    [SerializeField] List<Sprite> idleSprites;

    [SerializeField] GameObject textObj;
    
    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        idleSprites = SpriteManager.Instance.merchantSprites;
        sa.SetSprite(idleSprites, 0.2f);
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
    }

    // Update is called once per frame
    void Update()
    {
        if(p == null)
        {
            p = GameManager.Instance.Player;
        }
        float dist = Vector2.Distance(transform.position, p.transform.position);

        if (dist < 1.5f)
        {
            textObj.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                MerchantSystem.Instance.window.SetActive(true);
                MerchantSystem.Instance.SetInven();
                Time.timeScale = 0;
            }
        }
        else
        {
            textObj.SetActive(false);
        }
    }
}
