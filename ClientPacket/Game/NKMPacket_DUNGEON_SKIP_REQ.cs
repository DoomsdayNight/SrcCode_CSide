using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F66 RID: 3942
	[PacketId(ClientPacketId.kNKMPacket_DUNGEON_SKIP_REQ)]
	public sealed class NKMPacket_DUNGEON_SKIP_REQ : ISerializable
	{
		// Token: 0x060099AC RID: 39340 RVA: 0x003304E1 File Offset: 0x0032E6E1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.dungeonId);
			stream.PutOrGet(ref this.skip);
			stream.PutOrGet(ref this.unitUids);
		}

		// Token: 0x04008CB0 RID: 36016
		public int dungeonId;

		// Token: 0x04008CB1 RID: 36017
		public int skip = 1;

		// Token: 0x04008CB2 RID: 36018
		public List<long> unitUids = new List<long>();
	}
}
