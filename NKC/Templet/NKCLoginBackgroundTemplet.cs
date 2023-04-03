using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x02000855 RID: 2133
	public class NKCLoginBackgroundTemplet : INKMTemplet
	{
		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x060054B9 RID: 21689 RVA: 0x0019CC17 File Offset: 0x0019AE17
		public int Key
		{
			get
			{
				return this.ID;
			}
		}

		// Token: 0x060054BA RID: 21690 RVA: 0x0019CC1F File Offset: 0x0019AE1F
		public static NKCLoginBackgroundTemplet Find(int key)
		{
			return NKMTempletContainer<NKCLoginBackgroundTemplet>.Find(key);
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x0019CC28 File Offset: 0x0019AE28
		public static NKCLoginBackgroundTemplet GetCurrentBackgroundTemplet()
		{
			foreach (NKCLoginBackgroundTemplet nkcloginBackgroundTemplet in NKMTempletContainer<NKCLoginBackgroundTemplet>.Values)
			{
				if (nkcloginBackgroundTemplet.m_type == LoginScreenDataType.BACKGROUND)
				{
					return nkcloginBackgroundTemplet;
				}
			}
			Log.Error("[LoginBackgroundTemplet] 활성화된 Login Background 가 없습니다!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCLoginBackgroundTemplet.cs", 42);
			return null;
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x0019CC90 File Offset: 0x0019AE90
		public static List<NKCLoginBackgroundTemplet> GetEnabledPrefabList()
		{
			List<NKCLoginBackgroundTemplet> list = new List<NKCLoginBackgroundTemplet>();
			foreach (NKCLoginBackgroundTemplet nkcloginBackgroundTemplet in NKMTempletContainer<NKCLoginBackgroundTemplet>.Values)
			{
				if (nkcloginBackgroundTemplet.m_type == LoginScreenDataType.PREFAB)
				{
					list.Add(nkcloginBackgroundTemplet);
				}
			}
			return list;
		}

		// Token: 0x060054BD RID: 21693 RVA: 0x0019CCEC File Offset: 0x0019AEEC
		public static NKCLoginBackgroundTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCLoginBackgroundTemplet.cs", 62))
			{
				return null;
			}
			NKCLoginBackgroundTemplet nkcloginBackgroundTemplet = new NKCLoginBackgroundTemplet();
			bool flag = true;
			flag &= cNKMLua.GetData("ID", ref nkcloginBackgroundTemplet.ID);
			if (!cNKMLua.GetDataEnum<LoginScreenDataType>("m_type", out nkcloginBackgroundTemplet.m_type))
			{
				flag = false;
				Log.Error(string.Format("[BACKGROUND_TEMPLET][{0}] 데이터 타입을 지정해주세요", nkcloginBackgroundTemplet.ID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCLoginBackgroundTemplet.cs", 71);
			}
			flag &= cNKMLua.GetData("BundleName", ref nkcloginBackgroundTemplet.BundleName);
			flag &= cNKMLua.GetData("AssetName", ref nkcloginBackgroundTemplet.AssetName);
			if (!cNKMLua.GetData("m_MusicName", ref nkcloginBackgroundTemplet.m_MusicName) && nkcloginBackgroundTemplet.m_type == LoginScreenDataType.BACKGROUND)
			{
				flag = false;
				Log.Error(string.Format("[BACKGROUND_TEMPLET][{0}] BACKGROUND 타입에는 배경음악 설정이 필수입니다", nkcloginBackgroundTemplet.ID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCLoginBackgroundTemplet.cs", 83);
			}
			cNKMLua.GetData("m_MusicStartTime", ref nkcloginBackgroundTemplet.m_MusicStartTime);
			if (!flag)
			{
				return null;
			}
			return nkcloginBackgroundTemplet;
		}

		// Token: 0x060054BE RID: 21694 RVA: 0x0019CDDA File Offset: 0x0019AFDA
		public void Join()
		{
		}

		// Token: 0x060054BF RID: 21695 RVA: 0x0019CDDC File Offset: 0x0019AFDC
		public void Validate()
		{
		}

		// Token: 0x040043AC RID: 17324
		public int ID;

		// Token: 0x040043AD RID: 17325
		public LoginScreenDataType m_type;

		// Token: 0x040043AE RID: 17326
		public string BundleName;

		// Token: 0x040043AF RID: 17327
		public string AssetName;

		// Token: 0x040043B0 RID: 17328
		public string m_MusicName;

		// Token: 0x040043B1 RID: 17329
		public float m_MusicStartTime;
	}
}
