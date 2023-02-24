using System.Collections.Generic;
using System.IO;
using System.Xml;
using Animals.Core.Normalize;

namespace Animals.Core.Utililties
{
    public class SettingsDictionary
    {
        private readonly Dictionary<string, string> _settingsHash = new Dictionary<string, string>();
        private readonly List<string> _orderedKeys = new List<string>();
        protected Aeon ThisAeon;
        public int Count
        {
            get
            {
                return _orderedKeys.Count;
            }
        }
        public XmlDocument DictionaryAsXml
        {
            get
            {
                XmlDocument result = new XmlDocument();
                XmlDeclaration dec = result.CreateXmlDeclaration("1.0", "UTF-8", "");
                result.AppendChild(dec);
                XmlNode root = result.CreateNode(XmlNodeType.Element, "root", "");
                result.AppendChild(root);
                foreach (string key in _orderedKeys)
                {
                    XmlNode item = result.CreateNode(XmlNodeType.Element, "item", "");
                    XmlAttribute name = result.CreateAttribute("name");
                    name.Value = key;
                    XmlAttribute value = result.CreateAttribute( "value");
                    value.Value = _settingsHash[key];
                    if (item.Attributes != null)
                    {
                        item.Attributes.Append(name);
                        item.Attributes.Append(value);
                    }
                    root.AppendChild(item);
                }
                return result;
            }
        }
        public SettingsDictionary(Aeon thisAeon)
        {
            ThisAeon = thisAeon;
        }
        public void LoadSettings(string pathToSettings)
        {
            if (pathToSettings.Length > 0)
            {
                FileInfo fi = new FileInfo(pathToSettings);
                if (fi.Exists)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(pathToSettings);
                    LoadSettings(xmlDoc);
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
        public void LoadSettings(XmlDocument settingsAsXml)
        {
            // empty the hash
            ClearSettings();

            if (settingsAsXml.DocumentElement != null)
            {
                XmlNodeList rootChildren = settingsAsXml.DocumentElement.ChildNodes;

                foreach (XmlNode myNode in rootChildren)
                {
                    if (myNode.Attributes != null && (myNode.Name == "item") && (myNode.Attributes.Count == 2))
                    {
                        if ((myNode.Attributes[0].Name == "name") && (myNode.Attributes[1].Name == "value"))
                        {
                            string name = myNode.Attributes["name"].Value;
                            string value = myNode.Attributes["value"].Value;
                            if (name.Length > 0)
                            {
                                AddSetting(name, value);
                            }
                        }
                    }
                }
            }
        }
        public void AddSetting(string name, string value)
        {
            string key = MakeCaseInsensitive.TransformInput(name);
            if (key.Length > 0)
            {
                RemoveSetting(key);
                _orderedKeys.Add(key);
                _settingsHash.Add(MakeCaseInsensitive.TransformInput(key), value);
            }
        }
        public void RemoveSetting(string name)
        {
            string normalizedName = MakeCaseInsensitive.TransformInput(name);
            _orderedKeys.Remove(normalizedName);
            RemoveFromHash(normalizedName);
        }
        private void RemoveFromHash(string name)
        {
            string normalizedName = MakeCaseInsensitive.TransformInput(name);
            _settingsHash.Remove(normalizedName);
        }
        public void UpdateSetting(string name, string value)
        {
            string key = MakeCaseInsensitive.TransformInput(name);
            if (_orderedKeys.Contains(key))
            {
                RemoveFromHash(key);
                _settingsHash.Add(MakeCaseInsensitive.TransformInput(key), value);
            }
        }
        public void ClearSettings()
        {
            _orderedKeys.Clear();
            _settingsHash.Clear();
        }
        public string GrabSetting(string name)
        {
            string normalizedName = MakeCaseInsensitive.TransformInput(name);
            if (ContainsSettingCalled(normalizedName))
            {
                return _settingsHash[normalizedName];
            }
            else
            {
                return string.Empty;
            }
        }
        public bool ContainsSettingCalled(string name)
        {
            string normalizedName = MakeCaseInsensitive.TransformInput(name);
            if (normalizedName.Length > 0)
            {
                return _orderedKeys.Contains(normalizedName);
            }
            else
            {
                return false;
            }
        }
        public string[] SettingNames
        {
            get
            {
                string[] result = new string[_orderedKeys.Count];
                _orderedKeys.CopyTo(result, 0);
                return result;
            }
        }
        public void Clone(SettingsDictionary target)
        {
            foreach (string key in _orderedKeys)
            {
                target.AddSetting(key, GrabSetting(key));
            }
        }
    }
}
