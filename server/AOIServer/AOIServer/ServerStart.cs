using System;
using System.Threading;
using System.Threading.Tasks;
using PEUtils;

namespace AOIServer
{
    class ServerStart
    {
        static void Main(string[] args)
        {
            PELog.InitSettings();
            int monsterCount = 0;

            Task.Run(() => {
                ServerRoot.Instane.Init();
                while (true)
                {
                    for (int i = 0; i < monsterCount; i++)
                    {
                        ServerRoot.Instane.CreateServerEntity();
                    }
                    monsterCount = 0;
                    ServerRoot.Instane.Tick();
                    Thread.Sleep(10);
                }
            });

            while (true)
            {
                string ipt = Console.ReadLine();
                if (int.TryParse(ipt, out int val))
                {
                    monsterCount = val;
                }
                else
                {
                    PELog.Warn("输入不合法");
                }
            }
        }
    }
}
