using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_Button : VRUI_Container {
	public enum State {NORMAL, OVER, ACTIVATED, DISABLED}
	private State _state = State.NORMAL;

	private Color _clrText;
	private Color _clrOverBg;
	private Color _clrOverBorder;
	private Color _clrOverText;
	private Color _clrActivatedBg;
	private Color _clrActivatedBorder;
	private Color _clrActivatedText;
	private Color _clrDisabledBg;
	private Color _clrDisabledBorder;
	private Color _clrDisabledText;

	private float _gazeDelay = 1f;
	private float _gazeStartTime;
	private float _gazeEndTime;
	private bool _isGazeOver = false;
	private bool _isReticleOver = false;
	private bool _isReticleTriggerOn = false;

	private VRUI_Panel _indicator;

	private static int _counter = 0;

	protected VRUI_Button () : base () {}

	public static VRUI_Button Create (float width, float height, Layout layout) {
		VRUI_Container vruiContainer = VRUI_Container.Create (width, height, layout, Color.white, Color.black);
		VRUI_Button vruiButton = vruiContainer.gameObject.AddComponent<VRUI_Button> ();
		vruiContainer.CopyTo (vruiButton);
		vruiContainer.Clear ();
		Destroy (vruiContainer);
		BoxCollider bc = vruiButton.gameObject.AddComponent<BoxCollider> ();
		bc.size = new Vector3(width, height, 0.01f);
//		bc.transform.position = vruiContainer.transform.position;
//		bc.transform.localScale = vruiContainer.transform.localScale;

		++_counter;
		vruiButton.gameObject.name = "VRUI_Button ("+_counter+")";

		return vruiButton;
	}

	void LateUpdate () {
		Debug.Log ("Btn " + name + ": state " + _state + ", reticleOver: " + _isReticleOver + ", trigger: " + _isReticleTriggerOn);
		switch (_state) {
		case State.OVER:
			if (_isGazeOver) {
				if (Time.time <= _gazeEndTime) {
					ShowGazeIndicator (_gazeEndTime - Time.time, _gazeDelay);
				} else {
					SetState (State.ACTIVATED);
				}
			} else {
				if (_isReticleOver) {
					if (_isReticleTriggerOn) {
						SetState (State.ACTIVATED);
					}
				} else {
					SetState (State.NORMAL);
				}
			}
			break;
		case State.NORMAL:
			if (_isReticleOver) {
				SetState (State.OVER);
			}
			break;
		case State.ACTIVATED:
			if (!_isReticleOver) {
				SetState (State.NORMAL);
			}
			break;
		}
		_isGazeOver = _isReticleOver = _isReticleTriggerOn = false;
	}

	// TODO: make gaze indicator
	public void ShowGazeIndicator (float timeLeft, float timeTotal) {
		if (_indicator == null) {
			_indicator = VRUI_Panel.Create (_width, _height, _clrActivatedBg, Color.clear);
			_indicator.transform.parent = transform;
			_indicator.transform.localPosition = new Vector3 (0f, 0f, -_vruiPanel.bgDepth/2f);
			_indicator.name = "Indicator";
		}
		_indicator.gameObject.SetActive (true);
		float newWidth = (timeLeft < 0) ? 1 :
			(Mathf.Abs (timeTotal) < float.Epsilon) ? 1f : (1f - Mathf.Abs (timeLeft / timeTotal));
//		float newWidth = 0.5f;
		_indicator.transform.localScale = new Vector3 (newWidth, 1f, 1f);
	}

	public void HideGazeIndicator () {
		if (_indicator != null) {
			_indicator.gameObject.SetActive (false);
		}
	}

	public void SetStateColors (State theState, Color clrBg, Color clrBorder, Color clrText) {
		HideGazeIndicator ();
		switch (theState) {
		case State.NORMAL:
			_clrBg = clrBg;
			_clrBorder = clrBorder;
			_clrText = clrText;
			break;
		case State.OVER:
			_clrOverBg = clrBg;
			_clrOverBorder = clrBorder;
			_clrOverText = clrText;
			break;
		case State.ACTIVATED:
			_clrActivatedBg = clrBg;
			_clrActivatedBorder = clrBorder;
			_clrActivatedText = clrText;
			break;
		case State.DISABLED:
			_clrDisabledBg = clrBg;
			_clrDisabledBorder = clrBorder;
			_clrDisabledText = clrText;
			break;
		}

		if (_state == theState) {
			SetState (theState);
		}
	}

	public void SetStateImage (State theState, Texture2D texture) {
	}

	public void SetState (State newState) {
		HideGazeIndicator ();
		switch (newState) {
		case State.NORMAL:
			_vruiPanel.SetBgColor (_clrBg);
			_vruiPanel.SetBorderColor (_clrBorder);
			SetTextsColor (_clrText);
			break;
		case State.OVER:
			_vruiPanel.SetBgColor (_clrOverBg);
			_vruiPanel.SetBorderColor (_clrOverBorder);
			SetTextsColor (_clrOverText);

			_gazeStartTime = Time.time;
			_gazeEndTime = _gazeStartTime + _gazeDelay;
			break;
		case State.ACTIVATED:
			_vruiPanel.SetBgColor (_clrActivatedBg);
			_vruiPanel.SetBorderColor (_clrActivatedBorder);
			SetTextsColor (_clrActivatedText);
			// TODO: Do the action
			break;
		case State.DISABLED:
			_vruiPanel.SetBgColor (_clrDisabledBg);
			_vruiPanel.SetBorderColor (_clrDisabledBorder);
			SetTextsColor (_clrDisabledText);
			break;
		}
		_state = newState;
	}

	public void AddText (string s, float stringHeight, Color color, Font font) {
		VRUI_Text text = VRUI_Text.Create (s, stringHeight, color, font);
		Add (text);
		Refresh ();
	}

	public void AddText (string s, float stringHeight, Color color, string fontName) {
		AddText (s, stringHeight, color, VRUI_FontManager.GetFont(fontName));
	}

	public void AddText (string s, float stringHeight, Color color) {
		VRUI_Text text = VRUI_Text.Create (s, stringHeight, color);
		Add (text);
		Refresh ();
	}

	public void AddText (string s) {
		Color textColor = (_clrBg == Color.clear) ? ((_clrBorder == Color.clear) ? Color.blue : _clrBorder) : VRUI_Utils.AdditionalColor (_clrBg);
		AddText (s, _height/2, textColor);
	}

	public void AddImage (Texture2D texture, float imageHeight) {
		VRUI_Image img = VRUI_Image.Create (texture, imageHeight);
		Add (img);
	}

	public void AddImage (Texture2D texture) {
		VRUI_Image img = VRUI_Image.Create (texture, _height);
		Add (img);
	}

	public void OnReticle (VRUI_Reticle reticle) {
		_isReticleOver = true;
		_isGazeOver = reticle.IsGaze ();
		_isReticleTriggerOn = reticle.IsTriggerOn ();
	}

	public void ReadDataFromJson (JSONObject j) {
		(this as VRUI_Container).ReadDataFromJson (j);

		if (j.HasField ("states")) {
			Debug.Log ("states found");
			JSONObject jVruiStates = j.GetField ("states");
			if (jVruiStates.IsArray) {
				Debug.Log ("It's array");
				Debug.Log ("" + jVruiStates.list.Count + " entries");
				for (int i = 0; i < jVruiStates.list.Count; i++) {
					string sClr;
					JSONObject jObj = jVruiStates.list [i];
					string sId = jObj.HasField("id") ? jObj.GetField ("id").str : null;
					Debug.Log ("entry #" + i + ": type " + sId);
					Debug.Log (jObj.ToString ());
					if ("NORMAL".Equals(sId)) {
						sClr = jObj.HasField("color_background") ? jObj.GetField ("color_background").str : null;
						Debug.Log ("NORMAL: "+sClr);
						_clrBg = VRUI_Utils.ParseColor (sClr);
						sClr = jObj.HasField("color_border") ? jObj.GetField ("color_border").str : null;
						Debug.Log ("NORMAL: "+sClr);
						_clrBorder = VRUI_Utils.ParseColor (sClr);
						sClr = jObj.HasField("color_text") ? jObj.GetField ("color_text").str : null;
						Debug.Log ("NORMAL: "+sClr);
						_clrText = VRUI_Utils.ParseColor (sClr);
						Debug.Log ("NORMAL: "+_clrBg+", "+_clrBorder+", "+_clrText);
					} else if ("OVER".Equals(sId)) {
						sClr = jObj.HasField("color_background") ? jObj.GetField ("color_background").str : null;
						_clrOverBg = VRUI_Utils.ParseColor (sClr);
						sClr = jObj.HasField("color_border") ? jObj.GetField ("color_border").str : null;
						_clrOverBorder = VRUI_Utils.ParseColor (sClr);
						sClr = jObj.HasField("color_text") ? jObj.GetField ("color_text").str : null;
						_clrOverText = VRUI_Utils.ParseColor (sClr);
					} else if ("ACTIVATED".Equals(sId)) {
						sClr = jObj.HasField("color_background") ? jObj.GetField ("color_background").str : null;
						_clrActivatedBg = VRUI_Utils.ParseColor (sClr);
						sClr = jObj.HasField("color_border") ? jObj.GetField ("color_border").str : null;
						_clrActivatedBorder = VRUI_Utils.ParseColor (sClr);
						sClr = jObj.HasField("color_text") ? jObj.GetField ("color_text").str : null;
						_clrActivatedText = VRUI_Utils.ParseColor (sClr);
					} else if ("DISABLED".Equals(sId)) {
						sClr = jObj.HasField("color_background") ? jObj.GetField ("color_background").str : null;
						_clrDisabledBg = VRUI_Utils.ParseColor (sClr);
						sClr = jObj.HasField("color_border") ? jObj.GetField ("color_border").str : null;
						_clrDisabledBorder = VRUI_Utils.ParseColor (sClr);
						sClr = jObj.HasField("color_text") ? jObj.GetField ("color_text").str : null;
						_clrDisabledText = VRUI_Utils.ParseColor (sClr);
					}
				}
			}
		}
	}

	public static VRUI_Button CreateFromJSON (JSONObject j) {
		float width = 0f;
		string sWidth = j.HasField("width") ? j.GetField ("width").str : null;
		VRUI_Dimension d = new VRUI_Dimension ();
		d.Parse (sWidth);
		switch (d.type) {
		case VRUI_Dimension.Type.METERS:
			width = d.value;
			break;
		}

		float height = 0f;
		string sHeight = j.HasField("height") ? j.GetField ("height").str : null;
		d = new VRUI_Dimension ();
		d.Parse (sHeight);
		switch (d.type) {
		case VRUI_Dimension.Type.METERS:
			height = d.value;
			break;
		}

		string sLayout = j.HasField("layout") ? j.GetField ("layout").str : null;
		Layout layout = Layout.VERTICAL;
		if ("VERTICAL".Equals (sLayout)) {
			layout = Layout.VERTICAL;
		} else if ("HORIZONTAL".Equals (sLayout)) {
			layout = Layout.HORIZONTAL;
		} else if ("ZSORTED".Equals (sLayout)) {
			layout = Layout.ZSORTED;
//		} else if ("FRAME".Equals (sLayout)) {
//			layout = Layout.FRAME;
		} else if ("GRID".Equals (sLayout)) {
			layout = Layout.GRID;
		} else if ("LIST".Equals (sLayout)) {
			layout = Layout.LIST;
		} else if ("RELATIVE".Equals (sLayout)) {
			layout = Layout.RELATIVE;
		}

		Debug.Log ("============== Size: " + width + " x " + height + ", layout: " + layout);
		Debug.Log (j);

		VRUI_Button vruiButton = VRUI_Button.Create (width, height, layout);
		vruiButton.ReadDataFromJson (j);
		vruiButton.SetState (State.NORMAL);

		return vruiButton;
	}
}
