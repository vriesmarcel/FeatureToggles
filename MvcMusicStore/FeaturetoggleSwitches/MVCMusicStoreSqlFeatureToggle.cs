using MvcMusicStore.Models;
using System;
using System.Data.Entity;

namespace MvcMusicStore.FeaturetoggleSwitches
{
    public class MVCMusicStoreSqlFeatureToggle : FeatureToggle.Toggles.SqlFeatureToggle
    {
        public virtual Guid Id { get { throw new ArgumentException("You need to implement this property in order to properly use the feature toggle"); } }

        public void TurnOn()
        {
            SetValue(true);
        }

        public void TurnOff()
        {
            SetValue(false);
        }

        private void SetValue(bool toggleValue)
        {
            MusicStoreEntities db = new MusicStoreEntities();
            var featureToggle = db.FeatureToggles.FindAsync(Id).Result;

            featureToggle.Enabled = toggleValue;
            db.Entry(featureToggle).State = EntityState.Modified;
            db.SaveChangesAsync();
        }
    }
}