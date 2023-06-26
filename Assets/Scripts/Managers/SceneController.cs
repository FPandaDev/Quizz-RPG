using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels;

    private void Start()
    {
        GameManager.instance.Name = "Rafael";
    }

    public void OnClickOption(GameObject newPanel)
    {
        foreach (GameObject go in panels)
        {
            if (go == newPanel)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
    }
}
