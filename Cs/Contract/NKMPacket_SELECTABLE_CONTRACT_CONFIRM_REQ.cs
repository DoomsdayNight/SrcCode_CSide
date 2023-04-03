using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Contract
{
	// Token: 0x02000FBF RID: 4031
	[PacketId(ClientPacketId.kNKMPacket_SELECTABLE_CONTRACT_CONFIRM_REQ)]
	public sealed class NKMPacket_SELECTABLE_CONTRACT_CONFIRM_REQ : ISerializable
	{
		// Token: 0x06009A4E RID: 39502 RVA: 0x003313FF File Offset: 0x0032F5FF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.contractId);
		}

		// Token: 0x04008DA0 RID: 36256
		public int contractId;
	}
}
