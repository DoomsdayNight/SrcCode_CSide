using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DB0 RID: 3504
	[PacketId(ClientPacketId.kNKMPacket_DRAFT_PVP_PICK_OPERATOR_REQ)]
	public sealed class NKMPacket_DRAFT_PVP_PICK_OPERATOR_REQ : ISerializable
	{
		// Token: 0x06009659 RID: 38489 RVA: 0x0032B7BB File Offset: 0x003299BB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.operatorUid);
		}

		// Token: 0x04008859 RID: 34905
		public long operatorUid;
	}
}
