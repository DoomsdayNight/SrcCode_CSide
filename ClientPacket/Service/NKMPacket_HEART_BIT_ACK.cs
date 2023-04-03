using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D3F RID: 3391
	[PacketId(ClientPacketId.kNKMPacket_HEART_BIT_ACK)]
	public sealed class NKMPacket_HEART_BIT_ACK : ISerializable
	{
		// Token: 0x0600957B RID: 38267 RVA: 0x0032A424 File Offset: 0x00328624
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.time);
		}

		// Token: 0x04008737 RID: 34615
		public long time;
	}
}
