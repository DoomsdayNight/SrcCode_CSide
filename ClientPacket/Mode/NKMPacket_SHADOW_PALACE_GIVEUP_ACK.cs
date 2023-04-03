using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E4A RID: 3658
	[PacketId(ClientPacketId.kNKMPacket_SHADOW_PALACE_GIVEUP_ACK)]
	public sealed class NKMPacket_SHADOW_PALACE_GIVEUP_ACK : ISerializable
	{
		// Token: 0x06009784 RID: 38788 RVA: 0x0032D104 File Offset: 0x0032B304
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.palaceId);
		}

		// Token: 0x040089AA RID: 35242
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040089AB RID: 35243
		public int palaceId;
	}
}
