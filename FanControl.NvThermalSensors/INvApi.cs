using static FanControl.NvThermalSensors.NvApi;

namespace FanControl.NvThermalSensors
{
    internal interface INvApi
    {
        NvStatus NvAPI_GPU_GetFullName(NvPhysicalGpuHandle gpuHandle, ref NvShortString name);

        NvStatus NvAPI_GPU_ThermalGetSensors(NvPhysicalGpuHandle gpuHandle, ref NvApi.NvThermalSensors nvThermalSensors);
    }
}
