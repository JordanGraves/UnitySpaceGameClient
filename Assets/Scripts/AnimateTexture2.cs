using UnityEngine;
using System.Collections;

public class AnimateTexture2 : MonoBehaviour {
	public float scrollSpeed = 0.5F;
	void Update() {
		float offset = Time.time * scrollSpeed;
		renderer.materials[0].SetTextureOffset("_MainTex", new Vector2(0, -offset));
		renderer.materials[1].SetTextureOffset("_MainTex", new Vector2(0, -offset));
	}
}