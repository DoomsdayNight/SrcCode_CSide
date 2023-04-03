using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F34 RID: 3892
	[PacketId(ClientPacketId.kNKMPacket_GAME_LOAD_COMPLETE_ACK)]
	public sealed class NKMPacket_GAME_LOAD_COMPLETE_ACK : ISerializable
	{
		// Token: 0x06009948 RID: 39240 RVA: 0x0032FBEB File Offset: 0x0032DDEB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.isIntrude);
			stream.PutOrGet<NKMGameRuntimeData>(ref this.gameRuntimeData);
			stream.PutOrGet(ref this.rewardMultiply);
		}

		// Token: 0x04008C2E RID: 35886
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C2F RID: 35887
		public bool isIntrude;

		// Token: 0x04008C30 RID: 35888
		public NKMGameRuntimeData gameRuntimeData;

		// Token: 0x04008C31 RID: 35889
		public int rewardMultiply;
	}
}
