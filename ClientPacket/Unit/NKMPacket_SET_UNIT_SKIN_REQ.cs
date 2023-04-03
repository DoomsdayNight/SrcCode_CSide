using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CF4 RID: 3316
	[PacketId(ClientPacketId.kNKMPacket_SET_UNIT_SKIN_REQ)]
	public sealed class NKMPacket_SET_UNIT_SKIN_REQ : ISerializable
	{
		// Token: 0x060094E5 RID: 38117 RVA: 0x003295DF File Offset: 0x003277DF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUID);
			stream.PutOrGet(ref this.skinID);
		}

		// Token: 0x0400866B RID: 34411
		public long unitUID;

		// Token: 0x0400866C RID: 34412
		public int skinID;
	}
}
