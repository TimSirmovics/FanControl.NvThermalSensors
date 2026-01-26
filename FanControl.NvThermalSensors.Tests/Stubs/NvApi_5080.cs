namespace FanControl.NvThermalSensors.Tests.Stubs
{
    internal class NvApi_5080 : INvApi
    {
        public NvApi.NvStatus NvAPI_GPU_GetFullName(NvApi.NvPhysicalGpuHandle gpuHandle, ref NvApi.NvShortString name)
        {
            name = new NvApi.NvShortString
            {
                Value = "NVIDIA GeForce RTX 5080"
            };

            return NvApi.NvStatus.OK;
        }

        public NvApi.NvStatus NvAPI_GPU_ThermalGetSensors(NvApi.NvPhysicalGpuHandle gpuHandle, ref NvApi.NvThermalSensors nvThermalSensors)
        {
            if (nvThermalSensors.Mask > 1048575)
                return NvApi.NvStatus.InvalidArgument;

            nvThermalSensors.Temperatures = new int[32];

            nvThermalSensors.Temperatures[0] = -1;
            nvThermalSensors.Temperatures[1] = -2;
            nvThermalSensors.Temperatures[2] = 100 * 256;
            nvThermalSensors.Temperatures[3] = -4;
            nvThermalSensors.Temperatures[4] = -5;
            nvThermalSensors.Temperatures[5] = -6;
            nvThermalSensors.Temperatures[6] = -7;
            nvThermalSensors.Temperatures[7] = -8;
            nvThermalSensors.Temperatures[8] = -9;
            nvThermalSensors.Temperatures[9] = -10;
            nvThermalSensors.Temperatures[10] = -11;
            nvThermalSensors.Temperatures[11] = -12;
            nvThermalSensors.Temperatures[12] = -13;
            nvThermalSensors.Temperatures[13] = -14;
            nvThermalSensors.Temperatures[14] = -15;
            nvThermalSensors.Temperatures[15] = -16;
            nvThermalSensors.Temperatures[16] = -17;
            nvThermalSensors.Temperatures[17] = -18;
            nvThermalSensors.Temperatures[18] = -19;

            return NvApi.NvStatus.OK;
        }
    }
}
