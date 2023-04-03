using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000C97 RID: 3223
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_GAME_START_REQ)]
	public sealed class NKMPacket_WARFARE_GAME_START_REQ : ISerializable
	{
		// Token: 0x0600942B RID: 37931 RVA: 0x003284DD File Offset: 0x003266DD
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.warfareTempletID);
			stream.PutOrGet<NKMPacket_WARFARE_GAME_START_REQ.UnitPosition>(ref this.unitPositionList);
			stream.PutOrGet(ref this.friendCode);
			stream.PutOrGet(ref this.friendTileIndex);
			stream.PutOrGet(ref this.rewardMultiply);
		}

		// Token: 0x0400857A RID: 34170
		public int warfareTempletID;

		// Token: 0x0400857B RID: 34171
		public List<NKMPacket_WARFARE_GAME_START_REQ.UnitPosition> unitPositionList = new List<NKMPacket_WARFARE_GAME_START_REQ.UnitPosition>();

		// Token: 0x0400857C RID: 34172
		public long friendCode;

		// Token: 0x0400857D RID: 34173
		public short friendTileIndex;

		// Token: 0x0400857E RID: 34174
		public int rewardMultiply;

		// Token: 0x02001A28 RID: 6696
		public sealed class UnitPosition : ISerializable
		{
			// Token: 0x0600BB3B RID: 47931 RVA: 0x0036E8B9 File Offset: 0x0036CAB9
			void ISerializable.Serialize(IPacketStream stream)
			{
				stream.PutOrGet(ref this.isFlagShip);
				stream.PutOrGet(ref this.deckIndex);
				stream.PutOrGet(ref this.tileIndex);
			}

			// Token: 0x0400ADD1 RID: 44497
			public bool isFlagShip;

			// Token: 0x0400ADD2 RID: 44498
			public byte deckIndex;

			// Token: 0x0400ADD3 RID: 44499
			public short tileIndex;
		}
	}
}
