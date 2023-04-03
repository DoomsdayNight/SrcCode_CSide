using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CBE RID: 3262
	[PacketId(ClientPacketId.kNKMPacket_POST_ARRIVE_NOT)]
	public sealed class NKMPacket_POST_ARRIVE_NOT : ISerializable
	{
		// Token: 0x06009479 RID: 38009 RVA: 0x00328C5F File Offset: 0x00326E5F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.count);
		}

		// Token: 0x040085E9 RID: 34281
		public int count;
	}
}
