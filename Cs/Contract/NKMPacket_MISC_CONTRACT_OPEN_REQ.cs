using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Contract
{
	// Token: 0x02000FC3 RID: 4035
	[PacketId(ClientPacketId.kNKMPacket_MISC_CONTRACT_OPEN_REQ)]
	public sealed class NKMPacket_MISC_CONTRACT_OPEN_REQ : ISerializable
	{
		// Token: 0x06009A56 RID: 39510 RVA: 0x003314B3 File Offset: 0x0032F6B3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.miscItemId);
			stream.PutOrGet(ref this.count);
		}

		// Token: 0x04008DA8 RID: 36264
		public int miscItemId;

		// Token: 0x04008DA9 RID: 36265
		public int count;
	}
}
