# Nerve_SocketProgramming_Assignment
 
Steps to execute:

1. Open cmd for server
2. Compile the server file using command "csc server.cs"
3. Execute the server exe using command "server"
4. Compile the client file using command "csc client.cs"
5. Execute the client exe using command "client"
6. Once the server is running, enter the input in client window

Assignment Details:
The goal of this assignment is to implement a TCP client and server. Your TCP client/server
will communicate over the network and exchange data. The server will start in passive mode
listening for a transmission from the client. The client will then start and contact the server

(on a given IP address and port number). The client will pass the server a string (e.g.: “SetA-
One”).

On receiving a string from a client, the server should:
1) Check if the first part of the received string is present in the server collection.
{"SetA":[{"One":1,"Two":2}],"SetB":[{"Three":3,"Four":4}],"SetC":[{"Five":5,"Six":6}],"SetD":[
{"Seven":7,"Eight":8}],"SetE":[{"Nine":9,"Ten":10}]}
2) If yes, then retrieve the subset for the corresponding string and check if the second part of
the received string is present in the retrieved subset. If yes, then the final VALUE retrieved,
will be number of times the server has to send the response to client.
• The server will then send current time of the system at 1 second interval 'n' number
of times. (Where 'n' is the VALUE retrieved)
3) If not, then sever will send "EMPTY" message to client.
4) The client will display the received string.

The second part the assignment is to Encrypt and Decrypt every message transferred between
Client and the Server i.e., the client should encrypt the message before sending to the server
and decrypt the messages received from the server. Likewise, the server should be able to do
the same.
