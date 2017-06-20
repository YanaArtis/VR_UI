using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_Text : VRUI_Object {
	private TextMesh textMesh;
	private MeshRenderer meshRenderer;

	protected static string fontDefault = "Arial.ttf";
//	private static bool isFontDefaultSeekRequired = true;
	private static float _scaleFactor = 0.0595f;
	private static int fontSize = 150;

	private static int _counter = 0;

	protected VRUI_Text () : base () {}

	protected static VRUI_Text _Create (string ss, float stringHeight, Color color, Font font) {
		string s = DecodeEncodedNonAsciiCharacters(ss);

		GameObject go = new GameObject();
		VRUI_Text vruiText = go.AddComponent<VRUI_Text> ();
		vruiText.textMesh = go.AddComponent<TextMesh> ();
		vruiText.SetColor (color);

		vruiText.meshRenderer = go.GetComponent<MeshRenderer> ();
//		vruiText.meshRenderer.material = new Material (shaderStandard); //Shader.Find ("Standard"));
//		vruiText.meshRenderer.material.color = color;
		vruiText.SetFont (font);

		Debug.Log ("vruiText.textMesh.fontSize: " + vruiText.textMesh.fontSize);
		vruiText.textMesh.fontSize = fontSize;

		/*
		vruiText.textMesh.text = s;
		float scaleFactor = _scaleFactor * stringHeight;
		go.transform.localScale = new Vector3 (scaleFactor, scaleFactor, scaleFactor);

		Bounds bounds = vruiText.meshRenderer.bounds;
		vruiText.width = bounds.size.x;
		vruiText.height = bounds.size.y;
		*/
		vruiText.textMesh.text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz12334567890|";
		Bounds bounds = vruiText.meshRenderer.bounds;
		float scaleFactor = stringHeight / bounds.size.y;
		go.transform.localScale = new Vector3 (scaleFactor, scaleFactor, scaleFactor);

		vruiText.textMesh.text = s;
		bounds = vruiText.meshRenderer.bounds;
		vruiText._width = bounds.size.x;
		vruiText._height = bounds.size.y;

		++_counter;
		go.name = "VRUI_Text ("+_counter+")";
		return vruiText;
	}

	public static VRUI_Text Create (string s, float stringHeight, Color color) {
		VRUI_Text.Init ();
//		VRUI_Text vruiText = VRUI_Text._Create (s, stringHeight, color, fontDefault);
		VRUI_Text vruiText = VRUI_Text._Create (s, stringHeight, color, VRUI_FontManager.GetFont (fontDefault));
		return vruiText;
	}

	public static VRUI_Text Create (string s, float stringHeight, Color color, Font font) {
		return VRUI_Text._Create (s, stringHeight, color, font);
	}

	public static VRUI_Text Create (string s, float stringHeight, Color color, string fontName) {
		return VRUI_Text._Create (s, stringHeight, color, VRUI_FontManager.GetFont ((fontName == null) ? fontDefault : fontName));
	}

	public void SetFont (Font font) {
		textMesh.font = font;
		meshRenderer.sharedMaterial = font.material;
	}

	public void SetFont (string fontName) {
		SetFont (VRUI_FontManager.GetFont (fontName));
	}

	protected static void Init () {
//		if (isFontDefaultSeekRequired) {
//			fontDefault = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
//				//Instantiate (Resources.FindObjectsOfTypeAll (typeof (Font)) [0]) as Font;
//		}
	}

	public void SetAlignment (TextAlignment alignment) {
		textMesh.alignment = alignment;
	}

	public void SetAnchor (TextAnchor anchor) {
		textMesh.anchor = anchor;
	}

	public void SetColor (Color newColor) {
		Debug.Log ("VRUI_Text.SetColor(" + newColor + "), text=\"" + textMesh.text + "\"");
		textMesh.color = newColor;
	}

	public static VRUI_Text CreateFromJSON (JSONObject j, float parentHeight) {
//		Debug.Log ("------------- VRUI_Text CreateFromJSON ()");
//		Debug.Log (j);
		float stringHeight = j.HasField ("height") ? j.GetField ("height").f : parentHeight;
		string text = j.HasField ("text") ? j.GetField ("text").str : null;
		string sColor = j.HasField ("color") ? j.GetField ("color").str : null;
		Color color = (sColor == null) ? Color.black : ParseColor (sColor);
		string sFont = j.HasField ("font") ? j.GetField ("font").str : null;
//		Debug.Log ("text: \""+text+"\"");
//		Debug.Log ("stringHeight: "+stringHeight);
//		Debug.Log ("color: \t\""+color+"\"");
		return Create (text, stringHeight, color, sFont);
	}

	public static VRUI_Text CreateFromJSON (JSONObject j) {
		return CreateFromJSON (j, 1f);
	}
}
