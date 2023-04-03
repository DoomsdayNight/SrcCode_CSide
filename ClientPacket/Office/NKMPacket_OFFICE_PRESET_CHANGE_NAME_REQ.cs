using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E20 RID: 3616
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_PRESET_CHANGE_NAME_REQ)]
	public sealed class NKMPacket_OFFICE_PRESET_CHANGE_NAME_REQ : ISerializable
	{
		// Token: 0x06009734 RID: 38708 RVA: 0x0032CABA File Offset: 0x0032ACBA
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.presetId);
			stream.PutOrGet(ref this.newPresetName);
		}

		// Token: 0x0400894E RID: 35150
		public int presetId;

		// Token: 0x0400894F RID: 35151
		public string newPresetName;
	}
}
