using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_Reticle : VRUI_Object {
	private Vector3 _selectedObjectHitPosition;
	private GameObject _goSelectedObject;
	private Vector2 _selectedObjectPixelUV;
	private float _noHitDistance;

	private Texture2D _texture;
	private Material _material;
	private MeshRenderer _meshRenderer;

	private static int _counter = 0;

	protected VRUI_Reticle () : base () {}

	public static VRUI_Reticle Create (Texture2D texture, float cursorHeight, float noHitDistance) {

		GameObject go = GameObject.CreatePrimitive (PrimitiveType.Quad);
		Destroy (go.GetComponent<MeshCollider> ());
		VRUI_Reticle vruiReticle = go.AddComponent<VRUI_Reticle> ();
		VRUI_Reticle.Init ();
		vruiReticle._meshRenderer = go.GetComponent<MeshRenderer> ();

		try {
			vruiReticle._material = new Material (shaderTransparent);
		} catch (System.Exception e) {
			FileManager.WriteToLog (e.ToString());
		}

		vruiReticle.SetCursor (texture);
		go.transform.localScale = new Vector3 (cursorHeight*(float)texture.width/(float)texture.height, cursorHeight, 1f);
		go.transform.localRotation = Quaternion.Euler (0f, 0f, 0f);
		Bounds bounds = vruiReticle._meshRenderer.bounds;
		vruiReticle._width = bounds.size.x;
		vruiReticle._height = bounds.size.y;

		vruiReticle._noHitDistance = noHitDistance;

		++_counter;
		go.name = "VRUI_Reticle ("+_counter+")";

		/*
		GameObject go2 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		Destroy (go2.GetComponent<MeshCollider> ());
		go2.transform.SetParent (go.transform);
		go2.transform.localPosition = Vector3.zero;
		go2.transform.localScale = Vector3.one;
		*/

		return vruiReticle;
	}

	public RaycastHit CastRay (Vector3 origin, Vector3 direction, float maxDistance) {
		Ray ray = new Ray (origin, direction);
		return CastRay (ray, maxDistance);
	}

	public RaycastHit CastRay (Ray ray, float maxDistance) {
		RaycastHit hit;
		Vector3 newPosition;
		if (Physics.Raycast (ray, out hit, maxDistance)) {
			_goSelectedObject = hit.collider.gameObject;
			_selectedObjectHitPosition = hit.point;
			_selectedObjectPixelUV = hit.textureCoord;
			newPosition = _selectedObjectHitPosition;
		} else {
			_goSelectedObject = null;
			_selectedObjectHitPosition = Vector3.zero;
			_selectedObjectPixelUV = Vector2.zero;
			newPosition = ray.origin + ray.direction * _noHitDistance;
		}
		gameObject.transform.position = ray.origin;
		gameObject.transform.LookAt (newPosition);
		gameObject.transform.position = newPosition;
		return hit;
	}

	public GameObject GetSelectedObject () {
		return _goSelectedObject;
	}

	public Vector3 GetSelectedObjectHitPosition () {
		return _selectedObjectHitPosition;
	}

	public Vector2 GetSelectedObjectTextureCoordinates () {
		return _selectedObjectPixelUV;
	}

	public void SetCursor (Texture2D texture) {
		_material.mainTexture = texture;
		_meshRenderer.material = _material;
	}
}
