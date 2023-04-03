using System;
using AssetBundles;
using NKC.Localization;
using NKC.UI;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200080D RID: 2061
	public class NKCTestSceneInitializer : MonoBehaviour
	{
		// Token: 0x060051BE RID: 20926 RVA: 0x0018CD28 File Offset: 0x0018AF28
		private void Start()
		{
			NKCAssetResourceManager.Init();
			NKCStringTable.LoadFromLUA(NKM_NATIONAL_CODE.NNC_KOREA);
			AssetBundleManager.ActiveVariants = NKCLocalization.GetVariants(NKM_NATIONAL_CODE.NNC_KOREA, NKC_VOICE_CODE.NVC_KOR);
			if (this.m_bTempletInitMode == NKCTestSceneInitializer.eInitMode.NKCMainAll)
			{
				NKCMain.NKCInit();
			}
			else if (this.m_bTempletInitMode == NKCTestSceneInitializer.eInitMode.BasicTempletOnly)
			{
				NKMContentsVersionManager.LoadDefaultVersion();
				if (NKCDefineManager.DEFINE_UNITY_EDITOR())
				{
					NKCContentsVersionManager.TryRecoverTag();
				}
				NKMCommonConst.LoadFromLUA("LUA_COMMON_CONST");
				NKCClientConst.LoadFromLUA("LUA_CLIENT_CONST");
				NKMTempletContainer<NKMIntervalTemplet>.Load("AB_SCRIPT", "LUA_INTERVAL_TEMPLET_V2", "INTERVAL_TEMPLET", new Func<NKMLua, NKMIntervalTemplet>(NKMIntervalTemplet.LoadFromLUA), (NKMIntervalTemplet e) => e.StrKey);
				string[] fileNames = new string[]
				{
					"LUA_UNIT_TEMPLET_BASE",
					"LUA_UNIT_TEMPLET_BASE2"
				};
				NKMTempletContainer<NKMUnitTempletBase>.Load("AB_SCRIPT_UNIT_DATA", fileNames, "m_dicNKMUnitTempletBaseByStrID", new Func<NKMLua, NKMUnitTempletBase>(NKMUnitTempletBase.LoadFromLUA), (NKMUnitTempletBase e) => e.m_UnitStrID);
				NKMSkinManager.LoadFromLua();
			}
			if (this.m_bUseSountManager)
			{
				if (GameObject.Find("NKM_SOUND") == null)
				{
					new GameObject("NKM_SOUND");
				}
				NKCSoundManager.Init();
			}
			if (this.m_bUseUIManager)
			{
				NKCUIManager.Init();
			}
			if (this.m_bLoadAnimationTemplet)
			{
				NKCAnimationEventManager.LoadFromLua();
			}
		}

		// Token: 0x060051BF RID: 20927 RVA: 0x0018CE6B File Offset: 0x0018B06B
		private void Update()
		{
			if (Input.GetKeyUp(KeyCode.F5))
			{
				NKCAnimationEventManager.LoadFromLua();
			}
		}

		// Token: 0x04004215 RID: 16917
		public NKCTestSceneInitializer.eInitMode m_bTempletInitMode = NKCTestSceneInitializer.eInitMode.BasicTempletOnly;

		// Token: 0x04004216 RID: 16918
		public bool m_bUseSountManager = true;

		// Token: 0x04004217 RID: 16919
		[Header("이 옵션 켤때는 NKM_SCEN_UI 오브젝트 통채로 카피해와서 붙여둘 것")]
		public bool m_bUseUIManager;

		// Token: 0x04004218 RID: 16920
		[Header("F5 to reload")]
		public bool m_bLoadAnimationTemplet = true;

		// Token: 0x020014C9 RID: 5321
		public enum eInitMode
		{
			// Token: 0x04009F13 RID: 40723
			NKCMainAll,
			// Token: 0x04009F14 RID: 40724
			BasicTempletOnly,
			// Token: 0x04009F15 RID: 40725
			None
		}
	}
}
