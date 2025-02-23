using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class displayObject : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private GameObject[] images;
    [SerializeField] private GameObject whiteImageL;
    [SerializeField] private GameObject whiteImageR;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject CenterLine;
    [SerializeField] private GameObject GreenLine;

    [Header("Relative Transform")]
    [SerializeField] private Transform XROrigin;
    [Header("Choose a magnitude of Movement")]
    [SerializeField] private int movementMagnitude = 1;
    [Header("Choose a magnitude of scale")]
    [SerializeField] private float scaleMagnitude = 1f;
    [SerializeField] private float flashSpeed = 0.01f;

    private Vector3 changeVector;
    private int imageIndex;
    private bool isFlashing;
    private bool flashInstanceTracker;
    private bool isOneEye;
    private bool isFlashOneEye;

    // Enum for flashing toggle states
    public enum FlashingToggle
    {
        NoToggle,
        FlashingOff,
        FlashingOn,
    }
 
    public FlashingToggle flashingToggle = FlashingToggle.NoToggle;
    

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
        whiteImageL.SetActive(false);
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
    public void toggleFlashing()
    {
        if (!isFlashing)
        {

            if (!isFlashOneEye)
            {
                whiteImageL.SetActive(true);
                whiteImageR.SetActive(false);

            }
            else
            {
                whiteImageL.SetActive(true);
                whiteImageR.SetActive(true);
            }
            isFlashing = true;
            flashingToggle = FlashingToggle.FlashingOn;
            // StartCoroutine(flash());

        }
        else
        {
            whiteImageL.SetActive(false);
            whiteImageR.SetActive(false);
            isFlashing = false;
            flashingToggle = FlashingToggle.FlashingOff;
            // StopCoroutine(flash());

        }
    }

    // Flashing effect
    private IEnumerator flash()
    {
        //toggles white every x seconds
        while (isFlashing)
        {
            yield return new WaitForSeconds(flashSpeed);
            
            if (!isFlashOneEye)
            {
                whiteImageL.SetActive(!whiteImageL.activeSelf);
            } else
            {
                whiteImageR.SetActive(!whiteImageR.activeSelf);
            }
        }
        // on shutdown ensures that image returns to blank
        whiteImageL.SetActive(false);
        whiteImageR.SetActive(false);
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
            whiteImageL.SetActive(false);
        }
    }
}
