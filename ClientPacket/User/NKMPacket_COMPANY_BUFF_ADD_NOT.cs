using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CDB RID: 3291
	[PacketId(ClientPacketId.kNKMPacket_COMPANY_BUFF_ADD_NOT)]
	public sealed class NKMPacket_COMPANY_BUFF_ADD_NOT : ISerializable
	{
		// Token: 0x060094B3 RID: 38067 RVA: 0x00329188 File Offset: 0x00327388
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMCompanyBuffData>(ref this.companyBuffData);
		}

		// Token: 0x04008631 RID: 34353
		public NKMCompanyBuffData companyBuffData;
	}
}
