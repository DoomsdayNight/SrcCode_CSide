using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x0200100A RID: 4106
	[PacketId(ClientPacketId.kNKMPacket_EMOTICON_TEXT_CHANGE_ACK)]
	public sealed class NKMPacket_EMOTICON_TEXT_CHANGE_ACK : ISerializable
	{
		// Token: 0x06009AE4 RID: 39652 RVA: 0x00332024 File Offset: 0x00330224
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.presetIndex);
			stream.PutOrGet(ref this.emoticonId);
		}

		// Token: 0x04008E44 RID: 36420
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E45 RID: 36421
		public int presetIndex;

		// Token: 0x04008E46 RID: 36422
		public int emoticonId;
	}
}
