using System;
using System.Collections.Generic;
using ClientPacket.Common;

namespace NKC.Trim
{
	// Token: 0x02000899 RID: 2201
	public class NKCTrimData
	{
		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x060057D6 RID: 22486 RVA: 0x001A53AD File Offset: 0x001A35AD
		public NKMTrimIntervalData TrimIntervalData
		{
			get
			{
				if (this.trimIntervalData == null)
				{
					return new NKMTrimIntervalData();
				}
				return this.trimIntervalData;
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x060057D7 RID: 22487 RVA: 0x001A53C3 File Offset: 0x001A35C3
		public List<NKMTrimClearData> TrimClearList
		{
			get
			{
				if (this.trimClearList == null)
				{
					return new List<NKMTrimClearData>();
				}
				return this.trimClearList;
			}
		}

		// Token: 0x060057D9 RID: 22489 RVA: 0x001A53E1 File Offset: 0x001A35E1
		public void SetTrimIntervalData(NKMTrimIntervalData _trimIntervalData)
		{
			if (_trimIntervalData == null)
			{
				return;
			}
			this.trimIntervalData = _trimIntervalData;
		}

		// Token: 0x060057DA RID: 22490 RVA: 0x001A53EE File Offset: 0x001A35EE
		public void SetTrimClearList(List<NKMTrimClearData> _trimClearList)
		{
			if (_trimClearList == null)
			{
				return;
			}
			this.trimClearList = _trimClearList;
		}

		// Token: 0x060057DB RID: 22491 RVA: 0x001A53FC File Offset: 0x001A35FC
		public void SetTrimClearData(NKMTrimClearData trimClearData)
		{
			if (trimClearData == null || this.trimClearList == null)
			{
				return;
			}
			int num = this.trimClearList.FindIndex((NKMTrimClearData e) => e.trimId == trimClearData.trimId && e.trimLevel == trimClearData.trimLevel);
			if (num >= 0)
			{
				this.trimClearList[num] = trimClearData;
				return;
			}
			this.trimClearList.Add(trimClearData);
		}

		// Token: 0x060057DC RID: 22492 RVA: 0x001A5468 File Offset: 0x001A3668
		public int GetClearedTrimLevel(int trimId)
		{
			int clearedLevel = 0;
			if (this.trimClearList != null)
			{
				List<NKMTrimClearData> list = this.trimClearList.FindAll((NKMTrimClearData e) => e.trimId == trimId);
				if (list != null && list.Count > 0)
				{
					list.ForEach(delegate(NKMTrimClearData e)
					{
						if (e.isWin && clearedLevel < e.trimLevel)
						{
							clearedLevel = e.trimLevel;
						}
					});
				}
			}
			return clearedLevel;
		}

		// Token: 0x060057DD RID: 22493 RVA: 0x001A54D0 File Offset: 0x001A36D0
		public NKMTrimClearData GetTrimClearData(int trimId, int trimLevel)
		{
			List<NKMTrimClearData> list = this.trimClearList;
			if (list == null)
			{
				return null;
			}
			return list.Find((NKMTrimClearData e) => e.trimId == trimId && e.trimLevel == trimLevel);
		}

		// Token: 0x04004565 RID: 17765
		private NKMTrimIntervalData trimIntervalData;

		// Token: 0x04004566 RID: 17766
		private List<NKMTrimClearData> trimClearList;
	}
}
