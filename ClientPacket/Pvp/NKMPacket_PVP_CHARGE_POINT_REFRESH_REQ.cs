using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D79 RID: 3449
	[PacketId(ClientPacketId.kNKMPacket_PVP_CHARGE_POINT_REFRESH_REQ)]
	public sealed class NKMPacket_PVP_CHARGE_POINT_REFRESH_REQ : ISerializable
	{
		// Token: 0x060095ED RID: 38381 RVA: 0x0032AFD7 File Offset: 0x003291D7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.itemId);
		}

		// Token: 0x040087E7 RID: 34791
		public int itemId;
	}
}
