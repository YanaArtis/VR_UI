using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: add styles

public class VRUI_TestScript : MonoBehaviour {
	public GameObject mainCamera;
	public GameObject ovrCamera;
	public Transform gearVrHeadRaycastCenter;

	public Transform headRaycastCenter;
	public Transform controllerRaycastCenter;

	VRUI_Reticle reticleGaze = null;
	VRUI_Reticle reticleController = null;
	VRUI_Reticle reticleMouse = null;

	void Start () {
		InitReticles ();

		string sJson = FileManager.ReadTextFromResources ("TestMenu_json");
		JSONObject j = new JSONObject (sJson);
		VRUI_Container testMenu = VRUI_Container.CreateFromJSON (j);
		testMenu.transform.position = new Vector3 (-1.8f, 0f, 2f);

		sJson = FileManager.ReadTextFromResources ("MainMenu_json");
		j = new JSONObject (sJson);
		VRUI_Container mainMenu = VRUI_Container.CreateFromJSON (j);
		mainMenu.transform.position = new Vector3 (-1.15f, 0f, 2f);

		string sJson2 = FileManager.ReadTextFromResources ("TourMenu_json");
		JSONObject j2 = new JSONObject (sJson2);
		VRUI_Container tourMenu = VRUI_Container.CreateFromJSON (j2);
		tourMenu.transform.position = new Vector3 (0f, -1f, 2f);

		/*
//		TestVerticalLayout ();
//		TestHorizontalLayout ();
//		TestNestedLayouts1 ();
//		TestNestedLayouts2 ();
//		TestButtons ();
//		TestReticle ();

		Font fontGothic = (Font)Resources.Load("GOTHICB");
		Font fontShowcardGothic = (Font)Resources.Load("SHOWG");

		VRUI_Container infoContainer = VRUI_Container.Create (1f, 0.6f, VRUI_Container.Layout.VERTICAL, Color.gray, Color.blue);
		infoContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_HCENTER);
		infoContainer.SetPadding (0.03f);

		VRUI_Text text = VRUI_Text.Create ("Container #1", 0.1f, Color.white, fontShowcardGothic);
		infoContainer.Add (text);
		text = VRUI_Text.Create ("with VERTICAL layout and", 0.07f, Color.black, fontGothic);
		infoContainer.Add (text);
		text = VRUI_Text.Create ("GRAVITY_TOP|GRAVITY_HCENTER", 0.07f, Color.black, fontGothic);
		infoContainer.Add (text);

		VRUI_Container subContainer = VRUI_Container.Create (0.9f, 0.21f, VRUI_Container.Layout.HORIZONTAL, Color.clear, Color.black);
		subContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_HCENTER);
		subContainer.SetPadding (0.03f);

//		text = VRUI_Text.Create ("Sub-container #2", 0.1f, Color.white, fontGothic);
//		subContainer.Add (text);
//		text = VRUI_Text.Create ("with HORIZONTAL layout", 0.07f, Color.black, fontGothic);
//		subContainer.Add (text);

		VRUI_Button vruiButton = VRUI_Button.Create (0.3f, 0.15f, VRUI_Container.Layout.HORIZONTAL);
		vruiButton.SetStateColors (VRUI_Button.State.NORMAL, Color.white, Color.black, Color.black);
		vruiButton.SetStateColors (VRUI_Button.State.OVER, Color.cyan, Color.yellow, Color.blue);
		vruiButton.SetStateColors (VRUI_Button.State.ACTIVATED, Color.blue, Color.white, Color.yellow);
		vruiButton.SetStateColors (VRUI_Button.State.DISABLED, Color.gray, Color.grey, Color.grey);
		Texture2D imgInfo = FileManager.ReadImageFromResources (null, "info");
		vruiButton.AddImage (imgInfo, 0.12f);
		vruiButton.AddText (" Info");
		vruiButton.SetMargin (0.01f);
		//		vruiButton.AddText ("Info", 0.25f, Color.red);
		//		vruiButton.SetState (VRUI_Button.State.DISABLED);
		subContainer.Add (vruiButton);

		vruiButton = VRUI_Button.Create (0.3f, 0.15f, VRUI_Container.Layout.HORIZONTAL);
		vruiButton.SetStateColors (VRUI_Button.State.NORMAL, Color.white, Color.black, Color.black);
		vruiButton.SetStateColors (VRUI_Button.State.OVER, Color.cyan, Color.yellow, Color.blue);
		vruiButton.SetStateColors (VRUI_Button.State.ACTIVATED, Color.blue, Color.white, Color.yellow);
		vruiButton.SetStateColors (VRUI_Button.State.DISABLED, Color.gray, Color.grey, Color.grey);
		imgInfo = FileManager.ReadImageFromResources (null, "faq");
		vruiButton.AddImage (imgInfo, 0.12f);
		vruiButton.AddText (" FAQ");
		vruiButton.SetMargin (0.01f);
		subContainer.Add (vruiButton);

		infoContainer.Add (subContainer);

		infoContainer.transform.position = new Vector3 (0f, 0f, 2f);

		string sJson = FileManager.ReadTextFromResources ("TestMenu_json");
		JSONObject j = new JSONObject (sJson);
		VRUI_Container container2 = VRUI_Container.CreateFromJSON (j);
		container2.transform.position = new Vector3 (1.2f, 0f, 2f);
		*/
	}

