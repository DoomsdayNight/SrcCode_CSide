using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D9B RID: 3483
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_READY_ACK)]
	public sealed class NKMPacket_PRIVATE_PVP_READY_ACK : ISerializable
	{
		// Token: 0x0600962F RID: 38447 RVA: 0x0032B5B5 File Offset: 0x003297B5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008843 RID: 34883
		public NKM_ERROR_CODE errorCode;
	}
}
