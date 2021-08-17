using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CurrentTest : MonoBehaviour
{
    List<Keyboard> keyboards = new List<Keyboard>();

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboards.Count == 0 || keyboard != keyboards[0]) {
            keyboards.Add(keyboard);
        }

        if (keyboard != null) {
            //Debug.Log(keyboards.Count);
        }
        
    }
}