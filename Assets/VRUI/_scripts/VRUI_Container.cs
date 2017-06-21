using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_Container : VRUI_Object {
	public enum Surface {PLANE, CYLINDER, SPHERE}
	private Surface _surface;
	public enum Layout {VERTICAL, HORIZONTAL, GRID, RELATIVE, LIST
		, ABSOLUTE	// With coordinates of items
		, FRAME		// Place items over each others
	}
	public Layout _layout = Layout.VERTICAL;
	public const int GRAVITY_LEFT = 1;
	public const int GRAVITY_RIGHT = 2;
	public const int GRAVITY_TOP = 4;
	public const int GRAVITY_BOTTOM = 8;
	public const int GRAVITY_HCENTER = 16;
	public const int GRAVITY_VCENTER = 32;
	public const int GRAVITY_CENTER = GRAVITY_HCENTER | GRAVITY_VCENTER;
	public int _gravity = GRAVITY_CENTER;

	public VRUI_Panel _vruiPanel;
	public List <VRUI_Object> _objects = new List<VRUI_Object> ();

	protected Color _clrBg;
	protected Color _clrBorder;
	protected float _childZ = -0.001f;

	private static int _counter = 0;

	protected VRUI_Container () : base () {}

	public static VRUI_Container Create (float width, float height, Layout layout, Color clrBg, Color clrBorder /* , Surface surface */) {
		GameObject go = new GameObject ();
		VRUI_Container vruiContainer = go.AddComponent<VRUI_Container> ();
		VRUI_Container.Init ();

		vruiContainer._width = width;
		vruiContainer._height = height;
		vruiContainer._layout = layout;
//		vruiContainer.surface = surface;

		vruiContainer._clrBg = clrBg;
		vruiContainer._clrBorder = clrBorder;

//		if ((clrBg != Color.clear) || (clrBorder != Color.clear)) {
			vruiContainer._vruiPanel = VRUI_Panel.Create (width, height, clrBg, clrBorder);
			vruiContainer._vruiPanel.transform.SetParent (go.transform);
//		}

		++_counter;
		go.name = "VRUI_Container ("+_counter+")";

		return vruiContainer;
	}

	public void SetGravity (int gravity) {
		_gravity = gravity;
		Refresh ();
	}

	public void Add (VRUI_Object obj) {
		_objects.Add (obj);
		obj.transform.SetParent (transform);
		switch (_layout) {
		case Layout.ABSOLUTE:
			obj.transform.localPosition = obj.transform.position;
			break;
		}
		Refresh ();
	}

	public override void Refresh () {
		switch (_layout) {
		case Layout.ABSOLUTE:
//			obj.transform.localPosition = obj.transform.position;
			break;
		case Layout.VERTICAL:
			CalculateLayout_Vertical ();
			break;
		case Layout.HORIZONTAL:
			CalculateLayout_Horizontal ();
			break;
		}
	}

	private void CalculateLayout_Vertical () {
		float x, y;
		TextAnchor textAnchor = TextAnchor.MiddleCenter;
		if (IsGravityLeft (_gravity)) {
			x = -_width / 2f + _paddingLeft;
//			textAnchor = IsGravityTop (_gravity) ? TextAnchor.UpperLeft : (IsGravityBottom (_gravity) ? TextAnchor.LowerLeft : TextAnchor.MiddleLeft);
//			textAnchor = IsGravityBottom (_gravity) ? TextAnchor.LowerLeft : TextAnchor.UpperLeft;
		} else if (IsGravityRight (_gravity)) {
			x = _width / 2f - _paddingRight;
//			textAnchor = IsGravityTop (_gravity) ? TextAnchor.UpperRight : (IsGravityBottom (_gravity) ? TextAnchor.LowerRight : TextAnchor.MiddleRight);
//			textAnchor = IsGravityBottom (_gravity) ? TextAnchor.LowerRight : TextAnchor.UpperRight;
		} else {
			x = 0f;
//			textAnchor = IsGravityTop (_gravity) ? TextAnchor.UpperCenter : (IsGravityBottom (_gravity) ? TextAnchor.LowerCenter : TextAnchor.MiddleCenter);
//			textAnchor = IsGravityBottom (_gravity) ? TextAnchor.LowerCenter : TextAnchor.UpperCenter;
		}

		// Calculate total objects height
		float objectsHeight = 0;
		int i;
		for (i = 0; i < _objects.Count; i++) {
			objectsHeight += _objects [i]._height + _objects [i].marginTop + _objects [i].paddingTop + _objects [i].marginBottom + _objects [i].paddingBottom;
			if (_objects [i] is VRUI_Text) {
				(_objects [i] as VRUI_Text).SetAnchor (textAnchor);
			}
		}

		float topY = _height / 2f - _paddingTop;
		float bottomY = -_height / 2f + _paddingTop;
		if (IsGravityTop (_gravity)) {
			y = topY;
			for (i = 0; (i < _objects.Count) && (y > bottomY); i++) {
//				Debug.Log ("CalculateLayout_Vertical() IsGravityTop "+i+" setActive(true)");
				Debug.Log ("Object #"+i+" ("+_objects[i]._id+") margins top: "+_objects[i].marginTop+", bottom: "+_objects[i].marginBottom+" paddings top: "+_objects[i].paddingTop+", bottom: "+_objects[i].paddingBottom);
				_objects [i].gameObject.SetActive (true);
				float objX = x + GetObjectDeltaX_forVerticalLayout (_objects [i]._width);
				y -= (_objects [i].marginTop + _objects [i].paddingTop);
				_objects [i].transform.localPosition = new Vector3 (objX, y - _objects [i]._height / 2f, _childZ);
				y -= (_objects [i]._height + _objects [i].marginBottom + _objects [i].paddingBottom);
			}
			for (; i < _objects.Count; i++) {
//				Debug.Log ("CalculateLayout_Vertical() IsGravityTop "+i+" setActive(false)");
				_objects [i].gameObject.SetActive (false);
			}
		} else if (IsGravityBottom (_gravity)) {
			y = bottomY;
			for (i = _objects.Count-1; (i >= 0) && (y < topY); i--) {
//				Debug.Log ("CalculateLayout_Vertical() IsGravityBottom "+i+" setActive(true)");
				Debug.Log ("Object #"+i+" ("+_objects[i]._id+") margins top: "+_objects[i].marginTop+", bottom: "+_objects[i].marginBottom+" paddings top: "+_objects[i].paddingTop+", bottom: "+_objects[i].paddingBottom);
				_objects [i].gameObject.SetActive (true);
				float objX = x + GetObjectDeltaX_forVerticalLayout (_objects [i]._width);
				y += (_objects [i].marginBottom + _objects [i].paddingBottom);
				_objects [i].transform.localPosition = new Vector3 (objX, y + _objects [i]._height / 2f, _childZ);
				y += (_objects [i]._height + _objects [i].marginTop + _objects [i].paddingTop);
//				y += (_objects [i].height + _objects [i].marginTop + _objects [i].marginBottom + _objects [i].paddingTop + _objects [i].paddingBottom);
			}
			for (; i >= 0; i--) {
//				Debug.Log ("CalculateLayout_Vertical() IsGravityBottom "+i+" setActive(false)");
				_objects [i].gameObject.SetActive (false);
			}
		} else {
			y = objectsHeight / 2;
			for (i = 0; (i < _objects.Count) && (y > topY); i++) {
//				Debug.Log ("CalculateLayout_Vertical() IsGravity VCenter "+i+" setActive(false)");
				y -= (_objects [i]._height + _objects [i].marginTop + _objects [i].marginBottom + _objects [i].paddingTop + _objects [i].paddingBottom);
				_objects [i].gameObject.SetActive (false);
			}
			for (; (i < _objects.Count) && (y > bottomY); i++) {
//				Debug.Log ("CalculateLayout_Vertical() IsGravity VCenter "+i+" setActive(true)");
				_objects [i].gameObject.SetActive (true);
				float objX = x + GetObjectDeltaX_forVerticalLayout (_objects [i]._width);
				y -= (_objects [i].marginTop + _objects [i].paddingTop);
				_objects [i].transform.localPosition = new Vector3 (objX, y - _objects [i]._height / 2f, _childZ);
				y -= (_objects [i]._height + _objects [i].marginBottom + _objects [i].paddingBottom);
			}
			for (; i < _objects.Count; i++) {
//				Debug.Log ("CalculateLayout_Vertical() IsGravity VCenter "+i+" setActive(false)");
				_objects [i].gameObject.SetActive (false);
			}
		}
	}

	private float GetObjectDeltaX_forVerticalLayout (float objWidth) {
		if (IsGravityLeft (_gravity)) {
			return objWidth / 2f;
		} else if (IsGravityRight (_gravity)) {
			return -objWidth / 2f;
		}
		return 0f;
	}

	private void CalculateLayout_Horizontal () {
		float x, y;
		TextAnchor textAnchor = TextAnchor.MiddleCenter;

		if (IsGravityBottom (_gravity)) {
			y = -_height / 2f + _paddingBottom;
		} else if (IsGravityTop (_gravity)) {
			y = _height / 2f - _paddingTop;
		} else {
			y = 0f;
		}

		// Calculate total objects width
		float objectsWidth = 0;
		int i;
		for (i = 0; i < _objects.Count; i++) {
			objectsWidth += _objects [i]._width + _objects [i].marginLeft + _objects [i].paddingLeft + _objects [i].marginRight + _objects [i].paddingRight;
			if (_objects [i] is VRUI_Text) {
				(_objects [i] as VRUI_Text).SetAnchor (textAnchor);
			}
		}

		float rightX = _width / 2f - _paddingRight;
		float leftX = -_width / 2f + _paddingLeft;
		if (IsGravityLeft (_gravity)) {
			x = leftX;
			for (i = 0; (i < _objects.Count) && (x < rightX); i++) {
//				Debug.Log ("CalculateLayout_Horizontal() IsGravityLeft "+i+" setActive(true)");
				_objects [i].gameObject.SetActive (true);
				float objY = y + GetObjectDeltaY_forHorizontalLayout (_objects [i]._height);
				x += (_objects [i].marginLeft + _objects [i].paddingLeft);
				_objects [i].transform.localPosition = new Vector3 (x + _objects [i]._width / 2f, objY, _childZ);
				x += (_objects [i]._width + _objects [i].marginRight + _objects [i].paddingRight);
			}
			for (; i < _objects.Count; i++) {
//				Debug.Log ("CalculateLayout_Horizontal() IsGravityLeft "+i+" setActive(false)");
				_objects [i].gameObject.SetActive (false);
			}
		} else if (IsGravityRight (_gravity)) {
			x = rightX;
			for (i = _objects.Count-1; (i >= 0) && (x > leftX); i--) {
//				Debug.Log ("CalculateLayout_Horizontal() IsGravityRight "+i+" setActive(true)");
				_objects [i].gameObject.SetActive (true);
				float objY = y + GetObjectDeltaY_forHorizontalLayout (_objects [i]._height);
				x -= (_objects [i].marginRight + _objects [i].paddingRight);
				_objects [i].transform.localPosition = new Vector3 (x - _objects [i]._width / 2f, objY, _childZ);
				x -= (_objects [i]._width + _objects [i].marginLeft + _objects [i].paddingLeft);
			}
			for (; i >= 0; i--) {
//				Debug.Log ("CalculateLayout_Horizontal() IsGravityRight "+i+" setActive(false)");
				_objects [i].gameObject.SetActive (false);
			}
		} else {
			x = -objectsWidth / 2;
//			Debug.Log ("x = "+x+", leftX = "+leftX+", rightX = "+rightX);
//			Debug.Log ("(x < leftX): "+(x < leftX));
			for (i = 0; (i < _objects.Count) && (x < leftX); i++) {
//				Debug.Log ("CalculateLayout_Horizontal() IsGravity HCenter "+i+" setActive(false)");
				x += (_objects [i]._width + _objects [i].marginLeft + _objects [i].marginRight + _objects [i].paddingLeft + _objects [i].paddingRight);
				_objects [i].gameObject.SetActive (false);
			}
			for (; (i < _objects.Count) && (x < rightX); i++) {
//				Debug.Log ("CalculateLayout_Horizontal() IsGravity HCenter "+i+" setActive(true)");
				_objects [i].gameObject.SetActive (true);
				float objY = y + GetObjectDeltaY_forHorizontalLayout (_objects [i]._height);
				x += (_objects [i].marginLeft + _objects [i].paddingLeft);
				_objects [i].transform.localPosition = new Vector3 (x + _objects [i]._width / 2f, objY, _childZ);
				x += (_objects [i]._width + _objects [i].marginRight + _objects [i].paddingRight);
			}
			for (; i < _objects.Count; i++) {
//				Debug.Log ("CalculateLayout_Horizontal() IsGravity HCenter "+i+" setActive(false)");
				_objects [i].gameObject.SetActive (false);
			}
		}
	}

	private float GetObjectDeltaY_forHorizontalLayout (float objHeight) {
		if (IsGravityBottom (_gravity)) {
			return objHeight / 2f;
		} else if (IsGravityTop (_gravity)) {
			return -objHeight / 2f;
		}
		return 0f;
	}

	public void Clear () {
		for (int i = 0; i < _objects.Count; i++) {
			if (_objects [i] is VRUI_Container) {
				(_objects [i] as VRUI_Container).Clear ();
			}
			Destroy (_objects [i]);
		}
	}

	public static bool IsGravityTop (int gravity) {
		return ((gravity & GRAVITY_TOP) != 0);
	}

	public static bool IsGravityBottom (int gravity) {
		return ((gravity & GRAVITY_BOTTOM) != 0);
	}

	public static bool IsGravityLeft (int gravity) {
		return ((gravity & GRAVITY_LEFT) != 0);
	}

	public static bool IsGravityRight (int gravity) {
		return ((gravity & GRAVITY_RIGHT) != 0);
	}

	public void CopyTo (VRUI_Container other) {
		((VRUI_Object) this).CopyTo ((VRUI_Object) other);
		other._surface = this._surface;
		other._layout = this._layout;
		other._gravity = this._gravity;
		other._clrBg = this._clrBg;
		other._clrBorder = this._clrBorder;
		other._vruiPanel = this._vruiPanel;
		if (this._objects == null) {
			other._objects = null;
		} else {
			other._objects = new List<VRUI_Object> ();
			for (int i = 0; i < this._objects.Count; i++) {
				other._objects.Add (this._objects [i]);
			}
		}
	}

	public void ReadDataFromJson (JSONObject j) {
		(this as VRUI_Object).ReadDataFromJson (j);

		string sGravities = j.HasField("gravity") ? j.GetField ("gravity").str : null;
		if (sGravities != null) {
			string[] sGravity = sGravities.Split ('|');
			int gravity = 0;
			for (int i = 0; i < sGravity.Length; i++) {
				Debug.Log ("Gravity " + i + ": " + sGravity [i]);
				if ("LEFT".Equals (sGravity [i])) {
					gravity |= GRAVITY_LEFT;
				} else if ("RIGHT".Equals (sGravity [i])) {
					gravity |= GRAVITY_RIGHT;
				} else if ("TOP".Equals (sGravity [i])) {
					gravity |= GRAVITY_TOP;
				} else if ("BOTTOM".Equals (sGravity [i])) {
					gravity |= GRAVITY_BOTTOM;
				} else if ("HCENTER".Equals (sGravity [i])) {
					gravity |= GRAVITY_HCENTER;
				} else if ("VCENTER".Equals (sGravity [i])) {
					gravity |= GRAVITY_VCENTER;
				} else if ("CENTER".Equals (sGravity [i])) {
					gravity |= GRAVITY_CENTER;
				}
			}
			_gravity = gravity;
		}

		if (j.HasField ("vrui_objects")) {
//			Debug.Log ("vrui_objects found");
			JSONObject jVruiObjects = j.GetField ("vrui_objects");
			if (jVruiObjects.IsArray) {
//				Debug.Log ("It's array");
//				Debug.Log ("" + jVruiObjects.list.Count + " entries");
				for (int i = 0; i < jVruiObjects.list.Count; i++) {
					JSONObject jObj = jVruiObjects.list [i];
					string sType = jObj.HasField("type") ? jObj.GetField ("type").str : null;
//					Debug.Log ("entry #" + i + ": type " + sType);
//					Debug.Log (jObj.ToString ());
					if ("VRUI_Text".Equals(sType)) {
						VRUI_Text txt = VRUI_Text.CreateFromJSON (jObj, _height);
						Add (txt);
					} else if ("VRUI_Image".Equals(sType)) {
						VRUI_Image img = VRUI_Image.CreateFromJSON (jObj);
						Add (img);
					} else if ("VRUI_Button".Equals(sType)) {
						VRUI_Button btn = VRUI_Button.CreateFromJSON (jObj);
						Add (btn);
					} else if ("VRUI_Container".Equals(sType)) {
						VRUI_Container cnt = VRUI_Container.CreateFromJSON (jObj);
						Add (cnt);
					}
				}
			}
		}

//		_layout
//		_gravity
	}

	public static VRUI_Container CreateFromJSON (JSONObject j) {
		float width = j.HasField("width") ? j.GetField ("width").f : 1f;
		float height = j.HasField("height") ? j.GetField ("height").f : 1f;

		string sLayout = j.HasField("layout") ? j.GetField ("layout").str : null;
		Layout layout = Layout.VERTICAL;
		if ("VERTICAL".Equals (sLayout)) {
			layout = Layout.VERTICAL;
		} else if ("HORIZONTAL".Equals (sLayout)) {
			layout = Layout.HORIZONTAL;
		} else if ("ABSOLUTE".Equals (sLayout)) {
			layout = Layout.ABSOLUTE;
		} else if ("FRAME".Equals (sLayout)) {
			layout = Layout.FRAME;
		} else if ("GRID".Equals (sLayout)) {
			layout = Layout.GRID;
		} else if ("LIST".Equals (sLayout)) {
			layout = Layout.LIST;
		} else if ("RELATIVE".Equals (sLayout)) {
			layout = Layout.RELATIVE;
		}

		string sClrBg = j.HasField("color_background") ? j.GetField ("color_background").str : null;
		Color clrBg = VRUI_Object.ParseColor (sClrBg);
		string sClrBorder = j.HasField("color_border") ? j.GetField ("color_border").str : null;
		Color clrBorder = VRUI_Object.ParseColor (sClrBorder);
		VRUI_Container vruiContainer = Create (width, height, layout, clrBg, clrBorder);

		vruiContainer.ReadDataFromJson (j);
		return vruiContainer;
	}
}
