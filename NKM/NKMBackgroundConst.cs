using System;

namespace NKM
{
	// Token: 0x020003A5 RID: 933
	public sealed class NKMBackgroundConst
	{
		// Token: 0x06001870 RID: 6256 RVA: 0x00062A83 File Offset: 0x00060C83
		public NKMBackgroundConst.BackgroundUnitConst GetBackgroundUnitSlot(int index)
		{
			return this.unitSlot[index];
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x00062A8D File Offset: 0x00060C8D
		// (set) Token: 0x06001872 RID: 6258 RVA: 0x00062A95 File Offset: 0x00060C95
		public int DefaultBackgroundItem { get; private set; }

		// Token: 0x06001873 RID: 6259 RVA: 0x00062AA0 File Offset: 0x00060CA0
		public void Load(NKMLua lua)
		{
			using (lua.OpenTable("NKMBackgroundUnitInfo", "[NKMBackgroundConst] loading BackgroundUnitInfo table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMBackgroundConst.cs", 18))
			{
				for (int i = 0; i < 3; i++)
				{
					NKMBackgroundConst.BackgroundUnitConst backgroundUnitConst = new NKMBackgroundConst.BackgroundUnitConst();
					backgroundUnitConst.Load(lua, i);
					this.unitSlot[i] = backgroundUnitConst;
				}
			}
			using (lua.OpenTable("LobbyDefaultBackground", "[NKMBackgroundConst] loading LobbyDefaultBackground table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMBackgroundConst.cs", 28))
			{
				this.DefaultBackgroundItem = lua.GetInt32("DefaultBackgroundItem");
			}
		}

		// Token: 0x0400103D RID: 4157
		private const int BackgroundUnitCount = 3;

		// Token: 0x0400103E RID: 4158
		private NKMBackgroundConst.BackgroundUnitConst[] unitSlot = new NKMBackgroundConst.BackgroundUnitConst[3];

		// Token: 0x020011A9 RID: 4521
		public sealed class BackgroundUnitConst
		{
			// Token: 0x1700178E RID: 6030
			// (get) Token: 0x0600A054 RID: 41044 RVA: 0x0033DD6B File Offset: 0x0033BF6B
			// (set) Token: 0x0600A055 RID: 41045 RVA: 0x0033DD73 File Offset: 0x0033BF73
			public float UnitPosX { get; private set; }

			// Token: 0x1700178F RID: 6031
			// (get) Token: 0x0600A056 RID: 41046 RVA: 0x0033DD7C File Offset: 0x0033BF7C
			// (set) Token: 0x0600A057 RID: 41047 RVA: 0x0033DD84 File Offset: 0x0033BF84
			public float UnitPosY { get; private set; }

			// Token: 0x17001790 RID: 6032
			// (get) Token: 0x0600A058 RID: 41048 RVA: 0x0033DD8D File Offset: 0x0033BF8D
			// (set) Token: 0x0600A059 RID: 41049 RVA: 0x0033DD95 File Offset: 0x0033BF95
			public float UnitSize { get; private set; }

			// Token: 0x0600A05A RID: 41050 RVA: 0x0033DDA0 File Offset: 0x0033BFA0
			public void Load(NKMLua lua, int index)
			{
				using (lua.OpenTable(string.Format("UnitSlot{0}", index + 1), string.Format("[NKMBackgroundConst] open subTable 'UnitSlot{0}' failed.", index + 1), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMBackgroundConst.cs", 42))
				{
					this.UnitPosX = lua.GetFloat("unitPosX");
					this.UnitPosY = lua.GetFloat("unitPosY");
					this.UnitSize = lua.GetFloat("unitSize");
				}
			}
		}
	}
}
