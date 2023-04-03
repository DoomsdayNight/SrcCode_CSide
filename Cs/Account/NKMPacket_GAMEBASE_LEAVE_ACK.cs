using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x020010A2 RID: 4258
	[PacketId(ClientPacketId.kNKMPacket_GAMEBASE_LEAVE_ACK)]
	public sealed class NKMPacket_GAMEBASE_LEAVE_ACK : ISerializable
	{
		// Token: 0x06009C01 RID: 39937 RVA: 0x00334109 File Offset: 0x00332309
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04009037 RID: 36919
		public NKM_ERROR_CODE errorCode;
	}
}
