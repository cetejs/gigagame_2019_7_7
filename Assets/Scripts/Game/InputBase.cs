using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBase :Singleton<InputBase>
{
    private string _AxisX = "Horizontal";
    private string _AxisY = "Vertical";
    private KeyCode _Jump = KeyCode.Space;
    private KeyCode _Shin = KeyCode.J;
    private KeyCode _Sprint = KeyCode.L;
    private KeyCode _Quit = KeyCode.Escape;

    public float GetAxisX() {
        return Input.GetAxis(_AxisX);
    }

    public float GetAxisY() {
        return Input.GetAxis(_AxisY);
    }

    public float GetAxisRawX() {
        return Input.GetAxisRaw(_AxisX);
    }

    public float GetAxisRawY() {
        return Input.GetAxisRaw(_AxisY);
    }

    public bool IsJumpDown() {
        return Input.GetKeyDown(_Jump);
    }

    public bool IsJumpUp() {
        return Input.GetKeyUp(_Jump);
    }

    public bool IsShinDown() {
        return Input.GetKeyDown(_Shin);
    }

    public bool IsShinUp() {
        return Input.GetKeyUp(_Shin);
    }

    public bool IsSprintDown() {
        return Input.GetKeyDown(_Sprint);
    }

    public bool IsSprintUp() {
        return Input.GetKeyUp(_Sprint);
    }


}
