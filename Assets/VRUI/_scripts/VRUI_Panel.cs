using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_Panel: VRUI_Object {
	private MeshRenderer bgMeshRenderer;
	private BoxCollider bgBoxCollider;
	private GameObject goBg;
	private GameObject[] goBorder;
	private MeshRenderer[] borderMeshRenderer;

	private float borderWidth = 0.01f;
	private float bgDepth = 0.001f;

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


		if (clrBorder != Color.clear) {
		}

		++_counter;
		go.name = "VRUI_Panel ("+_counter+")";

		return vruiPanel;
	}

	public void SetBgColor (Color newBgColor) {
		if (goBg == null) {
			if (newBgColor != Color.clear) {
				goBg = GameObject.CreatePrimitive (PrimitiveType.Quad);
				goBg.transform.SetParent (transform);
				bgBoxCollider = goBg.AddComponent<BoxCollider> ();
				bgMeshRenderer = goBg.GetComponent<MeshRenderer> ();
				goBg.transform.localScale = new Vector3 (_width, _height, 1f);
				goBg.transform.localPosition = new Vector3 (0f, 0f, bgDepth);
			}
		}
		if (bgMeshRenderer != null) {
			bgMeshRenderer.material.color = newBgColor;
		}
	}

	public void SetBorderColor (Color newBorderColor) {
		if (goBorder == null) {
			if (newBorderColor != Color.clear) {
				goBorder = new GameObject[4];
				borderMeshRenderer = new MeshRenderer[4];

				goBorder [0] = GameObject.CreatePrimitive (PrimitiveType.Quad);
				goBorder [0].transform.localScale = new Vector3 (borderWidth, _height + borderWidth, 1f);
				goBorder [0].transform.SetParent (transform);
				goBorder [0].transform.localPosition = new Vector3 (-_width/2, 0f, 0f);
				borderMeshRenderer [0] = goBorder [0].GetComponent<MeshRenderer> ();

				goBorder [1] = GameObject.CreatePrimitive (PrimitiveType.Quad);
				goBorder [1].transform.localScale = new Vector3 (borderWidth, _height + borderWidth, 1f);
				goBorder [1].transform.SetParent (transform);
				goBorder [1].transform.localPosition = new Vector3 (_width/2, 0f, 0f);
				borderMeshRenderer [1] = goBorder [1].GetComponent<MeshRenderer> ();

				goBorder [2] = GameObject.CreatePrimitive (PrimitiveType.Quad);
				goBorder [2].transform.localScale = new Vector3 (_width + borderWidth, borderWidth, 1f);
				goBorder [2].transform.SetParent (transform);
				goBorder [2].transform.localPosition = new Vector3 (0f, -_height/2, 0f);
				borderMeshRenderer [2] = goBorder [2].GetComponent<MeshRenderer> ();

				goBorder [3] = GameObject.CreatePrimitive (PrimitiveType.Quad);
				goBorder [3].transform.localScale = new Vector3 (_width + borderWidth, borderWidth, 1f);
				goBorder [3].transform.SetParent (transform);
				goBorder [3].transform.localPosition = new Vector3 (0f, _height/2, 0f);
				borderMeshRenderer [3] = goBorder [3].GetComponent<MeshRenderer> ();
			}
		}
		if (borderMeshRenderer != null) {
			for (int i = 0; i < borderMeshRenderer.Length; i++) {
				borderMeshRenderer [i].material.color = newBorderColor;
			}
		}
	}
}
