using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CEA RID: 3306
	[PacketId(ClientPacketId.kNKMPacket_UNIT_SKILL_UPGRADE_REQ)]
	public sealed class NKMPacket_UNIT_SKILL_UPGRADE_REQ : ISerializable
	{
		// Token: 0x060094D1 RID: 38097 RVA: 0x00329402 File Offset: 0x00327602
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUID);
			stream.PutOrGet(ref this.skillID);
		}

		// Token: 0x04008652 RID: 34386
		public long unitUID;

		// Token: 0x04008653 RID: 34387
		public int skillID;
	}
}
