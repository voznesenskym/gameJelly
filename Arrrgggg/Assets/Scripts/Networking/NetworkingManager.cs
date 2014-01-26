using UnityEngine;
using System.Collections;
using System.Linq;

public class NetworkingManager : MonoBehaviour {
	public GameObject spawnObject;
	public Transform spawnPosition;
	public int connectionsAllowed = 4, portNumber = 5843;

	private string myExtIP = "";

	PlatformerCharacter2D character;

	float buttonWidth;
	float buttonHeight;

	float buttonX;
	float buttonY;

	private string gameName;
	private bool refreshing;
	private HostData[] hostData;
	private bool hostDataExists;
	private Vector3 spawnPositionPoint0;
	private Vector3 spawnPositionPoint1;
	private Vector3 spawnPositionPoint2;
	private bool connected;
	private GameObject playerObject;

	// Use this for initialization
	void Start () {
		buttonX = Screen.width * .01f;
		buttonY = Screen.height * .01f;

		buttonWidth = Screen.width * .1f;
		buttonHeight = Screen.width * .1f;

		gameName = "Blind Pirate Fight";

		spawnPositionPoint0 = spawnPosition.position;
		spawnPositionPoint1 = new Vector3 (spawnPosition.position.x + 5.0f, spawnPosition.position.y, spawnPosition.position.z);
		spawnPositionPoint2 = new Vector3 (spawnPosition.position.x + 10.0f, spawnPosition.position.y, spawnPosition.position.z);

		StartCoroutine(CheckIP());

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
			}
			if (GUI.Button (new Rect (buttonX, buttonY * 2 + buttonHeight, buttonWidth, buttonHeight), "Refresh Host")) {
				Debug.Log ("Refresh");
				refreshHostList();
			}
			if (hostDataExists) {
				for (int i = 0; i < hostData.Length; i++) {
					if (GUI.Button(new Rect(buttonX, buttonY * 2 + buttonHeight * 2 , buttonWidth, buttonHeight), hostData[i].gameName)){
						Network.Connect(hostData[i]);
					}				
				}		
			}
		}

		if (connected) {
			if (GUI.Button (new Rect (buttonX, buttonY, buttonWidth, buttonHeight), "Disconnect")) {
				Debug.Log ("disconnecting");
				connected = false;

				if (Network.isServer) {
					// TODO on server close clean up everyone
				}

				Network.Disconnect();
				Cleanup();
			};
		}

	}
	void startServer(){
		bool useNat = !Network.HavePublicAddress();
		Network.InitializeServer (connectionsAllowed, portNumber, useNat);
		MasterServer.RegisterHost(gameName, "ARRGHHHH!", "Game Jam 2014 creation. MV, MR, AB, JR");
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
		Debug.Log ("removed Player");
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
		int randomSpawn = Random.Range (0, 3);
		if (randomSpawn == 0) {
			Network.Instantiate (spawnObject, spawnPositionPoint0, Quaternion.identity, 0);		
		} else if (randomSpawn == 1){
			Network.Instantiate (spawnObject, spawnPositionPoint1, Quaternion.identity, 0);
		} else if (randomSpawn == 2){
			Network.Instantiate (spawnObject, spawnPositionPoint2, Quaternion.identity, 0);
		}
		Debug.Log (randomSpawn);
		Debug.Log (spawnPositionPoint0);
		Debug.Log (spawnPositionPoint1);
		Debug.Log (spawnPositionPoint2);
	}

	void Cleanup() {
		//GameObject myPlayer = GameObject.FindGameObjectsWithTag("Player").Where(p => p.GetComponent<NetworkView>().isMine == true).FirstOrDefault();
		GameObject[] myPlayer = GameObject.FindGameObjectsWithTag ("Player");
		//if (myPlayer) Destroy (myPlayer);
		foreach (GameObject player in myPlayer) {
			Destroy(player);
		}
	}

	IEnumerator CheckIP(){
		WWW myExtIPWWW = new WWW("http://checkip.dyndns.org");
		if(myExtIPWWW==null) yield break;
		yield return myExtIPWWW;
		myExtIP=myExtIPWWW.data;
		myExtIP=myExtIP.Substring(myExtIP.IndexOf(":")+1);
		myExtIP=myExtIP.Substring(0,myExtIP.IndexOf("<"));
		
	}
}
