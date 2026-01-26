using FanControl.NvThermalSensors.Tests.Fakes;
using FanControl.NvThermalSensors.Tests.Stubs;

namespace FanControl.NvThermalSensors.Tests
{
    [TestClass]
    public sealed class NvThermalSensorsPluginTests
    {
        [TestMethod]
        public void InitializeAndLoad_CurrentGpu()
        {
            var logger = new FakePluginLogger();
            var plugin = new NvThermalSensorsPlugin(logger);

            plugin.Initialize();

            var container = new FakePluginSensorsContainer();
            plugin.Load(container);
        }

        [TestMethod]
        public void VerifySensors_3080ti()
        {
            var logger = new FakePluginLogger();
            var plugin = new NvThermalSensorsPlugin(logger);
            plugin.NvidiaApi = new NvApi_3080ti();

            plugin.Initialize();

            var container = new FakePluginSensorsContainer();
            plugin.Load(container);

            Assert.HasCount(2, container.TempSensors);

            container.TempSensors[0].Update();
            Assert.EndsWith("Hot Spot", container.TempSensors[0].Name);
            Assert.AreEqual(50f, container.TempSensors[0].Value);

            container.TempSensors[1].Update();
            Assert.EndsWith("Memory Junction", container.TempSensors[1].Name);
            Assert.AreEqual(100f, container.TempSensors[1].Value);
        }

        [TestMethod]
        public void VerifySensors_5080()
        {
            var logger = new FakePluginLogger();
            var plugin = new NvThermalSensorsPlugin(logger);
            plugin.NvidiaApi = new NvApi_5080();

            plugin.Initialize();

            var container = new FakePluginSensorsContainer();
            plugin.Load(container);

            Assert.HasCount(1, container.TempSensors);

            container.TempSensors[0].Update();
            Assert.EndsWith("Memory Junction", container.TempSensors[0].Name);
            Assert.AreEqual(100f, container.TempSensors[0].Value);
        }
    }
}
