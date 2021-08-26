using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class SafePrefs
{
    public bool IsEqual (byte[] b1, byte[] b2)
        {
            if (b1.Length != b2.Length) return false;

            for (int i=0; i<b1.Length;i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }

    public static void Create(string key, int value)
    { 
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.SetString("CHECK" + key, CreateHash(value));
    }

    public static string CreateHash(int value)
    {
        string tmp = value.ToString();

        byte[] value_byte = UTF8Encoding.UTF8.GetBytes(tmp);
        Array.Reverse(value_byte);
        byte[] result = value_byte;

        HMACSHA256 hmac = new HMACSHA256(Key.GetKey());
        byte[] hash_value = hmac.ComputeHash(result);
        return (Convert.ToBase64String(hash_value));
    }

    public static bool IsEdited(string key)
    {
        if (!PlayerPrefs.HasKey("CHECK" + key))
            return true;
        string hash = PlayerPrefs.GetString("CHECK" + key);
        string check = CreateHash(PlayerPrefs.GetInt(key));
        string check_money = CreateHash(Managers.Player.money);
        if (hash.CompareTo(check) != 0) return true;
        if (hash.CompareTo(check_money) != 0) return true;

        return false;
    }
   

    
}
