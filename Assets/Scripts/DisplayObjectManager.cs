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
    [SerializeField] private float FlashFrequency = 0.01f;

    private Vector3 changeVector;
    private int imageIndex;
    private bool isFlashing;
    private bool isOneEye;
    private bool isFlashOneEye;

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

    void Start()
    {
        imageIndex = 0;
        isFlashing = false;
        isOneEye = false;
        isFlashOneEye = false;

        // Initialize images
        for (int i = 1; i < images.Length; i++)
        {
            images[i].SetActive(false);
        }
        images[0].SetActive(true);
        WhiteScreen.SetActive(false);
        blackScreen.SetActive(false);
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
    public void toggleFlashing(ScreenType screenType, FOV fov, bool isFlashing = false, bool isMonocular = false)
    {
        //First deactivate everything
        BlueScreenOfDeathRight.SetActive(false);
        BlueScreenOfDeath.SetActive(false);
        WhiteScreen.SetActive(false);
        WhiteScreenRight.SetActive(false);

        //Then activate what is needed
        if (screenType == ScreenType.BlueScreenOfDeath)
        {
            //BSOD logic, checks for monocularity
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
            //BSOD logic, checks for monocularity
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

        if (isFlashing)
        {
            if (flashCoroutine == null)
            {
                flashCoroutine = StartCoroutine(FlashRoutine());
            }
        }

        if (isMonocular)
        {

        }
        else
        {

        }
    }

    // Flashing effect
    private IEnumerator FlashRoutine()
    {
        //toggles white every x seconds
        while (isFlashing)
        {
            currentScreen.SetActive(!currentScreen.activeSelf == true);
            yield return new WaitForSeconds(FlashFrequency);
        }
        // on shutdown ensures that image returns to blank
        WhiteScreen.SetActive(false);
        WhiteScreenRight.SetActive(false);
    }

    public void StopFlashing()
    {
        if (flashCoroutine != null)
        {
            isFlashing = false;
            StopCoroutine(flashCoroutine);
            flashCoroutine = null;
            currentScreen.SetActive(true);
        }
    }

    // Methods to cycle through images
    public void nextImage()
    {
        GreenLine.SetActive(!GreenLine.activeSelf);
        CenterLine.SetActive(!CenterLine.activeSelf);
    }

    public void previousImage()
    {
        if (imageIndex - 1 < 0)
        {
            Debug.Log("No more images");
        }
        else
        {
            images[imageIndex].SetActive(false);
            imageIndex--;
            images[imageIndex].SetActive(true);
        }
    }

    public void toggleOneEye()
    {
        isOneEye = !isOneEye;
        blackScreen.SetActive(isOneEye);
    }

    public void toggleOneEyeFlash()
    {
        isFlashOneEye = !isFlashOneEye;
        if (isFlashOneEye)
        {
            WhiteScreen.SetActive(false);
        }
    }
}
