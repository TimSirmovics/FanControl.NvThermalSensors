using System;
using System.Runtime.InteropServices;

namespace FanControl.NvThermalSensors
{
    internal static class NvApi
    {
        [DllImport(@"nvapi.dll", EntryPoint = @"nvapi_QueryInterface", CallingConvention = CallingConvention.Cdecl, PreserveSig = true)]
        private static extern IntPtr NvAPI32_QueryInterface(uint interfaceId);

        [DllImport(@"nvapi64.dll", EntryPoint = @"nvapi_QueryInterface", CallingConvention = CallingConvention.Cdecl, PreserveSig = true)]
        private static extern IntPtr NvAPI64_QueryInterface(uint interfaceId);

        internal const int MAX_PHYSICAL_GPUS = 64;
        internal const int SHORT_STRING_MAX = 64;
        internal const int THERMAL_SENSOR_RESERVED_COUNT = 8;
        internal const int THERMAL_SENSOR_TEMPERATURE_COUNT = 32;

        internal static readonly NvAPI_EnumPhysicalGPUsDelegate NvAPI_EnumPhysicalGPUs;
        internal static readonly NvAPI_GPU_GetFullNameDelegate NvAPI_GPU_GetFullName;
        internal static readonly NvAPI_GPU_GetThermalSensorsDelegate NvAPI_GPU_ThermalGetSensors;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate NvStatus NvAPI_EnumPhysicalGPUsDelegate([Out] NvPhysicalGpuHandle[] gpuHandles, out int gpuCount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate NvStatus NvAPI_GPU_GetFullNameDelegate(NvPhysicalGpuHandle gpuHandle, ref NvShortString name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate NvStatus NvAPI_GPU_GetThermalSensorsDelegate(NvPhysicalGpuHandle gpuHandle, ref NvThermalSensors nvThermalSensors);

        static NvApi()
        {
            GetDelegate(0xE5AC921F, out NvAPI_EnumPhysicalGPUs);
            GetDelegate(0xCEEE8E9F, out NvAPI_GPU_GetFullName);
            GetDelegate(0x65FE3AAD, out NvAPI_GPU_ThermalGetSensors);
        }

        internal enum NvStatus
        {
            OK = 0,
            Error = -1,
            LibraryNotFound = -2,
            NoImplementation = -3,
            ApiNotInitialized = -4,
            InvalidArgument = -5,
            NvidiaDeviceNotFound = -6,
            EndEnumeration = -7,
            InvalidHandle = -8,
            IncompatibleStructVersion = -9,
            HandleInvalidated = -10,
            OpenGlContextNotCurrent = -11,
            NoGlExpert = -12,
            InstrumentationDisabled = -13,
            ExpectedLogicalGpuHandle = -100,
            ExpectedPhysicalGpuHandle = -101,
            ExpectedDisplayHandle = -102,
            InvalidCombination = -103,
            NotSupported = -104,
            PortIdNotFound = -105,
            ExpectedUnattachedDisplayHandle = -106,
            InvalidPerfLevel = -107,
            DeviceBusy = -108,
            NvPersistFileNotFound = -109,
            PersistDataNotFound = -110,
            ExpectedTvDisplay = -111,
            ExpectedTvDisplayOnConnector = -112,
            NoActiveSliTopology = -113,
            SliRenderingModeNotAllowed = -114,
            ExpectedDigitalFlatPanel = -115,
            ArgumentExceedMaxSize = -116,
            DeviceSwitchingNotAllowed = -117,
            TestingClocksNotSupported = -118,
            UnknownUnderscanConfig = -119,
            TimeoutReconfiguringGpuTopo = -120,
            DataNotFound = -121,
            ExpectedAnalogDisplay = -122,
            NoVidLink = -123,
            RequiresReboot = -124,
            InvalidHybridMode = -125,
            MixedTargetTypes = -126,
            Syswow64NotSupported = -127,
            ImplicitSetGpuTopologyChangeNotAllowed = -128,
            RequestUserToCloseNonMigratableApps = -129,
            OutOfMemory = -130,
            WasStillDrawing = -131,
            FileNotFound = -132,
            TooManyUniqueStateObjects = -133,
            InvalidCall = -134,
            D3D101LibraryNotFound = -135,
            FunctionNotFound = -136
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        internal struct NvShortString
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = SHORT_STRING_MAX)]
            internal string Value;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct NvPhysicalGpuHandle
        {
            private readonly IntPtr ptr;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        internal struct NvThermalSensors
        {
            internal uint Version;
            internal uint Mask;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = THERMAL_SENSOR_RESERVED_COUNT)]
            internal int[] Reserved;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = THERMAL_SENSOR_TEMPERATURE_COUNT)]
            internal int[] Temperatures;
        }

        internal static int MAKE_NVAPI_VERSION<T>(int ver)
        {
            return Marshal.SizeOf<T>() | (ver << 16);
        }

        private static void GetDelegate<T>(uint id, out T newDelegate) where T : class
        {
            IntPtr ptr = Environment.Is64BitProcess ? NvAPI64_QueryInterface(id) : NvAPI32_QueryInterface(id);

            if (ptr != IntPtr.Zero)
                newDelegate = Marshal.GetDelegateForFunctionPointer(ptr, typeof(T)) as T;
            else
                newDelegate = null;
        }
    }
}
