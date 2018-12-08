// Decompiled with JetBrains decompiler
// Type: Blynclight.BlyncController
// Assembly: Blynclight, Version=0.3.0.11, Culture=neutral, PublicKeyToken=null
// MVID: 5BBF77FB-8F3D-4A6F-B095-307E683776AC
// Assembly location: C:\Users\cecochr\Downloads\Embrava_SDK_For_Windows_v3.0.3\Embrava_SDK_For_Windows_v3.0.3\Binaries\AnyCpu\Blynclight.dll

using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace BlyncN
{
    public class BlyncController
    {
        public DeviceInfo[] AoDevInfo = DeviceInfo.NewInitArray(10UL);
        internal DeviceAccess oUsbDeviceAccess = new DeviceAccess();
        internal byte byBlyncControlCode = byte.MaxValue;
        internal byte byBlyncIBuddyLightColorMask = (byte) 112;
        internal byte byMaskLightOnOff =(byte) 1;
        internal byte byMaskLightDimControl = (byte) 2;
        internal byte byMaskLightFlashOnOff = (byte) 4;
        internal byte byMaskLightFlashSpeed = (byte) 56;
        internal byte byMaskMusicSelect = (byte) 15;
        internal byte byMaskMusicOnOff = (byte) 16;
        internal byte byMaskMusicRepeatOnOff = (byte) 32;
        internal byte byMaskBit6Bit7 = 192;
        internal byte byMaskVolumeControl = (byte) 15;
        internal byte byMaskMute = (byte) 128;
        private string strUserAppDir = "";
        private string strUserBlynclightTestDir = "";
        private string strLogFile = "";
        internal int m_nTotalDevices;
        internal bool bResult;
        internal const byte DEVICETYPE_NODEVICE_INVALIDDEVICE_TYPE = 0;
        internal const byte DEVICETYPE_BLYNC_CHIPSET_TENX_10 = 1;
        internal const byte DEVICETYPE_BLYNC_CHIPSET_TENX_20 = 2;
        internal const byte DEVICETYPE_BLYNC_CHIPSET_V30 = 3;
        internal const byte DEVICETYPE_BLYNC_CHIPSET_V30S = 4;
        internal const byte DEVICETYPE_BLYNC_HEADSET_CHIPSET_V30_LUMENA110 = 5;
        internal const byte DEVICETYPE_BLYNC_WIRELESS_CHIPSET_V30S = 6;
        internal const byte DEVICETYPE_BLYNC_MINI_CHIPSET_V30S = 7;
        internal const byte DEVICETYPE_BLYNC_HEADSET_CHIPSET_V30_LUMENA120 = 8;
        internal const byte DEVICETYPE_BLYNC_HEADSET_CHIPSET_V30_LUMENA = 9;
        internal const byte DEVICETYPE_BLYNC_HEADSET_CHIPSET_V30_LUMENA210 = 10;
        internal const byte DEVICETYPE_BLYNC_HEADSET_CHIPSET_V30_LUMENA220 = 11;
        internal const byte DEVICETYPE_BLYNC_EMBRAVA_EMBEDDED_V30 = 12;
        internal byte byRedValue;
        internal byte byGreenValue;
        internal byte byBlueValue;
        internal byte byLightControl;
        internal byte byMusicControl_1;
        internal byte byMusicControl_2;
        private bool bloggingEnabled;

        public BlyncController() => this.oUsbDeviceAccess.oBlyncController = this;

        private bool LookForBlyncDevices(ref int nNumberOfBlyncDevices)
        {
            this.WriteToLog("LookForBlyncDevices Entry");
            this.bResult = this.oUsbDeviceAccess.GetDevices(ref this.AoDevInfo,ref m_nTotalDevices);
            nNumberOfBlyncDevices = this.m_nTotalDevices;
            this.WriteToLog("LookForBlyncDevices Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool ResetLight(int nDeviceIndex)
        {
            this.WriteToLog("ResetLight Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 2)
                {
                    this.byBlyncControlCode = 115;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx20ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].szDevicePath,this.byBlyncControlCode);
                    this.byBlyncControlCode = 115;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx20ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].szDevicePath,this.byBlyncControlCode);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 1)
                {
                    this.byBlyncControlCode = byte.MaxValue;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byBlyncControlCode);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 9 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 12 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11))
                {
                    this.bResult = this.TurnOffV30Light(nDeviceIndex);
                }
            }
            this.WriteToLog("ResetLight Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool TurnOnRedLight(int nDeviceIndex)
        {
            this.WriteToLog("TurnOnRedLight Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 2)
                {
                    this.byBlyncControlCode = 115;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx20ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].szDevicePath,this.byBlyncControlCode);
                    this.byBlyncControlCode = 96;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx20ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].szDevicePath,this.byBlyncControlCode);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 1)
                {
                    this.byBlyncControlCode = byte.MaxValue;
                    this.byBlyncControlCode &= (byte) ~this.byBlyncIBuddyLightColorMask;
                    this.byBlyncControlCode |= 111;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byBlyncControlCode);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 9 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 12 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11))
                {
                    this.bResult = this.AoDevInfo[nDeviceIndex].byDeviceType == 7 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 ? this.TurnOnRGBLights(nDeviceIndex,128,0,0) : this.TurnOnRGBLights(nDeviceIndex,byte.MaxValue,0,0);
                }
            }
            this.WriteToLog("TurnOnRedLight Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool TurnOnGreenLight(int nDeviceIndex)
        {
            this.WriteToLog("TurnOnGreenLight Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 2)
                {
                    this.byBlyncControlCode = 115;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx20ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].szDevicePath,this.byBlyncControlCode);
                    this.byBlyncControlCode = 216;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx20ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].szDevicePath,this.byBlyncControlCode);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 1)
                {
                    this.byBlyncControlCode = byte.MaxValue;
                    this.byBlyncControlCode &= (byte) ~this.byBlyncIBuddyLightColorMask;
                    this.byBlyncControlCode |= 95;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byBlyncControlCode);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 9 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 12 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11))
                {
                    this.bResult = this.TurnOnRGBLights(nDeviceIndex,0,150,0);
                }
            }
            this.WriteToLog("TurnOnGreenLight Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool TurnOnYellowLight(int nDeviceIndex)
        {
            this.WriteToLog("TurnOnYellowLight Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 2)
                {
                    this.byBlyncControlCode = 115;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx20ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].szDevicePath,this.byBlyncControlCode);
                    this.byBlyncControlCode = 64;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx20ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].szDevicePath,this.byBlyncControlCode);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 1)
                {
                    this.byBlyncControlCode = byte.MaxValue;
                    this.byBlyncControlCode &= (byte) ~this.byBlyncIBuddyLightColorMask;
                    this.byBlyncControlCode |= 79;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byBlyncControlCode);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 9 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 12 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11))
                {
                    this.bResult = this.AoDevInfo[nDeviceIndex].byDeviceType != 7 ? (this.AoDevInfo[nDeviceIndex].byDeviceType != 6 ? this.TurnOnRGBLights(nDeviceIndex,byte.MaxValue,60,0) : this.TurnOnRGBLights(nDeviceIndex,100,60,0)) : this.TurnOnRGBLights(nDeviceIndex,90,60,0);
                }
            }
            this.WriteToLog("TurnOnYellowLight Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool TurnOnBlueLight(int nDeviceIndex)
        {
            this.WriteToLog("TurnOnBlueLight Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 2)
                {
                    this.byBlyncControlCode = 115;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx20ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].szDevicePath,this.byBlyncControlCode);
                    this.byBlyncControlCode = 53;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx20ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].szDevicePath,this.byBlyncControlCode);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 1)
                {
                    this.byBlyncControlCode = byte.MaxValue;
                    this.byBlyncControlCode &= (byte) ~this.byBlyncIBuddyLightColorMask;
                    this.byBlyncControlCode |= 63;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byBlyncControlCode);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 9 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 12 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11))
                {
                    this.bResult = this.TurnOnRGBLights(nDeviceIndex,0,0,150);
                }
            }
            this.WriteToLog("TurnOnBlueLight Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool TurnOnMagentaLight(int nDeviceIndex)
        {
            this.WriteToLog("TurnOnMagentaLight Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 2)
                {
                    this.byBlyncControlCode = 115;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx20ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].szDevicePath,this.byBlyncControlCode);
                    this.byBlyncControlCode = 32;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx20ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].szDevicePath,this.byBlyncControlCode);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 1)
                {
                    this.byBlyncControlCode = byte.MaxValue;
                    this.byBlyncControlCode &= (byte) ~this.byBlyncIBuddyLightColorMask;
                    this.byBlyncControlCode |= 47;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byBlyncControlCode);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 9 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 12 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11))
                {
                    this.bResult = this.AoDevInfo[nDeviceIndex].byDeviceType != 6 ? this.TurnOnRGBLights(nDeviceIndex,128,0,128) : this.TurnOnRGBLights(nDeviceIndex,68,0,128);
                }
            }
            this.WriteToLog("TurnOnMagentaLight Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool TurnOnWhiteLight(int nDeviceIndex)
        {
            this.WriteToLog("TurnOnWhiteLight Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 2)
                {
                    this.byBlyncControlCode = 115;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx20ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].szDevicePath,this.byBlyncControlCode);
                    this.byBlyncControlCode = 7;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx20ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].szDevicePath,this.byBlyncControlCode);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 1)
                {
                    this.byBlyncControlCode = byte.MaxValue;
                    this.byBlyncControlCode &= (byte) ~this.byBlyncIBuddyLightColorMask;
                    this.byBlyncControlCode |= 15;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byBlyncControlCode);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 9 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 12 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11))
                {
                    this.bResult = this.TurnOnRGBLights(nDeviceIndex,byte.MaxValue,125,50);
                }
            }
            this.WriteToLog("TurnOnWhiteLight Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool TurnOnCyanLight(int nDeviceIndex)
        {
            this.WriteToLog("TurnOnCyanLight Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 2)
                {
                    this.byBlyncControlCode = 115;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx20ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].szDevicePath,this.byBlyncControlCode);
                    this.byBlyncControlCode = 23;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx20ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].szDevicePath,this.byBlyncControlCode);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 1)
                {
                    this.byBlyncControlCode = byte.MaxValue;
                    this.byBlyncControlCode &= (byte) ~this.byBlyncIBuddyLightColorMask;
                    this.byBlyncControlCode |= 31;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncTenx10ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byBlyncControlCode);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 9 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 12 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11))
                {
                    this.bResult = this.TurnOnRGBLights(nDeviceIndex,0,byte.MaxValue,byte.MaxValue);
                }
            }
            this.WriteToLog("TurnOnCyanLight Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool TurnOnOrangeLight(int nDeviceIndex)
        {
            this.WriteToLog("TurnOnOrangeLight Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1 && (this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 9 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 12 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11)))
            {
                this.bResult = this.AoDevInfo[nDeviceIndex].byDeviceType != 7 ? (this.AoDevInfo[nDeviceIndex].byDeviceType != 6 ? this.TurnOnRGBLights(nDeviceIndex,byte.MaxValue,15,0) : this.TurnOnRGBLights(nDeviceIndex,100,15,0)) : this.TurnOnRGBLights(nDeviceIndex,90,20,0);
            }

            this.WriteToLog("TurnOnOrangeLight Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public void CloseDevices(int nNumberOfDevices)
        {
            this.WriteToLog("CloseDevices Entry");
            for(var index = 0; index < nNumberOfDevices; ++index)
            {
                if(uint.MaxValue != (uint) this.AoDevInfo[index].pHandle.ToInt64() && this.AoDevInfo[index].pHandle != IntPtr.Zero && (this.AoDevInfo[index].byDeviceType == 1 || this.AoDevInfo[index].byDeviceType == 3 || (this.AoDevInfo[index].byDeviceType == 4 || this.AoDevInfo[index].byDeviceType == 5) || (this.AoDevInfo[index].byDeviceType == 8 || this.AoDevInfo[index].byDeviceType == 9 || (this.AoDevInfo[index].byDeviceType == 6 || this.AoDevInfo[index].byDeviceType == 12)) || (this.AoDevInfo[index].byDeviceType == 7 || this.AoDevInfo[index].byDeviceType == 10 || this.AoDevInfo[index].byDeviceType == 11)))
                {
                    NativeMethods.CloseHandle(this.AoDevInfo[index].pHandle);
                    this.AoDevInfo[index].pHandle = IntPtr.Zero;
                }
            }
            this.WriteToLog("CloseDevices Exit");
        }

        public int InitBlyncDevices()
        {
            this.WriteToLog("InitBlyncDevices Entry");
            var nNumberOfBlyncDevices = 0;
            if(this.m_nTotalDevices > 0)
            {
                this.CloseDevices(this.m_nTotalDevices);
            }

            this.m_nTotalDevices = 0;
            this.LookForBlyncDevices(ref nNumberOfBlyncDevices);
            this.WriteToLog("InitBlyncDevices Exit: " + nNumberOfBlyncDevices);
            return nNumberOfBlyncDevices;
        }

        public bool TurnOnRGBLights(int nDeviceIndex,byte byRedLevel,byte byGreenLevel,byte byBlueLevel)
        {
            this.WriteToLog("TurnOnRGBLights Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1 && (this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 9 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 12 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)) || (this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11)))
            {
                this.bResult = this.TurnOnV30Light(nDeviceIndex);
                this.bResult = this.SetRedColorBrightnessLevel(nDeviceIndex,byRedLevel);
                if(this.bResult)
                {
                    this.bResult = this.SetGreenColorBrightnessLevel(nDeviceIndex,byGreenLevel);
                    if(this.bResult)
                    {
                        this.bResult = this.SetBlueColorBrightnessLevel(nDeviceIndex,byBlueLevel);
                        var num = this.bResult ? 1 : 0;
                    }
                }
            }
            this.WriteToLog("TurnOnRGBLights Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool SelectMusicToPlay(int nDeviceIndex,byte bySelectedMusic)
        {
            this.WriteToLog("SelectMusicToPlay Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1 && (this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7))
            {
                this.byMusicControl_1 &= (byte) ~this.byMaskMusicSelect;
                this.byMusicControl_1 |= (byte) (bySelectedMusic & 15U);
                this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
            }
            this.WriteToLog("SelectMusicToPlay Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool StartMusicPlay(int nDeviceIndex)
        {
            this.WriteToLog("StartMusicPlay Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1 && (this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7))
            {
                this.byMusicControl_1 |= this.byMaskMusicOnOff;
                this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
            }
            this.WriteToLog("StartMusicPlay Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool StopMusicPlay(int nDeviceIndex)
        {
            this.WriteToLog("StopMusicPlay Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1 && (this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7))
            {
                this.byMusicControl_1 &= (byte) ~this.byMaskMusicOnOff;
                this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
            }
            this.WriteToLog("StopMusicPlay Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool SetLightDim(int nDeviceIndex)
        {
            this.WriteToLog("SetLightDim Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 12) || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)
                {
                    this.byLightControl |= this.byMaskLightDimControl;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8 || this.AoDevInfo[nDeviceIndex].byDeviceType == 9)
                {
                    this.byLightControl |= this.byMaskLightDimControl;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbHeadset30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11)
                {
                    this.byLightControl |= this.byMaskLightDimControl;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbBluetoothHeadsetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                }
            }
            this.WriteToLog("SetLightDim Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool ClearLightDim(int nDeviceIndex)
        {
            this.WriteToLog("ClearLightDim Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 12) || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)
                {
                    this.byLightControl &= (byte) ~this.byMaskLightDimControl;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8 || this.AoDevInfo[nDeviceIndex].byDeviceType == 9)
                {
                    this.byLightControl &= (byte) ~this.byMaskLightDimControl;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbHeadset30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11)
                {
                    this.byLightControl &= (byte) ~this.byMaskLightDimControl;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbBluetoothHeadsetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                }
            }
            this.WriteToLog("ClearLightDim Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool SelectLightFlashSpeed(int nDeviceIndex,byte bySelectedFlashSpeed)
        {
            this.WriteToLog("SelectLightFlashSpeed Entry");
            this.bResult = false;
            if(bySelectedFlashSpeed == 3)
            {
                bySelectedFlashSpeed = 4;
            }

            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 12) || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)
                {
                    this.byLightControl &= (byte) ~this.byMaskLightFlashSpeed;
                    this.byLightControl |= (byte) ((bySelectedFlashSpeed & 15) << 3);
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8 || this.AoDevInfo[nDeviceIndex].byDeviceType == 9)
                {
                    this.byLightControl &= (byte) ~this.byMaskLightFlashSpeed;
                    this.byLightControl |= (byte) ((bySelectedFlashSpeed & 15) << 3);
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbHeadset30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11)
                {
                    this.byLightControl &= (byte) ~this.byMaskLightFlashSpeed;
                    this.byLightControl |= (byte) ((bySelectedFlashSpeed & 15) << 3);
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbBluetoothHeadsetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                }
            }
            this.WriteToLog("SelectLightFlashSpeed Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool StartLightFlash(int nDeviceIndex)
        {
            this.WriteToLog("StartLightFlash Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 12) || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)
                {
                    this.byLightControl |= this.byMaskLightFlashOnOff;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8 || this.AoDevInfo[nDeviceIndex].byDeviceType == 9)
                {
                    this.byLightControl |= this.byMaskLightFlashOnOff;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbHeadset30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11)
                {
                    this.byLightControl |= this.byMaskLightFlashOnOff;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbBluetoothHeadsetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                }
            }
            this.WriteToLog("StartLightFlash Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool StopLightFlash(int nDeviceIndex)
        {
            this.WriteToLog("StopLightFlash Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 12) || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)
                {
                    this.byLightControl &= (byte) ~this.byMaskLightFlashOnOff;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8 || this.AoDevInfo[nDeviceIndex].byDeviceType == 9)
                {
                    this.byLightControl &= (byte) ~this.byMaskLightFlashOnOff;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbHeadset30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11)
                {
                    this.byLightControl &= (byte) ~this.byMaskLightFlashOnOff;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbBluetoothHeadsetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                }
            }
            this.WriteToLog("StopLightFlash Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool SetMusicRepeat(int nDeviceIndex)
        {
            this.WriteToLog("SetMusicRepeat Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1 && (this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7))
            {
                this.byMusicControl_1 |= this.byMaskMusicRepeatOnOff;
                this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
            }
            this.WriteToLog("SetMusicRepeat Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool ClearMusicRepeat(int nDeviceIndex)
        {
            this.WriteToLog("ClearMusicRepeat Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1 && (this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7))
            {
                this.byMusicControl_1 &= (byte) ~this.byMaskMusicRepeatOnOff;
                this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
            }
            this.WriteToLog("ClearMusicRepeat Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool SetVolumeMute(int nDeviceIndex)
        {
            this.WriteToLog("SetVolumeMute Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1 && (this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7))
            {
                this.byMusicControl_2 |= this.byMaskMute;
                this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
            }
            this.WriteToLog("SetVolumeMute Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool ClearVolumeMute(int nDeviceIndex)
        {
            this.WriteToLog("ClearVolumeMute Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1 && (this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7))
            {
                this.byMusicControl_2 &= (byte) ~this.byMaskMute;
                this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
            }
            this.WriteToLog("ClearVolumeMute Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public bool SetMusicVolume(int nDeviceIndex,byte byVolumeLevel)
        {
            this.WriteToLog("SetMusicVolume Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1 && (this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 7))
            {
                this.byMusicControl_2 &= (byte) ~this.byMaskVolumeControl;
                this.byMusicControl_2 |= byVolumeLevel;
                this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
            }
            this.WriteToLog("SetMusicVolume Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public uint GetDeviceUniqueId(int nDeviceIndex)
        {
            uint unUniqueId = 0;
            var flag = true;
            this.WriteToLog("GetDeviceUniqueId Entry");
            try
            {
                if(nDeviceIndex >= 0)
                {
                    if(nDeviceIndex <= this.m_nTotalDevices - 1)
                    {
                        if(this.AoDevInfo[nDeviceIndex].byDeviceType != 7 && this.AoDevInfo[nDeviceIndex].byDeviceType != 12 && (this.AoDevInfo[nDeviceIndex].byDeviceType != 6 && this.AoDevInfo[nDeviceIndex].byDeviceType != 3))
                        {
                            if(this.AoDevInfo[nDeviceIndex].byDeviceType != 4)
                            {
                                goto label_8;
                            }
                        }
                        flag = this.oUsbDeviceAccess.GetDeviceUniqueId(this.AoDevInfo[nDeviceIndex].pHandle,ref unUniqueId);
                        if(!flag)
                        {
                            unUniqueId = 0U;
                        }
                    }
                }
            } catch(Exception ex)
            {
                unUniqueId = 0U;
            }
        label_8:
            this.WriteToLog("Device UniqueId: " + unUniqueId);
            this.WriteToLog("GetDeviceUniqueId Exit: " + flag.ToString());
            return unUniqueId;
        }

        public bool Display(BlyncController.Color color) => this.DisplayColor(color);

        public byte GetDeviceType(int nDeviceIndex)
        {
            byte num = 0;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                num = this.AoDevInfo[nDeviceIndex].byDeviceType;
            }

            return num;
        }

        private bool DisplayColor(BlyncController.Color color)
        {
            this.WriteToLog("DisplayColor Entry");
            switch(color.ToString())
            {
                case "Blue":
                return this.PlayBlyncLightBlue();
                case "Cyan":
                return this.PlayBlyncLightCyan();
                case "Green":
                return this.PlayBlyncLightGreen();
                case "None":
                case "Off":
                return this.PlayBlyncLightOff();
                case "Orange":
                return this.PlayBlyncLightOrange();
                case "Purple":
                return this.PlayBlyncLightMagenta();
                case "Red":
                return this.PlayBlyncLightRed();
                case "White":
                return this.PlayBlyncLightWhite();
                case "Yellow":
                return this.PlayBlyncLightYellow();
                default:
                this.WriteToLog("DisplayColor Exit: " + this.bResult.ToString());
                return false;
            }
        }

        private bool PlayBlyncLightCyan()
        {
            this.WriteToLog("PlayBlyncLightCyan Entry");
            for(var nDeviceIndex = 0; nDeviceIndex < this.m_nTotalDevices; ++nDeviceIndex)
            {
                this.bResult = this.TurnOnCyanLight(nDeviceIndex);
            }

            this.WriteToLog("PlayBlyncLightCyan Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        private bool PlayBlyncLightWhite()
        {
            this.WriteToLog("PlayBlyncLightWhite Entry");
            for(var nDeviceIndex = 0; nDeviceIndex < this.m_nTotalDevices; ++nDeviceIndex)
            {
                this.bResult = this.TurnOnWhiteLight(nDeviceIndex);
            }

            this.WriteToLog("PlayBlyncLightWhite Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        private bool PlayBlyncLightBlue()
        {
            this.WriteToLog("PlayBlyncLightBlue Entry");
            for(var nDeviceIndex = 0; nDeviceIndex < this.m_nTotalDevices; ++nDeviceIndex)
            {
                this.bResult = this.TurnOnBlueLight(nDeviceIndex);
            }

            this.WriteToLog("PlayBlyncLightBlue Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        private bool PlayBlyncLightYellow()
        {
            this.WriteToLog("PlayBlyncLightYellow Entry");
            for(var nDeviceIndex = 0; nDeviceIndex < this.m_nTotalDevices; ++nDeviceIndex)
            {
                this.bResult = this.TurnOnYellowLight(nDeviceIndex);
            }

            this.WriteToLog("PlayBlyncLightYellow Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        private bool PlayBlyncLightGreen()
        {
            this.WriteToLog("PlayBlyncLightGreen Entry");
            for(var nDeviceIndex = 0; nDeviceIndex < this.m_nTotalDevices; ++nDeviceIndex)
            {
                this.bResult = this.TurnOnGreenLight(nDeviceIndex);
            }

            this.WriteToLog("PlayBlyncLightGreen Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        private bool PlayBlyncLightRed()
        {
            this.WriteToLog("PlayBlyncLightRed Entry");
            for(var nDeviceIndex = 0; nDeviceIndex < this.m_nTotalDevices; ++nDeviceIndex)
            {
                this.bResult = this.TurnOnRedLight(nDeviceIndex);
            }

            this.WriteToLog("PlayBlyncLightRed Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        private bool PlayBlyncLightOff()
        {
            this.WriteToLog("PlayBlyncLightOff Entry");
            for(var nDeviceIndex = 0; nDeviceIndex < this.m_nTotalDevices; ++nDeviceIndex)
            {
                this.bResult = this.ResetLight(nDeviceIndex);
            }

            this.WriteToLog("PlayBlyncLightOff Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        private bool PlayBlyncLightMagenta()
        {
            this.WriteToLog("PlayBlyncLightMagenta Entry");
            for(var nDeviceIndex = 0; nDeviceIndex < this.m_nTotalDevices; ++nDeviceIndex)
            {
                this.bResult = this.TurnOnMagentaLight(nDeviceIndex);
            }

            this.WriteToLog("PlayBlyncLightMagenta Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        private bool PlayBlyncLightOrange()
        {
            this.WriteToLog("PlayBlyncLightOrange Entry");
            for(var nDeviceIndex = 0; nDeviceIndex < this.m_nTotalDevices; ++nDeviceIndex)
            {
                this.bResult = this.TurnOnOrangeLight(nDeviceIndex);
            }

            this.WriteToLog("PlayBlyncLightOrange Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        private bool TurnOnV30Light(int nDeviceIndex)
        {
            this.WriteToLog("TurnOnV30Light Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 12) || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)
                {
                    this.byLightControl &= (byte) ~this.byMaskLightOnOff;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8 || this.AoDevInfo[nDeviceIndex].byDeviceType == 9)
                {
                    this.byLightControl &= (byte) ~this.byMaskLightOnOff;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbHeadset30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11)
                {
                    this.byLightControl &= (byte) ~this.byMaskLightOnOff;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbBluetoothHeadsetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                }
            }
            this.WriteToLog("TurnOnV30Light Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        private bool TurnOffV30Light(int nDeviceIndex)
        {
            this.WriteToLog("TurnOffV30Light Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 12) || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)
                {
                    this.byLightControl |= this.byMaskLightOnOff;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8 || this.AoDevInfo[nDeviceIndex].byDeviceType == 9)
                {
                    this.byLightControl |= this.byMaskLightOnOff;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbHeadset30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11)
                {
                    this.byLightControl |= this.byMaskLightOnOff;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbBluetoothHeadsetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                }
            }
            this.WriteToLog("TurnOffV30Light Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        private bool SetRedColorBrightnessLevel(int nDeviceIndex,byte byBrightnessLevel)
        {
            this.WriteToLog("SetRedColorBrightnessLevel Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 12) || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)
                {
                    this.byRedValue = byBrightnessLevel;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8 || this.AoDevInfo[nDeviceIndex].byDeviceType == 9)
                {
                    this.byRedValue = byBrightnessLevel;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbHeadset30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11)
                {
                    this.byRedValue = byBrightnessLevel;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbBluetoothHeadsetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                }
            }
            this.WriteToLog("SetRedColorBrightnessLevel Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        private bool SetGreenColorBrightnessLevel(int nDeviceIndex,byte byBrightnessLevel)
        {
            this.WriteToLog("SetGreenColorBrightnessLevel Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 12) || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)
                {
                    this.byGreenValue = byBrightnessLevel;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8 || this.AoDevInfo[nDeviceIndex].byDeviceType == 9)
                {
                    this.byGreenValue = byBrightnessLevel;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbHeadset30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11)
                {
                    this.byGreenValue = byBrightnessLevel;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbBluetoothHeadsetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                }
            }
            this.WriteToLog("SetGreenColorBrightnessLevel Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        private bool SetBlueColorBrightnessLevel(int nDeviceIndex,byte byBrightnessLevel)
        {
            this.WriteToLog("SetBlueColorBrightnessLevel Entry");
            this.bResult = false;
            if(nDeviceIndex >= 0 && nDeviceIndex <= this.m_nTotalDevices - 1)
            {
                if(this.AoDevInfo[nDeviceIndex].byDeviceType == 3 || this.AoDevInfo[nDeviceIndex].byDeviceType == 4 || (this.AoDevInfo[nDeviceIndex].byDeviceType == 6 || this.AoDevInfo[nDeviceIndex].byDeviceType == 12) || this.AoDevInfo[nDeviceIndex].byDeviceType == 7)
                {
                    this.byBlueValue = byBrightnessLevel;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsb30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl,this.byMusicControl_1,this.byMusicControl_2);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 5 || this.AoDevInfo[nDeviceIndex].byDeviceType == 8 || this.AoDevInfo[nDeviceIndex].byDeviceType == 9)
                {
                    this.byBlueValue = byBrightnessLevel;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbHeadset30ChipSetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                } else if(this.AoDevInfo[nDeviceIndex].byDeviceType == 10 || this.AoDevInfo[nDeviceIndex].byDeviceType == 11)
                {
                    this.byBlueValue = byBrightnessLevel;
                    this.bResult = this.oUsbDeviceAccess.SendBlyncUsbBluetoothHeadsetControlCommand(this.AoDevInfo[nDeviceIndex].pHandle,this.byRedValue,this.byGreenValue,this.byBlueValue,this.byLightControl);
                }
            }
            this.WriteToLog("SetBlueColorBrightnessLevel Exit: " + this.bResult.ToString());
            return this.bResult;
        }

        public void InitLogging()
        {
            this.bloggingEnabled = true;
            this.InitLogging(true);
        }

        private void InitLogging(bool bDeleteExistingLogFile)
        {
            if(!this.bloggingEnabled)
            {
                return;
            }

            this.strUserAppDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            this.strUserBlynclightTestDir = this.strUserAppDir + "\\BlynclightTest";
            try
            {
                if(!Directory.Exists(this.strUserBlynclightTestDir))
                {
                    Directory.CreateDirectory(this.strUserBlynclightTestDir);
                }

                this.strLogFile = this.strUserBlynclightTestDir + "\\dbglog_blynclightlib.txt";
                if(bDeleteExistingLogFile && File.Exists(this.strLogFile))
                {
                    File.Delete(this.strLogFile);
                }

                if(File.Exists(this.strLogFile))
                {
                    return;
                }

                File.Create(this.strLogFile).Close();
                File.AppendAllText(this.strLogFile,"Debug Log written by Blynclight Library:");
                File.AppendAllText(this.strLogFile,Environment.NewLine);
            } catch(Exception ex)
            {
            }
        }

        internal void WriteToLog(string logText)
        {
            if(!this.bloggingEnabled)
            {
                return;
            }

            try
            {
                this.InitLogging(false);
                File.AppendAllText(this.strLogFile,DateTime.Now.ToString("HH:mm:ss.fff",DateTimeFormatInfo.InvariantInfo) + "    ");
                File.AppendAllText(this.strLogFile,logText);
                File.AppendAllText(this.strLogFile,Environment.NewLine);
            } catch(Exception ex)
            {
            }
        }

        public class DeviceInfo
        {
            public DeviceAccessDeclarations.OVERLAPPED oNativeOverlapped = new DeviceAccessDeclarations.OVERLAPPED();
            [MarshalAs(UnmanagedType.ByValTStr,SizeConst = 260)]
            public string szDevicePath;
            [MarshalAs(UnmanagedType.ByValTStr,SizeConst = 260)]
            public string szDeviceName;
            public byte byDeviceType;
            public IntPtr pHandle;
            public int nDeviceIndex;

            internal static BlyncController.DeviceInfo[] NewInitArray(ulong num)
            {
                var deviceInfoArray = new BlyncController.DeviceInfo[num];
                for(ulong index = 0; index < num; ++index)
                {
                    deviceInfoArray[index] = new BlyncController.DeviceInfo();
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
            Orange = 9,
        }

        public enum Music
        {
            Standard = 1,
            Ringading = 2,
            Invader = 3,
            Crystal = 4,
            Millipede = 5,
            Azure = 6,
            Cogi = 7,
            Techzor = 8,
            Freedom = 9,
            Circuit = 10, // 0x0000000A
            NoSound = 11, // 0x0000000B
        }
    }
}
