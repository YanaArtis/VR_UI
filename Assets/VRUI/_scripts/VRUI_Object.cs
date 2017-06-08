using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_Object : MonoBehaviour {
//	public Vector3 position = Vector3.zero;
//	public Vector3 rotation = Vector3.zero;

	public float _width;
	public float _height;

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

	protected static Shader shaderStandard;
	private static bool isShaderStandardSeekRequired = true;

	protected static Shader shaderTransparent;
	private static bool isShaderTransparentSeekRequired = true;

	public VRUI_Object () {}

	protected static void Init () {
		if (isShaderStandardSeekRequired) {
			shaderStandard = Shader.Find ("Standard");
		}
		if (isShaderTransparentSeekRequired) {
			shaderTransparent = Shader.Find ("Unlit/Transparent");
		}
	}

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

	public static Color AdditionalColor (Color clr) {
		float h, s, v;
		Color.RGBToHSV (clr, out h, out s, out v);

		Debug.Log ("old color: R:" + clr.r + " G:" + clr.g + " B:" + clr.b + ", H:" + h + " S:" + s + " V:" + v);

		float hNew, sNew, vNew;
		hNew = (h >= 0.5f) ? (h - 0.5f) : (h + 0.5f);
//		hNew = (h >= 0.5f) ? 0f : 1f;
		vNew = v * (s - 1) + 1;
		sNew = v * s / vNew;

		vNew = (vNew < 0.5f) ? 0f : 1f;

		Color newColor = Color.HSVToRGB (hNew, sNew, vNew);
		Debug.Log ("new color: R:" + newColor.r + " G:" + newColor.g + " B:" + newColor.b + ", H:" + hNew + " S:" + sNew + " V:" + vNew);
//		newColor.a = 1f;
		return newColor;
	}
}
