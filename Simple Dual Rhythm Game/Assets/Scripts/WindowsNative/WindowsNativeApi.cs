using System;
using System.Runtime.InteropServices;

namespace WindowsNative
{
    public class WindowsNativeApi
    {
        [DllImport("user32", SetLastError = true)]
        public static extern uint GetRawInputDeviceList([Out] RawInputDeviceListItem[] pRawInputDeviceList, ref uint puiNumDevices, uint cbSize);
    }

    /// <summary>
    /// RAWINPUTDEVICELIST
    /// </summary>
    public struct RawInputDeviceListItem
    {
        /// <summary>
        /// hDevice
        /// </summary>
        public RawInputDeviceHandle Device { get; set; }

        /// <summary>
        /// dwType
        /// </summary>
        public RawInputDeviceType Type { get; set; }
    }

    /// <summary>
    /// HANDLE
    /// </summary>
    public struct RawInputDeviceHandle : IEquatable<RawInputDeviceHandle>
    {
        readonly IntPtr value;

        public static RawInputDeviceHandle Zero => (RawInputDeviceHandle)IntPtr.Zero;

        RawInputDeviceHandle(IntPtr value) => this.value = value;

        public static IntPtr GetRawValue(RawInputDeviceHandle handle) => handle.value;

        public static explicit operator RawInputDeviceHandle(IntPtr value) => new RawInputDeviceHandle(value);

        public static bool operator ==(RawInputDeviceHandle a, RawInputDeviceHandle b) => a.Equals(b);

        public static bool operator !=(RawInputDeviceHandle a, RawInputDeviceHandle b) => !a.Equals(b);

        public bool Equals(RawInputDeviceHandle other) => value.Equals(other.value);

        public override bool Equals(object obj) =>
            obj is RawInputDeviceHandle other &&
            Equals(other);

        public override int GetHashCode() => value.GetHashCode();

        public override string ToString() => value.ToString();
    }

    public enum RawInputDeviceType
    {
        Mouse,
        Keyboard,
        Hid,
    }
}
