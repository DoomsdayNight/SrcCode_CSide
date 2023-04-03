using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F2E RID: 3886
	[PacketId(ClientPacketId.kNKMPacket_RAID_GAME_LOAD_REQ)]
	public sealed class NKMPacket_RAID_GAME_LOAD_REQ : ISerializable
	{
		// Token: 0x0600993C RID: 39228 RVA: 0x0032FB09 File Offset: 0x0032DD09
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.selectDeckIndex);
			stream.PutOrGet(ref this.raidUID);
			stream.PutOrGet(ref this.buffList);
			stream.PutOrGet(ref this.isTryAssist);
		}

		// Token: 0x04008C22 RID: 35874
		public byte selectDeckIndex;

		// Token: 0x04008C23 RID: 35875
		public long raidUID;

		// Token: 0x04008C24 RID: 35876
		public List<int> buffList = new List<int>();

		// Token: 0x04008C25 RID: 35877
		public bool isTryAssist;
	}
}
