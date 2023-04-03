using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E24 RID: 3620
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_PRESET_APPLY_THEMA_REQ)]
	public sealed class NKMPacket_OFFICE_PRESET_APPLY_THEMA_REQ : ISerializable
	{
		// Token: 0x0600973C RID: 38716 RVA: 0x0032CB42 File Offset: 0x0032AD42
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.roomId);
			stream.PutOrGet(ref this.themaIndex);
		}

		// Token: 0x04008956 RID: 35158
		public int roomId;

		// Token: 0x04008957 RID: 35159
		public int themaIndex;
	}
}
