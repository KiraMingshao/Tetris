using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris : MonoBehaviour
{
    public Vector2[] tetrisCell;

    public bool canFall;

    private float elapsedTime = 0.0f;

    private float minX = -5.36f;
    private float maxX = -1.04f;
    private float minY = -4.56f;
    private float maxY = 4.56f;
    private float step = 0.48f;

    World world;
    // Start is called before the first frame update
    void Start()
    {
        world = GameObject.Find("World").GetComponent<World>();
        canFall = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!canFall)
            return;
        else
        {
            Tick();
            Move();
            Rotate();
        }
    }

    public void Initiate(Vector2 vector2)
    {
        this.transform.position = vector2;
    }

    public void Move()
    {
        //bool canMoveLeft = true;
        //bool canMoveRight = true;

        //foreach(Transform square in this.transform) 
        //{
        //    square.position = new Vector3(Mathf.Clamp(square.position.x, minX, maxX), square.position.y);
        //    if(square.position.x == minX)
        //        canMoveLeft = false;
        //    else if(square.position.x == maxX) 
        //        canMoveRight = false;

        //    if(square.position.x > minX && square.position.x < maxX)
        //        canMoveLeft = canMoveRight = true;
        //}

        if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x - 1, this.transform.position.y), step);
            if(world.IsInEmptyCells(this.transform) == false) 
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x + 1, this.transform.position.y), step);
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x + 1, this.transform.position.y), step);
    }

    public void Fall()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, this.transform.position.y - 1), step);
        if(world.IsAtBottom(this.transform))
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, this.transform.position.y + 1), step);
            StopFalling();
        }

    }

    public void StopFalling()
    {
        canFall = false;
        world.tetris = null;
        world.MoveTetrisIntoCells();
    }

    public void Rotate()
    {
        Transform rotateCenter = transform.Find("RotateCenter");
        if (Input.GetKeyDown(KeyCode.Q) && world.IsInEmptyCells(this.transform))
        {
            this.transform.RotateAround(rotateCenter.transform.position, Vector3.forward, -90.0f);
        }
        if (Input.GetKeyDown(KeyCode.E) && world.IsInEmptyCells(this.transform))
        {
            this.transform.RotateAround(rotateCenter.transform.position, Vector3.forward, 90.0f);
        }
    }

    public void Tick()
    {
        float temp = world.tickInterval;

        if(Input.GetKeyDown(KeyCode.S)) 
        {
            world.tickInterval = 0.5f;
        }
        
        if(Input.GetKeyUp(KeyCode.S)) 
        {
            world.tickInterval = temp;
        }
        
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= world.tickInterval) 
        { 
            Fall();
            elapsedTime = 0;
        }
    }
}

