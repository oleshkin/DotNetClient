﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

// Client app is the one sending messages to a Server/listener.
// Both listener and client can send messages back and forth once a
// communication is established.
public class SocketClient
{
    public static int Main(String[] args)
    {
        StartClient();
        return 0;
    }

    public static void StartClient()
    {
        byte[] bytes = new byte[1024];

        try
        {
            // Connect to a Remote server
            // Get Host IP Address that is used to establish a connection
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1
            // If a host has multiple addresses, you will get a list of addresses
            IPHostEntry host = Dns.GetHostEntry("pythonserver");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 65432);

            // Create a TCP/IP socket.
            Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.
            try
            {
                // Connect to Remote EndPoint
                sender.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());

                    // Incoming data from the server.
                    string data = null;


                    while (true) 
                    {
                        bytes = new byte[1024];
                        int bytesRec = sender.Receive(bytes);

                        data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        
                        if (data == "space")
                        {
                            data = " ";
                        } 
                        else if (data == "enter") 
                        {
                            data = "\n";
                        }


                        


                        Console.Write(data);
                        if (data == null)
                        {
                            break;
                        }
                    }

                // Release the socket.
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}