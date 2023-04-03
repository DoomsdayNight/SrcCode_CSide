using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E39 RID: 3641
	[PacketId(ClientPacketId.kNKMPacket_DIVE_GIVE_UP_ACK)]
	public sealed class NKMPacket_DIVE_GIVE_UP_ACK : ISerializable
	{
		// Token: 0x06009762 RID: 38754 RVA: 0x0032CE4E File Offset: 0x0032B04E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008985 RID: 35205
		public NKM_ERROR_CODE errorCode;
	}
}
