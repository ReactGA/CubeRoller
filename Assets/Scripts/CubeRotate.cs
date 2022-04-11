using UnityEngine;
using System.Collections.Generic;

public class CubeRotate : MonoBehaviour
{
    public static CubeRotate instance;
    [SerializeField] Transform cube, front, back, left, right;
    [Tooltip("Must be divisible by 90")]
    [SerializeField] float rotspeed = 5, gravity = 0.2f;
    [SerializeField] Rigidbody cubeRb;
    [SerializeField] bool UseTouchControl;
    float rotAmount;
    bool RotatingCW, RotatingACW;
    Transform rotatingdir;
    Vector3 rotatingAxis;
    //setting the previous rot
    Vector3 previousPos, previousRot;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SetChecks();
    }
    void Update()
    {
        SetInputsKeyBoard();
        SetInputsTouch();
        PerformRotation();
        SetGravity();
        DebugInputs();
    }
    void SetInputsKeyBoard()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !RotatingACW)
        {
            transform.position = newPos("Front");
            LevelManager.instance.MoveSound.Play();
            RotatingACW = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !RotatingCW)
        {
            transform.position = newPos("Back");
            LevelManager.instance.MoveSound.Play();
            RotatingCW = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !RotatingCW)
        {
            transform.position = newPos("Left");
            LevelManager.instance.MoveSound.Play();
            RotatingCW = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !RotatingACW)
        {
            transform.position = newPos("Right");
            LevelManager.instance.MoveSound.Play();
            RotatingACW = true;
        }
    }
    Vector2 startPos, endPos;
    void SetInputsTouch()
    {
        if (!UseTouchControl || !Levelinstance.inst.BeganGame)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            SetTouchRot(startPos, endPos);
        }


    }
    void SetTouchRot(Vector2 start, Vector2 end)
    {
        string dir = CalculateDirection(start, end);
        transform.position = newPos(dir);
        if (dir == "Front" || dir == "Right")
        {
            RotatingACW = true;
        }
        else if (dir == "Back" || dir == "Left")
        {
            RotatingCW = true;
        }
        LevelManager.instance.MoveSound.Play();
    }/* var fl = new List<float>();
        fl.Sort(); */
    string CalculateDirection(Vector2 start, Vector2 end)
    {
        float xdiff = start.x - end.x;
        float ydiff = start.y - end.y;
        if (Mathf.Abs(xdiff) > Mathf.Abs(ydiff))
        {
            float movedir = Mathf.Sign(xdiff);
            if (movedir > 0)
            {
                return "Left";
            }
            else
            {
                return "Right";
            }
        }
        else
        {
            float movedir = Mathf.Sign(ydiff);
            if (movedir > 0)
            {
                return "Back";
            }
            else
            {
                return "Front";
            }
        }
        return null;
    }
    void PerformRotation()
    {
        if (RotatingCW)
        {
            if (rotAmount < 90)
            {
                cube.RotateAround(rotatingdir.position, rotatingAxis, rotspeed);
                rotAmount += rotspeed;
            }
            else
            {
                transform.position = cube.position;
                //cubeRb.isKinematic = false;
                RotatingCW = false;
            }
        }
        if (RotatingACW)
        {
            if (rotAmount > -90)
            {
                cube.RotateAround(rotatingdir.position, rotatingAxis, -rotspeed);
                rotAmount -= rotspeed;
            }
            else
            {
                transform.position = cube.position;
                //cubeRb.isKinematic = false;
                RotatingACW = false;
            }
        }
    }
    void SetGravity()
    {
        bool grounded = CheckGrounded();
        if (!grounded && !RotatingACW && !RotatingCW)
        {
            cubeRb.MovePosition(cubeRb.transform.position + Vector3.up * -gravity * Time.deltaTime);
        }
    }
    void DebugInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(CheckGrounded());
        }
    }
    public void ResetPos()
    {
        RotatingACW = false;
        RotatingCW = false;
        cube.localPosition = previousPos;
        cube.localEulerAngles = previousRot;
        //cubeRb.isKinematic = false;
    }

    Vector3 newPos(string Axis)
    {
        rotAmount = 0;
        previousPos = cube.transform.localPosition;
        previousRot = cube.transform.localEulerAngles;
        //cubeRb.isKinematic = true;
        List<float> pos = new List<float>();
        bool zAxis = Axis == "Front" || Axis == "Back";
        bool xAxis = Axis == "Left" || Axis == "Right";

        foreach (Transform child in cube.transform)
        {
            if (zAxis)
            {
                pos.Add(child.position.z);
            }
            else if (xAxis)
            {
                pos.Add(child.position.x);
            }
        }
        pos.Sort();
        switch (Axis)
        {
            case "Front":
                rotatingdir = front;
                rotatingAxis = Vector3.left;
                return new Vector3(0, 0, pos[pos.Count - 1]);
                break;
            case "Back":
                rotatingdir = back;
                rotatingAxis = Vector3.left;
                return new Vector3(0, 0, pos[0]);
                break;
            case "Left":
                rotatingdir = left;
                rotatingAxis = Vector3.forward;
                return new Vector3(pos[0], 0, 0);
                break;
            case "Right":
                rotatingdir = right;
                rotatingAxis = Vector3.forward;
                return new Vector3(pos[pos.Count - 1], 0, 0);
                break;
            default:
                break;
        }
        return Vector3.zero;
    }
    List<floorcheck> check = new List<floorcheck>();
    public void SetChecks()
    {
        check.Clear();
        foreach (Transform ch in cube)
        {
            check.Add(ch.GetComponent<floorcheck>());
        }
    }
    bool CheckGrounded()
    {
        for (var i = 0; i < check.Count; i++)
        {
            if (check[i].floored == true)
            {
                return true;
            }
        }
        return false;
    }
}