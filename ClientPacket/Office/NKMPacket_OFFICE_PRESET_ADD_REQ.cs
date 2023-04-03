using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E1E RID: 3614
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_PRESET_ADD_REQ)]
	public sealed class NKMPacket_OFFICE_PRESET_ADD_REQ : ISerializable
	{
		// Token: 0x06009730 RID: 38704 RVA: 0x0032CA6B File Offset: 0x0032AC6B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.addPresetCount);
		}

		// Token: 0x0400894A RID: 35146
		public int addPresetCount;
	}
}
