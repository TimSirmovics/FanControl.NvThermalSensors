using System.Runtime.InteropServices;

namespace FanControl.NvThermalSensors
{
    internal class NvApiAdapter : INvApi
    {
        public NvApi.NvStatus NvAPI_GPU_GetFullName(NvApi.NvPhysicalGpuHandle gpuHandle, ref NvApi.NvShortString name)
        {
            return NvApi.NvAPI_GPU_GetFullName(gpuHandle, ref name);
        }

        public NvApi.NvStatus NvAPI_GPU_ThermalGetSensors(NvApi.NvPhysicalGpuHandle gpuHandle, ref NvApi.NvThermalSensors nvThermalSensors)
        {
            return NvApi.NvAPI_GPU_ThermalGetSensors(gpuHandle, ref nvThermalSensors);
        }
    }
}
