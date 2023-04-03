using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CD8 RID: 3288
	[PacketId(ClientPacketId.kNKMPacket_BACKGROUND_CHANGE_REQ)]
	public sealed class NKMPacket_BACKGROUND_CHANGE_REQ : ISerializable
	{
		// Token: 0x060094AD RID: 38061 RVA: 0x00329119 File Offset: 0x00327319
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMBackgroundInfo>(ref this.backgroundInfo);
		}

		// Token: 0x0400862D RID: 34349
		public NKMBackgroundInfo backgroundInfo = new NKMBackgroundInfo();
	}
}
