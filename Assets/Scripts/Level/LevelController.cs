using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelController : MonoBehaviour
{
    [SerializeField] private List<LevelDataScriptable> levelDataScriptables;

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Image spriteLevel;

    [SerializeField] private Animator anim;

    [Header("Settings Fade")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    [SerializeField] private LevelLoader levelLoader;

    private int countLevels;
    private int currentLevelIndex = 0;

    private void Start()
    {
        countLevels = levelDataScriptables.Count;       
    }

    public void SetLevel()
    {
        titleText.text = levelDataScriptables[currentLevelIndex].nameLevel;
        //spriteLevel.sprite = levelDataScriptables[currentLevelIndex].spriteLevel;

        string nameLevel = levelDataScriptables[currentLevelIndex].nameEnemy;
        anim.SetTrigger(nameLevel);
    }

    public void OnClickPreviousLevel()
    {
        currentLevelIndex--;

        if (currentLevelIndex < 0)
            currentLevelIndex = countLevels - 1;

        SetLevel();
    }

    public void OnClickNextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= countLevels)
            currentLevelIndex = 0;

        SetLevel();
    }

    public void OnClickSelectLevel()
    {
        GameManager.instance.LevelData = levelDataScriptables[currentLevelIndex];

        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        fadeImage.gameObject.SetActive(true);

        float r = fadeImage.color.r;
        float g = fadeImage.color.g;
        float b = fadeImage.color.b;

        fadeImage.color = new Color(r, g, b, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(r, g, b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        levelLoader.LoadLevel();
        //SceneManager.LoadScene(1);
    }
}