using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Management;
using System.IO;
using Newtonsoft.Json;

namespace Compact_Control
{
    class HashPass
    {
         /* PBKDF2 with HMAC-SHA1, 128-bit salt, 256-bit subkey, 1000 iterations.
         (See also: SDL crypto guidelines v5.1, Part III)
         Format: { 0x00, salt, subkey }*/

        private const int PBKDF2IterCount = 1000; // default for Rfc2898DeriveBytes
        private const int PBKDF2SubkeyLength = 256 / 8; // 256 bits
        private const int SaltSize = 128 / 8; // 128 bits
        public static string LicType = "";

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] subkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, PBKDF2IterCount))
            {
                salt = deriveBytes.Salt;
                subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
            }

            byte[] outputBytes = new byte[1 + SaltSize + PBKDF2SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, PBKDF2SubkeyLength);
            return Convert.ToBase64String(outputBytes);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            // Wrong length or version header.
            if (hashedPasswordBytes.Length != (1 + SaltSize + PBKDF2SubkeyLength) || hashedPasswordBytes[0] != 0x00)
                return false;

            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SaltSize);
            byte[] storedSubkey = new byte[PBKDF2SubkeyLength];
            Buffer.BlockCopy(hashedPasswordBytes, 1 + SaltSize, storedSubkey, 0, PBKDF2SubkeyLength);

            byte[] generatedSubkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, PBKDF2IterCount))
            {
                generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
            }
            return storedSubkey.SequenceEqual(generatedSubkey);
        }

        public static bool WriteToReg(string value, int UserID)
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);
                if (!key.GetSubKeyNames().Contains("GAP"))
                    key.CreateSubKey("GAP");
                key = key.OpenSubKey("GAP", true);
                if (!key.GetSubKeyNames().Contains("1.0"))
                    key.CreateSubKey("1.0");
                key = key.OpenSubKey("1.0", true);
                key.SetValue("version" + UserID.ToString(), value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool WriteToReg(string value)
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);
                if (!key.GetSubKeyNames().Contains("Compact Control"))
                    key.CreateSubKey("Compact Control");
                key = key.OpenSubKey("Compact Control", true);
                if (!key.GetSubKeyNames().Contains("LicensedTo"))
                    key.CreateSubKey("LicensedTo");
                key = key.OpenSubKey("LicensedTo", true);
                key.SetValue("Name", value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ReadFromReg(int UserID)
        {
            string value = "";
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);
            if (!key.GetSubKeyNames().Contains("GAP"))
                return "";
            else
                key = key.OpenSubKey("GAP", true);
            if (!key.GetSubKeyNames().Contains("1.0"))
                return "";
            else
                key = key.OpenSubKey("1.0", true);
            if (!key.GetValueNames().Contains("version" + UserID.ToString()))
                return "";
            else
                value = key.GetValue("version" + UserID.ToString()).ToString();
            return value;
        }

        public static string ReadFromReg()
        {
            string value = "";
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);
            if (!key.GetSubKeyNames().Contains("Compact Control"))
                return "";
            else
                key = key.OpenSubKey("Compact Control", true);
            if (!key.GetSubKeyNames().Contains("LicensedTo"))
                return "";
            else
                key = key.OpenSubKey("LicensedTo", true);
            if (!key.GetValueNames().Contains("Name"))
                return "";
            else
                value = key.GetValue("Name").ToString();
            return value;
        }

        //Check License
        public static string CheckLicense()
        {
            ManagementObjectCollection mbsList = null;
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_processor");
            mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorID"].ToString();
            }

            //Get the motherboard serial number:
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            ManagementObjectCollection moc = mos.Get();
            string motherBoard = "";
            foreach (ManagementObject mo in moc)
            {
                motherBoard = (string)mo["SerialNumber"];
            }

            string ID = id + motherBoard;
            string ID_t = "A" + ID;
            string ID_p = "C" + ID;
            //string hashedID_t = HashPassword(ID_t);
            //string hashedID_p = HashPassword(ID_p);

            string LicenseContent = ReadLicense();

            if (LicenseContent == "error")
                return LicenseContent;
            else if (VerifyHashedPassword(LicenseContent, ID_t))
            {
                LicType = "t";
                return "t";
            }
            else if (VerifyHashedPassword(LicenseContent, ID_p))
            {
                //SetReg();
                LicType = "p";
                return "p";
            }
            else
            {
                LicType = "n";
                return "n";
            }
        }

        private static string ReadLicense()
        {
            string LicenseContent = "";
            string winPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            string licPath = Path.Combine(winPath, "clc.clc");
            using (StreamReader strdr = new StreamReader(licPath))
            {
                try
                {
                    LicenseContent = strdr.ReadLine();
                }
                catch (Exception ex)
                {
                    return "error";
                }
            }
            return LicenseContent;
        }

        public static bool SetReg()
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);
                if (!key.GetSubKeyNames().Contains("GAP"))
                    key.CreateSubKey("GAP");
                key = key.OpenSubKey("GAP", true);
                if (!key.GetSubKeyNames().Contains("1.0"))
                    key.CreateSubKey("1.0");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ReadElapsedFromReg()
        {
            string value = "";
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);
            if (!key.GetSubKeyNames().Contains("GAP"))
            {
                HashPass.SetReg();
                return "0";
            }
            else
                key = key.OpenSubKey("GAP", true);
            if (!key.GetSubKeyNames().Contains("1.0"))
            {
                HashPass.SetReg();
                return "0";
            }
            else
                key = key.OpenSubKey("1.0", true);
            if (!key.GetValueNames().Contains("elpsd"))
            {
                HashPass.SetReg();
                return "0";
            }
            else
                value = key.GetValue("elpsd").ToString();
            return value;
        }


        public static string ReadLastDateFromReg()
        {
            string value = "";
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);
            if (!key.GetSubKeyNames().Contains("GAP"))
                key.CreateSubKey("GAP");   
            key = key.OpenSubKey("GAP", true);
            if (!key.GetSubKeyNames().Contains("1.0"))
                key.CreateSubKey("1.0");
            key = key.OpenSubKey("1.0", true);
            if (!key.GetValueNames().Contains("LU"))
            {
                return "0";
            }
            else
                value = key.GetValue("LU").ToString();
            return value;
        }

        public static string ReadFirstDateFromReg()
        {
            string value = "";
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);
            if (!key.GetSubKeyNames().Contains("GAP"))
                key.CreateSubKey("GAP");
            key = key.OpenSubKey("GAP", true);
            if (!key.GetSubKeyNames().Contains("1.0"))
                key.CreateSubKey("1.0");
            key = key.OpenSubKey("1.0", true);
            if (!key.GetValueNames().Contains("FU"))
            {
                return "0";
            }
            else
                value = key.GetValue("FU").ToString();
            return value;
        }

        public static string ReadDaysFromReg()
        {
            string value = "";
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);
            if (!key.GetSubKeyNames().Contains("GAP"))
                return "";
            else
                key = key.OpenSubKey("GAP", true);
            if (!key.GetSubKeyNames().Contains("1.0"))
                return "";
            else
                key = key.OpenSubKey("1.0", true);
            if (!key.GetValueNames().Contains("Days"))
                return "";
            else
                value = key.GetValue("Days").ToString();
            return value;
        }

        public static bool WriteFirstDateToReg(string value)
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);
                if (!key.GetSubKeyNames().Contains("GAP"))
                    key.CreateSubKey("GAP");
                key = key.OpenSubKey("GAP", true);
                if (!key.GetSubKeyNames().Contains("1.0"))
                    key.CreateSubKey("1.0");
                key = key.OpenSubKey("1.0", true);
                key.SetValue("FU", value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool WriteLastDateToReg(string value)
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);
                if (!key.GetSubKeyNames().Contains("GAP"))
                    key.CreateSubKey("GAP");
                key = key.OpenSubKey("GAP", true);
                if (!key.GetSubKeyNames().Contains("1.0"))
                    key.CreateSubKey("1.0");
                key = key.OpenSubKey("1.0", true);
                key.SetValue("LU", value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool WriteBaudrateToReg(string value)
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);
                if (!key.GetSubKeyNames().Contains("GAP"))
                    key.CreateSubKey("GAP");
                key = key.OpenSubKey("GAP", true);
                if (!key.GetSubKeyNames().Contains("1.0"))
                    key.CreateSubKey("1.0");
                key = key.OpenSubKey("1.0", true);
                key.SetValue("BR", value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ReadBaudRateFromReg()
        {
            string value = "";
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);
            if (!key.GetSubKeyNames().Contains("GAP"))
                key.CreateSubKey("GAP");
            key = key.OpenSubKey("GAP", true);
            if (!key.GetSubKeyNames().Contains("1.0"))
                key.CreateSubKey("1.0");
            key = key.OpenSubKey("1.0", true);
            if (!key.GetValueNames().Contains("BR"))
            {
                return "0";
            }
            else
                value = key.GetValue("BR").ToString();
            return value;
        }

        public class CalibData
        {
            public string gant_gain { get; set; }
            public string gant_offset { get; set; }
            public string collim_gain { get; set; }
            public string collim_offset { get; set; }
            public string x1_gain { get; set; }
            public string x1_offset { get; set; }
            public string x2_gain { get; set; }
            public string x2_offset { get; set; }
            public string y1_gain { get; set; }
            public string y1_offset { get; set; }
            public string y2_gain { get; set; }
            public string y2_offset { get; set; }
        }
        public class LearnData
        {
            public string gant_zpnt { get; set; }
            public string gant_length { get; set; }
            public string gant_fine_length { get; set; }
            public string collim_zpnt { get; set; }
            public string collim_length { get; set; }
            public string collim_fine_length { get; set; }
        }

        public class ParametersData
        {
            public string gant_tol_1 { get; set; }
            public string gant_tol0 { get; set; }
            public string gant_tol1 { get; set; }
            public string gant_tol2 { get; set; }
            public string gant_v1 { get; set; }
            public string gant_v2 { get; set; }
            public string gant_v3 { get; set; }
            public string collim_tol_1 { get; set; }
            public string collim_tol0 { get; set; }
            public string collim_tol1 { get; set; }
            public string collim_tol2 { get; set; }
            public string collim_v1 { get; set; }
            public string collim_v2 { get; set; }
            public string collim_v3 { get; set; }
            public string x1_tol_1 { get; set; }
            public string x1_tol0 { get; set; }
            public string x1_tol1 { get; set; }
            public string x1_tol2 { get; set; }
            public string x1_v1 { get; set; }
            public string x1_v2 { get; set; }
            public string x1_v3 { get; set; }
            public string x2_tol_1 { get; set; }
            public string x2_tol0 { get; set; }
            public string x2_tol1 { get; set; }
            public string x2_tol2 { get; set; }
            public string x2_v1 { get; set; }
            public string x2_v2 { get; set; }
            public string x2_v3 { get; set; }
            public string y1_tol_1 { get; set; }
            public string y1_tol0 { get; set; }
            public string y1_tol1 { get; set; }
            public string y1_tol2 { get; set; }
            public string y1_v1 { get; set; }
            public string y1_v2 { get; set; }
            public string y1_v3 { get; set; }
            public string y2_tol_1 { get; set; }
            public string y2_tol0 { get; set; }
            public string y2_tol1 { get; set; }
            public string y2_tol2 { get; set; }
            public string y2_v1 { get; set; }
            public string y2_v2 { get; set; }
            public string y2_v3 { get; set; }
        }

        public class AppSettings
        {
            public string Port { get; set; }
            public string Baudrate { get; set; }
            public string clinicalTerminals { get; set; }
        }

        public static CalibData readCalibJson(string fileName)
        {
            using (StreamReader r = new StreamReader(fileName))
            {
                var json = r.ReadToEnd();
                CalibData deserializedSettings = JsonConvert.DeserializeObject<CalibData>(json);
                return deserializedSettings;
            }
        }
        public static LearnData readLearnJson(string fileName)
        {
            using (StreamReader r = new StreamReader(fileName))
            {
                var json = r.ReadToEnd();
                LearnData deserializedSettings = JsonConvert.DeserializeObject<LearnData>(json);
                return deserializedSettings;
            }
        }

        public static ParametersData readParametersJson(string fileName)
        {
            using (StreamReader r = new StreamReader(fileName))
            {
                var json = r.ReadToEnd();
                ParametersData deserializedSettings = JsonConvert.DeserializeObject<ParametersData>(json);
                return deserializedSettings;
            }
        }

        public static AppSettings readSettingsJson(string fileName)
        {
            using (StreamReader r = new StreamReader(fileName))
            {
                var json = r.ReadToEnd();
                AppSettings deserializedSettings = JsonConvert.DeserializeObject<AppSettings>(json);
                return deserializedSettings;
            }
        }

        public static void writeCalibJson(string fileName, string[] values)
        {
            StreamWriter sw = new StreamWriter(fileName);
            string[] lines = { "gant_gain", "gant_offset", "collim_gain", "collim_offset",
                                 "x1_gain", "x1_offset", "x2_gain", "x2_offset",
                                 "y1_gain", "y1_offset", "y2_gain", "y2_offset"};
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();

                for (int i = 0; i < lines.Length; i++)
                {
                    writer.WritePropertyName(lines[i]);
                    writer.WriteValue(values[i]);
                }

                writer.WriteEndObject();
            }
        }

        public static void writeLearnJson(string fileName, string[] values)
        {
            StreamWriter sw = new StreamWriter(fileName);
            string[] lines = { "gant_zpnt", "gant_length", "gant_fine_length",
                "collim_zpnt", "collim_length", "collim_fine_length" };
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();

                for (int i = 0; i < lines.Length; i++)
                {
                    writer.WritePropertyName(lines[i]);
                    writer.WriteValue(values[i]); 
                }

                writer.WriteEndObject();
            }
        }

        public static void writeParametersJson(string fileName, string[] values)
        {
            StreamWriter sw = new StreamWriter(fileName);
            string[] lines = {"gant_tol_1", "gant_tol0", "gant_tol1", "gant_tol2", "gant_v1", "gant_v2", "gant_v3",
                "collim_tol_1", "collim_tol0", "collim_tol1", "collim_tol2", "collim_v1", "collim_v2", "collim_v3",
                "y1_tol_1", "y1_tol0", "y1_tol1", "y1_tol2", "y1_v1", "y1_v2", "y1_v3",
                "y2_tol_1", "y2_tol0", "y2_tol1", "y2_tol2", "y2_v1", "y2_v2", "y2_v3",
                "x1_tol_1", "x1_tol0", "x1_tol1", "x1_tol2", "x1_v1", "x1_v2", "x1_v3",
                "x2_tol_1", "x2_tol0", "x2_tol1", "x2_tol2", "x2_v1", "x2_v2", "x2_v3" };

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();

                for (int i = 0; i < lines.Length; i++)
                {
                    writer.WritePropertyName(lines[i]);
                    writer.WriteValue(values[i]);
                }

                writer.WriteEndObject();
            }
        }

        public static void writeSettingsJson(string fileName, string port, string baudrate, string clinicalTerminals)
        {
            StreamWriter sw = new StreamWriter(fileName);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("Port");
                writer.WriteValue(port);
                writer.WritePropertyName("Baudrate");
                writer.WriteValue(baudrate);
                writer.WritePropertyName("clinicalTerminals");
                writer.WriteValue(clinicalTerminals);
                writer.WriteEndObject();
            }
        }


        public static string licenseName;
        public static string licenseType;
        public static bool isExpired = false;
        public static void refreshLicInfo()
        {
            licenseName = ReadFromReg();
            if (LicType == "t")
            {
                double today = DateTime.Today.ToOADate();
                int FirstUsed = int.Parse(ReadFirstDateFromReg());
                int LastUsed = int.Parse(ReadLastDateFromReg());
                if (FirstUsed == 0 && LastUsed == 0)
                {
                    WriteFirstDateToReg(today.ToString());
                    WriteLastDateToReg(today.ToString());
                    return;
                }
                else if ((FirstUsed == 0 && LastUsed != 0) || LastUsed > today )
                {
                    isExpired = true;
                    Form1.Board(true);
                    return;
                }
                string timeElapsed = HashPass.ReadElapsedFromReg();
                int hoursElapsed = int.Parse(timeElapsed) / (60 * 60);
                int remainingHours = 720 - hoursElapsed;
                double elapsedDays = today - FirstUsed;
                if (elapsedDays < 0 || elapsedDays > 30 || remainingHours <=0)
                {
                    isExpired = true;
                    Form1.Board(true);
                    return;
                }
                else
                {
                    isExpired = false;
                    Form1.Board(false);
                }
                if (isExpired == true)
                    licenseType = "Expired!";
                else
                    licenseType = "Trial Demo";
                WriteLastDateToReg(today.ToString());
            }
            else if (HashPass.LicType == "p")
            {
                isExpired = false;
                licenseType = "Permanent";
                Form1.Board(false);
            }
        }
    }


}
