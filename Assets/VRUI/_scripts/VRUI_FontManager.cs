using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_FontManager {
	private static Dictionary<string, Font> dict = new Dictionary<string, Font> ();

	public static Font GetFont (string fontName) {
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
		return font;
	}
}
