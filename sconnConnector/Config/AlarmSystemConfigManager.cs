﻿using iotDbConnector.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sconnConnector.POCO.Config;

namespace sconnConnector.Config
{
    public class AlarmSystemConfigManager
    {

        private EndpointInfo info;

        private sconnCfgMngr mngr;
        
        private DeviceCredentials creds;

        public sconnSite site;

        
        /****** Update interval -  cannot connect to remote device more often then specified    ******/
        public int MinUpdatePeriod { get; set; }
        public DateTime LastUpDateTime { get; set; }

        /****** Configuration of remote alarm system  ********/
        public AlarmSystemConfig Config { get; set; }



        public AlarmSystemConfigManager(EndpointInfo endp, DeviceCredentials cred)
        {
            info = endp;
            mngr = new sconnCfgMngr();
            creds = cred;
            MinUpdatePeriod = 500;
            site = new sconnSite("", 500, endp.Hostname, endp.Port, creds.Password);

        }


        private bool CanUpdateDueToTimingContraints()
        {
            return (DateTime.Now - LastUpDateTime).TotalMilliseconds > MinUpdatePeriod;
        }

        public void LoadSiteConfig()
        {
            if (CanUpdateDueToTimingContraints())
            {
                mngr.ReadSiteRunningConfig(site);
                LastUpDateTime = DateTime.Now;
                site.siteCfg.ReloadConfig();
            }
        }

        public void StoreDeviceConfig(int DevNo)
        {
            mngr.WriteDeviceCfgSingle(site, DevNo);
            mngr.WriteDeviceNamesCfgSingle(site, DevNo);
        }

        public bool ToogleArmStatus()
        {
            try
            {
                if (site.siteCfg.globalConfig.memCFG[ipcDefines.mAdrArmed] == 1)
                {
                    site.siteCfg.globalConfig.memCFG[ipcDefines.mAdrArmed] = (byte)0;
                }
                else
                {
                    site.siteCfg.globalConfig.memCFG[ipcDefines.mAdrArmed] = (byte)1;
                }
                return UploadSiteConfig();
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UploadSiteConfig()
        {
            try
            {
                return mngr.WriteGlobalCfg(site);
            }
            catch (Exception e)
            {   
                return false;
            }
        }


        public int GetDeviceNumber()
        {
            return mngr.getSiteDeviceNo(site);
        }




    }
}
