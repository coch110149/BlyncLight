// Decompiled with JetBrains decompiler
// Type: Blynclight.BlyncController
// Assembly: Blynclight, Version=0.3.0.11, Culture=neutral, PublicKeyToken=null
// MVID: 5BBF77FB-8F3D-4A6F-B095-307E683776AC
// Assembly location: C:\Users\cecochr\Downloads\Embrava_SDK_For_Windows_v3.0.3\Embrava_SDK_For_Windows_v3.0.3\Binaries\AnyCpu\Blynclight.dll

using System;
using System.Runtime.InteropServices;

namespace BlyncN
{
    public class BlyncController
    {
        public DeviceInfo[] AoDevInfo = DeviceInfo.NewInitArray(10UL);
        internal DeviceAccess OUsbDeviceAccess = new DeviceAccess();
        internal byte ByBlyncControlCode = byte.MaxValue;
        internal byte ByBlyncIBuddyLightColorMask = 112;
        internal int MnTotalDevices;
        internal bool BResult;
        private byte testNumber =239;

        public BlyncController() => OUsbDeviceAccess.oBlyncController = this;

        // ReSharper disable once RedundantAssignment
        private void LookForBlyncDevices(ref int nNumberOfBlyncDevices)
        {
            BResult = OUsbDeviceAccess.GetDevices(ref AoDevInfo,ref MnTotalDevices);
            nNumberOfBlyncDevices = MnTotalDevices;
        }

        public bool ResetLight(int nDeviceIndex)
        {
            BResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= MnTotalDevices - 1)
            {
                if(AoDevInfo[nDeviceIndex].ByDeviceType == 1)
                {
                    ByBlyncControlCode = byte.MaxValue;
                    BResult = OUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(AoDevInfo[nDeviceIndex].PHandle,
                        ByBlyncControlCode);
                }
            }

            return BResult;
        }

