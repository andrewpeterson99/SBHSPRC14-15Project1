using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

	public float speed = 1000f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime * speed;
	
	}
}
