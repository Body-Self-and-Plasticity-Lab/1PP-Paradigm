using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class ArduinoControl : MonoBehaviour {

	private SerialPort stream;
	public static string[] arduinoSplitValues;
	private string arduinoRaw;

	public string port;
	public int baudrate;

	// Use this for initialization
	void Start () {

		string[] ports = SerialPort.GetPortNames();

		if (EntryScreenManager.arduinoTrackingIsOn) {
			stream = new SerialPort (ports[EntryScreenManager.portIndex], baudrate); //should be able to set up from GUI
			stream.ReadTimeout = 50;
			stream.Open ();
		}
	}


	// Update is called once per frame
	void Update () {

		if (EntryScreenManager.arduinoTrackingIsOn) {
			
			arduinoRaw = ReadFromArduino (5);


			string[] charsToRemove = new string[] { "[", "]" };
			foreach (var c in charsToRemove) {
				arduinoRaw = arduinoRaw.Replace (c, string.Empty); //substitutes the values in charsToRemove["[" and "]"] by empty characters to clean string
			}

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