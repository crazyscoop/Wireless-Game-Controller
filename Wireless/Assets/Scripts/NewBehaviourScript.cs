using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class NewBehaviourScript : MonoBehaviour
{
    SerialPort serial1;
    byte[] buf = new byte[4]; // creates a byte array the size of the data you want to receive.
    int bufCount = 0;
    int a, b;
    public int distance;
    // Use this for initialization
    void Start()
    {
        serial1 = new SerialPort();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
    void parseValues(string av)
    {
        
       // string[] split = av.Split(',');

       // x1 = float.Parse(split[2]);
      //  y1 = float.Parse(split[3]);
      //  z1 = float.Parse(split[4]);
     //   Debug.Log(x1);
     //   Debug.Log(y1);
       // readyToMove = true;
    }
    */
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "read"))
        {
            string pn = "COM5";
            serial1.PortName =  pn;
            serial1.Parity = Parity.None;
            serial1.BaudRate = 9600;
            serial1.DataBits = 8;
            serial1.StopBits = StopBits.One;
            serial1.Open();
            // bufCount = 0;
            // bufCount += serial1.Read(buf, bufCount, buf.Length - bufCount);
            int distance = int.Parse(serial1.ReadLine());
            Debug.Log(distance);

            a = 0;
            b = 0;
            while (a < bufCount)
            {
                b += buf[a];
                a++;
            }
          //  print(b);
            serial1.Close();


        }
    }
}