using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MyNetworkInterface
{
    public class IP:IPParameters
    {
        public delegate void GettingCurrentHostName(object sender, bool status);
        public event GettingCurrentHostName OnGettingCurrentHostName;

        public delegate void GettingIpAddressCurentPC(object sender, bool status);
        public event GettingIpAddressCurentPC OnGettingIpAddressCurentPC;

        public delegate void GettingSubnetMask(object sender, bool status);
        public event GettingSubnetMask OnGettingSubnetMask;

        public delegate void GettingBroadcastAddressCurentNetwork(object sender, bool status);
        public event GettingBroadcastAddressCurentNetwork OnGettingBroadcastAddressCurentNetwork;


        /// <summary>
        /// Set name pc ASYNC
        /// </summary>
        /// <returns></returns>
        /// 
        public async void GetCurrentHostNameASYNC()
        {
            await Task.Run(() => GetCurrentHostName());
            //вызываем делегат

        }

        /// <summary>
        /// Set name pc
        /// </summary>
        /// <returns></returns>
        /// 
        public void GetCurrentHostName()
        {

            SetNameInNetwork(Dns.GetHostName());
            //вызываем делегат
            OnGettingCurrentHostName?.Invoke(this, true);
        }



        /// <summary>
        /// Set IP address curent pc ASYNC
        /// </summary>
        /// <returns></returns>
        public async void GetIpAddressCurentPCASYNC()
        {
            await Task.Run(() => GetIpAddressCurentPC());
        }


        /// <summary>
        /// Set IP address curent pc
        /// </summary>
        /// <returns></returns>
        public void GetIpAddressCurentPC()
        {
            try
            {
                IPAddress ipAddress;
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    ipAddress = endPoint.Address;
                }

                SetIpAddress(ipAddress);

                OnGettingIpAddressCurentPC?.Invoke(this, true);
            }
            catch
            {
                OnGettingIpAddressCurentPC?.Invoke(this, false);
            }
        }






        /// <summary>
        /// Set SubnetMask ASYNC
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public async void GetSubnetMaskASYNC()
        {
            await Task.Run(() => GetSubnetMask(ReturnIpAddress()));
            //вызываем делегат

        }
        /// <summary>
        /// Set SubnetMask
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public void GetSubnetMask(IPAddress address)
        {
            try
            {

                bool status = false;
                foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (!status)
                        foreach (UnicastIPAddressInformation unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)
                        {
                            if (!status)
                                if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                                {
                                    if (address.Equals(unicastIPAddressInformation.Address))
                                    {
                                        SetSubnetMask(unicastIPAddressInformation.IPv4Mask);
                                        OnGettingSubnetMask?.Invoke(this, true);
                                        break;
                                    }
                                }
                        }
                }
            }
            catch { }
            
            //throw new ArgumentException($"Can't find subnetmask for IP address '{address}'");
        }




        /// <summary>
        /// Set Broadcast Address in current network
        /// </summary>
        /// <param name="address"></param>
        /// <param name="subnetMask"></param>
        /// <returns></returns>
        public async void GetBroadcastAddressCurentNetworkASYNC()
        {
            await Task.Run(() => GetBroadcastAddressCurentNetwork(ReturnIpAddress(),ReturnSubnetMask()));
            //вызываем делегат

        }

        /// <summary>
        /// Return Broadcast Address in current network
        /// </summary>
        /// <param name="address"></param>
        /// <param name="subnetMask"></param>
        /// <returns></returns>
        public void GetBroadcastAddressCurentNetwork(IPAddress address, IPAddress subnetMask)
        {

            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] | (subnetMaskBytes[i] ^ 255));
            }
            SetBroadcastAddress(new IPAddress(broadcastAddress));
            OnGettingBroadcastAddressCurentNetwork?.Invoke(this, true);


        }




        private List<IPParameters> PCInNetwork = new List<IPParameters>();

        public void AddingNewPCInList(IPParameters pc)
        {
            PCInNetwork.Add(pc);
        }

        public List<IPParameters> ReturnListPCInNetwork()
        {
            return PCInNetwork;
        }

        public bool FindningInformationAboutPCInList(IPParameters recivedPC)
        {
            foreach(var PC in PCInNetwork)
            {
                if(PC?.ReturnIpAddress()!=null && PC?.ReturnIpAddress()!=recivedPC?.ReturnIpAddress())
                {
                    return true;
                }
            }
            return false;
        }

    }

       


    public class IPParameters
    {
        public IPParameters()
        {
            
        }
        public IPParameters(string nameInNetwork, IPAddress ipAddress)
        {
            _nameInNetwork = nameInNetwork;
            _ipAddress = ipAddress;
        }
        public IPParameters(string nameInNetwork, IPAddress ipAddress, IPAddress broadcastAddress)
        {
            _nameInNetwork = nameInNetwork;
            _ipAddress = ipAddress;
            _broadcastAddress = broadcastAddress;
        }
        public IPParameters(string nameInNetwork, IPAddress ipAddress, IPAddress broadcastAddress, IPAddress subnetMask)
        {
            _nameInNetwork = nameInNetwork;
            _ipAddress = ipAddress;
            _broadcastAddress = broadcastAddress;
            _subnetMask = subnetMask;
        }
        private string _nameInNetwork;
        private IPAddress _ipAddress;
        private IPAddress _broadcastAddress;
        private IPAddress _subnetMask;


        public string ReturnNameInNetwork()
        {
            return _nameInNetwork;
        }
        public IPAddress ReturnIpAddress()
        {
            return _ipAddress;
        }
        public IPAddress ReturnBroadcastAddress()
        {
            return _broadcastAddress;
        }

        public IPAddress ReturnSubnetMask()
        {
            return _subnetMask;
        }



        public void SetNameInNetwork(string value)
        {
            _nameInNetwork = value;
        }
        public void SetIpAddress(IPAddress value)
        {
            _ipAddress = value;
        }
        public void SetBroadcastAddress(IPAddress value)
        {
            _broadcastAddress = value;
        }

        public void SetSubnetMask(IPAddress value)
        {
            _subnetMask = value;
        }


    }
}
