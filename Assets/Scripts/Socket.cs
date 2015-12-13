using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;

public class Socket {

	private TcpClient client;
	private NetworkStream stream;
	private StreamWriter streamWriter;
	private StreamReader streamReader;
	private String hostname;
	private Int32 port;

	private Boolean socketReady = false;
	
	public Socket(String hostname, Int32 port){
		this.hostname = hostname;
		this.port = port;
		this.Connect();
	}

	public void Connect() {
		try {
			client = new TcpClient(hostname, port);
			stream = client.GetStream();
			streamWriter = new StreamWriter(stream);
			streamReader = new StreamReader(stream);
			socketReady = true;
		}
		catch (Exception e) {
			throw e;
		}
	}
	
	public void Write(string message) {
		if(!socketReady) return;
		message = message + "\n";
		streamWriter.Write(message);
		streamWriter.Flush();
	}
	
	public String Read() {
		if(!socketReady) return "";
		if(this.HasData())
			return streamReader.ReadLine();
		return null;
	}
	
	public void Close() {
		if (!socketReady) return;
		streamWriter.Close();
		streamReader.Close();
		client.Close();
		socketReady = false;
	}

	public bool HasData(){
		return stream.DataAvailable;
	}
}
