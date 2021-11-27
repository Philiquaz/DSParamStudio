using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Diagnostics;
using System.Threading;
using System.Security.Principal;
using System.Security.AccessControl;

namespace StudioCore
{
    public class StudioServer
    {

        private NamedPipeServerStream serverStream = null;
        private List<ServerClient> activeConnections = new List<ServerClient>();
        private PipeSecurity pipeSecurity;

        public StudioServer()
        {
            try
            {
            pipeSecurity = new PipeSecurity();
            pipeSecurity.SetAccessRule(new PipeAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null), PipeAccessRights.ReadWrite, AccessControlType.Allow));
            
            serverStream = NamedPipeServerStreamConstructors.New($@"\.\DSParamStudio\pipe\CommandQueue", PipeDirection.InOut, System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Message, PipeOptions.Asynchronous, 0, 0, pipeSecurity);
            
            serverStream.BeginWaitForConnection(handleConnection, serverStream);

            new Thread(TestThread).Start();
            }
            catch
            {
            }
        }

        private void handleConnection(IAsyncResult res){
            //serverStream = new NamedPipeServerStream($@"\.\DSParamStudio\pipe\CommandQueue", PipeDirection.In, System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances);
            

            NamedPipeServerStream srv = (NamedPipeServerStream)res.AsyncState;
            //srv.EndWaitForConnection(res);
            serverStream.EndWaitForConnection(res);
            serverStream = NamedPipeServerStreamConstructors.New($@"\.\DSParamStudio\pipe\CommandQueue", PipeDirection.InOut, System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Message, PipeOptions.Asynchronous, 0, 0, pipeSecurity);
            serverStream.BeginWaitForConnection(handleConnection, serverStream);
            ServerClient sv = new ServerClient(srv);
            activeConnections.Add(sv);

            while (srv.IsConnected)
            {
                string command = sv.reader.ReadLine();
                if (command != null && command.Length > 0)
                {
                    EditorCommandQueue.AddCommand("windowFocus");
                    EditorCommandQueue.AddCommand(command);
                }
            }
        }

        private void TestThread(){
            Thread.Sleep(5000);
            StudioClient c = StudioClient.StartClient();
            if (c == null)
                Console.WriteLine("Failed to start client");
            Console.WriteLine("Client created");
            Thread.Sleep(5000);
            c.sendMessage("param/view/new/EquipParamProtector/0");
            Console.WriteLine("test message sent");
            Thread.Sleep(5000);
            c.sendMessage("param/view/new/EquipParamWeapon/0");
            Thread.Sleep(5000);
            c.CloseClient();
            //StudioClient.StartParamStudio();
            Thread.Sleep(5000);
            c = StudioClient.StartClient();
            Thread.Sleep(5000);
            c.sendMessage("param/view/new/EquipParamProtector/0");
            c.CloseClient();
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
