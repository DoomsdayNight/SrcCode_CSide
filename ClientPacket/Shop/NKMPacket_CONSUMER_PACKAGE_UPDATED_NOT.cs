using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D38 RID: 3384
	[PacketId(ClientPacketId.kNKMPacket_CONSUMER_PACKAGE_UPDATED_NOT)]
	public sealed class NKMPacket_CONSUMER_PACKAGE_UPDATED_NOT : ISerializable
	{
		// Token: 0x0600956D RID: 38253 RVA: 0x0032A2CB File Offset: 0x003284CB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMConsumerPackageData>(ref this.list);
		}

		// Token: 0x04008724 RID: 34596
		public List<NKMConsumerPackageData> list = new List<NKMConsumerPackageData>();
	}
}
