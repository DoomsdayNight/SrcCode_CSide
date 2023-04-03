using System;
using Cs.Protocol;
using NKM.Templet;

namespace ClientPacket.Warfare
{
	// Token: 0x02000C90 RID: 3216
	public sealed class WarfareTileData : ISerializable
	{
		// Token: 0x0600941D RID: 37917 RVA: 0x003280DB File Offset: 0x003262DB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.index);
			stream.PutOrGetEnum<NKM_WARFARE_MAP_TILE_TYPE>(ref this.tileType);
			stream.PutOrGet(ref this.battleConditionId);
		}

		// Token: 0x0400853A RID: 34106
		public short index;

		// Token: 0x0400853B RID: 34107
		public NKM_WARFARE_MAP_TILE_TYPE tileType;

		// Token: 0x0400853C RID: 34108
		public int battleConditionId;
	}
}
