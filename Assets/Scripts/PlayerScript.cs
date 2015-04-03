using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour {

	public float moveSpeed;
	public float turnSpeed;
	private Transform player;
	public GameObject bulletSpawn;
	public GameObject bullet;
	public float bulletSpeed;
	public GameObject canvas;
	public bool canvasActive = false;
	public AudioSource bgm;
	public AudioClip laserSFX;
	public bool cursorEffects;
	public GameObject panel;
	public AudioClip exitSound;
	public bool exitLoad = false;
	
	// Use this for initialization
	void Start () {
		player = transform;
		bgm = bgm.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		player.Translate (Vector3.forward * moveSpeed * Time.deltaTime * Input.GetAxis ("Vertical"));
		player.Translate (Vector3.right * moveSpeed * Time.deltaTime * Input.GetAxis ("Horizontal"));
		if (Input.GetKey (KeyCode.E)) {
			transform.Rotate (0, turnSpeed * Time.deltaTime, 0, Space.World);
		} else if (Input.GetKey (KeyCode.Q)) {
			transform.Rotate (0, -turnSpeed * Time.deltaTime, 0, Space.World);
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			Shoot();
		}
		if (Input.GetKeyDown (KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
			canvasActive = !canvasActive;
			Time.timeScale = 0f;
			canvas.SetActive(canvasActive);
		}
		if (canvasActive == true) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			bgm.Pause ();
			if(Time.timeScale == 1f){
				bgm.UnPause();
			}
		} else {
			if(cursorEffects){
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			}
			Time.timeScale = 1f;
			bgm.UnPause();
		}
		if (exitLoad == true) {
			bgm.volume -=0.01f;
		}
	}
	void Shoot(){
		GameObject newBullet = (GameObject)Instantiate (bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
		newBullet.GetComponent<Rigidbody> ().AddForce (newBullet.transform.forward * bulletSpeed, ForceMode.VelocityChange);
		Destroy (newBullet, 10f);
		bgm.PlayOneShot (laserSFX);
	}
	public void LoadNextLevel(){
		Time.timeScale = 1f;
		bgm.UnPause ();
		panel.SetActive (true);
		StartCoroutine(LevelLoad());
	} 
	//exits game after 2.5 second delay (fade on panel), audio exit
	IEnumerator LevelLoad(){
		bgm.PlayOneShot (exitSound);
		yield return new WaitForSeconds (0.5f);
		exitLoad = true;
		yield return new WaitForSeconds(2.5f);
		Debug.Log ("Quit!");
		Application.Quit ();
	}
}




