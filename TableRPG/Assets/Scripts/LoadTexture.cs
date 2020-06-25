using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using SFB;

namespace TableRPG
{
    public enum Extensions
    {
        PNG,
        JPG,
        JPEG,
        TXT
    }
    public class LoadTexture : MonoBehaviour
    {        
        [SerializeField]
        private Extensions[] extensionsType;
        [SerializeField]
        private bool multiselect;        

        private void Start()
        {
            //LoadTexturePersistence();
        }

        private void SaveTexturePersistence()
        {
            // var bf = new BinaryFormatter();

            // FileStream file = File.Open(this.texturePersistence, FileMode.Open);

            // var imageBytes = (byte[])bf.Deserialize(file);

            // file.Close();

            // Texture2D skin = new Texture2D(1, 1);
            // skin.LoadImage(imageBytes);

            // this.spriteRender.sprite = Sprite.Create(skin, new Rect(0, 0, skin.width, skin.height), new Vector2(0, 0));
            // Debugs.Log(this.spriteRender);
        }

        private void LoadTexturePersistence()
        {
            var extensions = Array.ConvertAll(this.extensionsType, new Converter<Extensions, string>(ExtensionsToString));

            var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", new[] { new ExtensionFilter("", extensions) }, this.multiselect);

            for (int i = 0; i < paths.Length; i++)
            {
                Debugs.Log(paths[i]);
            }

            // if (!File.Exists(this.fileName)) return;

            // byte[] imageBytes = File.ReadAllBytes(this.fileName);

            // var bf = new BinaryFormatter();

            // FileStream file = File.Create(this.texturePersistence);

            // bf.Serialize(file, imageBytes);

            // file.Close();            
        }

        private static string ExtensionsToString(Extensions extension)
        {
            return extension.ToString().ToLower();
        }

    }
}