using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;

namespace MvcMusicStore
{

    public class EnvironmentConfigurationBuilder : ConfigurationBuilder
    {
        private readonly IDictionary _EnvVars;

        public EnvironmentConfigurationBuilder()
        {
            _EnvVars = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process);
            if (_EnvVars.Count == 0)
            {
                _EnvVars = Environment.GetEnvironmentVariables();
            }
        }

        public override XmlNode ProcessRawXml(XmlNode rawXml)
        {
            if (rawXml.LocalName == "connectionStrings")
            {
                ReplaceConnectionStrings(rawXml);
            }
            if (rawXml.LocalName == "AppSettings")
            {
                ReplaceAppsettings(rawXml);
            }
         

            return rawXml;
        }

        private void ReplaceConnectionStrings(XmlNode rawXml)
        {
            var connectionstringEnvVars = GetConnectionstringEnvironmentVars();

            foreach (DictionaryEntry connectionstringVar in connectionstringEnvVars)
            {
                var pair = (Key: connectionstringVar.Key.ToString(), Value: connectionstringVar.Value.ToString());

                if (rawXml.HasChildNodes
                    && rawXml.SelectSingleNode($"add[@name='{pair.Key}']") != null)
                {
                    rawXml.SelectSingleNode($"add[@name='{pair.Key}']")
                        .Attributes["connectionString"].Value = pair.Value;
                }
            }
        }

        private IDictionary GetConnectionstringEnvironmentVars()
        {
            const string connectionstringPrefix = "connectionStrings__";
            Dictionary<object,object> connectionstringEntries = new Dictionary<object, object>();

            foreach(DictionaryEntry envvar in _EnvVars)
            {
                if(envvar.Key.ToString().StartsWith(connectionstringPrefix,StringComparison.OrdinalIgnoreCase))
                {
                    var environmentVariableName = envvar.Key.ToString();
                    var variableName = environmentVariableName.Substring(connectionstringPrefix.Length, environmentVariableName.Length - connectionstringPrefix.Length);
                    connectionstringEntries.Add(variableName,envvar.Value);
                }
            }
            return connectionstringEntries;
        }

        private void ReplaceAppsettings(XmlNode rawXml)
        {
            foreach (DictionaryEntry envVar in _EnvVars)
            {
                var pair = (Key: envVar.Key.ToString(), Value: envVar.Value.ToString());

                if (rawXml.HasChildNodes
                    && rawXml.SelectSingleNode($"add[@key='{pair.Key}']") != null)
                {
                    rawXml.SelectSingleNode($"add[@key='{pair.Key}']")
                        .Attributes["value"].Value = pair.Value;
                }
            }
        }

        public override  ConfigurationSection ProcessConfigurationSection(
            ConfigurationSection configSection)
        {
            return base.ProcessConfigurationSection(configSection);
        }
    }
}
