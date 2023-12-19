// A C# program for Client
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client {

class Program {

// Main Method
static void Main(string[] args)
{
	ExecuteClient();
}

// ExecuteClient() Method
static void ExecuteClient()
{

	try {
		
		// Establish the remote endpoint 
		// for the socket. This example 
		// uses port 11111 on the local 
		// computer.
		IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
		IPAddress ipAddr = ipHost.AddressList[0];
		IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

		// Creation TCP/IP Socket using 
		// Socket Class Constructor
		Socket sender = new Socket(ipAddr.AddressFamily,
				SocketType.Stream, ProtocolType.Tcp);

		try {
            sender.Connect(localEndPoint);
            
            Console.WriteLine("*** Client Is Started Now ***\n\n");
            Console.WriteLine("Please input your message for the server: ");
            var input = Console.ReadLine();
			byte[] messageSent = Encoding.ASCII.GetBytes(input);
			int byteSent = sender.Send(messageSent);

			while(true){
                System.Threading.Thread.Sleep(1000);
                byte[] messageReceived = new byte[1024];

			    int byteRecv = sender.Receive(messageReceived);
                var data =Encoding.ASCII.GetString(messageReceived, 
											0, byteRecv);
                if(data=="-1") break;
                Console.WriteLine(data);
            }
            sender.Shutdown(SocketShutdown.Both);
			sender.Close();
		}
		
		// Manage of Socket's Exceptions
		catch (ArgumentNullException ane) {
			
			Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
		}
		
		catch (SocketException se) {
			
			Console.WriteLine("SocketException : {0}", se.ToString());
		}
		
		catch (Exception e) {
			Console.WriteLine("Unexpected exception : {0}", e.ToString());
		}
	}
	
	catch (Exception e) {
		
		Console.WriteLine(e.ToString());
	}
}
}
}
