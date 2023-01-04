using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.U2D;
using UnityEditor.U2D;

public class BuildBundle
{
    [MenuItem("Tool/Éú³ÉÍ¼¼¯")]
    public static void BuildAtlas()
    {
        string atlasPath = "Assets/SubAssets/Raw";
        string[] guids = AssetDatabase.FindAssets("t:Sprite",new string[] { atlasPath });
        List<Sprite> sprites = new List<Sprite>();
        foreach (string guid in guids)
        {
            sprites.Add(AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(guid)));
        }

        var atlas = new SpriteAtlas();
        atlas.Add(sprites.ToArray());
      
        AssetDatabase.CreateAsset(atlas, "Assets/NewAtlas.spriteatlas");


    }
}
