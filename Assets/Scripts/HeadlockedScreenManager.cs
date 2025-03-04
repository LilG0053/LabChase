using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadlockedScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject BlueScreenOfDeath;
    [SerializeField] private GameObject WhiteScreen;
    public float FlashFrequency = 0.5f;
    
    public enum ScreenType
    {
        BlueScreenOfDeath,
        WhiteScreen
    }

    public enum FOV {
        FOV80,
        FOV70,
        FOV60,
        FOV30
    }
    
    private GameObject currentScreen;
    private Coroutine flashCoroutine;

    private void Start()
    {
        // ShowScreen(ScreenType.BlueScreenOfDeath, FOV.FOV80, true);
        // HideScreen();
        ShowScreen(ScreenType.BlueScreenOfDeath, FOV.FOV30, true);
    }

    public void ShowScreen(ScreenType screenType, FOV fov, bool isFlashing = false, bool isMonocular = false)
    {
        if (screenType == ScreenType.BlueScreenOfDeath)
        {
            currentScreen = BlueScreenOfDeath;
            BlueScreenOfDeath.SetActive(true);
            WhiteScreen.SetActive(false);
        }
        else if (screenType == ScreenType.WhiteScreen)
        {
            currentScreen = WhiteScreen;
            BlueScreenOfDeath.SetActive(false);
            WhiteScreen.SetActive(true);
        }

        if (fov == FOV.FOV80)
        {
            currentScreen.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        }
        else if (fov == FOV.FOV70)
        {
            currentScreen.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
        }
        else if (fov == FOV.FOV60)
        {
            currentScreen.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        }
        else if (fov == FOV.FOV30)
        {
            currentScreen.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        }

        if (isFlashing && flashCoroutine == null)
        {
            flashCoroutine = StartCoroutine(FlashRoutine());
        }   
        
        if (isMonocular)
        {
            // change shader/material
        }
        else
        {

        }
        
    }

    public void HideScreen()
    {
        StopFlashing();
        BlueScreenOfDeath.SetActive(false);
        WhiteScreen.SetActive(false);
    }

    private IEnumerator FlashRoutine()
    {
        while (true)
        {
            currentScreen.SetActive(!currentScreen.activeSelf == true);
            yield return new WaitForSeconds(FlashFrequency);
        }
    }

    public void StopFlashing()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
            flashCoroutine = null;
            currentScreen.SetActive(true);
        }
    }
}
