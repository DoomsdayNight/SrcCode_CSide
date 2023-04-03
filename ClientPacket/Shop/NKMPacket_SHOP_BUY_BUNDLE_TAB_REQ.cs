using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D30 RID: 3376
	[PacketId(ClientPacketId.kNKMPacket_SHOP_BUY_BUNDLE_TAB_REQ)]
	public sealed class NKMPacket_SHOP_BUY_BUNDLE_TAB_REQ : ISerializable
	{
		// Token: 0x0600955D RID: 38237 RVA: 0x0032A0C7 File Offset: 0x003282C7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.tabType);
			stream.PutOrGet(ref this.subIndex);
		}

		// Token: 0x04008707 RID: 34567
		public string tabType;

		// Token: 0x04008708 RID: 34568
		public int subIndex;
	}
}
