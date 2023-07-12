using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum TypeText { NORMAL, CRITICAL, HEAL}

public class TextPopup : MonoBehaviour
{
    [SerializeField] private Color normalColor;
    [SerializeField] private Color criticalColor;
    [SerializeField] private Color healColor;

    private TextMeshProUGUI textMesh;
    private float disappearTimer;
    private Color textColor;
    private const float DISAPPEAR_TIMER_MAX = 0.5f;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
    }

    public void Setup(float amount, TypeText typeText)
    {
        disappearTimer = DISAPPEAR_TIMER_MAX;

        switch (typeText)
        {
            case TypeText.NORMAL:
                textMesh.color = normalColor;
                textMesh.text = string.Format("-{0}", amount);
                break;

            case TypeText.CRITICAL:
                textMesh.color = criticalColor;
                textMesh.text = string.Format("-{0}", amount);
                break;

            case TypeText.HEAL:
                textMesh.color = healColor;
                textMesh.text = string.Format("+{0}", amount);
                break;
        }

        textColor = textMesh.color;
    }

    private void Update()
    {
        float moveYSpeed = 5f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;

        if (disappearTimer <= 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}