	private void InitReticles () {
		float noHitDistance = 3f;
		if (Application.isEditor) {
			Texture2D imgMouse = null; // FileManager.ReadImageFromResources (null, "reticle_mouse");
			reticleMouse = VRUI_Reticle.Create (imgMouse, 0.1f, noHitDistance, false, 0f, -0.07f);
//			reticleMouse = VRUI_Reticle.Create (imgMouse, 0.1f, noHitDistance, true); // false);
		}

		Texture2D imgGaze = FileManager.ReadImageFromResources (null, "reticle_gaze");

		#if GEAR_VR || OCULUS
		reticleGaze = VRUI_Reticle.Create (imgGaze, 0.1f, noHitDistance, false);
		mainCamera.SetActive (false);
		ovrCamera.SetActive (true);
		Texture2D imgCross = FileManager.ReadImageFromResources (null, "reticle_cross");
		reticleController = VRUI_Reticle.Create (imgCross, 0.1f, noHitDistance, false);
		#else
		reticleGaze = VRUI_Reticle.Create (imgGaze, 0.1f, noHitDistance, true);
		mainCamera.SetActive (true);
		ovrCamera.SetActive (false);
		#endif
	}

	private void UpdateReticle () {
		GameObject selectedObject;
		VRUI_Object vruiObject;
		#if GEAR_VR || OCULUS
		headRaycastCenter.rotation = gearVrHeadRaycastCenter.rotation;
		if (OVRInput.GetActiveController () == OVRInput.Controller.LTrackedRemote ||
			OVRInput.GetActiveController () == OVRInput.Controller.RTrackedRemote ||
			OVRInput.GetActiveController () == OVRInput.Controller.LTouch ||
			OVRInput.GetActiveController () == OVRInput.Controller.RTouch ) {
			Quaternion q = OVRInput.GetLocalControllerRotation (OVRInput.GetActiveController ());
			controllerRaycastCenter.rotation = q;

			if (OVRInput.GetDown (OVRInput.Button.PrimaryIndexTrigger)) { // , OVRInput.GetActiveController ())) {
				reticleController.SetTriggerOn ();
			}
		} else {
			controllerRaycastCenter.localRotation = Quaternion.identity;
		}
//		reticleController.CastRay (controllerRaycastCenter.position, controllerRaycastCenter.forward, 100f);

		if (OVRInput.Get (OVRInput.Button.One)) {
			reticleGaze.SetTriggerOn ();
		}
		#elif CARDBOARD
		headRaycastCenter.rotation = mainCamera.transform.rotation;
		#endif

		if (reticleGaze != null) {
			reticleGaze.CastRay (headRaycastCenter.position, headRaycastCenter.forward, 100f);
			selectedObject = reticleGaze.GetSelectedObject ();
			if (selectedObject != null) {
				Debug.Log ("Gaze: \"" + reticleGaze.GetSelectedObject().name + "\"");
				vruiObject = selectedObject.GetComponent<VRUI_Object> ();
				if ((vruiObject != null) && (vruiObject is VRUI_Button)) {
					(vruiObject as VRUI_Button).OnReticle (reticleGaze);
				}
			}
		}

		if (reticleController != null) {
			reticleController.CastRay (controllerRaycastCenter.position, controllerRaycastCenter.forward, 100f);
			selectedObject = reticleController.GetSelectedObject ();
			if (selectedObject != null) {
				Debug.Log ("Controller: \"" + reticleController.GetSelectedObject ().name + "\"");
				vruiObject = selectedObject.GetComponent<VRUI_Object> ();
				if ((vruiObject != null) && (vruiObject is VRUI_Button)) {
					(vruiObject as VRUI_Button).OnReticle (reticleController);
				}
			}
		}

		if (reticleMouse != null) {
			if (Input.GetMouseButton (0)) {
				reticleMouse.SetTriggerOn ();
			}
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			reticleMouse.CastRay (ray, 100f);
			selectedObject = reticleMouse.GetSelectedObject ();
			if (selectedObject != null) {
				Debug.Log ("Mouse: \"" + selectedObject.name + "\"");
				vruiObject = selectedObject.GetComponent<VRUI_Object> ();
				if ((vruiObject != null) && (vruiObject is VRUI_Button)) {
					(vruiObject as VRUI_Button).OnReticle (reticleMouse);
				}
			}
		}
	}

