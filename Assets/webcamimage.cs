using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class webcamimage : MonoBehaviour {

	public RawImage rawimage;
	void Start () 
	{
		WebCamTexture webcamTexture = new WebCamTexture();
		rawimage.texture = webcamTexture;
		rawimage.material.mainTexture = webcamTexture;
		webcamTexture.Play();
	}

	public Button btnCapture;
	public void Capture(){
	
	}
	WebCamTexture _CamTex;
	private string _SavePath = "C:/Desktop/";
	int _CaptureCounter = 0;
	public void TakeSnapshot()
	{
		Texture2D snap = new Texture2D(_CamTex.width, _CamTex.height);
		snap.SetPixels(_CamTex.GetPixels());
		snap.Apply();

		System.IO.File.WriteAllBytes(_SavePath + _CaptureCounter.ToString() + ".png", snap.EncodeToPNG());
		++_CaptureCounter;
	}

}
