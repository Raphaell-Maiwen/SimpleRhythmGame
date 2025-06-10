using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class RawInputTest : MonoBehaviour
{
    [DllImport("RawInputDLL.dll")]
    public static extern int Oui();
    void Start()
    {
        int ok = Oui();
        Debug.Log(ok);
    }
}