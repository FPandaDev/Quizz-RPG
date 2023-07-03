using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private LevelLoader LevelLoader;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelLoader.LoadLevel();
        }
    }
}
