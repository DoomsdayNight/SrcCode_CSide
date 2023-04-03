using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DAC RID: 3500
	[PacketId(ClientPacketId.kNKMPacket_DRAFT_PVP_OPPONENT_BAN_REQ)]
	public sealed class NKMPacket_DRAFT_PVP_OPPONENT_BAN_REQ : ISerializable
	{
		// Token: 0x06009651 RID: 38481 RVA: 0x0032B763 File Offset: 0x00329963
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitIndex);
		}

		// Token: 0x04008855 RID: 34901
		public int unitIndex;
	}
}
