using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class INetworkManager
{
    private const int DEFAULT_PORT = 5000;
    private int portNumber;

    public delegate void NewNetworkCommand(string Command);
    public static event NewNetworkCommand OnNetworkCommand = null;

    #region private members 	
    /// <summary> 	
    /// TCPListener to listen for incomming TCP connection 	
    /// requests. 	
    /// </summary> 	
    private TcpListener tcpListener;
    /// <summary> 
    /// Background thread for TcpServer workload. 	
    /// </summary> 	
    private Thread tcpListenerThread;
    /// <summary> 	
    /// Create handle to connected tcp client. 	
    /// </summary> 	
    private TcpClient connectedTcpClient;
    private string clientMessage;
    #endregion

    private bool isNetworkWorking;

    public INetworkManager()
    {
        setPortNumber(DEFAULT_PORT);
    }

    public INetworkManager(int port)
    {
        portNumber = port;
    }


    public void setPortNumber(int port)
    {
        portNumber = port;
    }

    public void NetworkStart()
    {
        if (isNetworkWorking == false)
        {
            tcpListenerThread = new Thread(networkListenerLoop);
            tcpListenerThread.IsBackground = true;
            isNetworkWorking = true;
            tcpListenerThread.Start();
        }
    }

    public void NetworkStop()
    {
        isNetworkWorking = false;

        if (tcpListener != null)
            tcpListener.Stop();

        if (tcpListenerThread != null && tcpListenerThread.IsAlive)
        {
            tcpListenerThread.Abort();
        }
    }

    private void networkListenerLoop()
    {
        try
        {
            isNetworkWorking = true;

            // Create listener on localhost port 8052. 			
            tcpListener = new TcpListener(IPAddress.Any, portNumber);
            tcpListener.Start();
            Debug.Log("Server is listening : " + getMyIp() + " Port Number: " + portNumber);
            Byte[] bytes = new Byte[1024];
            while (isNetworkWorking)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    // Get a stream object for reading 					
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        int length;
                        // Read incomming stream into byte arrary. 						
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message. 							
                            clientMessage = Encoding.ASCII.GetString(incommingData);

                            OnNetworkCommand?.Invoke(clientMessage);

                            Debug.Log("client message received as: " + clientMessage);
                        }
                    }
                }
            }

        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }

    internal string getClientIP()
    {
        if (connectedTcpClient != null)
            return ((IPEndPoint)connectedTcpClient.Client.RemoteEndPoint).Address.ToString();

        return null;
    }

    internal bool isConnected()
    {
        if (connectedTcpClient != null && connectedTcpClient.Connected)
        {
            return true;
        }

        return false;
    }

    internal string getServerIP()
    {
        if (tcpListener != null)
            return tcpListener.LocalEndpoint.ToString();

        return null;
    }

    private string getMyIp()
    {

        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
            }
        }

        return localIP;
    }
}
