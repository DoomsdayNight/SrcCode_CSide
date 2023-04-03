using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E23 RID: 3619
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_PRESET_RESET_ACK)]
	public sealed class NKMPacket_OFFICE_PRESET_RESET_ACK : ISerializable
	{
		// Token: 0x0600973A RID: 38714 RVA: 0x0032CB20 File Offset: 0x0032AD20
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.presetId);
		}

		// Token: 0x04008954 RID: 35156
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008955 RID: 35157
		public int presetId;
	}
}
