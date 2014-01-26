using UnityEngine;
using System.Collections;
using System.Linq;

public class NetworkingManager : Photon.MonoBehaviour {
	public string spawnObject = "2D Character";
	public Transform spawnPosition;
	public int connectionsAllowed = 4, portNumber = 5843;
	
	GameObject objectToSpawn;
	
	PlatformerCharacter2D character;
	
	float buttonWidth;
	float buttonHeight;
	
	float buttonX;
	float buttonY;
	GameObject myPlayerGo;
	
	
	public GameObject disconnectButton;
	public GameObject joinButton;
	
	private string gameName;
	private bool refreshing;
	private RoomInfo[] roomInfo;
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
		
		PhotonNetwork.ConnectUsingSettings("v1.0");
		
		objectToSpawn = (GameObject)Resources.Load (spawnObject);
		
		//enableUI ();
	}
	
	// Update is called once per frame
	void Update (){
		//Ray ray = Camera.main.transform.position;
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100, 1<<LayerMask.NameToLayer("GUI"))){
				Debug.DrawRay(ray.origin, hit.point);
				Debug.Log (hit.collider);
				
				if (hit.collider.name == "StartServer"){
					Debug.Log ("STARTING SERVER");
					startServer();
					GameObject button = GameObject.Find ("StartServer");
					GameObject button2 = GameObject.Find ("Connect");
					button2.SetActive(false);
					button.SetActive(false);
				}
				
				
				if (hit.collider.name == "Connect"){
					Debug.Log ("Refresh");
					refreshHostList();
				}
				
				if (hit.collider.name == "Disconnect"){
					Debug.Log ("disconnecting");
					connected = false;
					
					if (PhotonNetwork.isMasterClient) {
						// TODO on server close clean up everyone
					}
					
					PhotonNetwork.LeaveRoom();
					Cleanup();
				}
				
				if (hit.collider.name == "Join"){
					PhotonNetwork.JoinRoom(roomInfo[0].name);
					joinButton.SetActive(false);
					GameObject button = GameObject.Find ("StartServer");
					GameObject button2 = GameObject.Find ("Connect");
					button2.SetActive(false);
					button.SetActive(false);
				}
			} else {
				Debug.Log ("");
			} 
		}
		
		
	}
	void enableUI(){
		
		if (!PhotonNetwork.isNonMasterClientInRoom && !PhotonNetwork.isMasterClient){
			if (GUI.Button (new Rect (buttonX, buttonY, buttonWidth, buttonHeight), "Start Server")) {
				Debug.Log ("Starting Server");
				startServer();
			}
			if (GUI.Button (new Rect (buttonX, buttonY * 2 + buttonHeight, buttonWidth, buttonHeight), "Refresh Host")) {
				Debug.Log ("Refresh");
				refreshHostList();
			}
			if (hostDataExists) {
				//for (int i = 0; i < roomInfo.Length; i++) {
				//if (GUI.Button(new Rect(buttonX + (80 * i), buttonY * 2 + buttonHeight * 2 , buttonWidth, buttonHeight), roomInfo[i].name)){
				// PhotonNetwork.JoinRoom(roomInfo[i].name);
				//} 
				
				
				
				//}  
			}
		}
		
		if (connected) {
			Debug.Log ("so tired");
			//if (GUI.Button (new Rect (buttonX, buttonY, buttonWidth, buttonHeight), "Disconnect")) {
			
			
			//};
		}
		
	}
	void startServer(){
		string nm = gameName + new System.DateTime().DateTimeToUnixTimestamp();
		PhotonNetwork.CreateRoom(nm);
	}
	
	void OnJoinedRoom () {
		spawnPlayer ();
		
	}
	
	void OnDisconnectedFromServer (){
		enableUI();
	}
	
	void showDisconnectButton(){
	}
	
	void refreshHostList() {
		roomInfo = PhotonNetwork.GetRoomList();
		if (roomInfo.Length > 0) {
			hostDataExists = true;
			joinButton.SetActive(true);
		}
	}
	
	void spawnPlayer () {
		
		//GameObject button = GameObject.Find ("Disconnect");
		disconnectButton.SetActive(true);
		
		
		connected = true;
		showDisconnectButton ();
		int randomSpawn = Random.Range (0, 3);
		if (randomSpawn == 0) {
			myPlayerGo = (GameObject)Instantiate (objectToSpawn, spawnPositionPoint0, Quaternion.identity);
			turnStuffOnAtInstantiationOfPlayer();
		} else if (randomSpawn == 1){
			myPlayerGo = (GameObject)Instantiate (objectToSpawn, spawnPositionPoint1, Quaternion.identity);
			turnStuffOnAtInstantiationOfPlayer();
		} else if (randomSpawn == 2){
			myPlayerGo = (GameObject)Instantiate (objectToSpawn, spawnPositionPoint2, Quaternion.identity);
			turnStuffOnAtInstantiationOfPlayer();
		}
		
		StartCoroutine(RegisterLives());
	}
	
	void turnStuffOnAtInstantiationOfPlayer(){
		((MonoBehaviour)myPlayerGo.GetComponent ("PlatformerCharacter2D")).enabled = true;
		((MonoBehaviour)myPlayerGo.GetComponent ("Platformer2DUserControl")).enabled = true;
		myPlayerGo.GetComponent<Animator>().enabled = true;
		myPlayerGo.GetComponent<CircleCollider2D>().enabled = true;
		myPlayerGo.GetComponent<SpriteRenderer>().enabled = true;
	}
	
	
	
	
	
	void Cleanup() {
		//GameObject myPlayer = GameObject.FindGameObjectsWithTag("Player").Where(p => p.GetComponent<PhotonView>().isMine == true).FirstOrDefault();
		GameObject[] myPlayer = GameObject.FindGameObjectsWithTag ("Player");
		//if (myPlayer) Destroy (myPlayer);
		foreach (GameObject player in myPlayer) {
			Destroy(player);
		}
	}
	
	IEnumerator RegisterLives() {
		yield return null;
		if (LifeManager.Instance != null) LifeManager.Instance.RegisterPlayerLives();
	}
}