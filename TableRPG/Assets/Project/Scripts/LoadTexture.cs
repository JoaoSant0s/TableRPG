using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using SFB;

namespace TableRPG
{
    public enum TextureExtensions
    {
        PNG,
        JPG,
        JPEG,
        TXT
    }
    public class LoadTexture
    {
        private static void SaveTexturePersistence()
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

        public static string[] LoadTexturePersistence(TextureExtensions[] extensionsType, bool multiselect = false)
        {
            var extensions = Array.ConvertAll(extensionsType, new Converter<TextureExtensions, string>(TextureExtensionsToString));

            var paths = StandaloneFileBrowser.OpenFilePanel("Textures", "", new[] { new ExtensionFilter("", extensions) }, multiselect);

            return paths;
            // for (int i = 0; i < paths.Length; i++)
            // {
            //     Debugs.Log(paths[i]);
            // }

            // if (!File.Exists(this.fileName)) return;

            // byte[] imageBytes = File.ReadAllBytes(this.fileName);

            // var bf = new BinaryFormatter();

            // FileStream file = File.Create(this.texturePersistence);

            // bf.Serialize(file, imageBytes);

            // file.Close();            
        }

        public static Sprite LoadSpriteByBytes(byte[] fileData, float pixelsPerUnit = 100)
        {            
            Texture2D skin = new Texture2D(1, 1);
            skin.LoadImage(fileData);
            
            var sprite = Sprite.Create(skin, new Rect(0, 0, skin.width, skin.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);            
            return sprite;
        }

        private static string TextureExtensionsToString(TextureExtensions extension)
        {
            return extension.ToString().ToLower();
        }

    }
}