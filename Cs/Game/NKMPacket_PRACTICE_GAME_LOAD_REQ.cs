using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F2F RID: 3887
	[PacketId(ClientPacketId.kNKMPacket_PRACTICE_GAME_LOAD_REQ)]
	public sealed class NKMPacket_PRACTICE_GAME_LOAD_REQ : ISerializable
	{
		// Token: 0x0600993E RID: 39230 RVA: 0x0032FB4E File Offset: 0x0032DD4E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMUnitData>(ref this.practiceUnitData);
		}

		// Token: 0x04008C26 RID: 35878
		public NKMUnitData practiceUnitData;
	}
}
