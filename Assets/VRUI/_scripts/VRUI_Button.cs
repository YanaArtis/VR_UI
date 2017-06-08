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
	
	private static int _counter = 0;

	protected VRUI_Button () : base () {}

	public static VRUI_Button Create (float width, float height, Layout layout) {
		VRUI_Container vruiContainer = VRUI_Container.Create (width, height, layout, Color.white, Color.black);
		VRUI_Button vruiButton = vruiContainer.gameObject.AddComponent<VRUI_Button> ();
		vruiContainer.CopyTo (vruiButton);
		vruiContainer.Clear ();
		Destroy (vruiContainer);

		++_counter;
		vruiButton.gameObject.name = "VRUI_Button ("+_counter+")";

		return vruiButton;
	}

	public void SetStateColors (State theState, Color clrBg, Color clrBorder, Color clrText) {
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

	private void SetTextsColor (Color newTextColor) {
		for (int i = 0; i < _objects.Count; i++) {
			if (_objects [i] is VRUI_Text) {
				(_objects [i] as VRUI_Text).SetColor (newTextColor);
			}
		}
	}

	public void SetStateImage (State theState, Texture2D texture) {
	}

	public void SetState (State newState) {
		switch (newState) {
		case State.NORMAL:
			vruiPanel.SetBgColor (_clrBg);
			vruiPanel.SetBorderColor (_clrBorder);
			SetTextsColor (_clrText);
			break;
		case State.OVER:
			vruiPanel.SetBgColor (_clrOverBg);
			vruiPanel.SetBorderColor (_clrOverBorder);
			SetTextsColor (_clrOverText);
			break;
		case State.ACTIVATED:
			vruiPanel.SetBgColor (_clrActivatedBg);
			vruiPanel.SetBorderColor (_clrActivatedBorder);
			SetTextsColor (_clrActivatedText);
			break;
		case State.DISABLED:
			vruiPanel.SetBgColor (_clrDisabledBg);
			vruiPanel.SetBorderColor (_clrDisabledBorder);
			SetTextsColor (_clrDisabledText);
			break;
		}
	}

	public void AddText (string s, float stringHeight, Color color, Font font) {
		VRUI_Text text = VRUI_Text.Create (s, stringHeight, color, font);
		Add (text);
		Refresh ();
	}

	public void AddText (string s, float stringHeight, Color color) {
		VRUI_Text text = VRUI_Text.Create (s, stringHeight, color);
		Add (text);
		Refresh ();
	}

	public void AddText (string s) {
		Color textColor = (_clrBg == Color.clear) ? ((_clrBorder == Color.clear) ? Color.blue : _clrBorder) : AdditionalColor (_clrBg);
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
}
