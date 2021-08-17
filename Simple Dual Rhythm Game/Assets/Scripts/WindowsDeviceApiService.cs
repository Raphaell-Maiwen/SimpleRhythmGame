using System;
using System.Runtime.InteropServices;
using WindowsNative;
using UnityEngine;

public static class WindowsDeviceApiService
{
    public static void ListWindowsRawDeviceApiDevicesToConsole()
    {
        
        var rawInputDeviceListItemStructSize = (uint) Marshal.SizeOf(typeof(RawInputDeviceListItem));
        var numberOfDevices = GetRawDeviceCount(rawInputDeviceListItemStructSize);
        var devices = new RawInputDeviceListItem[numberOfDevices];
        
        var returnValue =
            WindowsNativeApi.GetRawInputDeviceList(devices, ref numberOfDevices, rawInputDeviceListItemStructSize);

        if (returnValue == unchecked((uint) -1))
            throw new Exception("error calling GetRawInputDeviceList()");

        Debug.Log($"deviceCount: {numberOfDevices}");

        foreach (var device in devices)
            Debug.Log($"device: type: {device.Type}, device handle: {device.Device}, button:");

        var yoyo = RawInputDeviceHandle.GetRawValue(devices[0].Device);
        Debug.Log("Yo: " + yoyo);
    }

    public static void Yo() {
        /*var yoyo = RawInputDeviceHandle.GetRawValue(devices[0].Device);
        Debug.Log("Yo: " + yoyo);*/
    }

    private static uint GetRawDeviceCount(uint rawInputDeviceListItemStructSize)
    {
        uint deviceCount = 0;

        WindowsNativeApi.GetRawInputDeviceList(null, ref deviceCount, rawInputDeviceListItemStructSize);
        return deviceCount;
    }
}