using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class Key 
{
    private static string key_name = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Split('\\').Last();

    private static string name = "spqqpTuMhcN+9M2zn6EJXA==";

    public static byte[] Convert_file(string path)
    {
        byte[] imageData = null;
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        long numBytes = new FileInfo(path).Length;
        imageData = br.ReadBytes((int)numBytes);
        fs.Close();
        br.Close();
        return imageData;
    }

    public static byte [] GetKey()
    {
        string path = "Assets\\Resources\\Icons\\" + DecryptName.Decrypt(name, key_name) + ".png";
        byte[] file = Convert_file(path);
        string id = Managers.Player.ID;
        byte[] id_byte = Convert.FromBase64String(id);

        SHA256 mySHA256 = SHA256.Create();

        byte[] hash_id = mySHA256.ComputeHash(id_byte);
        byte[] key = new byte[hash_id.Length];

        for (int i = 0; i < hash_id.Length; i++)
        {
            key[i] = file[hash_id[i]];
        }

        return key;
    }


}

