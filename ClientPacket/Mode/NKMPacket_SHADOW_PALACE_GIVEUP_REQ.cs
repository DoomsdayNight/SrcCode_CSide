using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E49 RID: 3657
	[PacketId(ClientPacketId.kNKMPacket_SHADOW_PALACE_GIVEUP_REQ)]
	public sealed class NKMPacket_SHADOW_PALACE_GIVEUP_REQ : ISerializable
	{
		// Token: 0x06009782 RID: 38786 RVA: 0x0032D0EE File Offset: 0x0032B2EE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.palaceId);
		}

		// Token: 0x040089A9 RID: 35241
		public int palaceId;
	}
}
