using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class CS : MonoBehaviour {

    SerialPort serial1;
    byte[] buf = new byte[8]; // creates a byte array the size of the data you want to receive.
    int bufCount = 0;
    int a, b;
    public int distance;
    // Use this for initialization
    void Start ()
    {
        serial1 = new SerialPort();
        string pn = "COM5";
        serial1.PortName = pn;
        serial1.Parity = Parity.None;
        serial1.BaudRate = 9600;
        serial1.DataBits = 8;
        serial1.StopBits = StopBits.One;
        serial1.Open();
        StartCoroutine(ReadDataFromSerialPort());

    }

    IEnumerator ReadDataFromSerialPort()
    {
        while (true)
        {//loop
            distance = int.Parse(serial1.ReadLine());//we split our string value by , because we write string as carspeed,cartotation in our ardunio codes
            Debug.Log(distance);
            yield return new WaitForSeconds(.05f);//waiting seconds to read data. It should be same as ardunio code loop delay
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        Debug.Log(distance);
	
	}
}
