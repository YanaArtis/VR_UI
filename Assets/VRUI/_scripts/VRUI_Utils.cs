using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_Utils {

	// http://wiki.unity3d.com/index.php?title=HexConverter
	// Note that Color32 and Color implictly convert to each other. You may pass a Color object to this method without first casting it.
	public static string ColorToHex(Color32 color) {
		string hex = color.a.ToString("X2") + color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		return hex;
	}

	public static Color HexToColor(string hex) {
		byte a = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte r = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r,g,b, a);
	}

	public static Color ParseColor (string sColor) {
		int iColor;
		if (sColor != null) {
			if (sColor.StartsWith ("#")) {
				return HexToColor (sColor.Substring (1));
			} else if (sColor.StartsWith ("0x")) {
				return HexToColor (sColor.Substring (2));
			}
		}
		return Color.black;
	}

	// https://stackoverflow.com/questions/1615559/convert-a-unicode-string-to-an-escaped-ascii-string
	/*
	public static string EncodeNonAsciiCharacters( string value ) {
		StringBuilder sb = new StringBuilder();
		foreach( char c in value ) {
			if( c > 127 ) {
				// This character is too big for ASCII
				string encodedValue = "\\u" + ((int) c).ToString( "x4" );
				sb.Append( encodedValue );
			}
			else {
				sb.Append( c );
			}
		}
		return sb.ToString();
	}
	*/

	public static string DecodeEncodedNonAsciiCharacters( string value ) {
		return System.Text.RegularExpressions.Regex.Replace(
			value,
			@"\\u(?<Value>[a-zA-Z0-9]{4})",
			m => {
				return ((char) int.Parse( m.Groups["Value"].Value, System.Globalization.NumberStyles.HexNumber )).ToString();
			} );
	}

	public static Color AdditionalColor (Color clr) {
		float h, s, v;
		Color.RGBToHSV (clr, out h, out s, out v);

		Debug.Log ("old color: R:" + clr.r + " G:" + clr.g + " B:" + clr.b + ", H:" + h + " S:" + s + " V:" + v);

		float hNew, sNew, vNew;
		hNew = (h >= 0.5f) ? (h - 0.5f) : (h + 0.5f);
		//		hNew = (h >= 0.5f) ? 0f : 1f;
		vNew = v * (s - 1) + 1;
		sNew = (v < float.Epsilon) ? 1f : v * s / vNew;

		vNew = (vNew < 0.5f) ? 0f : 1f;

		Color newColor = Color.HSVToRGB (hNew, sNew, vNew);
		Debug.Log ("new color: R:" + newColor.r + " G:" + newColor.g + " B:" + newColor.b + ", H:" + hNew + " S:" + sNew + " V:" + vNew);
		//		newColor.a = 1f;
		return newColor;
	}
}
