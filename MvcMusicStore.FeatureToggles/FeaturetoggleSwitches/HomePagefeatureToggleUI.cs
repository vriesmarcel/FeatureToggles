using System;

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