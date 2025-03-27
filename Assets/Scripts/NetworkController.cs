using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks, IOnEventCallback
{

    public GameObject imageObject;
    public DisplayObjectManager DisplayObjectManager;
    [SerializeField] private GameObject CenterLine;
    [SerializeField] private GameObject GreenLine;
    //Define current flashing configs
    private DisplayObjectManager.ScreenType currentScreen;
    private DisplayObjectManager.FOV currentFOV;
    private DisplayObjectManager.AspectRatio currentAspectRatio;
    private bool isFlashing;
    private bool isMonocular;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        //reset flashing config
        currentScreen = DisplayObjectManager.ScreenType.BlueScreenOfDeath;
        currentFOV = DisplayObjectManager.FOV.FOV30;
        currentAspectRatio = DisplayObjectManager.AspectRatio.Square;
        isFlashing = false;
        isMonocular = false;
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("parthandpriyankaarebullies", roomOptions, TypedLobby.Default);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        Debug.Log("event recieved");
        //calls function based on raised event code from PUN
        switch (eventCode)
        {
            case Utility.PauseTrackerCode:
                // Get the Path component from the tracker GameObject
                Path.toggleTrackerMovement();
                break;
            case Utility.MoveLeftEventCode:
                DisplayObjectManager.moveLeft();
                break;
            case Utility.MoveRightEventCode:
                DisplayObjectManager.moveRight();
                break;
            case Utility.MoveUpEventCode:
                DisplayObjectManager.moveUp();
                break;
            case Utility.MoveDownEventCode:
                DisplayObjectManager.moveDown();
                break;
            case Utility.ScaleUpEventCode:
                DisplayObjectManager.scaleUp();
                break;
            case Utility.ScaleDownEventCode:
                DisplayObjectManager.scaleDown();
                break;
            case Utility.EnableFlashingEventCode:
                isFlashing = true;
                break;
            case Utility.DisableFlashingEventCode:
                isFlashing = false;
                break;
            case Utility.MoveLeftJEventCode:
                DisplayObjectManager.moveLeftJ();
                break;
            case Utility.MoveRightJEventCode:
                DisplayObjectManager.moveRightJ();
                break;
            case Utility.ToggleOneEyeEventCode:
                isMonocular = !isMonocular;
                break;
            case Utility.ShowScreenEventCode:
                Debug.Log("Showing screen");
                DisplayObjectManager.showScreen(currentScreen, currentFOV, currentAspectRatio, isFlashing, isMonocular);
                break;
            case Utility.HideScreenEventCode:
                DisplayObjectManager.HideScreen();
                break;
            //Fov swaps
            case Utility.ToggleFOV1:
                currentFOV = DisplayObjectManager.FOV.FOV30;
                break;
            case Utility.ToggleFOV2:
                currentFOV = DisplayObjectManager.FOV.FOV45;
                break;
            case Utility.ToggleFOV3:
                currentFOV = DisplayObjectManager.FOV.FOV60;
                break;
            case Utility.ToggleFOV4:
                currentFOV = DisplayObjectManager.FOV.FOV70;
                break;
            case Utility.ToggleFOV5:
                currentFOV = DisplayObjectManager.FOV.FOV80;
                break;
            case Utility.ToggleFOV6:
                currentFOV = DisplayObjectManager.FOV.FOV90;
                break;
            // aspect ratio swaps
            case Utility.ToggleSquareAspectRatio:
                currentAspectRatio = DisplayObjectManager.AspectRatio.Square;
                break;
            case Utility.ToggleWideAspectRatio:
                currentAspectRatio = DisplayObjectManager.AspectRatio.Wide;
                break;
            case Utility.ToggleTallAspectRatio:
                currentAspectRatio = DisplayObjectManager.AspectRatio.Tall;
                break;
            //Screen types
            case Utility.BlueScreen:
                currentScreen = DisplayObjectManager.ScreenType.BlueScreenOfDeath;
                break;
            case Utility.WhiteScreen:
                currentScreen = DisplayObjectManager.ScreenType.WhiteScreen;
                break;
            case Utility.NextEventCode:
                if (CenterLine.activeSelf) 
                {
                    CenterLine.SetActive(false);
                }
                if (GreenLine.activeSelf)
                {
                    GreenLine.SetActive(false);
                }
                break;
            case Utility.PreviousEventCode:
                if (!CenterLine.activeSelf)
                {
                    CenterLine.SetActive(true);
                }
                if (!GreenLine.activeSelf)
                {
                    GreenLine.SetActive(true);
                }
                break;
        }
    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
