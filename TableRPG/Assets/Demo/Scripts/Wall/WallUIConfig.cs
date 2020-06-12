using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class WallUIConfig : MonoBehaviour
{
    public delegate void OnSaveWall();
    public static OnSaveWall SaveWall;

    public delegate void OnLoadWall(WallData data);
    public static OnLoadWall LoadWall;

    private string savePath;

    private void Awake()
    {
        WallBuilder.SavingWallData += SavingWallData;
        this.savePath = $"{Application.persistentDataPath}/save.dap";
    }

    private void OnDestroy()
    {
        WallBuilder.SavingWallData -= SavingWallData;
    }

    private void SavingWallData(WallData wallData)
    {
        string json = JsonUtility.ToJson(wallData);

        Debugs.Log("Save wall in Path:", this.savePath);

        var bf = new BinaryFormatter();

        FileStream file = File.Create(this.savePath);

        bf.Serialize(file, json);

        file.Close();        
    }

    #region UI
    public void OnSaveCurrentWall()
    {
        if (SaveWall != null)
        {
            SaveWall();
        }
    }

    public void OnLoadLastSavedWall()
    {
        if (LoadWall == null || !File.Exists(this.savePath)) return;

        Debugs.Log("Load wall in Path:", this.savePath);           

        var bf = new BinaryFormatter();

        FileStream file = File.Open(this.savePath, FileMode.Open);

        var json = (string) bf.Deserialize(file);

        var lastWallData = JsonUtility.FromJson<WallData>(json);

        file.Close();

        LoadWall(lastWallData);
    }
    #endregion UI
}
