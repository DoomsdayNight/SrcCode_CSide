using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F58 RID: 3928
	[PacketId(ClientPacketId.kNKMPacket_GAME_TACTICAL_COMMAND_ACK)]
	public sealed class NKMPacket_GAME_TACTICAL_COMMAND_ACK : ISerializable
	{
		// Token: 0x06009990 RID: 39312 RVA: 0x003302B8 File Offset: 0x0032E4B8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMTacticalCommandData>(ref this.cTacticalCommandData);
		}

		// Token: 0x04008C94 RID: 35988
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C95 RID: 35989
		public NKMTacticalCommandData cTacticalCommandData;
	}
}
