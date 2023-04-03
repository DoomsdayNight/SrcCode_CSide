using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020003F3 RID: 1011
	public sealed class NKMPotentialOption : ISerializable
	{
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x00076300 File Offset: 0x00074500
		public int OpenedSocketCount
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.sockets.Length; i++)
				{
					if (this.sockets[i] != null)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00076331 File Offset: 0x00074531
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.optionKey);
			stream.PutOrGetEnum<NKM_STAT_TYPE>(ref this.statType);
			stream.PutOrGet<NKMPotentialOption.SocketData>(ref this.sockets);
		}

		// Token: 0x040013DD RID: 5085
		public int optionKey;

		// Token: 0x040013DE RID: 5086
		public NKM_STAT_TYPE statType;

		// Token: 0x040013DF RID: 5087
		public NKMPotentialOption.SocketData[] sockets = new NKMPotentialOption.SocketData[3];

		// Token: 0x020011E3 RID: 4579
		public sealed class SocketData : ISerializable
		{
			// Token: 0x0600A0DA RID: 41178 RVA: 0x0033EFDF File Offset: 0x0033D1DF
			void ISerializable.Serialize(IPacketStream stream)
			{
				stream.PutOrGet(ref this.statValue);
				stream.PutOrGet(ref this.statFactor);
				stream.PutOrGet(ref this.precision);
			}

			// Token: 0x040093BE RID: 37822
			public float statValue;

			// Token: 0x040093BF RID: 37823
			public float statFactor;

			// Token: 0x040093C0 RID: 37824
			public int precision;
		}
	}
}
