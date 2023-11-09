using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareManager : MonoBehaviour
{

    private void RangeLimitation()
    {
        this.transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5.36f, -0.8f), Mathf.Clamp(transform.position.y, -4.56f, 6.0f));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RangeLimitation();
    }
}
