using FanControl.Plugins;
using System;

namespace FanControl.NvThermalSensors
{
    public class NvThermalSensor : IPluginSensor
    {
        private readonly Func<float> _getSensorValue;

        public NvThermalSensor(int gpuIndex, string sensorName, Func<float> getSensorValue)
        {
            var sensorType = sensorName.Replace(" ", string.Empty);

            Id = $"{gpuIndex}_{sensorType}";
            Name = sensorName;

            _getSensorValue = getSensorValue;
        }

        public string Id { get; }

        public string Name { get; }

        public float? Value { get; private set; }

        public void Update()
        {
            Value = _getSensorValue();
        }
    }
}
