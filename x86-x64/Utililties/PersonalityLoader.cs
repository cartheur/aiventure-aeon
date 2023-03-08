using System;
using System.IO;
using System.Text;
using System.Xml;
using Animals.Core.Normalize;

namespace Animals.Core.Utililties
{
    public class PersonalityLoader
    {
        private string _filesPath;
        private string _platformPath;
        private readonly Aeon _aeon;
        public PersonalityLoader(Aeon aeon)
        {
            _aeon = aeon;
        }
        public void LoadPersonality(Aeon.Language language)
        {
            switch (language)
            {
                case Aeon.Language.English:
                    _filesPath = Path.Combine(_aeon.PathToPersonalityFiles, @"en\files");
                    LoadPersonalityFiles(_filesPath);
                    _filesPath = Path.Combine(_aeon.PathToPersonalityFiles, @"en\mindpixel");
                    //LoadFiles(_filesPath, "mindpixel");
                    _filesPath = Path.Combine(_aeon.PathToPersonalityFiles, @"en\reductions");
                    //LoadFiles(_filesPath, "reductions");
                    _filesPath = Path.Combine(_aeon.PathToPersonalityFiles, @"en\update");
                    //LoadFiles(_filesPath, "update");
                    _filesPath = Path.Combine(_aeon.PathToPersonalityFiles, @"en\add");
                    break;
                case Aeon.Language.French:
                    _filesPath = Path.Combine(_aeon.PathToPersonalityFiles, @"fr\files");
                    LoadPersonalityFiles(_filesPath);
                    break;
                case Aeon.Language.German:
                    _filesPath = Path.Combine(_aeon.PathToPersonalityFiles, @"de\files");
                    LoadPersonalityFiles(_filesPath);
                    break;
                case Aeon.Language.Spanish:
                    _filesPath = Path.Combine(_aeon.PathToPersonalityFiles, @"es\files");
                    LoadPersonalityFiles(_filesPath);
                    break;
                case Aeon.Language.Italian:
                    _filesPath = Path.Combine(_aeon.PathToPersonalityFiles, @"it\files");
                    LoadPersonalityFiles(_filesPath);
                    break;
                case Aeon.Language.Sal:
                    _filesPath = Path.Combine(_aeon.PathToPersonalityFiles, @"sal\files");
                    LoadPersonalityFiles(_filesPath);
                    break;
            }

        }
        public void LoadPlatform(Aeon.Platform platform)
        {
            switch (platform)
            {
                case Aeon.Platform.WinMobile:
                    _platformPath = Path.Combine(_aeon.PathToPersonalityFiles, @"wm\rhoda");
                    LoadPlatformFiles(_platformPath);
                    // The correct order?
                    LoadFiles(Path.Combine(_platformPath, "add"), "add");
                    LoadFiles(Path.Combine(_platformPath, "mindpixel"), "mindpixel");
                    LoadFiles(Path.Combine(_platformPath, "reductions"), "reductions");
                    LoadFiles(Path.Combine(_platformPath, "update"), "update");
                    break;
                case Aeon.Platform.Liva:
                    _platformPath = Path.Combine(_aeon.PathToPersonalityFiles, @"lv\rhoda");
                    LoadPlatformFiles(_platformPath);
                    // The correct order?
                    LoadFiles(Path.Combine(_platformPath, "add"), "add");
                    LoadFiles(Path.Combine(_platformPath, "mindpixel"), "mindpixel");
                    LoadFiles(Path.Combine(_platformPath, "reductions"), "reductions");
                    LoadFiles(Path.Combine(_platformPath, "update"), "update");
                    break;
                case Aeon.Platform.Laptop:
                    _platformPath = Path.Combine(_aeon.PathToPersonalityFiles, @"lp\files");
                    LoadPlatformFiles(_platformPath);
                    break;
                case Aeon.Platform.Basic:
                    _platformPath = Path.Combine(_aeon.PathToPersonalityFiles, @"basic");
                    LoadPlatformFiles(_platformPath);
                    break;
                case Aeon.Platform.Variant:
                    _platformPath = Path.Combine(_aeon.PathToPersonalityFiles, @"variant");
                    LoadPlatformFiles(_platformPath);
                    break;

            }
        }
        private void LoadFiles(string path, string fileType)
        {
            if (Directory.Exists(path))
            {
                _aeon.WriteToLog(@"Starting to process " + fileType + @" files found in the directory " + path);

                string[] fileEntries = Directory.GetFiles(Path.Combine(_aeon.PathToPersonalityFiles, path), "*.aeon");
                if (fileEntries.Length > 0)
                {
                    foreach (string filename in fileEntries)
                    {
                        LoadAeonFile(filename);
                    }
                    _aeon.WriteToLog("Finished processing the " + fileType + " files. " + Convert.ToString(_aeon.Size) + " categories processed.");
                }
                else
                {
                    _aeon.WriteToLog("Could not find any *.aeon files in the specified directory (" + path + ").");
                }
            }
            else
            {
                throw new FileNotFoundException("The directory specified as the path to the " + fileType + " files (" + path + ") cannot be found by the Loader process.");
            }
        }
        public void LoadPersonalityFiles(string path)
        {
            if (Directory.Exists(path))
            {
                _aeon.WriteToLog(@"Starting to process files found in the directory " + path);

                string[] fileEntries = Directory.GetFiles(Path.Combine(_aeon.PathToPersonalityFiles, _filesPath), "*.aeon");
                if (fileEntries.Length > 0)
                {
                    foreach (string filename in fileEntries)
                    {
                        LoadAeonFile(filename);
                    }
                    _aeon.WriteToLog("Finished processing the personality files. " + Convert.ToString(_aeon.Size) + " categories processed.");
                }
                else
                {
                    throw new FileNotFoundException("Could not find any *.aeon files in the specified directory (" + path + ").");
                }
            }
            else
            {
                throw new FileNotFoundException("The directory specified as the path to the personality files (" + path + ") cannot be found by the Loader process.");
            }
        }
        public void LoadPlatformFiles(string path)
        {
            if (Directory.Exists(path))
            {
                _aeon.WriteToLog(@"Starting to process files found in the directory " + path);

                string[] fileEntries = Directory.GetFiles(Path.Combine(_aeon.PathToPersonalityFiles, _platformPath), "*.aeon");
                if (fileEntries.Length > 0)
                {
                    foreach (string filename in fileEntries)
                    {
                        LoadAeonFile(filename);
                    }
                    _aeon.WriteToLog("Finished processing the personality files. " + Convert.ToString(_aeon.Size) + " categories processed.");
                }
                else
                {
                    throw new FileNotFoundException("Could not find any *.aeon files in the specified directory (" + path + ").");
                }
            }
            else
            {
                throw new FileNotFoundException("The directory specified as the path to the personality files (" + path + ") cannot be found by the Loader process.");
            }
        }
        public void LoadAeonFile(string filename)
        {
            _aeon.WriteToLog(@"Processing file: " + filename);
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            LoadXml(doc, filename);
        }
        public void LoadXml(XmlDocument doc, string filename)
        {
            if (doc.DocumentElement != null)
            {
                XmlNodeList rootChildren = doc.DocumentElement.ChildNodes;
                foreach (XmlNode currentNode in rootChildren)
                {
                    if (currentNode.Name == "topic")
                    {
                        ProcessTopic(currentNode, filename);
                    }
                    else if (currentNode.Name == "category")
                    {
                        ProcessCategory(currentNode, filename);
                    }
                }
            }
        }
        private void ProcessTopic(XmlNode node, string filename)
        {
            string topicName = "*";
            if (node.Attributes != null && (node.Attributes.Count == 1) && (node.Attributes[0].Name == "name"))
            {
                topicName = node.Attributes["name"].Value;
            }
            foreach (XmlNode thisNode in node.ChildNodes)
            {
                if (thisNode.Name == "category")
                {
                    ProcessCategory(thisNode, topicName, filename);
                }
            }
        }
        private void ProcessCategory(XmlNode node, string filename)
        {
            ProcessCategory(node, "*", filename);
        }
        private void ProcessCategory(XmlNode node, string topicName, string filename)
        {
            // Reference and check the required nodes.
            XmlNode pattern = FindNode("pattern", node);
            XmlNode template = FindNode("template", node);

            if (Equals(null, pattern))
            {
                throw new XmlException("Missing pattern tag in a node found in " + filename);
            }
            if (Equals(null, template))
            {
                throw new XmlException("Missing template tag in the node with pattern: " + pattern.InnerText + " found in " + filename);
            }
            string categoryPath = GeneratePath(node, topicName, false);
            // Add the processed structure.
            if (categoryPath.Length > 0)
            {
                try
                {
                    _aeon.Presence.AddCategory(categoryPath, template.OuterXml, filename);
                    // Keep count of the number of categories that have been processed.
                    _aeon.Size++;
                }
                catch
                {
                    _aeon.WriteToLog("Failed to load a new category where the path = " + categoryPath + " and template = " + template.OuterXml + " produced by a category in the file: " + filename);
                }
            }
            else
            {
                _aeon.WriteToLog("Attempted to load a new category with an empty pattern where the path = " + categoryPath + " and template = " + template.OuterXml + " produced by a category in the file: " + filename);
            }
        }
        public string GeneratePath(XmlNode node, string topicName, bool isUserInput)
        {
            XmlNode pattern = FindNode("pattern", node);
            XmlNode that = FindNode("that", node);
            string patternText;
            string thatText = "*";
            if (Equals(null, pattern))
            {
                patternText = string.Empty;
            }
            else
            {
                patternText = pattern.InnerText;
            }
            if (!Equals(null, that))
            {
                thatText = that.InnerText;
            }
            return GeneratePath(patternText, thatText, topicName, isUserInput);
        }
        private static XmlNode FindNode(string name, XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name == name)
                {
                    return child;
                }
            }
            return null;
        }
        public string GeneratePath(string pattern, string that, string topicName, bool isUserInput)
        {
            // Hold the normalized path to be entered into the aeon.
            StringBuilder normalizedPath = new StringBuilder();
            string normalizedPattern;
            string normalizedThat;
            string normalizedTopic;

            if (_aeon.TrustPersonalityFiles && !isUserInput)
            {
                normalizedPattern = pattern.Trim();
                normalizedThat = that.Trim();
                normalizedTopic = topicName.Trim();
            }
            else
            {
                normalizedPattern = Normalize(pattern, isUserInput).Trim();
                normalizedThat = Normalize(that, isUserInput).Trim();
                normalizedTopic = Normalize(topicName, isUserInput).Trim();
            }
            // Check sizes.
            if (normalizedPattern.Length > 0)
            {
                if (normalizedThat.Length == 0)
                {
                    normalizedThat = "*";
                }
                if (normalizedTopic.Length == 0)
                {
                    normalizedTopic = "*";
                }
                // This check is in place to avoid huge "that" elements having to be processed. 
                if (normalizedThat.Length > _aeon.MaxThatSize)
                {
                    normalizedThat = "*";
                }
                // Build the path.
                normalizedPath.Append(normalizedPattern);
                normalizedPath.Append(" <that> ");
                normalizedPath.Append(normalizedThat);
                normalizedPath.Append(" <topic> ");
                normalizedPath.Append(normalizedTopic);

                return normalizedPath.ToString();
            }
            return string.Empty;
        }
        public string Normalize(string input, bool isUserInput)
        {
            StringBuilder result = new StringBuilder();
            // Objects for normalization of the input.
            ApplySubstitutions substitutor = new ApplySubstitutions(_aeon);
            StripIllegalCharacters stripper = new StripIllegalCharacters(_aeon);
            string substitutedInput = substitutor.Transform(input);
            // Split the pattern into its component words.
            string[] substitutedWords = substitutedInput.Split(" \r\n\t".ToCharArray());
            // Normalize all words unless they're the wildcards "*" and "_" during loading.
            foreach (string word in substitutedWords)
            {
                string normalizedWord;
                if (isUserInput)
                {
                    normalizedWord = stripper.Transform(word);
                }
                else
                {
                    if ((word == "*") || (word == "_"))
                    {
                        normalizedWord = word;
                    }
                    else
                    {
                        normalizedWord = stripper.Transform(word);
                    }
                }
                result.Append(normalizedWord.Trim() + " ");
            }
            return result.ToString().Replace("  ", " "); // Make sure the whitespace is neat.
        }
    }
}
