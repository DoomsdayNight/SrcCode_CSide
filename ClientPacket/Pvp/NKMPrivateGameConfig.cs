using System;
using Cs.Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D8D RID: 3469
	public sealed class NKMPrivateGameConfig : ISerializable
	{
		// Token: 0x06009615 RID: 38421 RVA: 0x0032B34F File Offset: 0x0032954F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.applyEquipStat);
			stream.PutOrGet(ref this.applyAllUnitMaxLevel);
			stream.PutOrGet(ref this.applyBanUpSystem);
			stream.PutOrGet(ref this.draftBanMode);
		}

		// Token: 0x04008819 RID: 34841
		public bool applyEquipStat;

		// Token: 0x0400881A RID: 34842
		public bool applyAllUnitMaxLevel;

		// Token: 0x0400881B RID: 34843
		public bool applyBanUpSystem;

		// Token: 0x0400881C RID: 34844
		public bool draftBanMode;
	}
}
