using UnityEngine;
using System.Collections;

public class StaticTV : MonoBehaviour {

	public float limitRandomX = 0.1f;
//	public float limitRandomY = 0.1f;
	public float scrollY;
	public float scrollSpeed = 0.1f;

	private Renderer renderer;

	// Use this for initialization
	void Start () 
	{
		renderer = transform.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
//		renderer.material.SetTextureOffset("_MainTex", new Vector2(Random.Range (-limitRandomX, limitRandomX), Random.Range (-limitRandomY, limitRandomY)));
		scrollY += scrollSpeed * Time.deltaTime;
		renderer.material.SetTextureOffset("_MainTex", new Vector2(Random.Range (-limitRandomX, limitRandomX), scrollY));
	}
}
