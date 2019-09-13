using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using LightSwitch.Core.Commands;

namespace LightSwitch.Core
{
    public class CommandSender
    {
        public UdpClient Client = new UdpClient { EnableBroadcast = true };

        public int BroadcastPort { get; set; } = Constants.DefaultBroadcastPort;
        public int LoopbackPort { get; set; } = Constants.DefaultLoopbackPort;

        public bool Broadcast { get; set; } = true;

        public async Task SendCommand(ILightSwitchCommand command)
        {
            var bytes = command.ToBuffer();
            var destinations = GetDestinations(Broadcast);

            foreach (var (name, destination) in destinations)
            {
                try
                {
                    Debug.WriteLine($"Sending {command} to {name} / {destination}");

                    await Client.SendAsync(bytes, bytes.Length, destination);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"An error occurred when sending {command} to {name}/{destination}: {ex}");
                }
            }
        }

        public List<(string name, IPEndPoint endpoint)> GetDestinations(bool forBroadcast)
        {
            var ret = new List<(string name, IPEndPoint endpoint)> {("localhost", LoopbackEndpoint)};

            if (forBroadcast)
                ret.AddRange(GetBroadcastDestinations());

            return ret;
        }

        private List<(string name, IPEndPoint address)> GetBroadcastDestinations()
            => NetworkInterface
                    .GetAllNetworkInterfaces()
                    .Select(x => (x.Name, GetBroadcastAddress(x)))
                    .Where(x => x.Item2 != null)
                    .Select(x => (x.Name, new IPEndPoint(x.Item2, BroadcastPort)))
                    .ToList();
        
        private IPEndPoint LoopbackEndpoint
            => new IPEndPoint(IPAddress.Loopback, LoopbackPort);

        /// Adapted from sockets for pcl
        /// Which adapted from http://blogs.msdn.com/b/knom/archive/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks.aspx
        public static IPAddress GetBroadcastAddress(NetworkInterface networkInterface)
        {
            var ip =
                networkInterface
                    .GetIPProperties()
                    .UnicastAddresses
                    .FirstOrDefault(a => a.Address.AddressFamily == AddressFamily.InterNetwork);

            if (ip == null)
                return null;

            var address = ip?.Address;
            var subnetMask = ip?.IPv4Mask;

            var addressBytes = address.GetAddressBytes();
            var subnetBytes = subnetMask.GetAddressBytes();

            var broadcastBytes = addressBytes.Zip(subnetBytes, (a, s) => (byte)(a | (s ^ 255))).ToArray();

            return new IPAddress(broadcastBytes);
        }
    }
}