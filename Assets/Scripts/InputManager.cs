using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    Up, Down, Right, Left
}

public class InputManager : MonoBehaviour
{
    // variables
    private GameManager gm;

    private void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //right arrow move
            gm.Move(MoveDirection.Right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // left arrow move
            gm.Move(MoveDirection.Left);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // up arrow move
            gm.Move(MoveDirection.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // down arrow move
            gm.Move(MoveDirection.Down);
        }
    }
}
