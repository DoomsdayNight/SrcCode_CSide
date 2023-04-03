using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EB1 RID: 3761
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PRESET_APPLY_REQ)]
	public sealed class NKMPacket_EQUIP_PRESET_APPLY_REQ : ISerializable
	{
		// Token: 0x0600984E RID: 38990 RVA: 0x0032E3D7 File Offset: 0x0032C5D7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.presetIndex);
			stream.PutOrGet(ref this.applyUnitUid);
		}

		// Token: 0x04008AAF RID: 35503
		public int presetIndex;

		// Token: 0x04008AB0 RID: 35504
		public long applyUnitUid;
	}
}
