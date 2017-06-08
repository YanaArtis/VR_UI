using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_Image: VRUI_Object {
//	private Texture2D _texture;
	private Material _material;
	private MeshRenderer _meshRenderer;
	private BoxCollider _boxCollider;

	private static int _counter = 0;

	protected VRUI_Image () : base () {}

	public static VRUI_Image Create (Texture2D tex, float imageHeight) {
		GameObject go = GameObject.CreatePrimitive (PrimitiveType.Quad);
		VRUI_Image vruiImage = go.AddComponent<VRUI_Image> ();
		VRUI_Image.Init ();
		vruiImage._boxCollider = go.AddComponent<BoxCollider> ();
		vruiImage._meshRenderer = go.GetComponent<MeshRenderer> ();
		vruiImage._material = new Material (shaderTransparent);
		vruiImage._material.mainTexture = tex;
		vruiImage._meshRenderer.material = vruiImage._material;
		go.transform.localScale = new Vector3 (imageHeight*(float)tex.width/(float)tex.height, imageHeight, 1f);
		go.transform.localRotation = Quaternion.Euler (0f, 0f, 0f);

		Bounds bounds = vruiImage._meshRenderer.bounds;
		vruiImage._width = bounds.size.x;
		vruiImage._height = bounds.size.y;

		++_counter;
		go.name = "VRUI_Image ("+_counter+")";
		return vruiImage;
	}
}
