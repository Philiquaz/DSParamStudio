using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Veldrid;
using Veldrid.Sdl2;
using System.Security.Policy;
using System.Security.Cryptography;

namespace StudioCore.Scene
{
    public class Renderer
    {

        private static GraphicsDevice Device;
        private static CommandList MainCommandList;

        private static Queue<Action<GraphicsDevice, CommandList>> BackgroundUploadQueue;

        private static CommandList TransferCommandList;
        private static ConcurrentQueue<(DeviceBuffer, DeviceBuffer, Action<GraphicsDevice>)> _asyncTransfersPendingQueue;
        private static List<(Fence, Action<GraphicsDevice>)> _asyncTransfers;
        private static Queue<Fence> _freeTransferFences;

        private static int BUFFER_COUNT = 3;
        private static List<Fence> _drawFences = new List<Fence>();
        private static int _currentBuffer = 0;
        private static int _nextBuffer { get => (_currentBuffer + 1) % BUFFER_COUNT; }
        private static int _prevBuffer { get => (_currentBuffer - 1 + BUFFER_COUNT) % BUFFER_COUNT; }

        private static List<(CommandList, Fence)> _postDrawCommandLists = new List<(CommandList, Fence)>(2);

        public static ResourceFactory Factory
        {
            get
            {
                return Device.ResourceFactory;
            }
        }
        public unsafe static void Initialize(GraphicsDevice device)
        {
            Device = device;
            MainCommandList = device.ResourceFactory.CreateCommandList();
            BackgroundUploadQueue = new Queue<Action<GraphicsDevice, CommandList>>();

            TransferCommandList = device.ResourceFactory.CreateCommandList(new CommandListDescription(true));
            _asyncTransfers = new List<(Fence, Action<GraphicsDevice>)>();
            _asyncTransfersPendingQueue = new ConcurrentQueue<(DeviceBuffer, DeviceBuffer, Action<GraphicsDevice>)>();
            _freeTransferFences = new Queue<Fence>();
            for (int i = 0; i < 3; i++)
            {
                _freeTransferFences.Enqueue(device.ResourceFactory.CreateFence(false));
            }

            for (int i = 0; i < BUFFER_COUNT; i++)
            {
                _drawFences.Add(device.ResourceFactory.CreateFence(true));
            }
        }

        public static void AddBackgroundUploadTask(Action<GraphicsDevice, CommandList> action)
        {
            lock (BackgroundUploadQueue)
            {
                BackgroundUploadQueue.Enqueue(action);
            }
        }

        public static void AddAsyncTransfer(DeviceBuffer dest, DeviceBuffer source, Action<GraphicsDevice> onFinished)
        {
            _asyncTransfersPendingQueue.Enqueue((dest, source, onFinished));
        }

        public static Fence Frame(CommandList drawCommandList, bool backgroundOnly)
        {
            
            MainCommandList.Begin();

            Queue<Action<GraphicsDevice, CommandList>> work;
            lock (BackgroundUploadQueue)
            {

                work = new Queue<Action<GraphicsDevice, CommandList>>(BackgroundUploadQueue);
                BackgroundUploadQueue.Clear();
            }
            int workitems = work.Count();
            while (work.Count() > 0)
            {
                work.Dequeue().Invoke(Device, MainCommandList);
                //work.Dequeue().Invoke(Device, drawCommandList);
            }

            MainCommandList.End();
            Device.SubmitCommands(MainCommandList);
            

            if (backgroundOnly)
            {
                return null;
            }

            _currentBuffer = _nextBuffer;
            Device.WaitForFence(_drawFences[_prevBuffer]);
            _drawFences[_prevBuffer].Reset();
            return _drawFences[_prevBuffer];
        }
    }
}
