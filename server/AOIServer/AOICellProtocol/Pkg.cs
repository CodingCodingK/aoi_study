using System;

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

        SndMovePos,
        SndExit,
    }
}
