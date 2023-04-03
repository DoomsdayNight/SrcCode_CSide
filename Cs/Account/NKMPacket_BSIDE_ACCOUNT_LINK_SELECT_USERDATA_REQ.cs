using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001091 RID: 4241
	[PacketId(ClientPacketId.kNKMPacket_BSIDE_ACCOUNT_LINK_SELECT_USERDATA_REQ)]
	public sealed class NKMPacket_BSIDE_ACCOUNT_LINK_SELECT_USERDATA_REQ : ISerializable
	{
		// Token: 0x06009BDF RID: 39903 RVA: 0x00333F7D File Offset: 0x0033217D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.selectedUserUid);
		}

		// Token: 0x04009026 RID: 36902
		public long selectedUserUid;
	}
}
