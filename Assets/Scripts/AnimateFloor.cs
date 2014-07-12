using UnityEngine;
using System.Collections;

public class AnimateFloor : MonoBehaviour {
	public float scrollSpeed = 0.5F;


	void Update() {
		float offset = Time.time * scrollSpeed;
		renderer.materials[0].SetTextureOffset("_MainTex", new Vector2(0, -offset));
	}
}