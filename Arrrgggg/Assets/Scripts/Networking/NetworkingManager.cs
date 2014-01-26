using UnityEngine;
using System.Collections;

public class NetworkingManager : MonoBehaviour {
	public GameObject spawnObject;
	public Transform spawnPosition;

	PlatformerCharacter2D character;

	float buttonWidth;
	float buttonHeight;

	float buttonX;
	float buttonY;

	private string gameName;
	private bool refreshing;
	private HostData[] hostData;
	private bool hostDataExists;
	private Vector3 spawnPositionPoint;
	private bool connected;
	private GameObject playerObject;

	// Use this for initialization
	void Start () {
		buttonX = Screen.width * .01f;
		buttonY = Screen.height * .01f;

		buttonWidth = Screen.width * .1f;
		buttonHeight = Screen.width * .1f;

		gameName = "Game Jam Pirate Sight Simulator";

		spawnPositionPoint = spawnPosition.position;



	}

	void Update (){
		if (refreshing) {
			if (MasterServer.PollHostList().Length > 0){
				refreshing = false;
				hostDataExists = true;
				Debug.Log (MasterServer.PollHostList().Length);
				hostData = MasterServer.PollHostList();
			}		
		}
	}
	
	// Update is called once per frame
	void OnGUI (){
		enableUI ();
	}
	void enableUI(){

		if (!Network.isClient && !Network.isServer){
			if (GUI.Button (new Rect (buttonX, buttonY, buttonWidth, buttonHeight), "Start Server")) {
				Debug.Log ("Starting Server");
				startServer();
			};
			if (GUI.Button (new Rect (buttonX, buttonY * 2 + buttonHeight, buttonWidth, buttonHeight), "Refresh Host")) {
				Debug.Log ("Refresh");
				refreshHostList();
			};
			if (hostDataExists) {
				for (int i = 0; i < hostData.Length; i++) {
					if (GUI.Button(new Rect(buttonX, buttonY * 2 + buttonHeight * 2 , buttonWidth, buttonHeight), hostData[i].gameName)){
						//Network.Connect(HostData[] hostData);
						Network.Connect(hostData[i]);
					}				
				}		
			}
		}

		if (connected) {
			if (GUI.Button (new Rect (buttonX, buttonY, buttonWidth, buttonHeight), "Disconnect")) {
				Debug.Log ("disconnecting");
				connected = false;
				Network.Disconnect();
			};
		}

	}
	void startServer(){
		Network.InitializeServer (4, 25000, false);
		MasterServer.RegisterHost(gameName,"ARRGHHHH!", "Game Jam 2014 creation. MV, MR, AB, JR");
	}

	void OnServerInitialized () {
		Debug.Log ("server Initialized");
		spawnPlayer ();
	}

	void OnConnectedToServer () {
		spawnPlayer ();

	}

	void OnDisconnectedFromServer (){
		enableUI();
	}

	void showDisconnectButton(){

	}



	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Clean up after player " + player);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}
	
	void OnMasterServerEvent (MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.RegistrationSucceeded){
			Debug.Log ("registered Server");
		}

	}

	void refreshHostList(){
		MasterServer.RequestHostList (gameName);
		//yield return new WaitForSeconds(3);
		//Debug.Log (MasterServer.PollHostList ().Length);
		refreshing = true;
	}

	void spawnPlayer () {
		connected = true;
		showDisconnectButton ();
		Debug.Log ("spawning");
		Network.Instantiate (spawnObject, spawnPositionPoint, Quaternion.identity, 0);


	}
}
