using System;
using System.Collections.Generic;
using System.Text;
using AOICellProtocol;
using PENet;

namespace AOIServer
{
    public class ServerSession : AsyncSession<Pkg>
    {
        protected override void OnConnected(bool result)
        {
            this.LogGreen("New Client Online:{0}.", result);
        }

        protected override void OnDisConnected()
        {
            this.LogGreen("Client Offline.");
        }

        protected override void OnReceiveMsg(Pkg pkg)
        {
            ServerRoot.Instane.AddMsgPack(new NetPack(this, pkg));
        }
    }

    public class NetPack
    {
        public ServerSession session;
        public Pkg pkg;

        public NetPack(ServerSession session, Pkg pkg)
        {
            this.session = session;
            this.pkg = pkg;
        }
    }
}
