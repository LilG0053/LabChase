using UnityEngine;
using UnityEngine.Events;

public class displayObject : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private GameObject[] images;
    [SerializeField] private GameObject whiteImage;

    [Header("Choose a magnitute of Movement")]
    [SerializeField] private int movementMagnitute = 1;
    [Header("Choose a magnitute of scale")]
    [SerializeField] private float scaleMagnitute = 1f;

    private Vector3 changeVector;
    private int imageIndex;
    private bool isFlashing;

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
        for (int i = 1; i < images.Length; i++)
        {
            images[i].SetActive(false);
        }
        images[0].SetActive(true);
        whiteImage.SetActive(false);

    }


    void Update()
    {
        flash();
    }


    public void moveRight()
    {
        changeVector = new Vector3(.1f, 0, 0);
        gameObject.transform.position += movementMagnitute * changeVector;
    }
    public void moveRightJ()
    {
        changeVector = new Vector3(.1f, 0, 0);
        gameObject.transform.position += movementMagnitute * changeVector * 10;
    }
    public void moveLeft()
    {
        changeVector = new Vector3(-.1f, 0, 0);
        gameObject.transform.position += movementMagnitute * changeVector;
    }
    public void moveLeftJ() {
        changeVector = new Vector3(-.1f, 0, 0);
        gameObject.transform.position += movementMagnitute * changeVector * 10;
    }
    public void moveUp()
    {
        changeVector = new Vector3(0, .1f, 0);
        gameObject.transform.position += movementMagnitute * changeVector;
    }
    public void moveDown()
    {
        changeVector = new Vector3(0, -.1f, 0);
        gameObject.transform.position += movementMagnitute * changeVector;
    }
    public void scaleUp()
    {
        gameObject.transform.localScale *= scaleMagnitute;
    }
    public void scaleDown()
    {
        gameObject.transform.localScale /= scaleMagnitute;
    }

    public void toggleFlashing()
    {
        if (!isFlashing)
        {
            images[imageIndex].SetActive(false);
            whiteImage.SetActive(true);
            isFlashing = true;
            flashingToggle = FlashingToggle.FlashingOn;
        }
        else
        {
            images[imageIndex].SetActive(true);
            whiteImage.SetActive(false);
            isFlashing = false;
            flashingToggle = FlashingToggle.FlashingOff;
        }
    }

    private void flash()
    {
        if (isFlashing)
        {
            whiteImage.SetActive(!whiteImage.activeSelf);
        }
    }
    public void nextImage()
    {
        if (imageIndex + 1 >= images.Length)
        {
            Debug.Log("No more images");
        }
        else
        {
            images[imageIndex].SetActive(false);
            imageIndex++;
            images[imageIndex].SetActive(true);
        }

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
    
}
