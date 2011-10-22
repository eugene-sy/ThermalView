using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThermalView.Entities;
using ThermalView.SetUp;
using ThermalView.Views.AlertDialogs;

namespace ThermalView.Controllers
{
    public class SettingsController
    {
        public Settings Settings;

        public SettingsController()
        {
            Settings = SettingsProxy.GetSettings();
        }

        public SettingsController(Settings settings)
        {
            Settings = settings;
        }

        public Settings GetSettings()
        {
            return Settings;
        }

        public void SetSettings(Settings set)
        {
            Settings = set;
        }

        public void UpdateSettingsFile(Settings settings)
        {
            SetSettings(settings);
            SettingsProxy.UpdateSettings(settings);
        }

        public int GetPalletRange()
        {
            try
            {
                return Settings.ColorPallet;
            }
            catch (NullReferenceException e)
            {
                // TODO: Exception check needed, can't send null to MainPageController to draw temp-color pallet
                // TODO: Start new thread to repair settings file
                var alert = new AlertDialog("Cannot get settings. Please check settings file.");
                alert.Show();
                return 8;
            }
        }

        internal string GetColor(int p)
        {
            return Settings.ColorList[p];
        }
    }
}
