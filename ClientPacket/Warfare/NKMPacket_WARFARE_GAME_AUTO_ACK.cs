using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000CA2 RID: 3234
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_GAME_AUTO_ACK)]
	public sealed class NKMPacket_WARFARE_GAME_AUTO_ACK : ISerializable
	{
		// Token: 0x06009441 RID: 37953 RVA: 0x00328689 File Offset: 0x00326889
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.isAuto);
			stream.PutOrGet(ref this.isAutoRepair);
		}

		// Token: 0x0400858F RID: 34191
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008590 RID: 34192
		public bool isAuto;

		// Token: 0x04008591 RID: 34193
		public bool isAutoRepair;
	}
}
