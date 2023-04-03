using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CFE RID: 3326
	[PacketId(ClientPacketId.kNKMPacket_OPERATOR_LOCK_REQ)]
	public sealed class NKMPacket_OPERATOR_LOCK_REQ : ISerializable
	{
		// Token: 0x060094F9 RID: 38137 RVA: 0x003297A8 File Offset: 0x003279A8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUID);
			stream.PutOrGet(ref this.locked);
		}

		// Token: 0x04008686 RID: 34438
		public long unitUID;

		// Token: 0x04008687 RID: 34439
		public bool locked;
	}
}
