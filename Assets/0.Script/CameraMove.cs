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
        float x = p.transform.position.x+2f;
        float y = p.transform.position.y+2f;
        float z = -10;
        transform.position = new Vector3(x, y, z);
    }
}
