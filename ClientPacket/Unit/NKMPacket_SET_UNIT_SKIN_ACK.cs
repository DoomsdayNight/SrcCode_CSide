using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CF5 RID: 3317
	[PacketId(ClientPacketId.kNKMPacket_SET_UNIT_SKIN_ACK)]
	public sealed class NKMPacket_SET_UNIT_SKIN_ACK : ISerializable
	{
		// Token: 0x060094E7 RID: 38119 RVA: 0x00329601 File Offset: 0x00327801
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitUID);
			stream.PutOrGet(ref this.skinID);
		}

		// Token: 0x0400866D RID: 34413
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400866E RID: 34414
		public long unitUID;

		// Token: 0x0400866F RID: 34415
		public int skinID;
	}
}
