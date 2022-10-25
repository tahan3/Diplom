using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameStartWindow : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Image img;

    private IEnumerator Start()
    {
        for (int i = 0; i < 3; i++)
        {
            text.text = (3 - i).ToString();
            yield return new WaitForSeconds(1f);

            if (i == 2)
            {
                img.DOFade(0f, 2f);
            }
        }
        
        Destroy(gameObject);
    }
}
