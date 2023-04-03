using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CEE RID: 3310
	[PacketId(ClientPacketId.kNKMPacket_SHIP_LEVELUP_REQ)]
	public sealed class NKMPacket_SHIP_LEVELUP_REQ : ISerializable
	{
		// Token: 0x060094D9 RID: 38105 RVA: 0x003294C4 File Offset: 0x003276C4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.shipUID);
			stream.PutOrGet(ref this.nextLevel);
		}

		// Token: 0x0400865D RID: 34397
		public long shipUID;

		// Token: 0x0400865E RID: 34398
		public int nextLevel;
	}
}
