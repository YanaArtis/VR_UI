using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_Image: VRUI_Object {
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
		vruiImage._material = new Material (VRUI_ShaderManager.GetShader ("Unlit/Transparent"));
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

	public void ReadDataFromJson (JSONObject j) {
		float oldWidth = _width;
		(this as VRUI_Object).ReadDataFromJson (j);
		_width = oldWidth;
	}

	public static VRUI_Image CreateFromJSON (JSONObject j) {
		float height = 0f;
		string sHeight = j.HasField("height") ? j.GetField ("height").str : null;
		VRUI_Dimension d = new VRUI_Dimension ();
		d.Parse (sHeight);
		switch (d.type) {
		case VRUI_Dimension.Type.METERS:
			height = d.value;
			break;
		}

		string sTextureFname = j.HasField ("src") ? j.GetField ("src").str : null;
		if (sTextureFname != null) {
			Texture2D tex = null;
			if (sTextureFname.StartsWith ("file://")) {
				tex = new Texture2D (2, 2);
				FileManager.ReadImageFromDirectory(tex, sTextureFname);
			} else {
				tex = FileManager.ReadImageFromResources (null, sTextureFname);
			}
			VRUI_Image vruiImage = VRUI_Image.Create (tex, height);
			vruiImage.ReadDataFromJson (j);
			return vruiImage;
		}

		return null;
	}
}
