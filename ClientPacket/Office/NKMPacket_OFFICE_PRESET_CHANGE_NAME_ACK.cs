using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E21 RID: 3617
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_PRESET_CHANGE_NAME_ACK)]
	public sealed class NKMPacket_OFFICE_PRESET_CHANGE_NAME_ACK : ISerializable
	{
		// Token: 0x06009736 RID: 38710 RVA: 0x0032CADC File Offset: 0x0032ACDC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.presetId);
			stream.PutOrGet(ref this.newPresetName);
		}

		// Token: 0x04008950 RID: 35152
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008951 RID: 35153
		public int presetId;

		// Token: 0x04008952 RID: 35154
		public string newPresetName;
	}
}
