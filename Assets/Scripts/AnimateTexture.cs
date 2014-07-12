using UnityEngine;
using System.Collections;

public class AnimateTexture : MonoBehaviour {
	public float scrollSpeed = 1.5F;
	float angle;
	void Update() {
		angle = Random.Range (0.0f, 1.0f); 
		float offset = Time.time * scrollSpeed * angle;
		renderer.materials[0].SetTextureOffset("_MainTex", new Vector2(0, -offset));
		renderer.materials[1].SetTextureOffset("_MainTex", new Vector2(0, offset));
		renderer.materials[2].SetTextureOffset("_MainTex", new Vector2(0, -offset/2f));
		renderer.materials[3].SetTextureOffset("_MainTex", new Vector2(0, offset/2f));
		renderer.materials[4].SetTextureOffset("_MainTex", new Vector2(0, -offset/4f));
		renderer.materials[5].SetTextureOffset("_MainTex", new Vector2(0, offset/4));
		renderer.materials[6].SetTextureOffset("_MainTex", new Vector2(0, -offset/8));
		renderer.materials[7].SetTextureOffset("_MainTex", new Vector2(0, offset/8));
		renderer.materials[8].SetTextureOffset("_MainTex", new Vector2(0, -offset/16));
		renderer.materials[9].SetTextureOffset("_MainTex", new Vector2(0, offset/16));
		renderer.materials[10].SetTextureOffset("_MainTex", new Vector2(0, -offset*2));
		renderer.materials[11].SetTextureOffset("_MainTex", new Vector2(0, offset*2));
		renderer.materials[12].SetTextureOffset("_MainTex", new Vector2(0, -offset*1.5f));
		renderer.materials[13].SetTextureOffset("_MainTex", new Vector2(0, offset*1.5f));
		renderer.materials[14].SetTextureOffset("_MainTex", new Vector2(0, offset*1.8f));
	}
}