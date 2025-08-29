using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string fileNameData = "";

    public FileDataHandler(string dataDirPath, string fileNameData)
    {
        this.dataDirPath = dataDirPath;
        this.fileNameData = fileNameData;
    }

    public GameData Load()
    {
        // using Path.Combine for diffrent OS's
        string fullPath = Path.Combine(dataDirPath, fileNameData);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // Load Serialize game data
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //Deserialize from JSON to C# GameData
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error occured went trying to loaded the game data to file {fullPath}" +
                $"{e}");
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        // using Path.Combine for diffrent OS's
        string fullPath = Path.Combine(dataDirPath, fileNameData);

        try
        {
            //create directory for the file will be written to if its doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //Serialize teh C# game data to JSON
            string dataToStore = JsonUtility.ToJson(data, true);

            //write the Serialize data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error occured went trying to save game data to file {fullPath}" +
                $"{e}");
        }
    }

}
