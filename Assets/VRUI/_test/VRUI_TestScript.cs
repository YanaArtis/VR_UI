using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI_TestScript : MonoBehaviour {

	void Start () {
//		TestVerticalLayout ();
//		TestHorizontalLayout ();
//		TestNestedLayouts1 ();
//		TestNestedLayouts2 ();
		TestButtons ();
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
	
	void Update () {
		
	}
}
