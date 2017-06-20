using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_ShaderManager : MonoBehaviour {
	private static Dictionary<string, Shader> dict = new Dictionary<string, Shader> ();

	public static Shader GetShader (string shaderName) {
		if (dict.ContainsKey (shaderName)) {
			return dict [shaderName];
		}
		Shader shader = Shader.Find (shaderName);
		if (shader != null) {
			dict.Add (shaderName, shader);
		}
		return shader;
	}
}
