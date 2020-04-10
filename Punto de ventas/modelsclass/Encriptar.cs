using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.modelsclass
{
    public class Encriptar
    {
        private static RijndaelManaged rm = new RijndaelManaged();
        public Encriptar()
        {
            rm.Mode = CipherMode.CBC;

            rm.Padding = PaddingMode.PKCS7;

            rm.KeySize = 0x80;

            rm.BlockSize = 0x80;
        }

        public static string EncryptData(string textData, string EncryptionKey)
        {
            byte[] passBytes = Encoding.UTF8.GetBytes(EncryptionKey);

            byte[] EncryptionKeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00,};
            int len = passBytes.Length;
            if (len > EncryptionKeyBytes.Length)
            {
                len = EncryptionKeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionKeyBytes, len);
            rm.Key = EncryptionKeyBytes;
            rm.IV = EncryptionKeyBytes;

            ICryptoTransform objtransform = rm.CreateEncryptor();
            byte[] textDataByte = Encoding.UTF8.GetBytes(textData);

            return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
        }

        public static string DecryptData(string EncryptedText, string EncryptionKey)
        {
            byte[] encryptedTextByte = Convert.FromBase64String(EncryptedText);
            byte[] passBytes = Encoding.UTF8.GetBytes(EncryptionKey);
            byte[] EncryptionKeyBytes = new byte[0x10];

            int len = passBytes.Length;
            if (len > EncryptionKeyBytes.Length)
            {
                len = EncryptionKeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionKeyBytes, len);
            rm.Key = EncryptionKeyBytes;
            rm.IV = EncryptionKeyBytes;

            byte[] textByte = rm.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);

            return Encoding.UTF8.GetString(textByte);
        }
    }
}
