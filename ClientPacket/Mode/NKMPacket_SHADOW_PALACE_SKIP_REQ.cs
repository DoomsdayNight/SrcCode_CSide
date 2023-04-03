using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E68 RID: 3688
	[PacketId(ClientPacketId.kNKMPacket_SHADOW_PALACE_SKIP_REQ)]
	public sealed class NKMPacket_SHADOW_PALACE_SKIP_REQ : ISerializable
	{
		// Token: 0x060097C0 RID: 38848 RVA: 0x0032D63B File Offset: 0x0032B83B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.palaceId);
			stream.PutOrGet(ref this.skipCount);
		}

		// Token: 0x040089ED RID: 35309
		public int palaceId;

		// Token: 0x040089EE RID: 35310
		public int skipCount;
	}
}
