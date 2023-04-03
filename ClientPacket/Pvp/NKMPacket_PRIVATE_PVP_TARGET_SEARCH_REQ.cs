using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DC3 RID: 3523
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_TARGET_SEARCH_REQ)]
	public sealed class NKMPacket_PRIVATE_PVP_TARGET_SEARCH_REQ : ISerializable
	{
		// Token: 0x0600967F RID: 38527 RVA: 0x0032B9F1 File Offset: 0x00329BF1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.searchKeyword);
		}

		// Token: 0x04008871 RID: 34929
		public string searchKeyword;
	}
}
