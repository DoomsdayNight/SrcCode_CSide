using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DC9 RID: 3529
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_SEARCH_USER_REQ)]
	public sealed class NKMPacket_PRIVATE_PVP_SEARCH_USER_REQ : ISerializable
	{
		// Token: 0x0600968B RID: 38539 RVA: 0x0032BA80 File Offset: 0x00329C80
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.searchKeyword);
		}

		// Token: 0x04008877 RID: 34935
		public string searchKeyword;
	}
}
