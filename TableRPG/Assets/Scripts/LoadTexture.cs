using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[ExecuteAlways]
public class LoadTexture : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRender;

    [SerializeField]
    private bool saveTexture;

    [SerializeField]
    private bool loadSave;    

    [SerializeField]
    private string fileName = "C:/Users/Ricar/Desktop/tree.png";  

    private string texturePersistence;  

    private void OnValidate()
    {
        this.texturePersistence = $"{Application.persistentDataPath}/texture.dap";
        Debug.Log(this.texturePersistence);

        if(this.loadSave){
            this.loadSave = false;
            LoadTexturePersistence();
        }else if (this.saveTexture)
        {
            this.saveTexture = false;            
            SaveTexturePersistence();
        }
    }

    private void SaveTexturePersistence(){        
        if (!File.Exists(this.fileName)) return;

        byte[] imageBytes = File.ReadAllBytes(this.fileName);

        var bf = new BinaryFormatter();

        FileStream file = File.Create(this.texturePersistence);

        bf.Serialize(file, imageBytes);

        file.Close();
    }

    private void LoadTexturePersistence(){
        var bf = new BinaryFormatter();

        FileStream file = File.Open(this.texturePersistence, FileMode.Open);

        var imageBytes = (byte[])bf.Deserialize(file);       

        file.Close();        

        Texture2D skin = new Texture2D(1, 1);
        skin.LoadImage(imageBytes);

        this.spriteRender.sprite = Sprite.Create(skin, new Rect(0, 0, skin.width, skin.height), new Vector2(0, 0));
        Debug.Log(this.spriteRender);
    }    

}
