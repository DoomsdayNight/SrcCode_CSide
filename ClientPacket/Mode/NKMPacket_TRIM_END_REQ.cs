using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E5C RID: 3676
	[PacketId(ClientPacketId.kNKMPacket_TRIM_END_REQ)]
	public sealed class NKMPacket_TRIM_END_REQ : ISerializable
	{
		// Token: 0x060097A8 RID: 38824 RVA: 0x0032D41E File Offset: 0x0032B61E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.trimId);
		}

		// Token: 0x040089D4 RID: 35284
		public int trimId;
	}
}
