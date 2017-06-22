using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_Dimension {
	// Dimension may be:
	// 1) In meters - we use the exact value.
	// 2) In degrees - we calculates meters value by distance from camera.
	// 3) In percents of parent's dimension. For container with no parent we need to know camera FOV and container position to find the dimension.
	// 4) In "weight" units that shares parent's space between children in proportion of their weight.
	// 5) WRAP_CONTENT - dimension calculates by sum of its children's dimensions. If childrens have dimensions depends on parent's dimension
	//    we calculate parent dimension as the sum of all its children having dimensions independent from parent's one + 1m.

	// TO THINK: dimension in grades is OK when item lies in spherical surface around user.
	// But when it lies on planar surface it's works when the item is perpendicular to sight direction.
	// Let's left DEGREES for later...

	protected float _value;
	public float value {get {return _value;}}
	protected string _sParam;
	protected float _param;
	public enum Type {METERS, PERCENTS, PROPORTION, WRAP_CONTENT, ERROR, UNDEFINED} // , DEGREES}
	protected Type _type;
	public Type type {get {return _type;}}

	public Type Parse (string s) {
		if ((s == null) || (s.Length < 1)) {
			return _type = Type.ERROR;
		}
		if ("MATCH_PARENT".Equals (s)) {
			_param = 100f;
			return _type = Type.PERCENTS;
		}
		if ("WRAP_CONTENT".Equals (s)) {
			return _type = Type.WRAP_CONTENT;
		}
		char postfix = s [s.Length - 1];
		float value;
		if (postfix == '%') {
			if (float.TryParse (s.Substring (0, s.Length - 1), out _param)) {
				return _type = Type.PERCENTS;
			}
			return _type = Type.ERROR;
		}
		if (postfix == 'm') {
			if (float.TryParse (s.Substring (0, s.Length - 1), out _param)) {
				_value = _param;
				return _type = Type.METERS;
			}
			return _type = Type.ERROR;
		}
//		if (postfix == 'd') {
//			if (float.TryParse (s.Substring (0, s.Length - 1), out _param)) {
//				_value = _param;
//				return _type = Type.DEGREES;
//			}
//			return _type = Type.ERROR;
//		}
		if (postfix == 'p') {
			if (float.TryParse (s.Substring (0, s.Length - 1), out _param)) {
				_value = _param;
				return _type = Type.PROPORTION;
			}
			return _type = Type.ERROR;
		}
		return _type = Type.ERROR;
	}

//	public float CalculateGrades (float distanceFromCamera) {
//		return _value = 
//	}

	public float CalculatePercents (VRUI_Dimension parentDimension) {
		return _value = parentDimension.value * _param;
	}

	public float CalculateProportional (float spaceForProportionalChildren, float weightsSumOfProportionalChildren) {
		return _value = spaceForProportionalChildren * _param / weightsSumOfProportionalChildren;
	}

	public void SetUndefined () {
		_type = Type.UNDEFINED;
	}
}
