using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CF6 RID: 3318
	[PacketId(ClientPacketId.kNKMPacket_CONTRACT_PERMANENTLY_REQ)]
	public sealed class NKMPacket_CONTRACT_PERMANENTLY_REQ : ISerializable
	{
		// Token: 0x060094E9 RID: 38121 RVA: 0x0032962F File Offset: 0x0032782F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUID);
		}

		// Token: 0x04008670 RID: 34416
		public long unitUID;
	}
}
