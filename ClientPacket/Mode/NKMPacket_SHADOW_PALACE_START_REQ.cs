using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E47 RID: 3655
	[PacketId(ClientPacketId.kNKMPacket_SHADOW_PALACE_START_REQ)]
	public sealed class NKMPacket_SHADOW_PALACE_START_REQ : ISerializable
	{
		// Token: 0x0600977E RID: 38782 RVA: 0x0032D08C File Offset: 0x0032B28C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.palaceId);
		}

		// Token: 0x040089A4 RID: 35236
		public int palaceId;
	}
}