	void Update () {
		UpdateReticle ();
	}

	private void TestButtons () {
//		VRUI_Button vruiButton = VRUI_Button.Create (0.5f, 0.25f, VRUI_Container.Layout.HORIZONTAL, Color.grey, Color.black);
		VRUI_Button vruiButton = VRUI_Button.Create (0.5f, 0.25f, VRUI_Container.Layout.HORIZONTAL);
		vruiButton.SetStateColors (VRUI_Button.State.NORMAL, Color.grey, Color.black, Color.cyan);
		vruiButton.SetStateColors (VRUI_Button.State.OVER, Color.white, Color.red, Color.blue);
		vruiButton.SetStateColors (VRUI_Button.State.ACTIVATED, Color.blue, Color.white, Color.yellow);
		vruiButton.SetStateColors (VRUI_Button.State.DISABLED, Color.gray, Color.grey, Color.grey);

		Texture2D testImage = FileManager.ReadImageFromResources (null, "info");
		vruiButton.AddImage (testImage);

		vruiButton.AddText ("Info");
//		vruiButton.AddText ("Info", 0.25f, Color.red);

//		VRUI_Text text = VRUI_Text.Create ("Info", 0.1f, Color.red);
//		vruiButton.Add (text);

		vruiButton.transform.position = new Vector3 (0f, 0f, 1f);
		vruiButton.SetState (VRUI_Button.State.DISABLED);
	}

	private void TestNestedLayouts2 () {
		Font fontGothic = (Font)Resources.Load("GOTHICB");
		Font fontShowcardGothic = (Font)Resources.Load("SHOWG");

		VRUI_Container infoContainer = VRUI_Container.Create (0.4f, 0.25f, VRUI_Container.Layout.VERTICAL, Color.gray, Color.blue);

//		infoContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_LEFT);
//		infoContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_LEFT);
//		infoContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_LEFT);
//		infoContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_RIGHT);
//		infoContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_RIGHT);
//		infoContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_RIGHT);
//		infoContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_HCENTER);
//		infoContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_HCENTER);
		infoContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_HCENTER);

		Texture2D testImage = FileManager.ReadImageFromResources (null, "info");
		VRUI_Image img = VRUI_Image.Create (testImage, 0.1f);
		infoContainer.Add (img);

		img.marginBottom = 0.03f;

