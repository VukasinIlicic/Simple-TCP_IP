﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP_IP_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IPAddress ipAd = IPAddress.Parse("127.0.0.1");

                /* Initializes the Listener */
                TcpListener myListener = new TcpListener(ipAd, 8001);

                /* Start Listeneting at the specified port */
                myListener.Start();

                Console.WriteLine("The server is running at port 8001...");
                Console.WriteLine($"The local endpoint is  : {myListener.LocalEndpoint}");
                Console.WriteLine("Waiting for a connection.....");

                Socket s = myListener.AcceptSocket();
                Console.WriteLine($"Connection accepted from {s.RemoteEndPoint}");

                byte[] b = new byte[100];
                int k = s.Receive(b);
                Console.WriteLine("Recieved...");
                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(b[i]));

                ASCIIEncoding asen = new ASCIIEncoding();
                s.Send(asen.GetBytes("The string was recieved by the server."));
                Console.WriteLine("\nSent Acknowledgement");

                /* clean up */
                s.Close();
                myListener.Stop();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
