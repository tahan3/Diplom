using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private List<MenuItem> menuItems;
    [SerializeField] private BackButton backButton;
    private void Awake()
    {
        foreach (var menuItem in menuItems)
        {
            menuItem.Init(() =>
            {
                HideAll();
                backButton.SetActive(menuItem.type);
            });
        }
    }

    private void HideAll()
    {
        foreach (var item in menuItems)
        {
            item.Hide();
        }
    }
}
