
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMusicStore.FeaturetoggleSwitches
{

    public class ServiceAFeaturetoggle : MVCMusicStoreSqlFeatureToggle
    {
        public override Guid Id
        {
            get
            {
                return new Guid("67639DAE-13FE-4983-80F9-DD74796CB1B7");
            }
        }
    }
}