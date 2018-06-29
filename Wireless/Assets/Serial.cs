using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;
using System.Threading;

public class Serial : MonoBehaviour
{

    public SerialPort sp;
    public int bytes;
    public Thread serialThread;
    public float x1, y1, z1;
    public bool readyToMove = false;
    public float prevY = 0.0f;

    void Start()
    {
        String portname = "COM21";
        sp = new SerialPort(@"\\\\.\" + portname, 9600);
       // sp = new SerialPort(portname, 9600, Parity.None, 8, StopBits.One);
      //  sp.Open();
    }

    void parseValues(string av)
    {

        string[] split = av.Split(',');
        x1 = float.Parse(split[2]);
        y1 = float.Parse(split[3]);
        z1 = float.Parse(split[4]);
        readyToMove = true;
    }


    void moveObj(float x, float y)
    {
        Debug.Log(x);
        Debug.Log(y);
    }


    void recData()
    {
        if ((sp != null) && (sp.IsOpen))
        {
            Debug.Log("recData");
            byte tmp;
            string data = "";
            string avalues = "";
            tmp = (byte)sp.ReadByte();
            while (tmp != 255)
            {
                data += ((char)tmp);
                tmp = (byte)sp.ReadByte();
               // if ((tmp == '>') && (data.Length > 30))
                {
                    avalues = data;
                    parseValues(avalues);
                    data = "";
                }
            }
        }
        else
        {
            Debug.Log("Wrong");
        }
    }


    void connect()
    {
        Debug.Log("Connection started");
        try
        {
            sp.Open();
            sp.ReadTimeout = 400;
            sp.Handshake = Handshake.None;
            serialThread = new Thread(recData);
            serialThread.Start();
            Debug.Log("Port Opened!");
        }
        catch (SystemException e)
        {
            Debug.Log("Error opening = " + e.Message);
        }

    }


    void Update()
    {

        if (Input.GetKeyDown("x"))
        {
            Debug.Log("Connection establishing...");
            connect();
        }
        if (Input.GetKeyDown("y"))
        {
            Debug.Log("Connection establishing...");
            sp.Open();
            sp.ReadTimeout = 400;
            sp.Handshake = Handshake.None;
            // connect();
            int i = 0;
            for (i = 0; i < 6; i++)
            {
                sp.Write("A");
            }

        }
        //  Debug.Log(x1);
        // Debug.Log(y1);
        if (readyToMove == true)
        {
            moveObj(x1, y1);
        }

    }


}