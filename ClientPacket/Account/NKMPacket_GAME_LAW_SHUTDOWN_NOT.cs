using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001080 RID: 4224
	[PacketId(ClientPacketId.kNKMPacket_GAME_LAW_SHUTDOWN_NOT)]
	public sealed class NKMPacket_GAME_LAW_SHUTDOWN_NOT : ISerializable
	{
		// Token: 0x06009BBD RID: 39869 RVA: 0x00333CA1 File Offset: 0x00331EA1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKMPacket_GAME_LAW_SHUTDOWN_NOT.ApplyType>(ref this.applyType);
			stream.PutOrGet(ref this.applyTime);
			stream.PutOrGet(ref this.remainSpan);
		}

		// Token: 0x04008FFF RID: 36863
		public NKMPacket_GAME_LAW_SHUTDOWN_NOT.ApplyType applyType;

		// Token: 0x04009000 RID: 36864
		public DateTime applyTime;

		// Token: 0x04009001 RID: 36865
		public TimeSpan remainSpan;

		// Token: 0x02001A2E RID: 6702
		public enum ApplyType
		{
			// Token: 0x0400ADE6 RID: 44518
			None,
			// Token: 0x0400ADE7 RID: 44519
			TimeSelectionShutdown,
			// Token: 0x0400ADE8 RID: 44520
			ForceShutdown,
			// Token: 0x0400ADE9 RID: 44521
			ChildSelection
		}
	}
}
