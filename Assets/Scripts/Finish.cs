using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject LevelCompleteCanvas;
    private bool isActivated;

    public void Activate()
    {
        isActivated = true;
    }
    public void FinishLevel()
    {
        if (isActivated)
        {
            LevelCompleteCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
