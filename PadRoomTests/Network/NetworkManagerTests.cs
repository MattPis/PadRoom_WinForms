using Microsoft.VisualStudio.TestTools.UnitTesting;
using PadRoom.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadRoom.Network.Tests
{
    [TestClass()]
    public class NetworkManagerTests
    {
        [TestMethod()]
        public void ConnectionStausTest()
        {
            ConnectionStatusDto dto = new ConnectionStatusDto(
                client: true,
                sender: true,
                reciever: true);

            Assert.Equals(dto.networkClient, true);
            Assert.Equals(dto.lrReciever, true);
            Assert.Equals(dto.lrSender, true);

        }
    }
}