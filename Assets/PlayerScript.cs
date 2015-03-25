using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed;
	private Transform player;

	// Use this for initialization
	void Start () {
		player = transform;
	
	}
	
	// Update is called once per frame
	void Update () {
		player.Translate (Vector3.up * speed * Time.deltaTime * Input.GetAxis ("Vertical"));
		player.Translate (Vector3.right * speed * Time.deltaTime * Input.GetAxis("Horizontal"));
	
	}
}
