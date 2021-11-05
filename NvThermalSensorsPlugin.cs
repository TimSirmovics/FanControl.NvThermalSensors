using System.Collections.Generic;

using FanControl.Plugins;

namespace FanControl.NvThermalSensors
{
    public class NvThermalSensorsPlugin : IPlugin
    {
        private Dictionary<int, NvApi.NvPhysicalGpuHandle> _handles;

        public string Name => "NvThermalSensors";

        public void Close()
        {
            _handles = null;
        }

        public void Initialize()
        {
            var handles = new NvApi.NvPhysicalGpuHandle[NvApi.MAX_PHYSICAL_GPUS];
            if (NvApi.NvAPI_EnumPhysicalGPUs(handles, out int count) != NvApi.NvStatus.OK)
                return;

            _handles = new Dictionary<int, NvApi.NvPhysicalGpuHandle>(count);
            for (int i = 0; i < count; i++)
                _handles.Add(i, handles[i]);
        }

        public void Load(IPluginSensorsContainer _container)
        {
            if (_handles == null)
                return;

            foreach (var handle in _handles)
            {
                var sensors = new NvThermalSensors(handle.Key, handle.Value);
                foreach (var sensor in sensors.Sensors)
                    _container.TempSensors.Add(sensor);
            }
        }
    }
}
