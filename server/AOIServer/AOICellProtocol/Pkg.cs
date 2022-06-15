using System;
using System.Collections.Generic;
using PENet;

namespace AOICellProtocol
{
    // 存量增删，增量叠加
    public class CommonConfigs
    {
        public const int aoiSize = 50;
        public const float moveSpeed = 40;

        public const int borderX = 500;
        public const int borderZ = 500;

        public const int randomDirInterval = 1;
        public const int randomDirRate = 30;
    }
    public enum Cmd
    {
        ReqLogin,
        RspLogin,

        NtfCell,
        NtfAOIMsg,

        SndMovePos,//请求移动
        SndExit,//请求退出
    }

    [Serializable]
    public class Pkg : AsyncMsg
    {
        public Cmd cmd;

        public ReqLogin reqLogin;
        public RspLogin rspLogin;
        public NtfCell ntfCell;
        public NtfAOIMsg ntfAOIMsg;
        public SndMovePos sndMovePos;
        public SndExit sndExit;
    }
    [Serializable]
    public class ReqLogin
    {
        public string acct;
    }
    [Serializable]
    public class RspLogin
    {
        public uint entityID;
    }
    [Serializable]
    public class NtfAOIMsg
    {
        public int type;
        public List<EnterMsg> enterLst;
        public List<MoveMsg> moveLst;
        public List<ExitMsg> exitLst;
        public override string ToString()
        {
            string content = "";
            if (enterLst != null)
            {
                for (int i = 0; i < enterLst.Count; i++)
                {
                    EnterMsg em = enterLst[i];
                    content += $"Enter:{em.entityID} {em.PosX},{em.PosZ}\n";
                }
            }
            if (moveLst != null)
            {
                for (int i = 0; i < moveLst.Count; i++)
                {
                    MoveMsg mm = moveLst[i];
                    content += $"Move:{mm.entityID} {mm.PosX},{mm.PosZ}\n";
                }
            }
            if (exitLst != null)
            {
                for (int i = 0; i < exitLst.Count; i++)
                {
                    ExitMsg mm = exitLst[i];
                    content += $"Exit:{mm.entityID}\n";
                }
            }
            return content;
        }
    }
    [Serializable]
    public class EnterMsg
    {
        public uint entityID;
        public float PosX;
        public float PosZ;
    }
    [Serializable]
    public class MoveMsg
    {
        public uint entityID;
        public float PosX;
        public float PosZ;
    }
    [Serializable]
    public class ExitMsg
    {
        public uint entityID;
    }
    [Serializable]
    public class NtfCell
    {
        public int xIndex;
        public int zIndex;
    }
    [Serializable]
    public class SndMovePos
    {
        public uint entityID;
        public float PosX;
        public float PosZ;
    }
    [Serializable]
    public class SndExit
    {
        public uint entityID;
    }
}
