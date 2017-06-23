using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_Object : MonoBehaviour {
	public float _width;
	public float _height;

	public VRUI_Dimension _layoutWidth;
	public VRUI_Dimension _layoutHeight;

	// Margin: space between the element and other element
	public VRUI_Dimension _layoutMarginLeft;
	public VRUI_Dimension _layoutMarginTop;
	public VRUI_Dimension _layoutMarginRight;
	public VRUI_Dimension _layoutMarginBottom;
	protected float _marginLeft = 0f;
	protected float _marginTop = 0f;
	protected float _marginRight = 0f;
	protected float _marginBottom = 0f;

	// Padding: space between border and child element
	public VRUI_Dimension _layoutPaddingLeft;
	public VRUI_Dimension _layoutPaddingTop;
	public VRUI_Dimension _layoutPaddingRight;
	public VRUI_Dimension _layoutPaddingBottom;
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
		if (other == null) {
			return;
		}
		other._id = this._id;
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

		if (this._layoutWidth == null) {
			other._layoutWidth = null;
		} else {
			if (other._layoutWidth == null) {
				other._layoutWidth = new VRUI_Dimension ();
			}
			this._layoutWidth.CopyTo (other._layoutWidth);
		}

		if (this._layoutHeight == null) {
			other._layoutHeight = null;
		} else {
			if (other._layoutHeight == null) {
				other._layoutHeight = new VRUI_Dimension ();
			}
			this._layoutHeight.CopyTo (other._layoutHeight);
		}

		if (this._layoutMarginLeft == null) {
			other._layoutMarginLeft = null;
		} else {
			if (other._layoutMarginLeft == null) {
				other._layoutMarginLeft = new VRUI_Dimension ();
			}
			this._layoutMarginLeft.CopyTo (other._layoutMarginLeft);
		}

		if (this._layoutMarginTop == null) {
			other._layoutMarginTop = null;
		} else {
			if (other._layoutMarginTop == null) {
				other._layoutMarginTop = new VRUI_Dimension ();
			}
			this._layoutMarginTop.CopyTo (other._layoutMarginTop);
		}

		if (this._layoutMarginRight == null) {
			other._layoutMarginRight = null;
		} else {
			if (other._layoutMarginRight == null) {
				other._layoutMarginRight = new VRUI_Dimension ();
			}
			this._layoutMarginRight.CopyTo (other._layoutMarginRight);
		}

		if (this._layoutMarginBottom == null) {
			other._layoutMarginBottom = null;
		} else {
			if (other._layoutMarginBottom == null) {
				other._layoutMarginBottom = new VRUI_Dimension ();
			}
			this._layoutMarginBottom.CopyTo (other._layoutMarginBottom);
		}

		if (this._layoutPaddingLeft == null) {
			other._layoutPaddingLeft = null;
		} else {
			if (other._layoutPaddingLeft == null) {
				other._layoutPaddingLeft = new VRUI_Dimension ();
			}
			this._layoutPaddingLeft.CopyTo (other._layoutPaddingLeft);
		}

		if (this._layoutPaddingTop == null) {
			other._layoutPaddingTop = null;
		} else {
			if (other._layoutPaddingTop == null) {
				other._layoutPaddingTop = new VRUI_Dimension ();
			}
			this._layoutPaddingTop.CopyTo (other._layoutPaddingTop);
		}

		if (this._layoutPaddingRight == null) {
			other._layoutPaddingRight = null;
		} else {
			if (other._layoutPaddingRight == null) {
				other._layoutPaddingRight = new VRUI_Dimension ();
			}
			this._layoutPaddingRight.CopyTo (other._layoutPaddingRight);
		}

		if (this._layoutPaddingBottom == null) {
			other._layoutPaddingBottom = null;
		} else {
			if (other._layoutPaddingBottom == null) {
				other._layoutPaddingBottom = new VRUI_Dimension ();
			}
			this._layoutPaddingBottom.CopyTo (other._layoutPaddingBottom);
		}
	}

	public void ReadDataFromJson (JSONObject j) {
		_id = j.HasField("id") ? j.GetField ("id").str : null;

		if (_layoutWidth == null) {
			_layoutWidth = new VRUI_Dimension ();
		}
		if (_layoutHeight == null) {
			_layoutHeight = new VRUI_Dimension ();
		}
		_layoutWidth.SetUndefined ();
		_layoutHeight.SetUndefined ();
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

		if (_layoutMarginLeft == null) {
			_layoutMarginLeft = new VRUI_Dimension ();
		}
		if (_layoutMarginRight == null) {
			_layoutMarginRight = new VRUI_Dimension ();
		}
		if (_layoutMarginTop == null) {
			_layoutMarginTop = new VRUI_Dimension ();
		}
		if (_layoutMarginBottom == null) {
			_layoutMarginBottom = new VRUI_Dimension ();
		}
		_layoutMarginLeft.SetUndefined ();
		_layoutMarginRight.SetUndefined ();
		_layoutMarginTop.SetUndefined ();
		_layoutMarginBottom.SetUndefined ();
		if (j.HasField("margin")) {
			_layoutMarginLeft.Parse (j.GetField ("margin").str);
			_layoutMarginLeft.CopyTo (_layoutMarginRight);
			_layoutMarginLeft.CopyTo (_layoutMarginTop);
			_layoutMarginLeft.CopyTo (_layoutMarginBottom);
		}
		if (j.HasField("margin_left")) {
			_layoutMarginLeft.Parse (j.GetField ("margin_left").str);
		}
		if (j.HasField("margin_right")) {
			_layoutMarginRight.Parse (j.GetField ("margin_right").str);
		}
		if (j.HasField("margin_top")) {
			_layoutMarginTop.Parse (j.GetField ("margin_top").str);
		}
		if (j.HasField("margin_bottom")) {
			_layoutMarginBottom.Parse (j.GetField ("margin_bottom").str);
		}

		if (_layoutMarginLeft.type == VRUI_Dimension.Type.METERS) {
			_marginLeft = _layoutMarginLeft.value;
		}
		if (_layoutMarginRight.type == VRUI_Dimension.Type.METERS) {
			_marginRight = _layoutMarginRight.value;
		}
		if (_layoutMarginTop.type == VRUI_Dimension.Type.METERS) {
			_marginTop = _layoutMarginTop.value;
		}
		if (_layoutMarginBottom.type == VRUI_Dimension.Type.METERS) {
			_marginBottom = _layoutMarginBottom.value;
		}

		if (_layoutPaddingLeft == null) {
			_layoutPaddingLeft = new VRUI_Dimension ();
		}
		if (_layoutPaddingRight == null) {
			_layoutPaddingRight = new VRUI_Dimension ();
		}
		if (_layoutPaddingTop == null) {
			_layoutPaddingTop = new VRUI_Dimension ();
		}
		if (_layoutPaddingBottom == null) {
			_layoutPaddingBottom = new VRUI_Dimension ();
		}
		_layoutPaddingLeft.SetUndefined ();
		_layoutPaddingRight.SetUndefined ();
		_layoutPaddingTop.SetUndefined ();
		_layoutPaddingBottom.SetUndefined ();
		if (j.HasField("padding")) {
			_layoutPaddingLeft.Parse (j.GetField ("padding").str);
			_layoutPaddingLeft.CopyTo (_layoutPaddingRight);
			_layoutPaddingLeft.CopyTo (_layoutPaddingTop);
			_layoutPaddingLeft.CopyTo (_layoutPaddingBottom);
		}
		if (j.HasField("padding_left")) {
			_layoutPaddingLeft.Parse (j.GetField ("padding_left").str);
		}
		if (j.HasField("padding_right")) {
			_layoutPaddingRight.Parse (j.GetField ("padding_right").str);
		}
		if (j.HasField("padding_top")) {
			_layoutPaddingTop.Parse (j.GetField ("padding_top").str);
		}
		if (j.HasField("padding_bottom")) {
			_layoutPaddingBottom.Parse (j.GetField ("padding_bottom").str);
		}

		if (_layoutPaddingLeft.type == VRUI_Dimension.Type.METERS) {
			_paddingLeft = _layoutPaddingLeft.value;
		}
		if (_layoutPaddingRight.type == VRUI_Dimension.Type.METERS) {
			_paddingRight = _layoutPaddingRight.value;
		}
		if (_layoutPaddingTop.type == VRUI_Dimension.Type.METERS) {
			_paddingTop = _layoutPaddingTop.value;
		}
		if (_layoutPaddingBottom.type == VRUI_Dimension.Type.METERS) {
			_paddingBottom = _layoutPaddingBottom.value;
		}
	}

	public static VRUI_Object CreateFromJSON (JSONObject j) {
		VRUI_Object vruiObject = new VRUI_Object ();
		vruiObject.ReadDataFromJson (j);
		return vruiObject;
	}
}
