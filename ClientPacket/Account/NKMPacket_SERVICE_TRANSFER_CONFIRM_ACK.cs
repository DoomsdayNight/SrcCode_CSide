using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x020010A0 RID: 4256
	[PacketId(ClientPacketId.kNKMPacket_SERVICE_TRANSFER_CONFIRM_ACK)]
	public sealed class NKMPacket_SERVICE_TRANSFER_CONFIRM_ACK : ISerializable
	{
		// Token: 0x06009BFD RID: 39933 RVA: 0x003340C5 File Offset: 0x003322C5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04009033 RID: 36915
		public NKM_ERROR_CODE errorCode;
	}
}
