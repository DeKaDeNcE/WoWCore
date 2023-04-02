using Framework.Constants;

namespace Game.Networking.Packets
{
    public class RequestMythicPlusSeasonData : ClientPacket
    {
        public RequestMythicPlusSeasonData(WorldPacket packet) : base(packet) { }

        public override void Read() { }
    }

    public class MythicPlusSeasonData : ServerPacket
    {
        public bool SeasonActive;

        public MythicPlusSeasonData() : base(ServerOpcodes.MythicPlusSeasonData) { }

        public override void Write()
        {
            _worldPacket.WriteBit(SeasonActive);
        }
    }
}