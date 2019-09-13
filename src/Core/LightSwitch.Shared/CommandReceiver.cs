using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using LightSwitch.Core.Commands;

namespace LightSwitch.Core
{
    public class Constants
    {
        public const int DefaultBroadcastPort = 20100;
        public const int DefaultLoopbackPort = 20101;
    }

    public class CommandReceiver
    {
        public Action<ILightSwitchCommand> OnCommand { get; set; }

        public CommandReceiver(
            int broadcastPort = Constants.DefaultBroadcastPort,
            int loopbackPort = Constants.DefaultLoopbackPort)
        {
            // listen on broadcast port
            Task.Factory.StartNew(
                async () => await StartListening(IPAddress.Any, broadcastPort), 
                TaskCreationOptions.LongRunning);

            // listen on loopback port
            Task.Factory.StartNew(
                async () => await StartListening(IPAddress.Loopback, loopbackPort),
                TaskCreationOptions.LongRunning);
        }

        public async Task StartListening(IPAddress address, int port)
        {
            try
            {
                using (var listener = GetUdpClient())
                {
                    listener.Client.Bind(new IPEndPoint(address, port));

                    while (true)
                        OnMessage(await listener.ReceiveAsync());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when attempting to listen for commands: {ex}");
            }
        }

        public void OnMessage(UdpReceiveResult msg)
        {
            var kind = (CommandKind) msg.Buffer[0];

            ILightSwitchCommand command = null;

            switch (kind)
            {
                case CommandKind.SetOverrideMode:
                    command = SetOverrideCommand.FromBuffer(msg.Buffer);
                    break;
            }

            if (command != null)
                OnCommand?.Invoke(command);
        }

        private UdpClient GetUdpClient() =>
            new UdpClient
            {
                EnableBroadcast = true,
                ExclusiveAddressUse = false,
            };
    }
}
