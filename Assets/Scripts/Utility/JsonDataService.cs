using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class JsonDataService : IDataService
{
    private const string KEY = "NEED_TO_GIVE_IT_A_KEY";
    private const string IV = "NEED_TO_GIVE_IT_AN_IV";

    public bool SaveData<T>(string RelativePath, T Data, bool Encrypted = false)
    {
        // Save data to game path
        string path = Application.dataPath + "/" + RelativePath;

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using FileStream stream = File.Create(path);
            if (Encrypted)
            {
                WriteEncryptedData(Data, stream);
            }
            else
            {
                stream.Close();
                string tdata = JsonConvert.SerializeObject(Data);
                File.WriteAllText(path, JsonConvert.SerializeObject(Data));
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
            return false;
        }
    }

    private void WriteEncryptedData<T>(T Data, FileStream stream)
    {
        using Aes aesProveder = Aes.Create();
        aesProveder.Key = Convert.FromBase64String(KEY);
        aesProveder.IV = Convert.FromBase64String(IV);
        using ICryptoTransform cryptoTransform = aesProveder.CreateEncryptor();
        using CryptoStream cryptoStream = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Write);

        cryptoStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(Data)));
    }

    public T LoadData<T>(string RelativePath, bool Encrypted = false)
    {
        string path = Application.dataPath + "/" + RelativePath;
        if (File.Exists(path))
        {
            T data;
            if (Encrypted)
            {
                data = ReadEncryptedData<T>(path);
            }else
            {
                data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            }
            
            return data;
        }

        return default(T);
    }

    private T ReadEncryptedData<T>(string path)
    {
        byte[] fileBytes = File.ReadAllBytes(path);
        using Aes aesProveder = Aes.Create();
        aesProveder.Key = Convert.FromBase64String(KEY);
        aesProveder.IV = Convert.FromBase64String(IV);
        using ICryptoTransform cryptoTransform = aesProveder.CreateDecryptor(aesProveder.Key, aesProveder.IV);
        using MemoryStream decryptionStream = new MemoryStream();
        using CryptoStream cryptoStream = new CryptoStream(decryptionStream, cryptoTransform, CryptoStreamMode.Read);

        using StreamReader reader = new StreamReader(cryptoStream);

        string result = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<T>(result);
    }

}
