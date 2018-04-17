using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace ConsoleApp1
{
    class Program
    {
        private const string AES_IV = @"pf69DL6GrWFyZcMK";
        private const string AES_Key = @"9Fix4L4HB4PKeKWY";

        static void Main(string[] args)
        {
            // 平文の文字列
            string plainText = "Hello, world!";
            // 暗号化、復号された文字列
            string encrypted, decrypted;
            // 公開鍵と秘密鍵
            string publicKey, privateKey;

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // 公開鍵、秘密鍵をXML形式で取得する
                publicKey = rsa.ToXmlString(false);
                privateKey = rsa.ToXmlString(true);

                
                Debug.WriteLine("PlainText\n{0}\n", plainText);

                encrypted = Encrypt(plainText, publicKey);

                Debug.WriteLine("Encrypted\n{0}\n", encrypted);

                decrypted = Decrypt(encrypted, privateKey);

                Debug.WriteLine("Decrypted\n{0}\n", decrypted);
            }

        }
        /// <summary>
        /// 公開鍵暗号で文字列を暗号化する
        /// </summary>
        /// <param name="text">平文の文字列</param>
        /// <param name="publickey">公開鍵</param>
        /// <returns>暗号化された文字列</returns>
        public static string Encrypt(string text, string publickey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publickey);

                byte[] data = Encoding.UTF8.GetBytes(text);

                data = rsa.Encrypt(data, false);

                return Convert.ToBase64String(data);
            }

            //RSA暗号鍵方式のXMLをデータとして処理する。
        }

        /// <summary>
        /// 対称鍵暗号で暗号文を復号する
        /// </summary>
        /// <param name="cipher">平文の文字列</param>
        /// <param name="privatekey">秘密鍵</param>
        /// <returns>復号された文字列</returns>
        public static string Decrypt(string cipher, string privatekey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privatekey);

                byte[] data = Convert.FromBase64String(cipher);

                data = rsa.Decrypt(data, false);

                return Encoding.UTF8.GetString(data);
            }
        }
    }
}
