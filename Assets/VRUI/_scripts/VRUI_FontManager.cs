using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_FontManager {
	protected static string _sFontDefault = "Arial.ttf";
	protected static Font _fontDefault; // = GetFont (_sFontDefault);
	public static Font defaultFont {
		get {
			if ((dict == null) || (dict.Count < 1)) {
				Init ();
			}
			return _fontDefault;
		}
	}

	private static Dictionary<string, Font> dict; // = new Dictionary<string, Font> ();

	public static Font GetFont (string fontName) {
		if ((dict == null) || (dict.Count < 1)) {
			Init ();
		}

		if ((fontName == null) || (fontName.Length < 1)) {
			return _fontDefault;
		}
		if (dict.ContainsKey (fontName)) {
			return dict [fontName];
		}
		Font font = (Font)Resources.Load(fontName);
		if (font != null) {
			dict.Add (fontName, font);
		} else {
			font = (Font)Resources.GetBuiltinResource (typeof(Font), fontName);
			if (font != null) {
				dict.Add (fontName, font);
			}
		}
		return (font == null) ? _fontDefault : font;
	}

	private static void Init () {
		if (dict == null) {
			dict = new Dictionary<string, Font> ();
		}
		if (dict.Count < 1) {
			_fontDefault = (Font)Resources.GetBuiltinResource (typeof(Font), _sFontDefault);
			dict.Add (_sFontDefault, _fontDefault);
		}
	}
}
