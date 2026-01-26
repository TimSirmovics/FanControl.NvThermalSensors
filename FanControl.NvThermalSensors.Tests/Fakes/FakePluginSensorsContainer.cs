using FanControl.Plugins;

namespace FanControl.NvThermalSensors.Tests.Fakes
{
    internal class FakePluginSensorsContainer : IPluginSensorsContainer
    {
        private readonly List<IPluginControlSensor> _controlSensors = [];
        private readonly List<IPluginSensor> _fanSensors = [];
        private readonly List<IPluginSensor> _tempSensors = [];

        public List<IPluginControlSensor> ControlSensors => _controlSensors;

        public List<IPluginSensor> FanSensors => _fanSensors;

        public List<IPluginSensor> TempSensors => _tempSensors;
    }
}
