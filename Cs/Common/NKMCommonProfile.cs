using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x0200103A RID: 4154
	public sealed class NKMCommonProfile : ISerializable
	{
		// Token: 0x06009B36 RID: 39734 RVA: 0x003325F8 File Offset: 0x003307F8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userUid);
			stream.PutOrGet(ref this.friendCode);
			stream.PutOrGet(ref this.nickname);
			stream.PutOrGet(ref this.level);
			stream.PutOrGet(ref this.mainUnitId);
			stream.PutOrGet(ref this.mainUnitSkinId);
			stream.PutOrGet(ref this.frameId);
		}

		// Token: 0x04008EAB RID: 36523
		public long userUid;

		// Token: 0x04008EAC RID: 36524
		public long friendCode;

		// Token: 0x04008EAD RID: 36525
		public string nickname;

		// Token: 0x04008EAE RID: 36526
		public int level;

		// Token: 0x04008EAF RID: 36527
		public int mainUnitId;

		// Token: 0x04008EB0 RID: 36528
		public int mainUnitSkinId;

		// Token: 0x04008EB1 RID: 36529
		public int frameId;
	}
}
