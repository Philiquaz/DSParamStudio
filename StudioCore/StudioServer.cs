using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;

namespace StudioCore
{
    public class StudioServer
    {

        private NamedPipeServerStream serverStream = null;
        private List<ServerClient> activeConnections = new List<ServerClient>();

        public StudioServer()
        {
            try
            {
            serverStream = new NamedPipeServerStream($@"\.\DSParamStudio\pipe\CommandQueue", PipeDirection.InOut, System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances);
            serverStream.BeginWaitForConnection(handleConnection, serverStream);

            //new Thread(TestThread).Start();
            }
            catch
            {
            }
        }

        private void handleConnection(IAsyncResult res){
            serverStream = new NamedPipeServerStream($@"\.\DSParamStudio\pipe\CommandQueue", PipeDirection.InOut, System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances);
            serverStream.BeginWaitForConnection(handleConnection, serverStream);

            NamedPipeServerStream srv = (NamedPipeServerStream)res.AsyncState;
            srv.EndWaitForConnection(res);
            ServerClient sv = new ServerClient(srv);
            activeConnections.Add(sv);

            while (srv.IsConnected)
            {
                string command = sv.reader.ReadLine();
                EditorCommandQueue.AddCommand("windowFocus");
                EditorCommandQueue.AddCommand(command);
            }
        }

        private void TestThread(){
            Thread.Sleep(5000);
            StudioClient c = StudioClient.StartClient();
            if (c == null)
                Console.WriteLine("Failed to start client");
            Console.WriteLine("Client created");
            Thread.Sleep(10000);
            c.sendMessage("param/view/new/EquipParamProtector/0");
            Console.WriteLine("test message sent");
            Thread.Sleep(10000);
            c.sendMessage("param/view/new/EquipParamWeapon/0");
            Thread.Sleep(5000);
            StudioClient.StartParamStudio();
        }

        internal class ServerClient
        {
            internal NamedPipeServerStream server;
            internal StreamReader reader;
            internal StreamWriter writer;

            internal ServerClient(NamedPipeServerStream srv)
            {
                server = srv;
                reader = new StreamReader(server);
                writer = new StreamWriter(server);
            }
        }
    }
}
