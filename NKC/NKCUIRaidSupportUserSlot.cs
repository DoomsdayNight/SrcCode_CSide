using System;
using ClientPacket.Raid;
using NKC.UI.Guild;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009D4 RID: 2516
	public class NKCUIRaidSupportUserSlot : MonoBehaviour
	{
		// Token: 0x06006BC5 RID: 27589 RVA: 0x002328B4 File Offset: 0x00230AB4
		public static NKCUIRaidSupportUserSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WORLD_MAP_RAID", "NKM_UI_WORLD_MAP_RAID_SUPPORTSLOT", false, null);
			NKCUIRaidSupportUserSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIRaidSupportUserSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIRaidSupportUserSlot Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.Init();
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06006BC6 RID: 27590 RVA: 0x00232977 File Offset: 0x00230B77
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06006BC7 RID: 27591 RVA: 0x00232996 File Offset: 0x00230B96
		private void Init()
		{
			this.m_NKCUISlotUnit.Init();
		}

		// Token: 0x06006BC8 RID: 27592 RVA: 0x002329A4 File Offset: 0x00230BA4
		public void SetUI(NKMRaidDetailData cRaidData, NKMRaidJoinData joinData, int ranking)
		{
			if (cRaidData == null || joinData == null)
			{
				return;
			}
			NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(cRaidData.stageID);
			if (nkmraidTemplet == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_lbTryCount, string.Format("{0}/{1}", joinData.tryCount, nkmraidTemplet.RaidTryCount));
			NKCUtil.SetLabelText(this.m_lbRanking, ranking.ToString());
			bool flag = NKCScenManager.CurrentUserData().m_UserUID == joinData.userUID;
			this.m_lbUserName.text = joinData.nickName;
			this.m_lbUID.text = NKCUtilString.GetFriendCode(joinData.friendCode);
			if (flag)
			{
				this.m_lbUserName.color = NKCUtil.GetColor("#FFCF3B");
			}
			else
			{
				this.m_lbUserName.color = Color.white;
			}
			int num = (int)joinData.damage;
			float num2 = 0f;
			this.m_lbDamageAmount.text = num.ToString("N0");
			float maxHP = cRaidData.maxHP;
			if (maxHP != 0f)
			{
				num2 = joinData.damage / maxHP * 100f;
			}
			this.m_lbDamagePercent.text = string.Format("{0:0.##}", num2) + "%";
			this.SetGuildData(joinData);
			this.m_NKCUISlotUnit.SetUnitData(joinData.mainUnitID, 1, joinData.mainUnitSkinID, false, false, true, null);
			NKCUtil.SetGameobjectActive(this.m_objHighScore, joinData.highScore);
		}

		// Token: 0x06006BC9 RID: 27593 RVA: 0x00232B04 File Offset: 0x00230D04
		private void SetGuildData(NKMRaidJoinData data)
		{
			if (this.m_objGuild != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objGuild, data.guildData != null && data.guildData.guildUid > 0L);
				if (this.m_objGuild.activeSelf)
				{
					this.m_GuildBadgeUI.SetData(data.guildData.badgeId);
					NKCUtil.SetLabelText(this.m_lbGuildName, data.guildData.guildName);
				}
			}
		}

		// Token: 0x04005786 RID: 22406
		public NKCUISlot m_NKCUISlotUnit;

		// Token: 0x04005787 RID: 22407
		public Text m_lbUserName;

		// Token: 0x04005788 RID: 22408
		public Text m_lbUID;

		// Token: 0x04005789 RID: 22409
		public Text m_lbDamageAmount;

		// Token: 0x0400578A RID: 22410
		public Text m_lbDamagePercent;

		// Token: 0x0400578B RID: 22411
		public GameObject m_objHighScore;

		// Token: 0x0400578C RID: 22412
		public GameObject m_objGuild;

		// Token: 0x0400578D RID: 22413
		public NKCUIGuildBadge m_GuildBadgeUI;

		// Token: 0x0400578E RID: 22414
		public Text m_lbGuildName;

		// Token: 0x0400578F RID: 22415
		public Text m_lbTryCount;

		// Token: 0x04005790 RID: 22416
		public Text m_lbRanking;

		// Token: 0x04005791 RID: 22417
		private NKCAssetInstanceData m_InstanceData;
	}
}
