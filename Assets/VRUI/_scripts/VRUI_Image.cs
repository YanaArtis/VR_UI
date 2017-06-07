using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_Image: VRUI_Object {
	private Texture2D texture;
	private Material material;
	private MeshRenderer meshRenderer;
	private BoxCollider boxCollider;

	private static int _counter = 0;

	protected VRUI_Image () : base () {}

	public static VRUI_Image Create (Texture2D tex, float imageHeight) {
		GameObject go = GameObject.CreatePrimitive (PrimitiveType.Quad);
		VRUI_Image vruiImage = go.AddComponent<VRUI_Image> ();
		VRUI_Image.Init ();
		vruiImage.boxCollider = go.AddComponent<BoxCollider> ();
		vruiImage.meshRenderer = go.GetComponent<MeshRenderer> ();
		vruiImage.material = new Material (shaderTransparent);
		vruiImage.material.mainTexture = tex;
		vruiImage.meshRenderer.material = vruiImage.material;
		go.transform.localScale = new Vector3 (imageHeight*(float)tex.width/(float)tex.height, imageHeight, 1f);
		go.transform.localRotation = Quaternion.Euler (0f, 0f, 0f);

		Bounds bounds = vruiImage.meshRenderer.bounds;
		vruiImage.width = bounds.size.x;
		vruiImage.height = bounds.size.y;

		++_counter;
		go.name = "VRUI_Image ("+_counter+")";
		return vruiImage;
	}
}
