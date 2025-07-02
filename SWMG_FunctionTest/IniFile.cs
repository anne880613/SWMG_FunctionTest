using System.Text;
using System.Runtime.InteropServices;
using IniParser;
using IniParser.Model;
using System.Collections.Immutable;
using System.Windows.Input;


namespace SWMG_FunctionTest
{
    public class IniFile
    {
        private readonly string _FilePath;
        private readonly IniData _Data;
        private readonly FileIniDataParser _Parser;
        /// <summary>
        /// 如果用中文，請將格式儲存為UTF16-LE
        /// </summary>
        /// <param name="filePath"></param>
        public IniFile(string filePath)
        {
            if (filePath.Contains(":\\") || filePath.Contains("://"))
                _FilePath = filePath;
            else
                _FilePath = Application.StartupPath + filePath;

            if (!File.Exists(_FilePath))
            {
                File.Create(_FilePath);
                //throw new Exception("Ini file doesn't exist" + " : " + _FilePath);
            }
            _Parser = new FileIniDataParser();
            _Data = _Parser.ReadFile(_FilePath);
        }

        public string[] GetKeys(string section)
        {
            List<string> keyList = [];
            var keysData = _Data[section];
            foreach (var key in keysData.ToArray())
                keyList.Add(key.KeyName);
            return keyList.ToArray();
        }

        public string[] GetSections()
        {
            List<string> sectionList = [];
            foreach (var section in _Data.Sections)
                sectionList.Add(section.SectionName);
            return sectionList.ToArray();
        }

        public void DeleteSection(string section)
        {
            if (_Data.Sections.ContainsSection(section))
                _Data.Sections.RemoveSection(section);
            _Parser.WriteFile(_FilePath, _Data);
        }

        public void DeleteKey(string section, string key)
        {
            if (_Data[section].ContainsKey(key))
                _Data[section].RemoveKey(key);
            _Parser.WriteFile(_FilePath, _Data);
        }

        public dynamic ReadValue(string section, string key, Type valueType, dynamic defalutValue)
        {
            string value = _Data[section][key];
            if (value is null)
                return defalutValue;

            if (valueType == typeof(int))
            {
                if (int.TryParse(value, out var result))
                    return result;
            }
            else if (valueType == typeof(float))
            {
                if (float.TryParse(value, out var result))
                return result;
            }
            else if (valueType == typeof(double))
            {
                if (double.TryParse(value, out var result))
                    return result;
            }
            else if (valueType == typeof(bool))
            {
                if (bool.TryParse(value, out var result))
                    return result;
            }
            else if (valueType == typeof(string))
                return value;

            return defalutValue;
        }

        public void WriteValue(string section, string key, string value)
        {
            if (_Data[section][key] == value)
                return;
            _Data[section][key] = value;
            _Parser.WriteFile(_FilePath, _Data);
        }
        public static string StringToBase64(string text)
        {
            Byte[] bytesEncode = Encoding.UTF8.GetBytes(text); //取得 UTF8 2進位 Byte
            string resultEncode = Convert.ToBase64String(bytesEncode); // 轉換 Base64 索引表
            return resultEncode;

        }
        public static string Base64ToString(string text)
        {
            Byte[] bytesDecode = Convert.FromBase64String(text); // 還原 Byte
            string resultText = Encoding.UTF8.GetString(bytesDecode); // 還原 UTF8 字元
            return resultText;
        }
    }
}
