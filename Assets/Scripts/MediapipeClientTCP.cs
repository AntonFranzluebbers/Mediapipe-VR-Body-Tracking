using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

public class MediapipeClientTCP : DataServer
{
	public string IP = "127.0.0.1";
	public int port = 8642;


	// receive thread
	private void Update()
	{
		StartCoroutine(GetRequest());
	}


	IEnumerator GetRequest()
	{
		using (UnityWebRequest webRequest = UnityWebRequest.Get($"{IP}:{port}"))
		{
			yield return webRequest.SendWebRequest();
			LastPacket = webRequest.downloadHandler.text;
		}
	}
}