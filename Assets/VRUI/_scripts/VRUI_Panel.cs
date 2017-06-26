using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_Panel: VRUI_Object {
	private MeshRenderer _bgMeshRenderer;
	private BoxCollider _bgBoxCollider;
	private GameObject _goBg;
	private GameObject[] _goBorder;
	private MeshRenderer[] _borderMeshRenderer;

	private float _borderWidth = 0.01f;
	private float _bgDepth = 0.001f;
	public float bgDepth {
		get {
			return _bgDepth;
		}
	}

	private static int _counter = 0;

	private VRUI_Panel () : base () {}

	public static VRUI_Panel Create (float width, float height, Color clrBg, Color clrBorder) {
		GameObject go = new GameObject ();
		VRUI_Panel vruiPanel = go.AddComponent<VRUI_Panel> ();
		VRUI_Panel.Init ();
		vruiPanel._width = width;
		vruiPanel._height = height;

		vruiPanel.SetBgColor (clrBg);
		vruiPanel.SetBorderColor (clrBorder);


//		if (clrBorder != Color.clear) {}

		++_counter;
		go.name = "VRUI_Panel ("+_counter+")";

		return vruiPanel;
	}

	public void SetBgColor (Color newBgColor) {
		if (_goBg == null) {
			if (newBgColor != Color.clear) {
				_goBg = GameObject.CreatePrimitive (PrimitiveType.Quad);
				_goBg.transform.SetParent (transform);
				_bgBoxCollider = _goBg.AddComponent<BoxCollider> ();
				_bgMeshRenderer = _goBg.GetComponent<MeshRenderer> ();
				_goBg.transform.localScale = new Vector3 (_width, _height, 1f);
				_goBg.transform.localPosition = new Vector3 (0f, 0f, _bgDepth);
			}
		}
		if (_bgMeshRenderer != null) {
			if (newBgColor == Color.clear) {
				_goBg.SetActive (false);
			} else {
				_goBg.SetActive (true);
				_bgMeshRenderer.material.shader = VRUI_ShaderManager.GetShader ("Unlit/Color");
				_bgMeshRenderer.material.color = newBgColor;
			}
		}
	}

	public void SetBorderColor (Color newBorderColor) {
		if (_goBorder == null) {
			if (newBorderColor != Color.clear) {
				_goBorder = new GameObject[4];
				_borderMeshRenderer = new MeshRenderer[4];

				_goBorder [0] = GameObject.CreatePrimitive (PrimitiveType.Quad);
				_goBorder [0].transform.localScale = new Vector3 (_borderWidth, _height + _borderWidth, 1f);
				_goBorder [0].transform.SetParent (transform);
				_goBorder [0].transform.localPosition = new Vector3 (-_width/2, 0f, 0f);
				_borderMeshRenderer [0] = _goBorder [0].GetComponent<MeshRenderer> ();

				_goBorder [1] = GameObject.CreatePrimitive (PrimitiveType.Quad);
				_goBorder [1].transform.localScale = new Vector3 (_borderWidth, _height + _borderWidth, 1f);
				_goBorder [1].transform.SetParent (transform);
				_goBorder [1].transform.localPosition = new Vector3 (_width/2, 0f, 0f);
				_borderMeshRenderer [1] = _goBorder [1].GetComponent<MeshRenderer> ();

				_goBorder [2] = GameObject.CreatePrimitive (PrimitiveType.Quad);
				_goBorder [2].transform.localScale = new Vector3 (_width + _borderWidth, _borderWidth, 1f);
				_goBorder [2].transform.SetParent (transform);
				_goBorder [2].transform.localPosition = new Vector3 (0f, -_height/2, 0f);
				_borderMeshRenderer [2] = _goBorder [2].GetComponent<MeshRenderer> ();

				_goBorder [3] = GameObject.CreatePrimitive (PrimitiveType.Quad);
				_goBorder [3].transform.localScale = new Vector3 (_width + _borderWidth, _borderWidth, 1f);
				_goBorder [3].transform.SetParent (transform);
				_goBorder [3].transform.localPosition = new Vector3 (0f, _height/2, 0f);
				_borderMeshRenderer [3] = _goBorder [3].GetComponent<MeshRenderer> ();
			}
		}
		if (_borderMeshRenderer != null) {
			if (newBorderColor == Color.clear) {
				for (int i = 0; i < _borderMeshRenderer.Length; i++) {
					_goBorder [i].SetActive (false);
				}
			} else {
				for (int i = 0; i < _borderMeshRenderer.Length; i++) {
					_goBorder [i].SetActive (true);
					_borderMeshRenderer [i].material.shader = VRUI_ShaderManager.GetShader ("Unlit/Color");
					_borderMeshRenderer [i].material.color = newBorderColor;
				}
			}
		}
	}


	public override void Refresh () {
		if ((_goBg.transform.localScale.x != _width) || (_goBg.transform.localScale.y != _height)) {
			_goBg.transform.localScale = new Vector3 (_width, _height, 1f);
			for (int i = 0; i < _borderMeshRenderer.Length; i++) {
				_goBorder [0].transform.localScale = new Vector3 (_borderWidth, _height + _borderWidth, 1f);
				_goBorder [0].transform.localPosition = new Vector3 (-_width / 2, 0f, 0f);

				_goBorder [1].transform.localScale = new Vector3 (_borderWidth, _height + _borderWidth, 1f);
				_goBorder [1].transform.localPosition = new Vector3 (_width / 2, 0f, 0f);

				_goBorder [2].transform.localScale = new Vector3 (_width + _borderWidth, _borderWidth, 1f);
				_goBorder [2].transform.localPosition = new Vector3 (0f, -_height / 2, 0f);

				_goBorder [3].transform.localScale = new Vector3 (_width + _borderWidth, _borderWidth, 1f);
				_goBorder [3].transform.localPosition = new Vector3 (0f, _height / 2, 0f);
			}
		}
	}
}
