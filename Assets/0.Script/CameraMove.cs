using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Player p;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(p == null)
        {
            p = GameManager.Instance.Player;
        }
        float x = p.transform.position.x;
        /*
        if (p.transform.localScale.x > 0)
        {
            x = p.transform.position.x + 2f;
        }
        else
        {
            x = p.transform.position.x - 2f;
        }
        */
        float y = p.transform.position.y+1.5f;
        float z = -10;
        transform.position = new Vector3(x, y, z);
    }
}
