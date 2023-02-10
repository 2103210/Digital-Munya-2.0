using System.Collections;
using System.Collections.Generic;
using TefDara.Managers;
using UnityEngine;

namespace TefDara.Lore
{
    public enum LoreType
    {
        Text, 
        Ivory
    }

    [CreateAssetMenu(menuName = "Lore")]
    public class LoreData : ScriptableObject
    {
        public LoreType loreType;
        public Sprite image;
        public GameObject mesh;
        [TextArea(15, 20)] public string text;
    }
}