		VRUI_Text text = VRUI_Text.Create ("Info", 0.1f, Color.red, fontShowcardGothic);
		infoContainer.Add (text);

		infoContainer.marginRight = 0.1f;

		VRUI_Container faqContainer = VRUI_Container.Create (0.4f, 0.25f, VRUI_Container.Layout.VERTICAL, Color.gray, Color.blue);

//		faqContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_LEFT);
//		faqContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_LEFT);
//		faqContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_LEFT);
//		faqContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_RIGHT);
//		faqContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_RIGHT);
//		faqContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_RIGHT);
//		faqContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_HCENTER);
//		faqContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_HCENTER);
//		faqContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_HCENTER);

		testImage = FileManager.ReadImageFromResources (null, "faq");
		img = VRUI_Image.Create (testImage, 0.1f);
		faqContainer.Add (img);

		text = VRUI_Text.Create ("FAQ", 0.1f, Color.red, fontGothic);
		text.marginTop = 0.05f;

		faqContainer.Add (text);

		VRUI_Container mainContainer = VRUI_Container.Create (1f, 1f, VRUI_Container.Layout.HORIZONTAL, Color.gray, Color.blue);

		mainContainer.Add (infoContainer);
		mainContainer.Add (faqContainer);

		mainContainer.transform.position = new Vector3 (0f, 0f, 1f);
	}

	private void TestNestedLayouts1 () {
		Font fontGothic = (Font)Resources.Load("GOTHICB");
		Font fontShowcardGothic = (Font)Resources.Load("SHOWG");

		VRUI_Container infoContainer = VRUI_Container.Create (0.4f, 0.2f, VRUI_Container.Layout.HORIZONTAL, Color.gray, Color.blue);
		infoContainer.SetPadding (0.01f);

//		infoContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_LEFT);
//		infoContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_LEFT);
//		infoContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_LEFT);
//		infoContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_RIGHT);
//		infoContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_RIGHT);
//		infoContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_RIGHT);
//		infoContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_HCENTER);
//		infoContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_HCENTER);
		infoContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_HCENTER);

		Texture2D testImage = FileManager.ReadImageFromResources (null, "info");
		VRUI_Image img = VRUI_Image.Create (testImage, 0.1f);
		img.marginRight = 0.03f;
		infoContainer.Add (img);

		infoContainer.marginBottom = 0.1f;

		VRUI_Text text = VRUI_Text.Create ("Info", 0.1f, Color.red, fontShowcardGothic);
		infoContainer.Add (text);

		VRUI_Container faqContainer = VRUI_Container.Create (0.4f, 0.2f, VRUI_Container.Layout.HORIZONTAL, Color.gray, Color.blue);

