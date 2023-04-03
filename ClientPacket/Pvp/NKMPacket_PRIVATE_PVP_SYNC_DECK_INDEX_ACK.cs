using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DC8 RID: 3528
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_SYNC_DECK_INDEX_ACK)]
	public sealed class NKMPacket_PRIVATE_PVP_SYNC_DECK_INDEX_ACK : ISerializable
	{
		// Token: 0x06009689 RID: 38537 RVA: 0x0032BA6A File Offset: 0x00329C6A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008876 RID: 34934
		public NKM_ERROR_CODE errorCode;
	}
}
