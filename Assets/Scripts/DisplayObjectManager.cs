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

    // Enum for flashing toggle states
    public enum FlashingToggle
    {
        NoToggle,
        FlashingOff,
        FlashingOn,
    }

    public enum FOV
    {
        FOV80,
        FOV70,
        FOV60,
        FOV30
    }

    public enum ScreenType
    {
        BlueScreenOfDeath,
        WhiteScreen
    }

    public FlashingToggle flashingToggle = FlashingToggle.NoToggle;
    private GameObject currentScreen;
    private Coroutine flashCoroutine;
    private FOV currFOV; //keeps track of current FOV
    private bool isMono = false;
    private bool isOn = false;

    void Start()
    {
        HideScreen();
        currFOV = FOV.FOV30;
        showScreen(ScreenType.WhiteScreen, FOV.FOV30, true);
    }

    void Update()
    {

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
    public void showScreen(ScreenType screenType, FOV fov, bool isFlashing = false, bool isMonocular = false)
    {
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

        if (fov == FOV.FOV80)
        {
            currentScreen.transform.localScale = new Vector3(363f, 363f, 363f);
        }
        else if (fov == FOV.FOV70)
        {
            currentScreen.transform.localScale = new Vector3(330f, 330f, 330f);
        }
        else if (fov == FOV.FOV60)
        {
            currentScreen.transform.localScale = new Vector3(300f, 300f, 300f);
        }
        else if (fov == FOV.FOV30)
        {
            currentScreen.transform.localScale = new Vector3(150f, 150f, 150f);
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
