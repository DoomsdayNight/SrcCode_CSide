using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D4B RID: 3403
	[PacketId(ClientPacketId.kNKMPacket_ACCOUNT_LEAVE_STATE_REQ)]
	public sealed class NKMPacket_ACCOUNT_LEAVE_STATE_REQ : ISerializable
	{
		// Token: 0x06009593 RID: 38291 RVA: 0x0032A508 File Offset: 0x00328708
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.leave);
		}

		// Token: 0x04008740 RID: 34624
		public bool leave;
	}
}
