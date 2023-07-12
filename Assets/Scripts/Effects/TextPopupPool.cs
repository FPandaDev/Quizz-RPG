using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopupPool : MonoBehaviour
{
    [SerializeField] private TextPopup damagePopup;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private List<TextPopup> damagePopupList;

    private static TextPopupPool instance;
    public static TextPopupPool Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    void Start()
    {
        AddDamagePopupPool(poolSize);
    }

    private void AddDamagePopupPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            TextPopup popup = Instantiate(damagePopup);
            popup.gameObject.SetActive(false);
            damagePopupList.Add(popup);
            popup.transform.SetParent(transform);
        }
    }

    public TextPopup RequestPopup()
    {
        for (int i = 0; i < damagePopupList.Count; i++)
        {
            if (!damagePopupList[i].gameObject.activeSelf)
            {
                damagePopupList[i].gameObject.SetActive(true);
                return damagePopupList[i];
            }
        }
        return null;
    }
}