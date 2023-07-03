using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Level Data", menuName = "Level Data")]
public class LevelDataScriptable : ScriptableObject
{
    public string nameLevel;
    public string nameEnemy;

    public Sprite sprite;
    public RuntimeAnimatorController animator;   
}
