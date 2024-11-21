using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloorManager : MonoBehaviour
{
    [SerializeField] FallenFloor[] floors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FloorBack(int idx)
    {
        floors[idx].gameObject.SetActive(true);
    }
}
