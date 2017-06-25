using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FileShare
{
    static class Utility
    {
        public static bool IsWin10()
        {
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            string productName = (string)reg.GetValue("ProductName");

            return productName.StartsWith("Windows 10");
        }

        public static string GetLocalIPAddress()
        {
            string hostName = Dns.GetHostName();

            IPAddress[] ips = Dns.GetHostAddresses(hostName);
            for (int i = 0; i < ips.Length; i++)
            {
                if (ips[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    return ips[i].ToString();
                }
            }
            throw new ResolveIPException("Failed to resolve host: " + hostName);
        }
        public static async Task<string> GetLocalIPAddressAsync()
        {
            string hostName = Dns.GetHostName();

            IPAddress[] ips = await Dns.GetHostAddressesAsync(hostName);
            for (int i = 0; i < ips.Length; i++)
            {
                if (ips[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    return ips[i].ToString();
                }
            }
            throw new ResolveIPException("Failed to resolve host: " + hostName);
        }

        public static async Task<bool> IsLocalAddressAsync(IPAddress address)
        {
            string hostName = Dns.GetHostName();

            IPAddress[] ips = await Dns.GetHostAddressesAsync(hostName);
            for (int i = 0; i < ips.Length; i++)
            {
                if (ips[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    if (address.ToString() == ips[i].ToString())
                        return true;
                }
            }
            return false;
        }

        public static class ByteConversion
        {
            public static int Byte { get { return 8; } }
            public static int Kilobyte { get { return 1024; } }
            public static int Megabyte { get { return 1048576; } }
            public static int Gigabyte { get { return 1073741824; } }
        }

        public static string ConvertFileSize(long fileSize)
        {
            string sizeString;
            if (fileSize < Utility.ByteConversion.Kilobyte)
                sizeString = fileSize.ToString() + " B";

            else if (fileSize >= Utility.ByteConversion.Kilobyte && fileSize < Utility.ByteConversion.Megabyte)
                sizeString = fileSize.ToSize(SizeUnit.KB) + " KB";
            else if (fileSize >= Utility.ByteConversion.Megabyte && fileSize < Utility.ByteConversion.Gigabyte)
                sizeString = fileSize.ToSize(SizeUnit.MB) + " MB";
            else
                sizeString = fileSize.ToSize(SizeUnit.GB) + " GB";

            return sizeString;
        }

    }
}
