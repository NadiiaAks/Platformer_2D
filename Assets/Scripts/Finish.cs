using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject LevelCompleteCanvas;
    [SerializeField] private GameObject messageUI;

    private bool isActivated;

    public void Activate()
    {
        isActivated = true;
        messageUI.SetActive(false);
    }
    public void FinishLevel()
    {
        if (isActivated)
        {
            LevelCompleteCanvas.SetActive(true);
            gameObject.SetActive(false);
            Time.timeScale = 0f;
        }
        else
        {
            messageUI.SetActive(true);
        }
    }
}
