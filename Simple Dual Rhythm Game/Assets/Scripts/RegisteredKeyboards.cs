using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class RegisteredKeyboards : ScriptableObject
{
    [ReadOnly] public List<int> _keyboardsDeviceIDList = new List<int>();
    public List<int> KeyboardsDeviceIDList =>  _keyboardsDeviceIDList;

    public void AddKeyboard(int deviceID)
    {
        _keyboardsDeviceIDList.Add(deviceID);
    }

    public void ClearKeyboards()
    {
        _keyboardsDeviceIDList.Clear();
    }

    void OnApplicationQuit()
    {
        Debug.Log("Quit");
        ClearKeyboards();
    }
}
