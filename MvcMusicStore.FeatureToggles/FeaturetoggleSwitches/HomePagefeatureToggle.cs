using System;

namespace MvcMusicStore.FeaturetoggleSwitches
{
    public class HomePagefeatureToggle : MVCMusicStoreSqlFeatureToggle
    {

        public override Guid Id
        {
            get
            {
                return new Guid("A5144D6F-3E07-47D9-A3B9-6B630EF1A4B5");
            }     
        }
    }

}