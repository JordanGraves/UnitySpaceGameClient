using UnityEngine;
using System.Collections;

public class AnimateBlackHole : MonoBehaviour {
	public float scrollSpeed = 3.5F;
	void Update() {
		float randomX = Random.Range (0.0f, 0.10f); 
		float randomY = Random.Range (0.0f, 0.10f);
		float offset = Time.time * scrollSpeed;

		renderer.materials[0].SetTextureOffset("_MainTex", new Vector2(-offset/4 + randomY, -offset + randomX));
		renderer.materials[1].SetTextureOffset("_MainTex", new Vector2(-offset/2 + randomY, offset + randomX));
		renderer.materials[2].SetTextureOffset("_MainTex", new Vector2(offset + randomY, -offset/2 + randomX));
		renderer.materials[3].SetTextureOffset("_MainTex", new Vector2(offset/2 + randomY, offset/2 + randomX));
		renderer.materials[4].SetTextureOffset("_MainTex", new Vector2(-offset/4 + randomY, -offset/4 + randomX));
	}
}
