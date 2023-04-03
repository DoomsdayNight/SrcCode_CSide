using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001008 RID: 4104
	[PacketId(ClientPacketId.kNKMPacket_EMOTICON_ANI_CHANGE_ACK)]
	public sealed class NKMPacket_EMOTICON_ANI_CHANGE_ACK : ISerializable
	{
		// Token: 0x06009AE0 RID: 39648 RVA: 0x00331FD4 File Offset: 0x003301D4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.presetIndex);
			stream.PutOrGet(ref this.emoticonId);
		}

		// Token: 0x04008E3F RID: 36415
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E40 RID: 36416
		public int presetIndex;

		// Token: 0x04008E41 RID: 36417
		public int emoticonId;
	}
}
