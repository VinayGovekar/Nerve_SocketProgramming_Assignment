
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
namespace Client {

class Program {

private static string _password ="Thala07";
static void Main(string[] args)
{   
	ExecuteClient();
}

static string EncryptOrDecrpyt(string input,string password,bool IsEncrypt){
    var resultString = new List<byte>();
    var count = password.Length;
    foreach(var x in Encoding.ASCII.GetBytes(input)){
        if(IsEncrypt)
            resultString.Add(Convert.ToByte(Convert.ToInt32(x)+count));
        else
            resultString.Add(Convert.ToByte(Convert.ToInt32(x)-count));
    }
    return Encoding.ASCII.GetString(resultString.ToArray());
}
static void ExecuteClient()
{

	try {
		
		IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
		IPAddress ipAddr = ipHost.AddressList[0];
		IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

		Socket sender = new Socket(ipAddr.AddressFamily,
				SocketType.Stream, ProtocolType.Tcp);

		try {
            sender.Connect(localEndPoint);
            
            Console.WriteLine("*** Client Is Started Now ***\n\n");
            Console.WriteLine("Please input your message for the server: ");
            var input = Console.ReadLine();
			byte[] messageSent = Encoding.ASCII.GetBytes(EncryptOrDecrpyt(input,_password,true));
			int byteSent = sender.Send(messageSent);

			while(true){
                System.Threading.Thread.Sleep(1000);
                byte[] messageReceived = new byte[1024];

			    int byteRecv = sender.Receive(messageReceived);
                var data =EncryptOrDecrpyt(Encoding.ASCII.GetString(messageReceived,0, byteRecv),_password,false);
                if(data=="-1" || data=="") break;
                if(data!="-1") Console.WriteLine(data);
            }
            sender.Shutdown(SocketShutdown.Both);
			sender.Close();
		}
		
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
