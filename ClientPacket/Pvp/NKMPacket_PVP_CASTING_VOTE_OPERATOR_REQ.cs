using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DCB RID: 3531
	[PacketId(ClientPacketId.kNKMPacket_PVP_CASTING_VOTE_OPERATOR_REQ)]
	public sealed class NKMPacket_PVP_CASTING_VOTE_OPERATOR_REQ : ISerializable
	{
		// Token: 0x0600968F RID: 38543 RVA: 0x0032BAC3 File Offset: 0x00329CC3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.operatorIdList);
		}

		// Token: 0x0400887A RID: 34938
		public List<int> operatorIdList = new List<int>();
	}
}
