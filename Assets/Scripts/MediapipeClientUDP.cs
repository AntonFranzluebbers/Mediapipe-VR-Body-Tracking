using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class MediapipeClientUDP : DataServer
{
    // receiving Thread
    Thread receiveThread;
 
    // udpclient object
    UdpClient client;
 
    public string IP = "127.0.0.1";
    public int port = 8642;
   
    
    public void Start()
    {
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.Start();
    }

    // receive thread
    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (true)
        {
            try
            {
                // receive bytes
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);
 
                // convert to string
                string text = Encoding.UTF8.GetString(data);
                
                LastPacket = text;
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }
}
