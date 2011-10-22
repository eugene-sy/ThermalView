using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThermalView.Entities;
using System.Xml.Linq;
using System.Collections;
using System.Windows.Documents;
using System.Xml;
using ThermalView.Views.AlertDialogs;

namespace ThermalView.SetUp
{
    /// <summary>
    /// Proxy class connecting Setting entity and Settings.xml document
    /// </summary>
    
    public class SettingsProxy
    {
        const string ConfigFileName = "Configuration.xml";

        /// <summary>
        /// Upload settings file and parse it
        /// </summary>
        /// <returns>
        /// return a settings object
        /// </returns>
        public static Settings GetSettings()
        {
            if (!CheckSettingsHealth())
            {
                SetUpSettings();
                const string alertText = "Settings file was corrupted, please, check settings.";
                var alertDlg = new AlertDialog(alertText);
                alertDlg.Show();
            } 
            return ReadSettings();
        }

        public static void UpdateSettings(Settings settings)
        {
            WriteSettings(settings);
        }

        /// <summary>
        /// Creates configuration file
        /// </summary>
        public static void SetUpSettings()
        {
            WriteSettings(new Settings());
        }

        /// <summary>
        /// check if configuration file is changed manually and cannot be read
        /// </summary>
        /// <returns>
        /// true if file is ok
        /// and false if it is corrupted
        /// </returns>
        private static bool CheckSettingsHealth()
        {
            // here must be sanity check of config file
            return true;
        }

        /// <summary>
        /// read configuration file
        /// </summary>
        /// <returns>
        /// settings object
        /// </returns>
        private static Settings ReadSettings()
        {
            var xmlRSettings = new XmlReaderSettings();
            var settings = new Settings();

            using (XmlReader reader = XmlReader.Create(ConfigFileName, xmlRSettings))
            {
                reader.MoveToContent();
                reader.ReadStartElement();
                settings.DPI = reader.ReadElementContentAsInt("DPI", "");
                settings.ColorPallet = reader.ReadElementContentAsInt("ColorPallet", "");
                settings.ColorList.Add( 
                    reader.ReadElementContentAsString("_1color", ""));
                settings.ColorList.Add(
                    reader.ReadElementContentAsString("_2color", ""));
                settings.ColorList.Add( 
                    reader.ReadElementContentAsString("_3color", ""));
                settings.ColorList.Add( 
                    reader.ReadElementContentAsString("_4color", ""));
                settings.ColorList.Add( 
                    reader.ReadElementContentAsString("_5color", ""));
                settings.ColorList.Add( 
                    reader.ReadElementContentAsString("_6color", ""));
                settings.ColorList.Add( 
                    reader.ReadElementContentAsString("_7color", ""));
                settings.ColorList.Add(
                    reader.ReadElementContentAsString("_8color", ""));
                reader.ReadEndElement();
            }
            return settings;
        }

        private static void WriteSettings(Settings settings)
        {
            var ws = new XmlWriterSettings();

            using (var writer = XmlWriter.Create(ConfigFileName, ws))
            {
                writer.WriteStartElement("configuration", "");
                writer.WriteElementString("DPI", settings.DPI.ToString());
                writer.WriteElementString("ColorPallet", settings.ColorPallet.ToString());
                writer.WriteElementString("_1color", settings.ColorList[0]);
                writer.WriteElementString("_2color", settings.ColorList[1]);
                writer.WriteElementString("_3color", settings.ColorList[2]);
                writer.WriteElementString("_4color", settings.ColorList[3]);
                writer.WriteElementString("_5color", settings.ColorList[4]);
                writer.WriteElementString("_6color", settings.ColorList[5]);
                writer.WriteElementString("_7color", settings.ColorList[6]);
                writer.WriteElementString("_8color", settings.ColorList[7]);
                writer.WriteEndElement();
            }
        }
    }
}
