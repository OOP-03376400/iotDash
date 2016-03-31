﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using sconnRem.View.Config.AlarmSystem.Auth;
using sconnRem.Wnd.Config;

namespace sconnRem.View.Config.AlarmSystem.Zone
{
    [ModuleExport(typeof(ZoneConfigModule))]
    public class ZoneConfigModule : IModule
    {
        [Import]
        public IRegionManager RegionManager;

        public void Initialize()
        {
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(ZoneConfigViewNavigationItem));
        }
    }

}
