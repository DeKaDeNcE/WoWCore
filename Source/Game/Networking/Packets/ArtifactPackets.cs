// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using Framework.Constants;
using Game.Entities;

namespace Game.Networking.Packets
{
    public class ArtifactAddPower : ClientPacket
    {
        public ObjectGuid ArtifactGUID;
        public ObjectGuid ForgeGUID;
        public Array<ArtifactPowerChoice> PowerChoices = new(1);

        public struct ArtifactPowerChoice
        {
            public uint ArtifactPowerID;
            public byte Rank;
        }

        public ArtifactAddPower(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            ArtifactGUID = _worldPacket.ReadPackedGuid();
            ForgeGUID = _worldPacket.ReadPackedGuid();

            var powerCount = _worldPacket.ReadUInt32();
            for (var i = 0; i < powerCount; ++i)
            {
                ArtifactPowerChoice artifactPowerChoice;
                artifactPowerChoice.ArtifactPowerID = _worldPacket.ReadUInt32();
                artifactPowerChoice.Rank = _worldPacket.ReadUInt8();
                PowerChoices[i] = artifactPowerChoice;
            }
        }
    }

    public class ArtifactSetAppearance : ClientPacket
    {
        public ObjectGuid ArtifactGUID;
        public ObjectGuid ForgeGUID;
        public int ArtifactAppearanceID;

        public ArtifactSetAppearance(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            ArtifactGUID = _worldPacket.ReadPackedGuid();
            ForgeGUID = _worldPacket.ReadPackedGuid();
            ArtifactAppearanceID = _worldPacket.ReadInt32();
        }
    }

    public class ConfirmArtifactRespec : ClientPacket
    {
        public ObjectGuid ArtifactGUID;
        public ObjectGuid NpcGUID;

        public ConfirmArtifactRespec(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            ArtifactGUID = _worldPacket.ReadPackedGuid();
            NpcGUID = _worldPacket.ReadPackedGuid();
        }
    }

    public class OpenArtifactForge : ServerPacket
    {
        public ObjectGuid ArtifactGUID;
        public ObjectGuid ForgeGUID;

        public OpenArtifactForge() : base(ServerOpcodes.OpenArtifactForge) { }

        public override void Write()
        {
            _worldPacket.WritePackedGuid(ArtifactGUID);
            _worldPacket.WritePackedGuid(ForgeGUID);
        }
    }

    public class ArtifactRespecPrompt : ServerPacket
    {
        public ObjectGuid ArtifactGUID;
        public ObjectGuid NpcGUID;

        public ArtifactRespecPrompt() : base(ServerOpcodes.ArtifactRespecPrompt) { }

        public override void Write()
        {
            _worldPacket.WritePackedGuid(ArtifactGUID);
            _worldPacket.WritePackedGuid(NpcGUID);
        }
    }

    public class ArtifactXpGain : ServerPacket
    {
        public ObjectGuid ArtifactGUID;
        public ulong Amount;

        public ArtifactXpGain() : base(ServerOpcodes.ArtifactXpGain) { }

        public override void Write()
        {
            _worldPacket.WritePackedGuid(ArtifactGUID);
            _worldPacket.WriteUInt64(Amount);
        }
    }
}