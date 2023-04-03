using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D91 RID: 3473
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_CANCEL_REQ)]
	public sealed class NKMPacket_PRIVATE_PVP_CANCEL_REQ : ISerializable
	{
		// Token: 0x0600961D RID: 38429 RVA: 0x0032B410 File Offset: 0x00329610
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.targetUserUid);
		}

		// Token: 0x04008823 RID: 34851
		public long targetUserUid;
	}
}
