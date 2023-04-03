using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E3B RID: 3643
	[PacketId(ClientPacketId.kNKMPacket_DIVE_AUTO_ACK)]
	public sealed class NKMPacket_DIVE_AUTO_ACK : ISerializable
	{
		// Token: 0x06009766 RID: 38758 RVA: 0x0032CE7A File Offset: 0x0032B07A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.isAuto);
		}

		// Token: 0x04008987 RID: 35207
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008988 RID: 35208
		public bool isAuto;
	}
}
