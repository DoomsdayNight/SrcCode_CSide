using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E3F RID: 3647
	[PacketId(ClientPacketId.kNKMPacket_DIVE_SUICIDE_REQ)]
	public sealed class NKMPacket_DIVE_SUICIDE_REQ : ISerializable
	{
		// Token: 0x0600976E RID: 38766 RVA: 0x0032CEEA File Offset: 0x0032B0EA
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.selectDeckIndex);
		}

		// Token: 0x0400898D RID: 35213
		public byte selectDeckIndex;
	}
}
