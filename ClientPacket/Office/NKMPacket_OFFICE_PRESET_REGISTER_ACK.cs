using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E1B RID: 3611
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_PRESET_REGISTER_ACK)]
	public sealed class NKMPacket_OFFICE_PRESET_REGISTER_ACK : ISerializable
	{
		// Token: 0x0600972A RID: 38698 RVA: 0x0032C9B5 File Offset: 0x0032ABB5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMOfficePreset>(ref this.preset);
		}

		// Token: 0x04008941 RID: 35137
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008942 RID: 35138
		public NKMOfficePreset preset = new NKMOfficePreset();
	}
}
