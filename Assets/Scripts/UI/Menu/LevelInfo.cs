using System;
using UnityEditor;
using UnityEngine;
namespace UI.Menu
{
    [Serializable]
    public struct LevelInfo
    {
        public SceneAsset Scene;
        public Sprite SceneSprite;
        public string LevelName;
    }
}
