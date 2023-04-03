using System;
using ClientPacket.Common;
using ClientPacket.Guild;
using NKC.UI.Guild;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Friend
{
	// Token: 0x02000B14 RID: 2836
	public class NKCUIFriendMentoringSlot : MonoBehaviour
	{
		// Token: 0x060080D7 RID: 32983 RVA: 0x002B6630 File Offset: 0x002B4830
		public void SetData(MentoringIdentity myMentoringType, NKMCommonProfile userProfile, NKMGuildData guildData)
		{
			NKMGuildSimpleData nkmguildSimpleData = new NKMGuildSimpleData();
			if (guildData != null)
			{
				nkmguildSimpleData.guildUid = guildData.guildUid;
				nkmguildSimpleData.badgeId = guildData.badgeId;
				nkmguildSimpleData.guildName = guildData.name;
			}
			this.SetData(myMentoringType, userProfile, nkmguildSimpleData);
		}

		// Token: 0x060080D8 RID: 32984 RVA: 0x002B6674 File Offset: 0x002B4874
		public void SetData(MentoringIdentity myMentoringType, NKMCommonProfile userProfile, NKMGuildSimpleData guildData)
		{
			NKCUtil.SetGameobjectActive(this.m_Deco_Right, myMentoringType == MentoringIdentity.Mentee);
			NKCUtil.SetGameobjectActive(this.m_Deco_Left, myMentoringType == MentoringIdentity.Mentor);
			NKCUtil.SetGameobjectActive(this.m_MENTORING_TAG, userProfile != null && myMentoringType == MentoringIdentity.Mentor);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_SLOT_INFO, userProfile != null);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_SLOT_ADD_BUTTON, myMentoringType != MentoringIdentity.Mentee && userProfile == null);
			NKCUtil.SetGameobjectActive(this.m_CONSORTIUM, guildData != null && guildData.guildUid != 0L);
			if (guildData != null && guildData.guildUid != 0L)
			{
				this.m_NKM_UI_CONSORTIUM_MARK.SetData(guildData.badgeId);
				NKCUtil.SetLabelText(this.m_CONSORTIUM_NAME, guildData.guildName);
			}
			if (userProfile != null)
			{
				NKCUtil.SetLabelText(this.m_LV, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, userProfile.level));
				NKCUtil.SetLabelText(this.m_NICKNAME, userProfile.nickname);
				NKCUtil.SetLabelText(this.m_UID, string.Format("#{0}", userProfile.friendCode));
				NKCUtil.SetGameobjectActive(this.m_INVEN_ICON_Root, true);
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(userProfile.mainUnitId);
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(userProfile.mainUnitSkinId);
				Sprite sprite;
				if (skinTemplet != null && skinTemplet.m_SkinEquipUnitID == userProfile.mainUnitId)
				{
					sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, skinTemplet);
				}
				else
				{
					sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase);
				}
				if (sprite != null)
				{
					NKCUtil.SetImageSprite(this.m_INVEN_ICON_Root, sprite, false);
				}
			}
		}

		// Token: 0x04006CE9 RID: 27881
		public GameObject m_Deco_Right;

		// Token: 0x04006CEA RID: 27882
		public GameObject m_Deco_Left;

		// Token: 0x04006CEB RID: 27883
		public GameObject m_MENTORING_TAG;

		// Token: 0x04006CEC RID: 27884
		public GameObject m_NKM_UI_FRIEND_MENTORING_SLOT_INFO;

		// Token: 0x04006CED RID: 27885
		public NKCUIComStateButton m_NKM_UI_FRIEND_MENTORING_SLOT_ADD_BUTTON;

		// Token: 0x04006CEE RID: 27886
		[Header("유닛 정보")]
		public Image m_INVEN_ICON_Root;

		// Token: 0x04006CEF RID: 27887
		public Text m_LV;

		// Token: 0x04006CF0 RID: 27888
		public Text m_NICKNAME;

		// Token: 0x04006CF1 RID: 27889
		public Text m_UID;

		// Token: 0x04006CF2 RID: 27890
		public GameObject m_CONSORTIUM;

		// Token: 0x04006CF3 RID: 27891
		public Text m_CONSORTIUM_NAME;

		// Token: 0x04006CF4 RID: 27892
		public NKCUIGuildBadge m_NKM_UI_CONSORTIUM_MARK;
	}
}
