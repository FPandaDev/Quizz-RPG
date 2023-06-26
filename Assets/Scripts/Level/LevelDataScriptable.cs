using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Level Data", menuName = "Level Data")]
public class LevelDataScriptable : ScriptableObject
{
    public string nameLevel;
    public Sprite spriteLevel;
}
