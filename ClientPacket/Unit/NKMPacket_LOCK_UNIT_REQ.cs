using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CE4 RID: 3300
	[PacketId(ClientPacketId.kNKMPacket_LOCK_UNIT_REQ)]
	public sealed class NKMPacket_LOCK_UNIT_REQ : ISerializable
	{
		// Token: 0x060094C5 RID: 38085 RVA: 0x003292FE File Offset: 0x003274FE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUID);
			stream.PutOrGet(ref this.isLock);
		}

		// Token: 0x04008645 RID: 34373
		public long unitUID;

		// Token: 0x04008646 RID: 34374
		public bool isLock;
	}
}
