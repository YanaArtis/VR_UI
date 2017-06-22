using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_Reticle : VRUI_Object {
	private Vector3 _selectedObjectHitPosition;
	private GameObject _goSelectedObject;
	private Vector2 _selectedObjectPixelUV;
	private float _noHitDistance;
	private bool _isGaze;
	private bool _isTriggerOn = false;

	private Texture2D _texture;
	private Material _material;
	private MeshRenderer _meshRenderer;
	private GameObject _goCursor;
	private float _dx = 0f;
	private float _dy = 0f;

	private const float DISTANCE_TO_SURFACE = 0.04f;

	private static int _counter = 0;

	protected VRUI_Reticle () : base () {}

	public static VRUI_Reticle Create (Texture2D texture, float cursorHeight
		, float noHitDistance, bool isGaze,float dx, float dy) {

		GameObject go = new GameObject ();
		VRUI_Reticle vruiReticle = go.AddComponent<VRUI_Reticle> ();
		VRUI_Reticle.Init ();
		vruiReticle._goCursor = GameObject.CreatePrimitive (PrimitiveType.Quad);
		Destroy (vruiReticle._goCursor.GetComponent<MeshCollider> ());
		vruiReticle._meshRenderer = vruiReticle._goCursor.GetComponent<MeshRenderer> ();
		vruiReticle._isGaze = isGaze;
		vruiReticle._dx = dx;
		vruiReticle._dy = dy;
		vruiReticle._height = cursorHeight;

		try {
			vruiReticle._material = new Material (VRUI_ShaderManager.GetShader ("Unlit/Transparent"));
		} catch (System.Exception e) {
			FileManager.WriteToLog (e.ToString());
		}

		go.transform.localRotation = Quaternion.Euler (0f, 0f, 0f);
		vruiReticle._goCursor.transform.SetParent (go.transform);
		vruiReticle.SetCursor (texture);
		vruiReticle._goCursor.transform.localRotation = Quaternion.Euler (0f, 0f, 0f);
		vruiReticle._goCursor.transform.localPosition = new Vector3 (vruiReticle._dx, vruiReticle._dy, 0f);
		vruiReticle._goCursor.name = "Cursor";
		Bounds bounds = vruiReticle._meshRenderer.bounds;
		vruiReticle._width = bounds.size.x;
		vruiReticle._height = bounds.size.y;

		vruiReticle._noHitDistance = noHitDistance;

		++_counter;
		go.name = "VRUI_Reticle ("+_counter+")";
		return vruiReticle;
	}

	public static VRUI_Reticle Create (Texture2D texture, float cursorHeight
		, float noHitDistance, bool isGaze) {
		return Create (texture, cursorHeight, noHitDistance, isGaze, 0f, 0f);
	}

	void LateUpdate () {
		_isTriggerOn = false;
	}

	public RaycastHit CastRay (Vector3 origin, Vector3 direction, float maxDistance) {
		Ray ray = new Ray (origin, direction);
		return CastRay (ray, maxDistance);
	}

	public RaycastHit CastRay (Ray ray, float maxDistance) {
		RaycastHit hit;
		Vector3 newPosition;
		bool flagHit;
		if (flagHit = Physics.Raycast (ray, out hit, maxDistance)) {
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
		if (flagHit) {
			gameObject.transform.Translate ((ray.origin - newPosition).normalized * DISTANCE_TO_SURFACE);
		}
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
		if (texture == null) {
			_width = _height;
			_goCursor.SetActive (false);
		} else {
			_width = _height * (float)texture.width / (float)texture.height;
			_goCursor.SetActive (true);
		}
		_goCursor.transform.localScale = new Vector3 (_width, _height, 1f);
		_material.mainTexture = texture;
		_meshRenderer.material = _material;
	}

	public bool IsGaze () {
		return _isGaze;
	}

	public bool IsTriggerOn () {
		return _isTriggerOn;
	}

	public void SetTriggerOn () {
		_isTriggerOn = true;
	}
}
