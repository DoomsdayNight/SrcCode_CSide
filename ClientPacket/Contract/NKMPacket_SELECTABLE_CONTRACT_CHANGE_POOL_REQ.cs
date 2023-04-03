using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Contract
{
	// Token: 0x02000FBD RID: 4029
	[PacketId(ClientPacketId.kNKMPacket_SELECTABLE_CONTRACT_CHANGE_POOL_REQ)]
	public sealed class NKMPacket_SELECTABLE_CONTRACT_CHANGE_POOL_REQ : ISerializable
	{
		// Token: 0x06009A4A RID: 39498 RVA: 0x003313BC File Offset: 0x0032F5BC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.contractId);
		}

		// Token: 0x04008D9D RID: 36253
		public int contractId;
	}
}
