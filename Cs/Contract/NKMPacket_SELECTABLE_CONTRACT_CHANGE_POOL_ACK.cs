using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Contract
{
	// Token: 0x02000FBE RID: 4030
	[PacketId(ClientPacketId.kNKMPacket_SELECTABLE_CONTRACT_CHANGE_POOL_ACK)]
	public sealed class NKMPacket_SELECTABLE_CONTRACT_CHANGE_POOL_ACK : ISerializable
	{
		// Token: 0x06009A4C RID: 39500 RVA: 0x003313D2 File Offset: 0x0032F5D2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMSelectableContractState>(ref this.selectableContractState);
		}

		// Token: 0x04008D9E RID: 36254
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D9F RID: 36255
		public NKMSelectableContractState selectableContractState = new NKMSelectableContractState();
	}
}
