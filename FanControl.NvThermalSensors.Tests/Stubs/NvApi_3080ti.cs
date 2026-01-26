namespace FanControl.NvThermalSensors.Tests.Stubs
{
    internal class NvApi_3080ti : INvApi
    {
        public NvApi.NvStatus NvAPI_GPU_GetFullName(NvApi.NvPhysicalGpuHandle gpuHandle, ref NvApi.NvShortString name)
        {
            name = new NvApi.NvShortString
            {
                Value = "NVIDIA GeForce RTX 3080 Ti"
            };

            return NvApi.NvStatus.OK;
        }

        public NvApi.NvStatus NvAPI_GPU_ThermalGetSensors(NvApi.NvPhysicalGpuHandle gpuHandle, ref NvApi.NvThermalSensors nvThermalSensors)
        {
            if (nvThermalSensors.Mask > 1023)
                return NvApi.NvStatus.InvalidArgument;

            nvThermalSensors.Temperatures = new int[32];

            nvThermalSensors.Temperatures[0] = -1;
            nvThermalSensors.Temperatures[1] = 50 * 256;
            nvThermalSensors.Temperatures[2] = -3;
            nvThermalSensors.Temperatures[3] = -4;
            nvThermalSensors.Temperatures[4] = -5;
            nvThermalSensors.Temperatures[5] = -6;
            nvThermalSensors.Temperatures[6] = -7;
            nvThermalSensors.Temperatures[7] = -8;
            nvThermalSensors.Temperatures[8] = -9;
            nvThermalSensors.Temperatures[9] = 100 * 256;

            return NvApi.NvStatus.OK;
        }
    }
}
