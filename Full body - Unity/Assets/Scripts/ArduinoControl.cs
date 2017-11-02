using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class ArduinoControl : MonoBehaviour {

	private SerialPort stream;
	public static string[] arduinoSplitValues;
	private string arduinoRaw;

	public bool arduinoOn;
	public static bool arduinoTracking;

	// Use this for initialization
	void Start () {

		arduinoTracking = arduinoOn;

		if (arduinoOn) {
			stream = new SerialPort ("/dev/tty.usbmodem1421", 115200); //should be able to set up from GUI
			stream.ReadTimeout = 50;
			stream.Open ();
		}
	}


	// Update is called once per frame
	void Update () {

		if (arduinoOn) {
			
			arduinoRaw = ReadFromArduino (1);


			string[] charsToRemove = new string[] { "[", "]" };
			foreach (var c in charsToRemove) {
				arduinoRaw = arduinoRaw.Replace (c, string.Empty); //substitutes the values in charsToRemove["[" and "]"] by empty characters to clean string
			}

			//Debug.Log("Arduino now is translated into: " + arduinoRaw);

			arduinoSplitValues = arduinoRaw.Split (new string[] { "," }, StringSplitOptions.None);
		}
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