using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D43 RID: 3395
	[PacketId(ClientPacketId.kNKMPacket_SERVER_TIME_ACK)]
	public sealed class NKMPacket_SERVER_TIME_ACK : ISerializable
	{
		// Token: 0x06009583 RID: 38275 RVA: 0x0032A458 File Offset: 0x00328658
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.utcServerTimeTicks);
		}

		// Token: 0x04008738 RID: 34616
		public long utcServerTimeTicks;
	}
}
