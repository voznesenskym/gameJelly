using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PhotonView))]
public class Player3DTrigger : MonoBehaviour {

	public string cannonHitParticle = "CannonPlayerHit";
	public Transform[] respawnPoints;
	public GameObject player;

	public int deathYellCount = 3;

	private AudioClip[] _yells;
	private Transform _transform;
	private int particleCount;
	private AudioSource _source;
	private PhotonView _view;

	void Awake() {
		_transform = transform;
		if (collider) {
			collider.isTrigger = true;
		}
		particleCount = 0;
	}

	void Start() {
		_view = GetComponent<PhotonView>();
		_yells = new AudioClip[deathYellCount];
		_source = GetComponent<AudioSource>();
		for (int i = 0, count = deathYellCount; i < count; ++i) {
			_yells[i] = (AudioClip)Resources.Load("Audio/Death_" + i.ToString());
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("CannonBall")) {
			GameObject ps =  (GameObject)PhotonNetwork.Instantiate(cannonHitParticle, _transform.position, Quaternion.identity, 0);
			ParticleSystem p = ps.particleSystem; 
			particleCount ++;
			StartCoroutine(DestroyParticle(p));
			if (other && other.gameObject) PhotonNetwork.Destroy(other.gameObject);
			//Destroy(other.gameObject);
			LockPlayerInPlace();
			StartCoroutine(Respawn());
		}
	}

	private IEnumerator DestroyParticle(ParticleSystem ps) {
		yield return new WaitForSeconds(1.5f);
		particleCount --;
		if (ps && ps.gameObject) PhotonNetwork.Destroy(ps.gameObject);
	}

	private void LockPlayerInPlace() {
		player.GetComponent<Platformer2DUserControl>().enabled = false;
		player.rigidbody2D.Sleep();
	}
	
	private IEnumerator SplatPlayer() {
		float t = 0;
		float duration = 2;
		while (t < duration) {
			player.transform.position += new Vector3(0, 3, -6) * Time.deltaTime;
			t += Time.deltaTime;
			yield return null;
		}
	}
	
	private void LoseLife() {
		LifeManager.Instance.RemoveLifeFrom(player.GetInstanceID());
	}
	
	private IEnumerator Respawn() {
		_view.RPC("DieSound", PhotonTargets.All);
		yield return StartCoroutine(SplatPlayer());
		LoseLife();
		int r = Random.Range(0, respawnPoints.Length);
		yield return new WaitForSeconds(1.0f);
		player.transform.position = respawnPoints[r].position;
		player.GetComponent<Platformer2DUserControl>().enabled = true;
		Camera.main.GetComponent<SightController>().ResetSight();
		player.rigidbody2D.WakeUp();
	}

	[RPC]
	public void DieSound() {
		_source.PlayOneShot(_yells[Random.Range(0, deathYellCount)]);
	}
}
