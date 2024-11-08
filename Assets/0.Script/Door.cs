using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Player p;

    // Start is called before the first frame update
    void Start()
    {

        p = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        if(p == null)
        {
            p = GameManager.Instance.Player;
        }
        float dist = Vector2.Distance(transform.position, p.transform.position);

        if(dist<1.5f)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                SceneChanger.Instance.GoShip();
            }
        }
        else
        {
            return;
        }
    }
}
