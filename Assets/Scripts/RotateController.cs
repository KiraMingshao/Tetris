using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    public GameObject tetris; 
    
    public void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            tetris.transform.RotateAround(this.transform.position, Vector3.forward, -90.0f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            tetris.transform.RotateAround(this.transform.position, Vector3.forward, 90.0f);
        }
    }

    public void Start()
    {
        tetris = this.transform.parent.gameObject;
    }

    public void Update()
    {
        Rotate();
    }
}
