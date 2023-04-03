using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E10 RID: 3600
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_RANDOM_VISIT_ACK)]
	public sealed class NKMPacket_OFFICE_RANDOM_VISIT_ACK : ISerializable
	{
		// Token: 0x06009714 RID: 38676 RVA: 0x0032C7C3 File Offset: 0x0032A9C3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMOfficeState>(ref this.officeState);
		}

		// Token: 0x04008928 RID: 35112
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008929 RID: 35113
		public NKMOfficeState officeState = new NKMOfficeState();
	}
}
