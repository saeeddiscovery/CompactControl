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
        private bool codeSentToMicro = false;
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

        public class PortSettings
        {
            //public string Port { get; set; }
            public string Baudrate { get; set; }
        }

        public static PortSettings readSettingsJson(string fileName)
        {
            using (StreamReader r = new StreamReader(fileName))
            {
                var json = r.ReadToEnd();
                PortSettings deserializedSettings = JsonConvert.DeserializeObject<PortSettings>(json);
                return deserializedSettings;
            }
        }
        public static void writeSettingsJson(string fileName, string port, string baudrate)
        {
            StreamWriter sw = new StreamWriter(fileName);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                //writer.WritePropertyName("Port");
                //writer.WriteValue(port);
                writer.WritePropertyName("Baudrate");
                writer.WriteValue(baudrate);
                writer.WriteEndObject();
            }
        }

        public static string licenseName;
        public static string remMinutes;
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
