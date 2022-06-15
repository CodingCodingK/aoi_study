using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using AOICellProtocol;
using PENet;

namespace AOIServer
{
    public class ServerRoot
    {
        private static ServerRoot instance;
        public static ServerRoot Instane
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServerRoot();
                }
                return instance;
            }
        }

        AsyncNet<ServerSession, Pkg> server = new AsyncNet<ServerSession, Pkg>();
        ConcurrentQueue<NetPack> packQue = new ConcurrentQueue<NetPack>();
        BattleStage stage = new BattleStage();
        Random rd = new Random();

        public void Init()
        {
            server.StartAsServer("127.0.0.1", 17666);
            stage.InitStage(101);
        }

        public void Tick()
        {
            while (!packQue.IsEmpty)
            {
                if (packQue.TryDequeue(out NetPack pack))
                {
                    switch (pack.pkg.cmd)
                    {
                        case Cmd.ReqLogin:
                            LoginStage(pack);
                            break;
                        case Cmd.SndMovePos:
                            stage.MoveEntity(pack.pkg.sndMovePos);
                            break;
                        case Cmd.SndExit:
                            stage.Exit(pack.pkg.sndExit.entityID);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    this.Error($"Dequeue packQue fail.");
                }
            }

            stage.TickStage();
        }
        public void UnInit()
        {
            stage.UnInitStage();
        }
        public void AddMsgPack(NetPack pack)
        {
            packQue.Enqueue(pack);
        }
        public void CreateServerEntity()
        {
            float rdx = rd.Next(-CommonConfigs.borderX, CommonConfigs.borderX);
            float rdz = rd.Next(-CommonConfigs.borderZ, CommonConfigs.borderZ);

            BattleEntity entity = new BattleEntity
            {
                entityID = GetServerUniqueEntityID(),
                targetPos = new Vector3(rdx, 0, rdz),
                playerStateEnum = PlayerStateEnum.None,
                driverEnum = EntityDriverEnum.Server
            };

            stage.EnterStage(entity);
        }

        void LoginStage(NetPack pack)
        {
            BattleEntity entity = new BattleEntity
            {
                entityID = GetClientUniqueEntityID(),
                session = pack.session,
                targetPos = new Vector3(10, 0, 10),
                playerStateEnum = PlayerStateEnum.None,
                driverEnum = EntityDriverEnum.Client
            };

            stage.EnterStage(entity);

            entity.SendMsg(new Pkg
            {
                cmd = Cmd.RspLogin,
                rspLogin = new RspLogin
                {
                    entityID = entity.entityID
                }
            });
        }
        uint uid = 1000;
        uint GetClientUniqueEntityID()
        {
            return ++uid;
        }
        uint sid = 2000;
        uint GetServerUniqueEntityID()
        {
            return ++sid;
        }
    }

}
