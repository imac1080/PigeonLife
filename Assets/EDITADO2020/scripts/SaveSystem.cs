using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    public static void SavePlayer (Player player)
    {
        Debug.Log("saving... Player: "+player.username+ ", "+ player.ranking);
        BinaryFormatter formatter = new BinaryFormatter();
        String path = Application.persistentDataPath + "/player.data";
        //File.Delete(path);
        FileStream stream = new FileStream(path, FileMode.Create);
        
        PlayerData data  =new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("saved...");
    }

    public static PlayerData LoadPLayer()
    {

        String path = Application.persistentDataPath + "/player.data";
        //File.Delete(path);
        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data= formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("no existe el archivo");
            return null;
        }
    }
}