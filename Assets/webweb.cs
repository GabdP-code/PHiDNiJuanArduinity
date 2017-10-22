using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
//using UnityEngine;
using System;

public class webweb : MonoBehaviour {

	string resultString;
	string enc;
	public Button okButton;
	public Text textdialog, textCamera;
	public Dropdown dropdown;
	public GameObject panelDialog;
	public InputField RFID,PDLastName, PDFirstName, PDMiddleName,PDBarangay, PDTown, PDCity, PDContactNumber, PDEmailAddress, PDSSSNumber, PDPhilHealthNumber,PDPagIbig,PDTin, PDGSIS,PDOFW;
	public InputField CPLastName, CPContactNumber, CPEmailAddress, CPRelation, CPAddress;
	public RawImage rawimage;  //Image for rendering what the camera sees.
	WebCamTexture webcamTexture = null;

	void Start()
	{
	

		WebCamDevice[] camDevices = WebCamTexture.devices;

		//Get the used camera name for the WebCamTexture initialization.
		string camName = camDevices[0].name;
		webcamTexture = new WebCamTexture(camName);

		//Render the image in the screen.
		rawimage.texture = webcamTexture;
		rawimage.material.mainTexture = webcamTexture;
		webcamTexture.Play();

		RFID.enabled = false;
		panelDialog.SetActive (false);
		webcamTexture.Stop ();
		textCamera.text = "Start";
	}

	public void TakeSnap()
	{
		//This is to take the picture, save it and stop capturing the camera image.
//			SaveImage();
		//webcamTexture.Stop();
		if(textCamera.text == "Start"){
			webcamTexture.Play ();
			textCamera.text = "Take";
		}
		else if(textCamera.text == "Take"){
			SaveImage();
			textCamera.text = "Save";
			webcamTexture.Stop ();
		}
		else if(textCamera.text == "Save"){
			StartCoroutine (SavingProcess ());

		}
	}
		
	void SaveImage()
	{
		//Create a Texture2D with the size of the rendered image on the screen.
		Texture2D texture = new Texture2D(rawimage.texture.width, rawimage.texture.height, TextureFormat.ARGB32, false);

		//Save the image to the Texture2D
		texture.SetPixels(webcamTexture.GetPixels());
		texture.Apply();

		//Encode it as a PNG.
		byte[] bytes = texture.EncodeToPNG();

		enc = Convert.ToBase64String(bytes);
		//Save it in a file.
		//File.WriteAllBytes(Application.dataPath + "/testing.png", bytes);

		//StartCoroutine (SavingProcess (enc));
	}

	IEnumerator SavingProcess(){
		WWWForm form = new WWWForm ();
		form.AddField ("job_category_iD", dropdown.value);
		form.AddField ("rfid", RFID.text);
		form.AddField ("lname", PDLastName.text);
		form.AddField ("fname", PDFirstName.text);
		form.AddField ("mname", PDMiddleName.text);
		form.AddField ("bry", PDBarangay.text);
		form.AddField ("town", PDTown.text);
		form.AddField ("city", PDCity.text);
		form.AddField ("mobile", PDContactNumber.text);
		form.AddField ("email", PDEmailAddress.text);
		form.AddField ("SSS", PDSSSNumber.text);
		form.AddField ("philhealth", PDPhilHealthNumber.text);
		form.AddField ("pagibig", PDPagIbig.text);
		form.AddField ("tin", PDTin.text);
		form.AddField ("GSIS", PDGSIS.text);
		form.AddField ("ofw_country_id", PDOFW.text);
		form.AddField ("cp_name", CPLastName.text);
		form.AddField ("cp_address", CPAddress.text);
		form.AddField ("cp_number", CPContactNumber.text);
		form.AddField ("cp_email", CPEmailAddress.text);
		form.AddField ("cp_relationship", CPRelation.text);
		//http://idnijuan.x10host.com/login.php
		WWW itemsData = new WWW ("http://idnijuan.x10host.com/idjuan/reg.php", form);
		yield return itemsData;
		resultString = itemsData.text;

		if (resultString == "Registration Sucessful") {
			panelDialog.SetActive (true);
			textdialog.text = "Successfully Registered";
		} else {
			panelDialog.SetActive (true);
			textdialog.text = "Registration Failed";

		}
	}
	public void okBtn(){
		panelDialog.SetActive (false);
		clearInput ();
	}

	void clearInput(){
		RFID.text = "";
		PDLastName.text = "";
		PDFirstName.text = "";
		PDMiddleName.text = "";
		PDContactNumber.text = "";
		PDEmailAddress.text = "";
		PDBarangay.text = "";
		PDTown.text = "";
		PDCity.text = "";
		PDSSSNumber.text = "";
		PDGSIS.text = "";
		PDPhilHealthNumber.text = "";
		PDPagIbig.text = "";
		PDTin.text = "";
		PDOFW.text = "";
		CPLastName.text = "";
		CPAddress.text = "";
		CPContactNumber.text = "";
		CPEmailAddress.text = "";
		CPRelation.text = "";
	}

	IEnumerator UpdatingProcess(string enc){
		WWWForm form = new WWWForm ();
		form.AddField ("image", enc);
		form.AddField ("rfid", RFID.text);
		WWW itemsData = new WWW ("http://idnijuan.x10host.com/idjuan/update.php", form);
		yield return itemsData;
	}
}
