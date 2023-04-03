using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E05 RID: 3589
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_STATE_ACK)]
	public sealed class NKMPacket_OFFICE_STATE_ACK : ISerializable
	{
		// Token: 0x060096FE RID: 38654 RVA: 0x0032C5F5 File Offset: 0x0032A7F5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.userUid);
			stream.PutOrGet<NKMOfficeState>(ref this.officeState);
		}

		// Token: 0x04008912 RID: 35090
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008913 RID: 35091
		public long userUid;

		// Token: 0x04008914 RID: 35092
		public NKMOfficeState officeState = new NKMOfficeState();
	}
}
