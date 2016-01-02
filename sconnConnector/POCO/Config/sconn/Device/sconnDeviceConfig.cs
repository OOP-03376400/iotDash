﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sconnConnector.POCO.Config.Abstract;
using sconnConnector.POCO.Config.Abstract.Device;

namespace sconnConnector.POCO.Config.sconn
{
    public class sconnDeviceConfig : IAlarmSystemConfigurationEntity, ISerializableConfiguration, IFakeAbleConfiguration
    {
        public List<sconnDevice> Devices { get; set; }

        public sconnDeviceConfig()
        {
                this.Devices = new List<sconnDevice>();
        }

        public sconnDeviceConfig(ipcSiteConfig cfg)
        {
            Devices = new List<sconnDevice>();
            for (int i = 0; i < cfg.deviceNo; i++)
            {
                sconnDevice dev = new sconnDevice(cfg.deviceConfigs[i]);
                dev.Id = i;
                Devices.Add(dev);
            }
        }

        public byte[] Serialize()
        {
           byte[] devSerialized = new byte[ipcDefines.deviceConfigSize*Devices.Count];
            for (int i = 0; i < Devices.Count; i++)
            {
                Devices[i].memCFG.CopyTo(devSerialized, i * ipcDefines.AUTH_RECORD_SIZE);
            }
            return devSerialized;
        }

        public void Deserialize(byte[] buffer)
        {
         
        }

        public void Fake()
        {
            sconnDevice zone = new sconnDevice();
            zone.Fake();
            Devices.Add(zone);
        }
    }

}