        public bool TurnOnRedLight(int nDeviceIndex)
        {
            BResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= MnTotalDevices - 1)
            {
                if(AoDevInfo[nDeviceIndex].ByDeviceType == 1)
                {
                    ByBlyncControlCode = byte.MaxValue;
                    ByBlyncControlCode &= (byte) ~ByBlyncIBuddyLightColorMask;
                    ByBlyncControlCode |= 111;
                    BResult = OUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(AoDevInfo[nDeviceIndex].PHandle,ByBlyncControlCode);
                }
            }
            return BResult;
        }

        public bool TurnOnGreenLight(int nDeviceIndex)
        {
            BResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= MnTotalDevices - 1)
            {
                if(AoDevInfo[nDeviceIndex].ByDeviceType == 1)
                {
                    ByBlyncControlCode = byte.MaxValue;
                    ByBlyncControlCode &= (byte) ~ByBlyncIBuddyLightColorMask;
                    ByBlyncControlCode |= 95;
                    BResult = OUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(AoDevInfo[nDeviceIndex].PHandle,ByBlyncControlCode);
                }
            }
            return BResult;
        }

        public bool TurnOnYellowLight(int nDeviceIndex)
        {
            BResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= MnTotalDevices - 1)
            {
                if(AoDevInfo[nDeviceIndex].ByDeviceType == 1)
                {
                    ByBlyncControlCode = byte.MaxValue;
                    ByBlyncControlCode &= (byte) ~ByBlyncIBuddyLightColorMask;
                    ByBlyncControlCode |= 79;
                    BResult = OUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(AoDevInfo[nDeviceIndex].PHandle,ByBlyncControlCode);
                }
            }
            return BResult;
        }

        public bool TurnOnBlueLight(int nDeviceIndex)
        {
            BResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= MnTotalDevices - 1)
            {
                if(AoDevInfo[nDeviceIndex].ByDeviceType == 1)
                {
                    ByBlyncControlCode = byte.MaxValue;
                    ByBlyncControlCode &= (byte) ~ByBlyncIBuddyLightColorMask;
                    ByBlyncControlCode |= 63;
                    BResult = OUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(AoDevInfo[nDeviceIndex].PHandle,ByBlyncControlCode);
                }
            }
            return BResult;
        }

        public bool TurnOnMagentaLight(int nDeviceIndex)
        {
            BResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= MnTotalDevices - 1)
            {
                if(AoDevInfo[nDeviceIndex].ByDeviceType == 1)
                {
                    ByBlyncControlCode = byte.MaxValue;
                    ByBlyncControlCode &= (byte) ~ByBlyncIBuddyLightColorMask;
                    ByBlyncControlCode |= 47;
                    BResult = OUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(AoDevInfo[nDeviceIndex].PHandle,ByBlyncControlCode);
                }
            }
            return BResult;
        }

        public bool TurnOnWhiteLight(int nDeviceIndex)
        {
            BResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= MnTotalDevices - 1)
            {
                if(AoDevInfo[nDeviceIndex].ByDeviceType == 1)
                {
                    ByBlyncControlCode = byte.MaxValue;
                    ByBlyncControlCode &= (byte) ~ByBlyncIBuddyLightColorMask;
                    ByBlyncControlCode |= 15;
                    BResult = OUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(AoDevInfo[nDeviceIndex].PHandle,ByBlyncControlCode);
                }
            }
            return BResult;
        }

        public bool TurnOnCyanLight(int nDeviceIndex)
        {
            BResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= MnTotalDevices - 1)
            {
                if(AoDevInfo[nDeviceIndex].ByDeviceType == 1)
                {
                    ByBlyncControlCode = byte.MaxValue;
                    ByBlyncControlCode &= (byte) ~ByBlyncIBuddyLightColorMask;
                    ByBlyncControlCode |= 31;
                    BResult = OUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(AoDevInfo[nDeviceIndex].PHandle,ByBlyncControlCode);
                }
            }
            return BResult;
        }

        public bool TurnOnTestLight(int nDeviceIndex)
        {
            BResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= MnTotalDevices - 1)
            {
                if(AoDevInfo[nDeviceIndex].ByDeviceType == 1)
                {
                    ByBlyncControlCode = byte.MaxValue;
                    ByBlyncControlCode &= (byte) ~ByBlyncIBuddyLightColorMask;
                    ByBlyncControlCode |= 239;
                    
                    BResult = OUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(AoDevInfo[nDeviceIndex].PHandle,ByBlyncControlCode);
                }
            }
            return BResult;
        }


        public void CloseDevices(int nNumberOfDevices)
        {
            for(var index = 0; index < nNumberOfDevices; ++index)
            {
                if(uint.MaxValue != (uint) AoDevInfo[index].PHandle.ToInt64() && AoDevInfo[index].PHandle != IntPtr.Zero && (AoDevInfo[index].ByDeviceType == 1))
                {
                    NativeMethods.CloseHandle(AoDevInfo[index].PHandle);
                    AoDevInfo[index].PHandle = IntPtr.Zero;
                }
            }
        }

        public int InitBlyncDevices()
        {
            var nNumberOfBlyncDevices = 0;
            if(MnTotalDevices > 0)
            {
                CloseDevices(MnTotalDevices);
            }

            MnTotalDevices = 0;
            LookForBlyncDevices(ref nNumberOfBlyncDevices);
            return nNumberOfBlyncDevices;
        }

        public uint GetDeviceUniqueId(int nDeviceIndex)
        {
            uint unUniqueId = 0;
            try
            {
                if(nDeviceIndex >= 0)
                {
                    if(nDeviceIndex <= MnTotalDevices - 1)
                    {
                        if(AoDevInfo[nDeviceIndex].ByDeviceType != 7 && AoDevInfo[nDeviceIndex].ByDeviceType != 12 && (AoDevInfo[nDeviceIndex].ByDeviceType != 6 && AoDevInfo[nDeviceIndex].ByDeviceType != 3))
                        {
                            if(AoDevInfo[nDeviceIndex].ByDeviceType != 4)
                            {
                                goto label_8;
                            }
                        }
                        var flag = OUsbDeviceAccess.GetDeviceUniqueId(AoDevInfo[nDeviceIndex].PHandle,ref unUniqueId);
                        if(!flag)
                        {
                            unUniqueId = 0U;
                        }
                    }
                }
            } catch(Exception)
            {
                unUniqueId = 0U;
            }
        label_8:
            return unUniqueId;
        }

        public bool Display(Color color) => DisplayColor(color);

        public byte GetDeviceType(int nDeviceIndex)
        {
            byte num = 0;
            if(nDeviceIndex >= 0 && nDeviceIndex <= MnTotalDevices - 1)
            {
                num = AoDevInfo[nDeviceIndex].ByDeviceType;
            }

            return num;
        }

        private bool DisplayColor(Color color)
        {
            switch(color.ToString())
            {
                case "Blue":
                return PlayBlyncLightBlue();
                case "Cyan":
                return PlayBlyncLightCyan();
                case "Green":
                return PlayBlyncLightGreen();
                case "None":
                case "Off":
                return PlayBlyncLightOff();
                case "Purple":
                return PlayBlyncLightMagenta();
                case "Red":
                return PlayBlyncLightRed();
                case "White":
                return PlayBlyncLightWhite();
                case "Yellow":
                return PlayBlyncLightYellow();
                case "Test":
                return PlayBlyncLightTest();
                default:
                return false;
            }
        }

        private bool PlayBlyncLightTest()
        {
            for(var nDeviceIndex = 0; nDeviceIndex < MnTotalDevices; ++nDeviceIndex)
            {
                BResult = TurnOnTestLight(nDeviceIndex);
            }
            return BResult;
        }

        private bool PlayBlyncLightCyan()
        {
            for(var nDeviceIndex = 0; nDeviceIndex < MnTotalDevices; ++nDeviceIndex)
            {
                BResult = TurnOnCyanLight(nDeviceIndex);
            }
            return BResult;
        }

        private bool PlayBlyncLightWhite()
        {
            for(var nDeviceIndex = 0; nDeviceIndex < MnTotalDevices; ++nDeviceIndex)
            {
                BResult = TurnOnWhiteLight(nDeviceIndex);
            }
            return BResult;
        }

        private bool PlayBlyncLightBlue()
        {
            for(var nDeviceIndex = 0; nDeviceIndex < MnTotalDevices; ++nDeviceIndex)
            {
                BResult = TurnOnBlueLight(nDeviceIndex);
            }
            return BResult;
        }

        private bool PlayBlyncLightYellow()
        {
            for(var nDeviceIndex = 0; nDeviceIndex < MnTotalDevices; ++nDeviceIndex)
            {
                BResult = TurnOnYellowLight(nDeviceIndex);
            }
            return BResult;
        }

        private bool PlayBlyncLightGreen()
        {
            for(var nDeviceIndex = 0; nDeviceIndex < MnTotalDevices; ++nDeviceIndex)
            {
                BResult = TurnOnGreenLight(nDeviceIndex);
            }
            return BResult;
        }

        private bool PlayBlyncLightRed()
        {
            for(var nDeviceIndex = 0; nDeviceIndex < MnTotalDevices; ++nDeviceIndex)
            {
                BResult = TurnOnRedLight(nDeviceIndex);
            }
            return BResult;
        }

        private bool PlayBlyncLightOff()
        {
            for(var nDeviceIndex = 0; nDeviceIndex < MnTotalDevices; ++nDeviceIndex)
            {
                BResult = ResetLight(nDeviceIndex);
            }
            return BResult;
        }

        private bool PlayBlyncLightMagenta()
        {
            for(var nDeviceIndex = 0; nDeviceIndex < MnTotalDevices; ++nDeviceIndex)
            {
                BResult = TurnOnMagentaLight(nDeviceIndex);
            }
            return BResult;
        }

        public class DeviceInfo
        {
            [MarshalAs(UnmanagedType.ByValTStr,SizeConst = 260)]
            public string SzDevicePath;
            [MarshalAs(UnmanagedType.ByValTStr,SizeConst = 260)]
            public string SzDeviceName;
            public byte ByDeviceType;
            public IntPtr PHandle;
            public int NDeviceIndex;

            internal static DeviceInfo[] NewInitArray(ulong num)
            {
                var deviceInfoArray = new DeviceInfo[num];
                for(ulong index = 0; index < num; ++index)
                {
                    deviceInfoArray[index] = new DeviceInfo();
                }

                return deviceInfoArray;
            }
        }

        public enum Color
        {
            Cyan = 1,
            White = 2,
            Blue = 3,
            Yellow = 4,
            Green = 5,
            Red = 6,
            Off = 7,
            Purple = 8,
            Test = 9,
        }
    }
}
