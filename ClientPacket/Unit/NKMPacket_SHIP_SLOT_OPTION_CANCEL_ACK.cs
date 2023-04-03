using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D1B RID: 3355
	[PacketId(ClientPacketId.kNKMPacket_SHIP_SLOT_OPTION_CANCEL_ACK)]
	public sealed class NKMPacket_SHIP_SLOT_OPTION_CANCEL_ACK : ISerializable
	{
		// Token: 0x06009533 RID: 38195 RVA: 0x00329CC4 File Offset: 0x00327EC4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x040086CE RID: 34510
		public NKM_ERROR_CODE errorCode;
	}
}
