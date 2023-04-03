using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E44 RID: 3652
	public sealed class NKMPalaceData : ISerializable
	{
		// Token: 0x06009778 RID: 38776 RVA: 0x0032CF9F File Offset: 0x0032B19F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.palaceId);
			stream.PutOrGet(ref this.currentDungeonId);
			stream.PutOrGet<NKMPalaceDungeonData>(ref this.dungeonDataList);
		}

		// Token: 0x04008997 RID: 35223
		public int palaceId;

		// Token: 0x04008998 RID: 35224
		public int currentDungeonId;

		// Token: 0x04008999 RID: 35225
		public List<NKMPalaceDungeonData> dungeonDataList = new List<NKMPalaceDungeonData>();
	}
}
