using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E4F RID: 3663
	[PacketId(ClientPacketId.kNKMPacket_PHASE_START_ACK)]
	public sealed class NKMPacket_PHASE_START_ACK : ISerializable
	{
		// Token: 0x0600978E RID: 38798 RVA: 0x0032D1DD File Offset: 0x0032B3DD
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<PhaseModeState>(ref this.state);
		}

		// Token: 0x040089B7 RID: 35255
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040089B8 RID: 35256
		public PhaseModeState state = new PhaseModeState();
	}
}
