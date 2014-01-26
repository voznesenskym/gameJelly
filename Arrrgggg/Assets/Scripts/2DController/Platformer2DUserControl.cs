using UnityEngine;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour 
{
	private PlatformerCharacter2D character;
    private bool jump;
	private PhotonView _photonView;


	void Awake()
	{
		character = GetComponent<PlatformerCharacter2D>();
		_photonView = gameObject.GetComponent<PhotonView>();
	}

    void Update ()
    {
		if (_photonView.isMine){
	        // Read the jump input in Update so button presses aren't missed.
			if (CrossPlatformInput.GetButtonDown("Jump"))
	            jump = true;
		}
	}

	void FixedUpdate()
	{
		if (_photonView.isMine) {
			// Read the inputs.
			bool crouch = Input.GetKey (KeyCode.LeftControl);
			float h = CrossPlatformInput.GetAxis ("Horizontal");

			// Pass all parameters to the character control script.
			character.Move (h, crouch, jump);

			// Reset the jump input once it has been used.
			jump = false;
		}
	}
}
