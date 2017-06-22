using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_Object : MonoBehaviour {
	public float _width;
	public float _height;

	public VRUI_Dimension _layoutWidth;
	public VRUI_Dimension _layoutHeight;

	// Margin: space between the element and other element
	protected float _marginLeft = 0f;
	protected float _marginTop = 0f;
	protected float _marginRight = 0f;
	protected float _marginBottom = 0f;

	// Padding: space between border and child element
	protected float _paddingLeft = 0f;
	protected float _paddingTop = 0f;
	protected float _paddingRight = 0f;
	protected float _paddingBottom = 0f;

	public string _id = null;

	public float marginLeft {
		get {
			return _marginLeft;
		}
		set {
			_marginLeft = value;
			Refresh ();
		}
	}
	public float marginTop {
		get {
			return _marginTop;
		}
		set {
			_marginTop = value;
			Refresh ();
		}
	}
	public float marginRight {
		get {
			return _marginRight;
		}
		set {
			_marginRight = value;
			Refresh ();
		}
	}
	public float marginBottom {
		get {
			return _marginBottom;
		}
		set {
			_marginBottom = value;
			Refresh ();
		}
	}

	public float paddingLeft {
		get {
			return _paddingLeft;
		}
		set {
			_paddingLeft = value;
			Refresh ();
		}
	}
	public float paddingTop {
		get {
			return _paddingTop;
		}
		set {
			_paddingTop = value;
			Refresh ();
		}
	}
	public float paddingRight {
		get {
			return _paddingRight;
		}
		set {
			_paddingRight = value;
			Refresh ();
		}
	}
	public float paddingBottom {
		get {
			return _paddingBottom;
		}
		set {
			_paddingBottom = value;
			Refresh ();
		}
	}

	public VRUI_Object () {}

	protected static void Init () {}

	public void SetMargin (float marginSpace) {
		_marginLeft = _marginTop = _marginRight =  _marginBottom = marginSpace;
		Refresh ();
	}

	public void SetPadding (float paddingSpace) {
		_paddingLeft = _paddingTop = _paddingRight =  _paddingBottom = paddingSpace;
		Refresh ();
	}

	public virtual void Refresh () {}

	public void CopyTo (VRUI_Object other) {
		other._width = this._width;
		other._height = this._height;
		other._marginLeft = this._marginLeft;
		other._marginTop = this._marginTop;
		other._marginRight = this._marginRight;
		other._marginBottom = this._marginBottom;
		other._paddingLeft = this._paddingLeft;
		other._paddingTop = this._paddingTop;
		other._paddingRight = this._paddingRight;
		other._paddingBottom = this._paddingBottom;
	}

	public void ReadDataFromJson (JSONObject j) {
		_id = j.HasField("id") ? j.GetField ("id").str : null;

		if (j.HasField("width")) {
			_layoutWidth.Parse (j.GetField ("width").str);
			if (_layoutWidth.type == VRUI_Dimension.Type.METERS) {
				_width = _layoutWidth.value;
			}
		} else {
			_layoutWidth.SetUndefined ();
		}

		if (j.HasField("height")) {
			_layoutHeight.Parse (j.GetField ("height").str);
			if (_layoutHeight.type == VRUI_Dimension.Type.METERS) {
				_height = _layoutHeight.value;
			}
		} else {
			_layoutHeight.SetUndefined ();
		}

		if (j.HasField("margin")) {
			float newMargin = j.GetField ("margin").f;
			_marginLeft = _marginRight = _marginTop = _marginBottom = newMargin;
		}
		if (j.HasField("margin_left")) {
			float newMarginLeft = j.GetField ("margin_left").f;
			_marginLeft = newMarginLeft;
		}
		if (j.HasField("margin_right")) {
			float newMarginRight = j.GetField ("margin_right").f;
			_marginRight = newMarginRight;
		}
		if (j.HasField("margin_top")) {
			float newMarginTop = j.GetField ("margin_top").f;
			Debug.Log ("Object \""+_id+"\": margin_top: "+newMarginTop);
			_marginTop = newMarginTop;
			Debug.Log ("_margin_top: "+_marginTop);
		}
		if (j.HasField("margin_bottom")) {
			float newMarginBottom = j.GetField ("margin_bottom").f;
			Debug.Log ("Object \""+_id+"\": margin_bottom: "+newMarginBottom);
			_marginBottom = newMarginBottom;
			Debug.Log ("_marginBottom: "+_marginBottom);
		}

		if (j.HasField("padding")) {
			float padding = j.GetField ("padding").f;
			_paddingLeft = _paddingRight = _paddingTop = _paddingBottom = padding;
		}
		if (j.HasField("padding_left")) {
			float newPaddingLeft = j.GetField ("padding_left").f;
			_paddingLeft = newPaddingLeft;
		}
		if (j.HasField("padding_right")) {
			float newPaddingRight = j.GetField ("padding_right").f;
			_paddingRight = newPaddingRight;
		}
		if (j.HasField("padding_top")) {
			float newPaddingTop = j.GetField ("padding_top").f;
			_paddingTop = newPaddingTop;
		}
		if (j.HasField("padding_bottom")) {
			float newPaddingBottom = j.GetField ("padding_bottom").f;
			_paddingBottom = newPaddingBottom;
		}
	}

	public static VRUI_Object CreateFromJSON (JSONObject j) {
		VRUI_Object vruiObject = new VRUI_Object ();
		vruiObject.ReadDataFromJson (j);
		return vruiObject;
	}
}
