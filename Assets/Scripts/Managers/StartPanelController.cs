using UnityEngine;

public class StartPanelController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject startPanel;

    void Start()
    {
        string nameLevel = GameManager.instance.LevelData.nameEnemy;
        anim.SetTrigger(nameLevel);
    }

    public void DesactiveStartPanel()
    {
        startPanel.SetActive(false);
    }
}