using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CD6 RID: 3286
	[PacketId(ClientPacketId.kNKMPacket_REFRESH_COMPANY_BUFF_ACK)]
	public sealed class NKMPacket_REFRESH_COMPANY_BUFF_ACK : ISerializable
	{
		// Token: 0x060094A9 RID: 38057 RVA: 0x003290BF File Offset: 0x003272BF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMCompanyBuffData>(ref this.companyBuffDataList);
		}

		// Token: 0x04008629 RID: 34345
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400862A RID: 34346
		public List<NKMCompanyBuffData> companyBuffDataList = new List<NKMCompanyBuffData>();
	}
}
