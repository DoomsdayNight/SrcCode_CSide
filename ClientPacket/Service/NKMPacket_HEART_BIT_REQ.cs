using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D3E RID: 3390
	[PacketId(ClientPacketId.kNKMPacket_HEART_BIT_REQ)]
	public sealed class NKMPacket_HEART_BIT_REQ : ISerializable
	{
		// Token: 0x06009579 RID: 38265 RVA: 0x0032A40E File Offset: 0x0032860E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.time);
		}

		// Token: 0x04008736 RID: 34614
		public long time;
	}
}
