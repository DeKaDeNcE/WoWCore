using Framework.Constants;
using Game.Networking;
using Game.Networking.Packets;

namespace Game
{
    public partial class WorldSession
    {
        [WorldPacketHandler(ClientOpcodes.RequestMythicPlusSeasonData, Processing = PacketProcessing.Inplace)]
        void RequestMythicPlusSeasonData(RequestMythicPlusSeasonData packet)
        {
            SendPacket(new MythicPlusSeasonData());
        }
    }
}