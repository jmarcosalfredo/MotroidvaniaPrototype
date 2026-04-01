using UnityEngine;
using System.IO;
using System;

public class FileDataHandler
{
    private string fullPath;
    private bool encryptData;
    private string codeWord = "eaemeu";

    public FileDataHandler(string dataDirPath, string dataFileName, bool encryptData)
    {
        fullPath = Path.Combine(dataDirPath, dataFileName);
        this.encryptData = encryptData;
    }

    public void SaveData(GameData gameData)
    {
        try
        {
            // Create the directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Convert the GameData object to a JSON string
            string dataToSave = JsonUtility.ToJson(gameData, true);

            if (encryptData)
            {
                dataToSave = EncryptDecrypt(dataToSave);
            }

            // Open/Create a new file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                // Write the JSON string to the file
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToSave);
                }
            }
        }

        catch (Exception e)
        {
            // Log any error that happens during the save process
            Debug.LogError("Error on trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    public GameData LoadData()
    {
        GameData loadData = null;

        // Check if the save file exists
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                // Open the file
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    // Read the JSON string from the file
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (encryptData)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                // Convert the JSON string back to a GameData object
                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }

            catch (Exception e)
            {
                // Log any error that happens during the load process
                Debug.LogError("Error on trying to load data from file: " + fullPath + "\n" + e);
            }
        }

        return loadData;
    }

    public void Delete()
    {
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData ="";

        for (int i =0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ codeWord[i % codeWord.Length]);
        }

        return modifiedData;
    }
}
