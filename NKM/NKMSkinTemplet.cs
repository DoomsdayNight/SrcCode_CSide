using System;
using Cs.Logging;
using NKC;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000474 RID: 1140
	public class NKMSkinTemplet : INKMTemplet
	{
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001F04 RID: 7940 RVA: 0x0009363D File Offset: 0x0009183D
		public int Key
		{
			get
			{
				return this.m_SkinID;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x00093645 File Offset: 0x00091845
		public bool EnableByTag
		{
			get
			{
				return NKMOpenTagManager.IsOpened(this.m_OpenTag);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x00093652 File Offset: 0x00091852
		public bool HasLoginCutin
		{
			get
			{
				return !string.IsNullOrEmpty(this.m_LoginCutin);
			}
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x00093662 File Offset: 0x00091862
		public static NKMSkinTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			return NKMSkinTemplet.LoadFromLUA(cNKMLua, false);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0009366C File Offset: 0x0009186C
		private static NKMSkinTemplet LoadFromLUA(NKMLua cNKMLua, bool bIgnoreContentVersion)
		{
			if (!bIgnoreContentVersion && !NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMSkinManagerEx.cs", 83))
			{
				return null;
			}
			NKMSkinTemplet nkmskinTemplet = new NKMSkinTemplet();
			bool flag = true;
			cNKMLua.GetData("m_OpenTag", ref nkmskinTemplet.m_OpenTag);
			bool flag2 = flag & cNKMLua.GetData("m_SkinID", ref nkmskinTemplet.m_SkinID) & cNKMLua.GetData("m_SkinStrID", ref nkmskinTemplet.m_SkinStrID) & cNKMLua.GetData("m_SkinEquipUnitID", ref nkmskinTemplet.m_SkinEquipUnitID) & cNKMLua.GetData("m_Title", ref nkmskinTemplet.m_Title) & cNKMLua.GetData("m_SkinDesc", ref nkmskinTemplet.m_SkinDesc);
			cNKMLua.GetData("m_ReturnItemID", ref nkmskinTemplet.m_ReturnItemId);
			cNKMLua.GetData("m_ReturnItemCount", ref nkmskinTemplet.m_ReturnItemCount);
			bool flag3 = flag2 & cNKMLua.GetDataEnum<NKMSkinTemplet.SKIN_GRADE>("m_SkinGrade", out nkmskinTemplet.m_SkinGrade) & cNKMLua.GetData("m_bLimited", ref nkmskinTemplet.m_bLimited) & cNKMLua.GetData("m_bEffect", ref nkmskinTemplet.m_bEffect) & cNKMLua.GetData<NKMSkinTemplet.SKIN_CUTIN>("m_SkinSkillCutIn", ref nkmskinTemplet.m_SkinSkillCutIn);
			cNKMLua.GetData("m_Conversion", ref nkmskinTemplet.m_Conversion);
			cNKMLua.GetData("m_LobbyFace", ref nkmskinTemplet.m_LobbyFace);
			cNKMLua.GetData("m_Collabo", ref nkmskinTemplet.m_Collabo);
			cNKMLua.GetData("m_Gauntlet", ref nkmskinTemplet.m_Gauntlet);
			cNKMLua.GetData("m_VoiceBundleName", ref nkmskinTemplet.m_VoiceBundleName);
			bool flag4 = flag3 & cNKMLua.GetData("m_SpriteBundleName", ref nkmskinTemplet.m_SpriteBundleName) & cNKMLua.GetData("m_SpriteName", ref nkmskinTemplet.m_SpriteName);
			cNKMLua.GetData("m_SpriteMaterialName", ref nkmskinTemplet.m_SpriteMaterialName);
			cNKMLua.GetData("m_SpriteBundleNameSub", ref nkmskinTemplet.m_SpriteBundleNameSub);
			cNKMLua.GetData("m_SpriteNameSub", ref nkmskinTemplet.m_SpriteNameSub);
			cNKMLua.GetData("m_SpriteMaterialNameSub", ref nkmskinTemplet.m_SpriteMaterialNameSub);
			bool flag5 = flag4 & cNKMLua.GetData("m_FaceCardName", ref nkmskinTemplet.m_FaceCardName) & cNKMLua.GetData("m_SpineIllustName", ref nkmskinTemplet.m_SpineIllustName) & cNKMLua.GetData("m_SpineSDName", ref nkmskinTemplet.m_SpineSDName) & cNKMLua.GetData("m_InvenIconName", ref nkmskinTemplet.m_InvenIconName);
			cNKMLua.GetData("m_HyperSkillCutin", ref nkmskinTemplet.m_HyperSkillCutin);
			cNKMLua.GetData("m_CutscenePurchase", ref nkmskinTemplet.m_CutscenePurchase);
			cNKMLua.GetData("m_CutsceneLifetime_start", ref nkmskinTemplet.m_CutsceneLifetime_start);
			cNKMLua.GetData("m_CutsceneLifetime_end", ref nkmskinTemplet.m_CutsceneLifetime_end);
			cNKMLua.GetData("m_CutsceneLifetime_BG", ref nkmskinTemplet.m_CutsceneLifetime_BG);
			cNKMLua.GetData("m_bExclude", ref nkmskinTemplet.m_bExclude);
			cNKMLua.GetData("m_LoginCutin", ref nkmskinTemplet.m_LoginCutin);
			if (!flag5)
			{
				Log.Error(string.Format("NKMSkinTemplet Load Fail - {0}", nkmskinTemplet.m_SkinID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMSkinManagerEx.cs", 141);
				return null;
			}
			return nkmskinTemplet;
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x00093921 File Offset: 0x00091B21
		public string GetTitle()
		{
			return NKCStringTable.GetString(this.m_Title, false);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x0009392F File Offset: 0x00091B2F
		public string GetSkinDesc()
		{
			return NKCStringTable.GetString(this.m_SkinDesc, false);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x0009393D File Offset: 0x00091B3D
		public bool ChangesHyperCutin()
		{
			return !string.IsNullOrEmpty(this.m_HyperSkillCutin);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0009394D File Offset: 0x00091B4D
		public bool ChangesVoice()
		{
			return !string.IsNullOrEmpty(this.m_VoiceBundleName);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0009395D File Offset: 0x00091B5D
		public void Join()
		{
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0009395F File Offset: 0x00091B5F
		public void Validate()
		{
		}

		// Token: 0x04001F70 RID: 8048
		public int m_SkinID;

		// Token: 0x04001F71 RID: 8049
		public int m_SkinEquipUnitID;

		// Token: 0x04001F72 RID: 8050
		public string m_SkinDesc;

		// Token: 0x04001F73 RID: 8051
		public string m_Title;

		// Token: 0x04001F74 RID: 8052
		public int m_ReturnItemId;

		// Token: 0x04001F75 RID: 8053
		public int m_ReturnItemCount;

		// Token: 0x04001F76 RID: 8054
		private string m_OpenTag;

		// Token: 0x04001F77 RID: 8055
		public string m_SkinStrID;

		// Token: 0x04001F78 RID: 8056
		public NKMSkinTemplet.SKIN_GRADE m_SkinGrade;

		// Token: 0x04001F79 RID: 8057
		public bool m_bLimited;

		// Token: 0x04001F7A RID: 8058
		public bool m_bEffect;

		// Token: 0x04001F7B RID: 8059
		public bool m_Conversion;

		// Token: 0x04001F7C RID: 8060
		public bool m_LobbyFace;

		// Token: 0x04001F7D RID: 8061
		public bool m_Collabo;

		// Token: 0x04001F7E RID: 8062
		public bool m_Gauntlet;

		// Token: 0x04001F7F RID: 8063
		public NKMSkinTemplet.SKIN_CUTIN m_SkinSkillCutIn;

		// Token: 0x04001F80 RID: 8064
		public string m_VoiceBundleName;

		// Token: 0x04001F81 RID: 8065
		public string m_SpriteBundleName;

		// Token: 0x04001F82 RID: 8066
		public string m_SpriteName;

		// Token: 0x04001F83 RID: 8067
		public string m_SpriteMaterialName = "";

		// Token: 0x04001F84 RID: 8068
		public string m_SpriteBundleNameSub;

		// Token: 0x04001F85 RID: 8069
		public string m_SpriteNameSub;

		// Token: 0x04001F86 RID: 8070
		public string m_SpriteMaterialNameSub = "";

		// Token: 0x04001F87 RID: 8071
		public string m_FaceCardName;

		// Token: 0x04001F88 RID: 8072
		public string m_SpineIllustName;

		// Token: 0x04001F89 RID: 8073
		public string m_SpineSDName;

		// Token: 0x04001F8A RID: 8074
		public string m_InvenIconName;

		// Token: 0x04001F8B RID: 8075
		public string m_HyperSkillCutin;

		// Token: 0x04001F8C RID: 8076
		public string m_CutscenePurchase;

		// Token: 0x04001F8D RID: 8077
		public string m_CutsceneLifetime_start;

		// Token: 0x04001F8E RID: 8078
		public string m_CutsceneLifetime_end;

		// Token: 0x04001F8F RID: 8079
		public string m_CutsceneLifetime_BG;

		// Token: 0x04001F90 RID: 8080
		public string m_LoginCutin;

		// Token: 0x04001F91 RID: 8081
		public bool m_bExclude;

		// Token: 0x02001209 RID: 4617
		public enum SKIN_GRADE
		{
			// Token: 0x0400944D RID: 37965
			SG_VARIATION,
			// Token: 0x0400944E RID: 37966
			SG_NORMAL,
			// Token: 0x0400944F RID: 37967
			SG_RARE,
			// Token: 0x04009450 RID: 37968
			SG_PREMIUM,
			// Token: 0x04009451 RID: 37969
			SG_SPECIAL
		}

		// Token: 0x0200120A RID: 4618
		public enum SKIN_CUTIN
		{
			// Token: 0x04009453 RID: 37971
			CUTIN_EMPTY,
			// Token: 0x04009454 RID: 37972
			CUTIN_NORMAL,
			// Token: 0x04009455 RID: 37973
			CUTIN_PRIVATE
		}
	}
}
