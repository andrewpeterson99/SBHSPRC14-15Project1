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
	public AudioClip explosionSFX;
	public int lives;
	public int enemyAmount;
	public GameObject lifeOne;
	public GameObject lifeTwo;
	public GameObject lifeThree;
	public GameObject explosion;
	private GameObject instantiatedExplosion;
	
	// Use this for initialization
	void Start () {
		player = transform;
		bgm = bgm.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (lives > 0) {
			player.Translate (Vector3.forward * moveSpeed * Time.deltaTime * Input.GetAxis ("Vertical"));
			player.Translate (Vector3.right * moveSpeed * Time.deltaTime * Input.GetAxis ("Horizontal"));
		}
		if (Input.GetKey (KeyCode.E)) {
			transform.Rotate (0, turnSpeed * Time.deltaTime, 0, Space.World);
		} else if (Input.GetKey (KeyCode.Q)) {
			transform.Rotate (0, -turnSpeed * Time.deltaTime, 0, Space.World);
		}
		if(Input.GetKeyDown(KeyCode.Space) && canvasActive == false && lives > 0){
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
		//CODE CONTROLLING THE WRAP
		if (transform.position.x <= -1120.767f) {
			transform.position = new Vector3(1142.68f, transform.position.y, transform.position.z);
		}
		if (transform.position.x >= 1143f) {
			transform.position = new Vector3(-1115.766f, transform.position.y, transform.position.z);
		}
		if (transform.position.z >= 523.1021f) {
			transform.position = new Vector3(transform.position.x, transform.position.y, -589.5607f);
		}
		if (transform.position.z <= -589.5608f) {
			transform.position = new Vector3(transform.position.x, transform.position.y, 523.102f);
		}
		//END OF WRAPPING CODE
		if (lives == 2) {
			lifeOne.SetActive(false);
		}
		if (lives == 1) {
			lifeTwo.SetActive(false);
		}
		if (lives == 0) {
			lifeThree.SetActive(false);
			StartCoroutine(GameOver());
			bgm.PlayOneShot(explosionSFX);
			instantiatedExplosion = (GameObject) Instantiate(explosion, this.transform.position, Quaternion.identity);
			Destroy(instantiatedExplosion, 2f);
		}
	}
	void Shoot(){
		GameObject newBullet = (GameObject)Instantiate (bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
		//newBullet.GetComponent<Rigidbody> ().AddForce (newBullet.transform.forward * bulletSpeed, ForceMode.VelocityChange);
		Destroy (newBullet, 6f);
		bgm.PlayOneShot (laserSFX);
	}
	public void LoadNextLevel(){
		Time.timeScale = 1f;
		bgm.UnPause ();
		panel.SetActive (true);
		StartCoroutine(ExitGame());
	}
	//exits game after 2.5 second delay (fade on panel), audio exit
	IEnumerator ExitGame(){
		bgm.PlayOneShot (exitSound);
		yield return new WaitForSeconds (0.5f);
		exitLoad = true;
		yield return new WaitForSeconds(2.25f);
		Debug.Log ("Quit!");
		Application.Quit ();
	}
	IEnumerator GameOver(){
		yield return new WaitForSeconds (5f);
		Application.LoadLevel ("GameOverScene");
	}
}




