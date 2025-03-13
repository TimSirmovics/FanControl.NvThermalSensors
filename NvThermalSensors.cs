using System.Collections.Generic;

namespace FanControl.NvThermalSensors
{
    internal class NvThermalSensors
    {
        internal NvThermalSensors(int gpuIndex, NvApi.NvPhysicalGpuHandle handle)
        {
            Sensors = new List<NvThermalSensor>();

            var mask = FindThermalSensorMask(handle);

            if (mask == 0)
                return;

            var gpuName = GetGpuName(handle, gpuIndex);
            var sensorcount = GetSensorCount(mask);

            if (GetSensorValue(handle, mask, 1) != null)
                Sensors.Add(new NvThermalSensor(gpuIndex, $"{gpuName} - Hot Spot", () => GetSensorValue(handle, mask, 1)));
            if (GetSensorValue(handle, mask, sensorcount - 1) != null)
                Sensors.Add(new NvThermalSensor(gpuIndex, $"{gpuName} - Memory Junction", () => GetSensorValue(handle, mask, sensorcount - 1)));
        }

        internal List<NvThermalSensor> Sensors { get; }

        private uint FindThermalSensorMask(NvApi.NvPhysicalGpuHandle handle)
        {
            uint mask = 0;
            for (var thermalSensorsMaxBit = 0; thermalSensorsMaxBit < 32; thermalSensorsMaxBit++)
            {
                mask = 1u << thermalSensorsMaxBit;

                GetThermalSensors(handle, mask, out NvApi.NvStatus thermalSensorsStatus);
                if (thermalSensorsStatus != NvApi.NvStatus.OK)
                    break;
            }

            return --mask;
        }

        private int GetSensorCount(uint mask)
        {
            var sensorCount = 0;

            while (mask > 0)
            {
                mask &= (mask - 1);
                sensorCount++;
            }

            return sensorCount;
        }

        private NvApi.NvThermalSensors GetThermalSensors(NvApi.NvPhysicalGpuHandle handle, uint mask, out NvApi.NvStatus status)
        {
            var thermalSensors = new NvApi.NvThermalSensors()
            {
                Version = (uint)NvApi.MAKE_NVAPI_VERSION<NvApi.NvThermalSensors>(2),
                Mask = mask
            };

            status = NvApi.NvAPI_GPU_ThermalGetSensors(handle, ref thermalSensors);
            return status == NvApi.NvStatus.OK ? thermalSensors : default;
        }

        private string GetGpuName(NvApi.NvPhysicalGpuHandle handle, int gpuIndex)
        {
            var gpuName = new NvApi.NvShortString();
            var status = NvApi.NvAPI_GPU_GetFullName(handle, ref gpuName);

            return status == NvApi.NvStatus.OK ? gpuName.Value.Trim() : $"GPU - {gpuIndex}";
        }

        private float? GetSensorValue(NvApi.NvPhysicalGpuHandle handle, uint mask, int index)
        {
            var thermalSensors = GetThermalSensors(handle, mask, out NvApi.NvStatus thermalSensorsStatus);
            if (thermalSensorsStatus != NvApi.NvStatus.OK)
                return null;

            return thermalSensors.Temperatures[index] / 256.0f;
        }
    }
}
