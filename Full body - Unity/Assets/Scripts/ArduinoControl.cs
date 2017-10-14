using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class ArduinoControl : MonoBehaviour {

	private SerialPort stream;


	// Use this for initialization
	void Start () {
		
		stream = new SerialPort ("/dev/tty.usbmodem14141", 115200); 
		stream.ReadTimeout = 50;
		stream.Open();
	}


	// Update is called once per frame
	void Update () {

		Debug.Log ("El valor es:" + ReadFromArduino(1).ToString()); 

	}

	//Read from arduino
	public string ReadFromArduino(int timeout = 0) {
		
		stream.ReadTimeout = timeout;

		try {
			return stream.ReadLine();

		}

		catch (TimeoutException) {
			return null;
		}
	}


	public IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity) {
		
		DateTime initialTime = DateTime.Now;
		DateTime nowTime;
		TimeSpan diff = default(TimeSpan);

		string dataString = null;

		do {
			// A single read attempt
				try {
					dataString = stream.ReadLine();
				}

				catch (TimeoutException) {
					dataString = null;
				}

				if (dataString != null) {
					callback(dataString);
					yield return null;
				} 

				else
					yield return new WaitForSeconds(0.05f);

				nowTime = DateTime.Now;
				diff = nowTime - initialTime;
			} 

		while (diff.Milliseconds < timeout);

		if (fail != null)
			fail();
		yield return null;
	}



	public void Close() {
		stream.Close();
	}
		
}