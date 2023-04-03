using System;
using System.Collections.Generic;
using ClientPacket.Community;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000CA7 RID: 3239
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_FRIEND_LIST_ACK)]
	public sealed class NKMPacket_WARFARE_FRIEND_LIST_ACK : ISerializable
	{
		// Token: 0x0600944B RID: 37963 RVA: 0x00328759 File Offset: 0x00326959
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<WarfareSupporterListData>(ref this.friends);
			stream.PutOrGet<WarfareSupporterListData>(ref this.guests);
		}

		// Token: 0x0400859B RID: 34203
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400859C RID: 34204
		public List<WarfareSupporterListData> friends = new List<WarfareSupporterListData>();

		// Token: 0x0400859D RID: 34205
		public List<WarfareSupporterListData> guests = new List<WarfareSupporterListData>();
	}
}
