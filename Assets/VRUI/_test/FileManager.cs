using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager {
	private static string gear360Path = FindGear360Path ();

	public static void WriteToLog (string s) {
		WriteTextToFile (Application.persistentDataPath+Path.DirectorySeparatorChar+"log.txt", s);
	}

	public static void WriteTextToFile (string fname, string data) {
		using (StreamWriter file = new StreamWriter(fname)) {
			file.Write (data);
			file.Close ();
		}
	}

	public static string ReadTextFromResources (string fname) {
		string fname2;
		int n = fname.LastIndexOf (".");
		if (n > 0) {
			fname2 = fname.Substring (0, n) + "_" + fname.Substring(n+1);
		} else {
			fname2 = fname;
		}
		TextAsset txt = (TextAsset)Resources.Load(fname2, typeof(TextAsset));
		return txt.text;
	}

	public static string ReadTextFromFile (string path) {
		string s = null;

		if (System.IO.File.Exists(path)) {
			try {
				using (StreamReader file = new StreamReader(path)) {
					s = file.ReadToEnd ();
					file.Close();
				}
			} catch (FileNotFoundException e) {
				Debug.Log (e.Message);
			} catch (DirectoryNotFoundException e) {
				Debug.Log (e.Message);
			}
		}
		return s;
	}

/*
	public static bool ReadImage_fromServer (Texture2D t, string subfolder, string fname) {
		bool ret = false;
		//		string path = urlServer+((subfolder == null) ? fname : (subfolder+"/"+fname));
		string path = ((subfolder == null) ? urlServer+fname : (subfolder+fname));
		Debug.Log ("ReadImage_fromServer: \""+path+"\"");
		WWW www = new WWW (path);
		int timeout = 20000; // 20 sec
		while (!www.isDone) {
			System.Threading.Thread.Sleep (100);
			timeout -= 100;
		}
		if (www.isDone) {
			ret = (www.error == null);
			if (ret) {
				www.LoadImageIntoTexture (t);
			} else {
				Debug.Log ("ReadImage_fromServer() ERROR: \"" + www.error + "\"");
			}
		} else {
			Debug.Log ("ReadImage_fromServer() ERROR: !www.isDone");
		}
		www.Dispose ();
		www = null;
		return ret;
	}
*/

	public static Texture2D ReadImageFromResources (/* Texture2D t, */ string subfolder, string fname) {
		string fname2;
		int n = fname.LastIndexOf (".");
		if (n > 0) {
			fname2 = fname.Substring (0, n);// + "_" + fname.Substring(n+1);
		} else {
			fname2 = fname;
		}
		string path = (subfolder == null) ? fname2 : (subfolder + "/" + fname2);
		Debug.Log ("ReadImage_fromResources: \""+path+"\"");
		Texture2D newT = Resources.Load<Texture2D>(path);
		return newT;
	}

	public static bool ReadImageFromPersistentData (Texture2D t, string subfolder, string fname) {
		bool ret = false;
		string path = "file:///"+Path.Combine(Application.persistentDataPath, (subfolder == null) ? fname : (subfolder+"/"+fname));
		WWW www = new WWW (path);
		int timeout = 20000; // 20 sec
		while (!www.isDone) {
			System.Threading.Thread.Sleep (100);
			timeout -= 100;
		}
		if (www.isDone) {
			ret = (www.error == null);
			if (ret) {
				www.LoadImageIntoTexture (t);
			} else {
				Debug.Log ("ReadImage_fromPersistentData() ERROR: \"" + www.error + "\"");
			}
		} else {
			Debug.Log ("ReadImage_fromPersistentData() ERROR: !www.isDone");
		}
		www.Dispose ();
		www = null;
		return ret;
	}

	public static bool ReadImageFromDirectory (Texture2D t, string fname) {
		bool ret = false;
		string path = "file:///"+fname;
		Debug.Log (path);

		WWW www = new WWW (path);
		int timeout = 20000; // 20 sec
		while (!www.isDone) {
			System.Threading.Thread.Sleep (100);
			timeout -= 100;
		}
		if (www.isDone) {
			ret = (www.error == null);
			if (ret) {
				www.LoadImageIntoTexture (t);
			} else {
				Debug.Log ("ReadImage_fromDirectory() ERROR: \"" + www.error + "\"");
			}
		} else {
			Debug.Log ("ReadImage_fromDirectory() ERROR: !www.isDone");
		}
		www.Dispose ();
		www = null;
		Debug.Log ("ReadImage_fromDirectory() texture loaded: " + t.width + "x" + t.height);
		return ret;
	}

	public static string[] LoadFilesList () {
		string[] files = System.IO.Directory.GetFiles (gear360Path);
		foreach(string file in System.IO.Directory.GetFiles(gear360Path)) {
			Debug.Log("file: "+file);
			Debug.Log("file: "+System.IO.Path.GetFileNameWithoutExtension (file));
		}
		return files;
	}

	public static string FindGear360Path () {
		if (Application.isEditor) {
			return "C:\\DCIM/Gear 360";
		}
		string s = Application.persistentDataPath;
		int n = s.IndexOf ("ndroid/") - 1;
		if (n < 1) {
			return "";
		}
		return s.Substring (0, n)+"DCIM/Gear 360";
	}
}
