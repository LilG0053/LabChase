using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DisplayObjectManager : MonoBehaviour
{
    [Header("Images")]
    //For generic images
    [SerializeField] private GameObject[] images;
    //For specific images
    [SerializeField] private GameObject WhiteScreen;
    [SerializeField] private GameObject WhiteScreenRight;
    [SerializeField] private GameObject BlueScreenOfDeath;
    [SerializeField] private GameObject BlueScreenOfDeathRight;

    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject CenterLine;
    [SerializeField] private GameObject GreenLine;

    [Header("Relative Transform")]
    [SerializeField] private Transform XROrigin;
    [Header("Choose a magnitude of Movement")]
    [SerializeField] private int movementMagnitude = 1;
    [Header("Choose a magnitude of scale")]
    [SerializeField] private float scaleMagnitude = 1f;
    [SerializeField] private float FlashPeriod = 0.01f; // Higher number is slower flashing

    private Vector3 changeVector;
    private float smallScale = 225;
    private float bigScale = 1500f;

    // Enum for flashing toggle states
    public enum FlashingToggle
    {
        NoToggle,
        FlashingOff,
        FlashingOn,
    }

    public enum FOV
    {
        FOV90,
        FOV80,
        FOV70,
        FOV60,
        FOV45,
        FOV30
    }

    public enum ScreenType
    {
        BlueScreenOfDeath,
        WhiteScreen
    }


    public enum AspectRatio {
        Tall,
        Wide,
        Square
    }

    public FlashingToggle flashingToggle = FlashingToggle.NoToggle;
    private GameObject currentScreen;
    private Coroutine flashCoroutine;
    private FOV currFOV; //keeps track of current FOV
    private AspectRatio currAspectRatio;
    private bool isMono = false;
    private bool isOn = false;

    void Start()
    {
        HideScreen();
        currFOV = FOV.FOV30;
        //showScreen(ScreenType.WhiteScreen, FOV.FOV30, true);
    }

    // Movement methods
    public void moveRight()
    {
        changeVector = XROrigin.right * .1f;
        gameObject.transform.position += movementMagnitude * changeVector;
    }

    public void moveRightJ()
    {
        changeVector = XROrigin.right * .1f;
        gameObject.transform.position += movementMagnitude * changeVector * 10;
    }

    public void moveLeft()
    {
        changeVector = XROrigin.right * -.1f;
        gameObject.transform.position += movementMagnitude * changeVector;
    }

    public void moveLeftJ()
    {
        changeVector = XROrigin.right * -.1f;
        gameObject.transform.position += movementMagnitude * changeVector * 10;
    }

    public void moveUp()
    {
        changeVector = XROrigin.up * .1f;
        gameObject.transform.position += movementMagnitude * changeVector;
    }

    public void moveDown()
    {
        changeVector = XROrigin.up * -.1f;
        gameObject.transform.position += movementMagnitude * changeVector;
    }

    public void scaleUp()
    {
        gameObject.transform.localScale *= scaleMagnitude;
    }

    public void scaleDown()
    {
        gameObject.transform.localScale /= scaleMagnitude;
    }

    // Method to toggle flashing
    public void showScreen(ScreenType screenType, FOV fov, AspectRatio aspectRatio = AspectRatio.Square, bool isFlashing = false, bool isMonocular = false)
    {
        Debug.Log("Showing screen");
        //First deactivate everything
        BlueScreenOfDeathRight.SetActive(false);
        BlueScreenOfDeath.SetActive(false);
        WhiteScreen.SetActive(false);
        WhiteScreenRight.SetActive(false);
        isMono = isMonocular;
        //Then activate what is needed
        if (screenType == ScreenType.BlueScreenOfDeath)
        {
            if (isMonocular)
            {
                currentScreen = BlueScreenOfDeathRight;
                BlueScreenOfDeathRight.SetActive(true);
            } else
            {
                currentScreen = BlueScreenOfDeath;
                BlueScreenOfDeath.SetActive(true);
            }
            
        }
        else if (screenType == ScreenType.WhiteScreen)
        {
            if (isMonocular)
            {
                currentScreen = WhiteScreenRight;
                WhiteScreenRight.SetActive(true);
            }
            else
            {
                currentScreen = WhiteScreen;
                WhiteScreen.SetActive(true);
            }
        }
        //TODO: change to switch statement
        if (fov == FOV.FOV90)
        {
            smallScale = 675f;
            currFOV = FOV.FOV90;
        }
        if (fov == FOV.FOV80)
        {
            smallScale = 544.5f;
            currFOV = FOV.FOV80; 
        }
        else if (fov == FOV.FOV70)
        {
            smallScale = 495f;
            currFOV = FOV.FOV70;
        }
        else if (fov == FOV.FOV60)
        {
            smallScale = 450f;
            currFOV = FOV.FOV60;
        }
        else if (fov == FOV.FOV45)
        {
            smallScale = 337.5f;
            currFOV = FOV.FOV45;
        }
        else if (fov == FOV.FOV30)
        {
            smallScale = 225f;
            currFOV = FOV.FOV30;
        }

        if (aspectRatio == AspectRatio.Tall)
        {
            currentScreen.transform.localScale = new Vector3(smallScale, bigScale, currentScreen.transform.localScale.z);
            currAspectRatio = AspectRatio.Tall;
        }
        else if (aspectRatio == AspectRatio.Wide)
        {
            currentScreen.transform.localScale = new Vector3(bigScale, smallScale, currentScreen.transform.localScale.z);
            currAspectRatio = AspectRatio.Wide;
        }
        else if (aspectRatio == AspectRatio.Square)
        {
            currentScreen.transform.localScale = new Vector3(smallScale, smallScale, currentScreen.transform.localScale.z);
            currAspectRatio = AspectRatio.Square;
        }

        if (isFlashing && flashCoroutine == null)
        {
            flashCoroutine = StartCoroutine(FlashRoutine());
        }
        isOn = true;
    }

    public void HideScreen()
    {
        StopFlashing();
        // deactivate everything
        BlueScreenOfDeathRight.SetActive(false);
        BlueScreenOfDeath.SetActive(false);
        WhiteScreen.SetActive(false);
        WhiteScreenRight.SetActive(false);
        isOn = false;
    }

    // Flashing effect
    private IEnumerator FlashRoutine()
    {
        while (true)
        {
            currentScreen.SetActive(!currentScreen.activeSelf == true);
            yield return new WaitForSeconds(FlashPeriod);
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

    public override string ToString()
    {
        string str = "";
        if (currentScreen == BlueScreenOfDeath || currentScreen == BlueScreenOfDeath)
        {
            str += "BSOD";
        }
        else if (currentScreen == WhiteScreen || currentScreen == WhiteScreenRight)
        {
            str += "White";
        }
        // Just gets the number from the string, for example FOV30 goes to 30
        str += currFOV.ToString().Substring(3,2);
        if (currAspectRatio == AspectRatio.Tall)
        {
            str += "Vertical";
        }
        else if (currAspectRatio == AspectRatio.Wide)
        {
            str += "Horizontal";
        }
        else
        {
            str += "Square";
        }
        if (isMono)
        {
            str += "Monocular";
        }
        else
        {
            str += "Binocular";
        }
        if (isOn)
        {
            str += "Start";
        }
        else
        {
            str += "Stop";
        }

        return str;
    }

}
