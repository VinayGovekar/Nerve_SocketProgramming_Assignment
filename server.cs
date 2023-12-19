
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace Server {

class Program {

private static string _password ="Thala07";
static void Main(string[] args)
{
	ExecuteServer();
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

public static Dictionary<string,int> GetSubData(string[] number,int[] num){
    var result = new Dictionary<string,int>();
    for(int i=0;i<number.Length;i++){
        result.Add(number[i],num[i]);
    }
    return result;
}

public static Dictionary<string,Dictionary<string,int>> CreateServerCollection(){
    
    var serverCollection = new Dictionary<string,Dictionary<string,int>>();
    serverCollection.Add("SetA",GetSubData(new string[]{"One","Two"},new int[]{1,2}));
    serverCollection.Add("SetB",GetSubData(new string[]{"Three","Four"},new int[]{3,4}));
    serverCollection.Add("SetC",GetSubData(new string[]{"Five","Six"},new int[]{5,6}));
    serverCollection.Add("SetD",GetSubData(new string[]{"Seven","Eight"},new int[]{7,8}));
    serverCollection.Add("SetE",GetSubData(new string[]{"Nine","Ten"},new int[]{9,10}));
    return serverCollection;
}

public static int ProcessClientData(Dictionary<string,Dictionary<string,int>> serverData,string clientData){
    if(clientData==null || clientData.Length<7) return -1;
    string[] clientDataParts = clientData.Split('-');
    if(clientDataParts.Length<2) return -1;
    if(!serverData.ContainsKey(clientDataParts[0])) return -1;
    var dataOne = serverData[clientDataParts[0]];
    if(!dataOne.ContainsKey(clientDataParts[1])) return -1;
    return dataOne[clientDataParts[1]];
}

public static void ExecuteServer()
{
	
	IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
	IPAddress ipAddr = ipHost.AddressList[0];
	IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

	Socket listener = new Socket(ipAddr.AddressFamily,SocketType.Stream, ProtocolType.Tcp);
    
    var serverData = CreateServerCollection();

	try {
		
		listener.Bind(localEndPoint);

		listener.Listen(10);
        
		Console.WriteLine("\n*** Server Started Now ***\n");

		while (true) {
			
			Console.WriteLine("\nWaiting connection ... \n");

			Socket clientSocket = listener.Accept();

			byte[] bytes = new Byte[1024];
			string data = null;
            int result=-1;

			while (true) {

				int numByte = clientSocket.Receive(bytes);
				data += Encoding.ASCII.GetString(bytes,0, numByte);
                result = ProcessClientData(serverData,EncryptOrDecrpyt(data,_password,false));
                break;
			}

            if(result!=-1){
                for(int i=1;i<=result;i++){
                    var currentTime = DateTime.Now.ToString();
                    Console.WriteLine(currentTime);
                    byte[] messageI = Encoding.ASCII.GetBytes(EncryptOrDecrpyt(currentTime,_password,true));
			        clientSocket.Send(messageI);
                    System.Threading.Thread.Sleep(1000);
                }
            }
            else{
                byte[] message3 = Encoding.ASCII.GetBytes(EncryptOrDecrpyt("EMPTY",_password,true));
		        clientSocket.Send(message3);
                System.Threading.Thread.Sleep(1000);
            }
            byte[] message2 = Encoding.ASCII.GetBytes(EncryptOrDecrpyt("-1",_password,true));
		    clientSocket.Send(message2);
            

			clientSocket.Shutdown(SocketShutdown.Both);
			clientSocket.Close();
		}
	}
	
	catch (Exception e) {
		Console.WriteLine(e.ToString());
	}
}
}
}
