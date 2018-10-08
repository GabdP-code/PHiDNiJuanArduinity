using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System;

public class canMove : MonoBehaviour {
	SerialPort stream = new SerialPort("COM5",9600);
	public InputField rfid;
//	int buttonState = 0;
	public
	void Start () {
		stream.Open ();
		stream.ReadTimeout = 100;
	}
	
	// Update is called once per frame
	void Update () 
	{
		rfid.text = stream.ReadLine ();
	}

}