//		faqContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_LEFT);
//		faqContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_LEFT);
//		faqContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_LEFT);
//		faqContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_RIGHT);
//		faqContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_RIGHT);
//		faqContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_RIGHT);
//		faqContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_HCENTER);
//		faqContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_HCENTER);
//		faqContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_HCENTER);

		testImage = FileManager.ReadImageFromResources (null, "faq");
		img = VRUI_Image.Create (testImage, 0.1f);
		faqContainer.Add (img);

		text = VRUI_Text.Create ("FAQ", 0.1f, Color.red, fontGothic);
		text.marginLeft = 0.03f;
		faqContainer.Add (text);

		VRUI_Container mainContainer = VRUI_Container.Create (1f, 1f, VRUI_Container.Layout.VERTICAL, Color.gray, Color.blue);
		mainContainer.Add (infoContainer);
		mainContainer.Add (faqContainer);

		mainContainer.transform.position = new Vector3 (0f, 0f, 1f);
	}

	private void TestHorizontalLayout () {
		VRUI_Container vruiContainer = VRUI_Container.Create (1f, 1f, VRUI_Container.Layout.HORIZONTAL, Color.gray, Color.blue);
		vruiContainer.transform.position = new Vector3 (0f, 0f, 1f);

//		vruiContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_LEFT);
//		vruiContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_LEFT);
//		vruiContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_LEFT);
//		vruiContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_RIGHT);
//		vruiContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_RIGHT);
//		vruiContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_RIGHT);
//		vruiContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_HCENTER);
//		vruiContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_HCENTER);
		vruiContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_HCENTER);

		Font fontGothic = (Font)Resources.Load("GOTHICB");
		Font fontShowcardGothic = (Font)Resources.Load("SHOWG");

		VRUI_Text text = VRUI_Text.Create ("Info ", 0.1f, Color.red, fontShowcardGothic);
		vruiContainer.Add (text);

		Texture2D testImage = FileManager.ReadImageFromResources (null, "info");
		VRUI_Image img = VRUI_Image.Create (testImage, 0.1f);
		vruiContainer.Add (img);

		text = VRUI_Text.Create (" Icon", 0.1f, Color.red, fontGothic);
		vruiContainer.Add (text);
	}

	private void TestVerticalLayout () {
		VRUI_Container vruiContainer = VRUI_Container.Create (1f, 1f, VRUI_Container.Layout.VERTICAL, Color.gray, Color.blue);
//		VRUI_Container vruiContainer = VRUI_Container.Create (1f, 1f, VRUI_Container.Layout.VERTICAL, Color.clear, Color.blue);

		vruiContainer.transform.position = new Vector3 (0f, 0f, 1f);
//		vruiContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_LEFT);
//		vruiContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_LEFT);
//		vruiContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_LEFT);
//		vruiContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_RIGHT);
//		vruiContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_RIGHT);
//		vruiContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_RIGHT);
//		vruiContainer.SetGravity (VRUI_Container.GRAVITY_TOP | VRUI_Container.GRAVITY_HCENTER);
//		vruiContainer.SetGravity (VRUI_Container.GRAVITY_BOTTOM | VRUI_Container.GRAVITY_HCENTER);
		vruiContainer.SetGravity (VRUI_Container.GRAVITY_VCENTER | VRUI_Container.GRAVITY_HCENTER);

		Font fontGothic = (Font)Resources.Load("GOTHICB");
		Font fontShowcardGothic = (Font)Resources.Load("SHOWG");

		/*
		Texture2D testImage = FileManager.ReadImageFromResources (null, "info");
		VRUI_Image img = VRUI_Image.Create (testImage, 0.25f);
		vruiContainer.Add (img);

		VRUI_Text text;
		int stringsNum = 5;
		for (int i = 0; i < stringsNum; i++) {
			text = VRUI_Text.Create ("String#" + (i + 1), 1f / (stringsNum * 2f), Color.white); //, font);
			vruiContainer.Add (text);
		}
		*/
		VRUI_Text text2 = VRUI_Text.Create ("String #1", 0.333f, Color.red, fontShowcardGothic);
		vruiContainer.Add (text2);

		text2 = VRUI_Text.Create ("String #2", 0.333f, Color.green, fontGothic);
		vruiContainer.Add (text2);

		text2 = VRUI_Text.Create ("String #3", 0.333f, Color.blue);
		vruiContainer.Add (text2);

		/*
		Texture2D testImage = FileManager.ReadImageFromResources (null, "info");
		VRUI_Image img = VRUI_Image.Create (testImage);
		img.transform.position = new Vector3 (0f, 0f, 1f);
		vruiContainer.Add (img);

		VRUI_Text text3 = VRUI_Text.Create ("The third string", Color.green);
		text3.SetAlignment (TextAlignment.Center);
		text3.SetAnchor (TextAnchor.MiddleCenter);
		vruiContainer.Add (text3);
		*/
		/*
//		VRUI_Panel panel = VRUI_Panel.Create (5f, 2f, Color.gray, Color.black);
		VRUI_Panel panel = VRUI_Panel.Create (5f, 2f, Color.gray, Color.red);
		panel.transform.position = new Vector3 (0f, 0f, 10.1f);
		*/
	}
}
