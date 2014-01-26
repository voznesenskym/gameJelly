using UnityEngine;
using System.Linq;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PhotonView))]
public class WeaponFire : Photon.MonoBehaviour {
	
	public  Transform	bulletSpawnPoint;
	public  string		bullet 			=	"AARRRR";
	public 	string 		muzzleFire		=	"MuzzleFire";
	public  float		cooldown		=	2f;
	public	int			numberOfArrs	=	7;
	public  AudioClip[]	arrs;
	
	private Vector3		_forward;
	private float		_cooldownTimer	=	0f;
	private	bool		_isOnCooldown	=	false;
	private AudioSource	_source;
	private PhotonView _view;
	
	public bool IsOnCooldown {
		get { return _isOnCooldown; }
	}
	
	// Use this for initialization
	void Start () {
		if (!bulletSpawnPoint)	bulletSpawnPoint = transform;

		_source = GetComponent<AudioSource>();

		arrs = new AudioClip[numberOfArrs];
		for (int i = 0, count = numberOfArrs; i < count; ++i) {
			arrs[i] = (AudioClip)Resources.Load("Audio/Arr_" + i.ToString());
		}

		_view = GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponentInParent<PhotonView>().isMine && (CrossPlatformInput.GetButtonDown("Fire1") || CrossPlatformInput.GetAxis("Fire1") == 1) && !_isOnCooldown) {
			FireBullet();
		}
		
		if (_isOnCooldown) {
			UpdateCooldown();
		}
	}
	
	public void SetForward(Vector3 forward) {
		_forward = forward;
	}
	
	private void FireBullet() {
		GameObject 		b = (GameObject)PhotonNetwork.Instantiate("AARRRR", bulletSpawnPoint.position, bulletSpawnPoint.rotation, 0);
		GameObject 		g = (GameObject)PhotonNetwork.Instantiate("MuzzleFlash", bulletSpawnPoint.position, bulletSpawnPoint.rotation, 0);

		ParticleSystem	p = g.particleSystem;

		_view.RPC("PlaySound", PhotonTargets.All);

		Camera.main.GetComponent<SightController>().GoodSight();

		StartCoroutine(MuzzleFlare (p));
		p.Play();
		b.transform.right	=	_forward;
		_isOnCooldown		=	true;
	}
	
	private void UpdateCooldown() {
		_cooldownTimer	+=	Time.deltaTime;
		if (_cooldownTimer > cooldown) {
			OffCooldown();
		}
	}
	
	private void OffCooldown() {
		_cooldownTimer	=	0f;
		_isOnCooldown	=	false;
	}

	private IEnumerator MuzzleFlare(ParticleSystem flare) {
		flare.Play ();
		while (flare.isPlaying) yield return null;
		if (flare && flare.gameObject) PhotonNetwork.Destroy(flare.gameObject);
	}

	[RPC]
	public void PlaySound() {
		_source.PlayOneShot(arrs[Random.Range(0, numberOfArrs)]);
	}
}
