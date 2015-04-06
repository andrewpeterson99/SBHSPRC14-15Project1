using UnityEngine;
using System.Collections;

public class PlayerColliderRefScript : MonoBehaviour {

	public GameObject player;
	public AudioClip hitSFX;

	void Update(){
		if (player.GetComponent<PlayerScript> ().lives == 0) {
			this.gameObject.GetComponent<MeshCollider>().enabled = false;
			this.gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "EnemyBullet") {
			GetComponent<AudioSource>().PlayOneShot(hitSFX);
			Destroy(other.gameObject);
			player.GetComponent<PlayerScript>().lives -= 1;
		}
	}
}
