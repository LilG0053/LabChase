using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks, IOnEventCallback
{

    public GameObject imageObject;
    public DisplayObjectManager DisplayObjectManager;
    //Define current flashing configs
    private DisplayObjectManager.ScreenType currentScreen;
    private DisplayObjectManager.FOV currentFOV;
    private bool isFlashing;
    private bool isMonocular;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        //reset flashing config
        currentScreen = DisplayObjectManager.ScreenType.BlueScreenOfDeath;
        currentFOV = DisplayObjectManager.FOV.FOV30;
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
                break;
            case Utility.ToggleFlashingEventCode:
                isFlashing = true;
                break;
            case Utility.MoveLeftJEventCode:
                DisplayObjectManager.moveLeftJ();
                break;
            case Utility.MoveRightJEventCode:
                DisplayObjectManager.moveRightJ();
                break;
            case Utility.ToggleOneEyeEventCode:
                isMonocular = true;
                break;
            case Utility.ShowScreenEventCode:
                DisplayObjectManager.showScreen(currentScreen, currentFOV, isFlashing, isMonocular);
                break;
            case Utility.HideScreenEventCode:
                DisplayObjectManager.HideScreen();
                break;
            //Fov swaps
            case Utility.Toggle30FOV:
                currentFOV = DisplayObjectManager.FOV.FOV30;
                break;
            case Utility.Toggle60FOV:
                currentFOV = DisplayObjectManager.FOV.FOV60;
                break;
            case Utility.Toggle70FOV:
                currentFOV = DisplayObjectManager.FOV.FOV70;
                break;
            case Utility.Toggle80FOV:
                currentFOV = DisplayObjectManager.FOV.FOV80;
                break;
            //Screen types
            case Utility.BlueScreen:
                currentScreen = DisplayObjectManager.ScreenType.BlueScreenOfDeath;
                break;
            case Utility.WhiteScreen:
                currentScreen = DisplayObjectManager.ScreenType.WhiteScreen;
                break;
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
