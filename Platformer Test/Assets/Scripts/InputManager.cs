using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class InputManager
{
    public static bool Left()
    {
        return (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
    }


    public static bool Right()
    {
        return (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));
    }


    public static bool LeftKeyDown()
    {
        return (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow));
    }


    public static bool RightKeyDown()
    {
        return (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow));
    }


    public static bool Down()
    {
        return (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow));
    }


    public static bool Jump()
    {
        return (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow));
    }


    public static bool Attack()
    {
        return (Input.GetKeyDown(KeyCode.F));
    }


    public static bool Magic()
    {
        return (Input.GetKeyDown(KeyCode.G));
    }

    public static bool One()
    {
        return (Input.GetKey(KeyCode.Alpha1));
    }

    public static bool Two()
    {
        return (Input.GetKey(KeyCode.Alpha2));
    }
}