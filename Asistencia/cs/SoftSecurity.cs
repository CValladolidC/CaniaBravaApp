using System;  
using System.Management;  
using System.Net.NetworkInformation;  
using System.Security.Cryptography;  
using System.IO;  
using System.Text;

namespace CaniaBrava
{
    class SoftSecurity
    {
        public static string GetCPUId()
        {
            string cpuInfo = String.Empty;
            string temp = String.Empty;
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == String.Empty)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
            }
            return cpuInfo;
        }

        public static string GetMacAddress()
        {
            string macs = "";
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                PhysicalAddress pa = ni.GetPhysicalAddress();
                macs += pa.ToString();
            }
            return macs;
        }

        /// <summary>  
        /// return Volume Serial Number from hard drive  
        /// </summary>  
        /// <param name="strDriveLetter">[optional] Drive letter</param>  
        /// <returns>[string] VolumeSerialNumber</returns>  
        public static string GetVolumeSerial()
        {
            string strDriveLetter = "";

            ManagementClass mc = new ManagementClass("Win32_PhysicalMedia");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                try
                {
                    if ((UInt16)mo["MediaType"] == 29)
                    {
                        String serial = mo["SerialNumber"].ToString().Trim();
                        if (!String.IsNullOrEmpty(serial))
                        {
                            strDriveLetter = (string)mo["SerialNumber"];
                            return strDriveLetter;
                        }
                    }

                }
                catch { }

            }
            return strDriveLetter;
        }

        public string GetUniqueID()
        {
            string ID = GetCPUId() + GetVolumeSerial();
            HMACSHA1 hmac = new HMACSHA1();
            hmac.Key = Encoding.ASCII.GetBytes(GetCPUId());
            hmac.ComputeHash(Encoding.ASCII.GetBytes(ID));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hmac.Hash.Length; i++)
            {

                sb.Append(hmac.Hash[i].ToString("X2"));
            }

            return sb.ToString();

        }
    }
}