using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.IO.Ports; //Library to read our ardunio data
using System.Collections;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

        SerialPort serial1;
        public float h;//horizontal (car direction)
        public float distance;
        public float v;
        public Byte[] bt;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }
        void Start()
        {
            serial1 = new SerialPort();
            string pn = "COM5";
            serial1.PortName = pn;
            serial1.Parity = Parity.None;
            serial1.BaudRate = 9600;
            serial1.DataBits = 8;
            serial1.StopBits = StopBits.One;
            serial1.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            serial1.Open();
           serial1.ReadTimeout = 10000;
            ///serial1.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            StartCoroutine(ReadDataFromSerialPort());

        }
        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // read one byte data into bt
            Debug.Log("af");
            Byte[] bt = new byte[1024];
            ///serial1.Read(bt,0,1);
            for(int i = 0;i < 100;i++)
            {
                Debug.Log(Convert.ToString(bt[0]));
            }
            
            // all the code to sample data
        }

        IEnumerator ReadDataFromSerialPort()
        {
            while (true)
            {//loop


                string[] val = serial1.ReadLine().Split(',');
                // int val = serial1.ReadChar();
                //  Debug.Log(val);

                //  int bytes = serial1.BytesToRead;
                /*
                  Byte[] bt = new byte[1];
                  int count = serial1.Read(bt, 0, 1);
                  int offset = 0;
                  while(offset < count)
                  {
                      int read = serial1.Read(bt, offset, count - offset);
                      if(read == 0)
                      {
                          Debug.Log("Nothing");
                      }
                      offset += read;
                  }
                  */
               // Byte[] bt = new byte[12];
                ///int val = serial1.ReadChar();
              //  int count = serial1.Read(bt, 0, 1);
               // Debug.Log(count);
                Debug.Log(float.Parse(val[0]));

                /*
                for (int i = 0; i < 100; i++)
                {
                    Debug.Log("af");
               //     Debug.Log(Convert.ToString(bt[0]));
                   // 
                }
                */
                ///if (serial1.BytesToRead > 0)


                // int bytes = serial1.BytesToRead;
                //    byte[] buffer = new byte[256];

                //  serial1.Read(buffer, 0, 256);
                // Debug.Log(buffer.ToString());


                //distance = (float.Parse(val[0]))/100;//we split our string value by , because we write string as carspeed,cartotation in our ardunio codes
                //v = (float.Parse(val[1])) / 100;
                if (distance < 0.20f && distance > -0.20f)
                {
                    h = 0;
                }
                if(Math.Abs(distance) > 0.2f && Math.Abs(distance) < 0.4f)
                {
                    h = 0.25f*Math.Sign(distance);
                }
                if(Math.Abs(distance) > 0.4f && Math.Abs(distance) > 0.6f)
                {
                    h = 0.45f*Math.Sign(distance);
                }
                if(Math.Abs(distance) > 0.6f)
                {
                    h = 0.65f*Math.Sign(distance);
                }
                
                yield return new WaitForSeconds(.05f);//waiting seconds to read data. It should be same as ardunio code loop delay
            }
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            // float h = CrossPlatformInputManager.GetAxis("Horizontal");
            //float v = 0.1f;
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");

            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
