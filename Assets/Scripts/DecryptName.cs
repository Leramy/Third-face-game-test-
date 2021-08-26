using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class DecryptName : MonoBehaviour
{

    private static int _iterations = 2;
    private static int _keySize = 256;

    private static string _hash = "SHA1";
    private static string _salt = "aselrias38490a32"; // Random
    private static string _vector = "8947az34awl34kjq"; // Random

    public static string Decrypt(string value, string password)
    {
        return Decrypt<AesManaged>(value, password);
    }
    public static string Decrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
    {
        byte[] vectorBytes = ASCIIEncoding.ASCII.GetBytes(_vector);
        byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(_salt);
        byte[] valueBytes = Convert.FromBase64String(value);

        byte[] decrypted;
        int decryptedByteCount = 0;

        using (T cipher = new T())
        {
            PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
            byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

            cipher.Mode = CipherMode.CBC;

            try
            {
                using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                {
                    using (MemoryStream from = new MemoryStream(valueBytes))
                    {
                        using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                        {
                            decrypted = new byte[valueBytes.Length];
                            decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return String.Empty;
            }

            cipher.Clear();
        }
        return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
    }

}
