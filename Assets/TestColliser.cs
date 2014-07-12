using UnityEngine;
using System.Collections;

public class TestColliser : MonoBehaviour {

	public float zSpeed = .01f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		this.transform.Translate(0,0, zSpeed);
	}
}
