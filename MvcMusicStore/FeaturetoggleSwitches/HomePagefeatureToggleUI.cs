using FeatureToggle.Core;
using FeatureToggle.Toggles;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MvcMusicStore.Models;

namespace MvcMusicStore.FeaturetoggleSwitches
{
    public class HomePagefeatureToggleUI : MVCMusicStoreSqlFeatureToggle
    {
        public override Guid Id
        {
            get
            {
                return new Guid("9821753F-524C-4588-8F0C-6E263995AF6A");
            }
        }
    }
}