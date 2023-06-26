using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textName;

    private void Start()
    {
        textName.text = GameManager.instance.Name;
    }
}
