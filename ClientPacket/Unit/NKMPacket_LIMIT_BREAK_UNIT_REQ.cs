using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CE8 RID: 3304
	[PacketId(ClientPacketId.kNKMPacket_LIMIT_BREAK_UNIT_REQ)]
	public sealed class NKMPacket_LIMIT_BREAK_UNIT_REQ : ISerializable
	{
		// Token: 0x060094CD RID: 38093 RVA: 0x003293B3 File Offset: 0x003275B3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUID);
		}

		// Token: 0x0400864E RID: 34382
		public long unitUID;
	}
}
