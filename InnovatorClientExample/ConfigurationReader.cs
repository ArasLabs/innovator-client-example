using System;
using System.Xml;
using System.Security.Cryptography;
using System.Text;

namespace InnovatorClientExample
{
    public class ServerInfo
    {
        public string Vault;
        public string Server;
        public string Database;
        public string Username;
        public string Password;

        public ServerInfo(string vault, string server, string database, string username, string password)
        {
            Vault = vault;
            Server = server;
            Database = database;
            Username = username;
            Password = password;
        }
    }

    /// <summary>
    /// Reads the configuration information.
    /// </summary>
    public static class ConfigurationReader
    {
        public static string ConvertPasswordToMD5(string plainPasswd)
        {
            var ascii = new ASCIIEncoding();
            byte[] data = ascii.GetBytes(plainPasswd);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);
            // Convert the MD5 result to Hexadecimal string.
            return BinaryToHex(result).ToLower();
        }

        /// <summary>
        /// Use this function to convert your MD5
        /// hash 16 bytes array to 32 hexadecimals string.
        /// Note: This code taken from www.gotdotnet.com - Topic: Function to convert your MD5 16 byte hash to 32 hexadecimals
        /// </summary>
        public static string BinaryToHex(byte[] binaryArray)
        {
            string result = "";

            foreach (byte singleByte in binaryArray)
            {
                long lowerByte = singleByte & 15;
                long upperByte = singleByte >> 4;

                result += NumberToHex(upperByte);
                result += NumberToHex(lowerByte);
            }
            return result;
        }

        /// <summary>
        /// Convert the number to hexadecimal
        /// Note: This code taken from www.gotdotnet.com - Topic: Function to convert your MD5 16 byte hash to 32 hexadecimals 
        /// </summary>
        private static char NumberToHex(long number)
        {
            if (number > 9)
            {
                return Convert.ToChar(65 + (number - 10));
            }
            else
            {
                return Convert.ToChar(48 + number);
            }
        }

        public static ServerInfo ParseXML(string fileName)
        {
            try
            {
                StartForm.ConfigDoc.Load(fileName);
            }
            catch (Exception exception)
            {
                StartForm.ErrorMsg = "XML File Load Error: " + exception;
                return null;
            }

            XmlNode nodeTest;
            XmlElement root = StartForm.ConfigDoc.DocumentElement;
            XmlNode rootNode = root.SelectSingleNode("//Innovator");
            if (rootNode == null)
            {
                StartForm.ErrorMsg = "Missing <Innovator> tag";
                return null;
            }
            // Test for the nodes, for more graceful error handling.
            nodeTest = rootNode.SelectSingleNode("vault");
            if (nodeTest == null)
            {
                StartForm.ErrorMsg = "Missing <vault> tag";
                return null;
            }

            nodeTest = rootNode.SelectSingleNode("server");
            if (nodeTest == null)
            {
                StartForm.ErrorMsg = "Missing <server> tag";
                return null;
            }

            nodeTest = rootNode.SelectSingleNode("database");
            if (nodeTest == null)
            {
                StartForm.ErrorMsg = "Missing <database> tag";
                return null;
            }

            nodeTest = rootNode.SelectSingleNode("username");
            if (nodeTest == null)
            {
                StartForm.ErrorMsg = "Missing <username> tag";
                return null;
            }

            nodeTest = rootNode.SelectSingleNode("password");
            if (nodeTest == null)
            {
                StartForm.ErrorMsg = "Missing <password> tag";
                return null;
            }

            ServerInfo serverInfo;
            try
            {
                serverInfo = new ServerInfo(
                    rootNode["vault"].InnerText,
                    rootNode["server"].InnerText,
                    rootNode["database"].InnerText,
                    rootNode["username"].InnerText,
                    ConvertPasswordToMD5(rootNode["password"].InnerText)
                    );
            }
            catch
            {
                StartForm.ErrorMsg = "Creating ServerInfo Structure";
                return null;
            }

            return serverInfo;
        }
    }
}