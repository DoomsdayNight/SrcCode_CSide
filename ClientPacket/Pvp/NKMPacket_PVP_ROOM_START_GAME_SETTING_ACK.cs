using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DDC RID: 3548
	[PacketId(ClientPacketId.kNKMPacket_PVP_ROOM_START_GAME_SETTING_ACK)]
	public sealed class NKMPacket_PVP_ROOM_START_GAME_SETTING_ACK : ISerializable
	{
		// Token: 0x060096AF RID: 38575 RVA: 0x0032BD00 File Offset: 0x00329F00
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008899 RID: 34969
		public NKM_ERROR_CODE errorCode;
	}
}
