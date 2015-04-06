using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerObj;
	private GameObject instantiatedExplosion;
	public float speed;
	public GameObject bullet;
	public GameObject bulletSpawn;
	public float bulletSpeed;
	public float shootDelay;
	public float shootCompare;
	public AudioClip laserSFX;

	// Use this for initialization
	void Start () {
		playerObj.GetComponent<PlayerScript> ().enemyAmount += 1;
		shootCompare = Random.Range (1f, 5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (playerObj.GetComponent<PlayerScript>().canvasActive == false) {
			shootDelay += 0.01f;
		}
		gameObject.transform.LookAt (playerObj.transform);
		gameObject.transform.position = Vector3.MoveTowards (transform.position, playerObj.transform.position, speed * Time.deltaTime);
		if (shootDelay >= shootCompare) {
			shootDelay -= shootDelay;
			Shoot();
		}
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Bullet") {
			this.gameObject.GetComponent<MeshCollider>().enabled = false;
			instantiatedExplosion = (GameObject) Instantiate(explosion, this.gameObject.transform.position, Quaternion.identity);
			playerObj.GetComponent<PlayerScript>().enemyAmount -= 1;
			Destroy(instantiatedExplosion, 2f);
			Destroy (other.gameObject);
			Destroy(this.gameObject, .3f);
		}
	}
	void Shoot(){
		GetComponent<AudioSource> ().PlayOneShot (laserSFX);
		GameObject newBullet = (GameObject)Instantiate (bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
		newBullet.GetComponent<Rigidbody> ().AddForce (newBullet.transform.forward * bulletSpeed, ForceMode.VelocityChange);
		Destroy (newBullet, 6f);
	}
}




