using System;
using System.Collections.Generic;
using NKC.UI.Tooltip;
using NKM;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000625 RID: 1573
	public class NKCGameHudCombo : MonoBehaviour
	{
		// Token: 0x06003090 RID: 12432 RVA: 0x000F0200 File Offset: 0x000EE400
		public void Awake()
		{
			if (this.m_arNKCGameHudComboSlot != null)
			{
				this.m_SlotMaxCount = this.m_arNKCGameHudComboSlot.Length;
			}
			if (this.m_cbtnSkill != null)
			{
				this.m_cbtnSkill.PointerDown.RemoveAllListeners();
				this.m_cbtnSkill.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPointerDownSkill));
			}
			this.m_TimeWarningColor = NKCUtil.GetColor("#c2190d");
			this.m_TimeNormalColor = NKCUtil.GetColor("#e8c043");
			this.m_SkillLvOriginalColor = this.m_lbSkillLevel.color;
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x000F028E File Offset: 0x000EE48E
		public void OnPointerDownSkill(PointerEventData e)
		{
			if (this.m_NKMTacticalCommandTemplet == null)
			{
				return;
			}
			NKCUITooltip.Instance.Open(this.m_NKMTacticalCommandTemplet, this.m_TCLevel, new Vector2?(e.position));
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x000F02BA File Offset: 0x000EE4BA
		private void OnDestroy()
		{
			NKCUtil.SetImageSprite(this.m_imgSkill, null, false);
			NKCUtil.SetImageSprite(this.m_imgSkillGray, null, false);
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000F02D8 File Offset: 0x000EE4D8
		public void UpdatePerFrame(NKMTacticalCommandData cNKMTacticalCommandData)
		{
			if (this.m_NKMTacticalCommandTemplet == null || cNKMTacticalCommandData == null)
			{
				return;
			}
			if (this.m_NKMTacticalCommandTemplet.m_TCID != cNKMTacticalCommandData.m_TCID)
			{
				return;
			}
			if (this.m_arNKCGameHudComboSlot == null)
			{
				return;
			}
			int count = this.m_NKMTacticalCommandTemplet.m_listComboType.Count;
			if (count <= 0)
			{
				return;
			}
			int num = this.m_SlotMaxCount - count;
			int num2 = 0;
			for (int i = num; i < this.m_arNKCGameHudComboSlot.Length; i++)
			{
				NKCGameHudComboSlot nkcgameHudComboSlot = this.m_arNKCGameHudComboSlot[i];
				if (!(nkcgameHudComboSlot == null))
				{
					NKMTacticalCombo nkmtacticalCombo = this.m_NKMTacticalCommandTemplet.m_listComboType[num2];
					nkcgameHudComboSlot.SetComboSucess(num2 < (int)cNKMTacticalCommandData.m_ComboCount);
					num2++;
				}
			}
			this.m_sldCoolTime.value = cNKMTacticalCommandData.m_fComboResetCoolTimeNow / this.m_NKMTacticalCommandTemplet.m_fComboResetCoolTime;
			if (this.m_imgSldFill != null && this.m_sldCoolTime != null)
			{
				if ((double)this.m_sldCoolTime.value < 0.33)
				{
					this.m_imgSldFill.color = this.m_TimeWarningColor;
				}
				else
				{
					this.m_imgSldFill.color = this.m_TimeNormalColor;
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objSkillCoolTime, !cNKMTacticalCommandData.m_bCoolTimeOn);
			if (this.m_cgComboSlots != null)
			{
				float alpha = this.m_cgComboSlots.alpha;
				if (cNKMTacticalCommandData.m_bCoolTimeOn)
				{
					this.m_cgComboSlots.alpha = 1f;
				}
				else
				{
					this.m_cgComboSlots.alpha = 0.4f;
				}
				if (alpha != 1f && alpha != this.m_cgComboSlots.alpha && this.m_amtCoolTimeOn != null)
				{
					this.m_amtCoolTimeOn.Play("NKM_UI_GAME_TACTICAL_COMMAND_DECK_CARD_READY", -1, 0f);
				}
			}
			this.SetSkillImg(cNKMTacticalCommandData);
			if (this.m_imgSkillCoolTime != null && this.m_NKMTacticalCommandTemplet.m_fCoolTime > 0f)
			{
				this.m_imgSkillCoolTime.fillAmount = (this.m_NKMTacticalCommandTemplet.m_fCoolTime - cNKMTacticalCommandData.m_fCoolTimeNow) / this.m_NKMTacticalCommandTemplet.m_fCoolTime;
			}
			this.SetResetCoolTimeVisible(cNKMTacticalCommandData);
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000F04D7 File Offset: 0x000EE6D7
		private void SetSkillImg(NKMTacticalCommandData cNKMTacticalCommandData)
		{
			if (cNKMTacticalCommandData == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_imgSkill, cNKMTacticalCommandData.m_bCoolTimeOn);
			NKCUtil.SetGameobjectActive(this.m_imgSkillGray, !cNKMTacticalCommandData.m_bCoolTimeOn);
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000F0502 File Offset: 0x000EE702
		private void SetResetCoolTimeVisible(NKMTacticalCommandData cNKMTacticalCommandData)
		{
			if (cNKMTacticalCommandData == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objCoolTime, cNKMTacticalCommandData.m_fCoolTimeNow <= 0f && cNKMTacticalCommandData.m_ComboCount > 0);
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x000F052C File Offset: 0x000EE72C
		public void SetUI(NKMGameData cNKMGameData, NKM_TEAM_TYPE myTeamType)
		{
			if (cNKMGameData == null)
			{
				return;
			}
			int num = 0;
			List<NKMTacticalCombo> list = null;
			string text = "";
			NKMTacticalCommandData cNKMTacticalCommandData = null;
			NKMGameTeamData teamData = cNKMGameData.GetTeamData(myTeamType);
			if (teamData != null)
			{
				int num2 = 0;
				Color col = this.m_SkillLvOriginalColor;
				if (teamData.m_Operator != null && cNKMGameData.m_dicNKMBanOperatorData != null && cNKMGameData.m_dicNKMBanOperatorData.Count > 0)
				{
					foreach (KeyValuePair<int, NKMBanOperatorData> keyValuePair in cNKMGameData.m_dicNKMBanOperatorData)
					{
						if (keyValuePair.Value.m_OperatorID == teamData.m_Operator.id && keyValuePair.Value.m_BanLevel > 0)
						{
							col = NKCOperatorUtil.BAN_COLOR_RED;
							num2 = (int)keyValuePair.Value.m_BanLevel;
						}
					}
				}
				for (int i = 0; i < teamData.m_listTacticalCommandData.Count; i++)
				{
					NKMTacticalCommandData nkmtacticalCommandData = teamData.m_listTacticalCommandData[i];
					if (nkmtacticalCommandData != null)
					{
						NKMTacticalCommandTemplet tacticalCommandTempletByID = NKMTacticalCommandManager.GetTacticalCommandTempletByID((int)nkmtacticalCommandData.m_TCID);
						if (tacticalCommandTempletByID != null && tacticalCommandTempletByID.m_NKM_TACTICAL_COMMAND_TYPE == NKM_TACTICAL_COMMAND_TYPE.NTCT_COMBO)
						{
							text = tacticalCommandTempletByID.m_TCIconName;
							num = tacticalCommandTempletByID.m_listComboType.Count;
							list = tacticalCommandTempletByID.m_listComboType;
							this.m_NKMTacticalCommandTemplet = tacticalCommandTempletByID;
							if (num2 > 0)
							{
								this.m_NKMTacticalCommandTemplet.m_fCoolTime = this.m_NKMTacticalCommandTemplet.m_fCoolTime * (1f + 0.2f * (float)num2);
								Debug.Log(string.Format("<color=red>SetUI.Operator Skill Cool Time - {0} modified -> {1}</color>", this.m_NKMTacticalCommandTemplet.m_TCName, this.m_NKMTacticalCommandTemplet.m_fCoolTime));
							}
							cNKMTacticalCommandData = nkmtacticalCommandData;
							string arg = "???";
							this.m_TCLevel = 1;
							if (teamData.m_Operator != null && teamData.m_Operator.mainSkill != null && NKMTempletContainer<NKMOperatorSkillTemplet>.Find(teamData.m_Operator.mainSkill.id).m_OperSkillTarget == tacticalCommandTempletByID.m_TCStrID)
							{
								arg = teamData.m_Operator.mainSkill.level.ToString();
								this.m_TCLevel = (int)teamData.m_Operator.mainSkill.level;
							}
							NKCUtil.SetLabelText(this.m_lbSkillLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, arg));
							NKCUtil.SetLabelTextColor(this.m_lbSkillLevel, col);
							break;
						}
					}
				}
			}
			if (num <= 0 || num > this.m_SlotMaxCount)
			{
				return;
			}
			if (this.m_arNKCGameHudComboSlot != null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, true);
				int num3 = this.m_SlotMaxCount - num;
				int num4 = 0;
				for (int j = 0; j < this.m_arNKCGameHudComboSlot.Length; j++)
				{
					NKCGameHudComboSlot nkcgameHudComboSlot = this.m_arNKCGameHudComboSlot[j];
					if (!(nkcgameHudComboSlot == null))
					{
						NKCUtil.SetGameobjectActive(nkcgameHudComboSlot, j >= num3);
						if (j >= num3)
						{
							NKMTacticalCombo cNKMTacticalCombo = list[num4];
							nkcgameHudComboSlot.SetUI(cNKMTacticalCombo, false);
							num4++;
						}
					}
				}
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_TACTICAL_COMMAND_ICON", text, false);
				NKCUtil.SetImageSprite(this.m_imgSkill, orLoadAssetResource, false);
				NKCUtil.SetImageSprite(this.m_imgSkillGray, orLoadAssetResource, false);
			}
			else
			{
				NKCUtil.SetImageSprite(this.m_imgSkill, null, false);
				NKCUtil.SetImageSprite(this.m_imgSkillGray, null, false);
			}
			this.UpdatePerFrame(cNKMTacticalCommandData);
		}

		// Token: 0x04002FFC RID: 12284
		public CanvasGroup m_cgComboSlots;

		// Token: 0x04002FFD RID: 12285
		public NKCGameHudComboSlot[] m_arNKCGameHudComboSlot;

		// Token: 0x04002FFE RID: 12286
		public Image m_imgSkill;

		// Token: 0x04002FFF RID: 12287
		public Image m_imgSkillGray;

		// Token: 0x04003000 RID: 12288
		public NKCUIComStateButton m_cbtnSkill;

		// Token: 0x04003001 RID: 12289
		public GameObject m_objSkillCoolTime;

		// Token: 0x04003002 RID: 12290
		public Image m_imgSkillCoolTime;

		// Token: 0x04003003 RID: 12291
		public Animator m_amtCoolTimeOn;

		// Token: 0x04003004 RID: 12292
		public Text m_lbSkillLevel;

		// Token: 0x04003005 RID: 12293
		public GameObject m_objCoolTime;

		// Token: 0x04003006 RID: 12294
		public Slider m_sldCoolTime;

		// Token: 0x04003007 RID: 12295
		public Image m_imgSldFill;

		// Token: 0x04003008 RID: 12296
		private int m_SlotMaxCount;

		// Token: 0x04003009 RID: 12297
		private NKMTacticalCommandTemplet m_NKMTacticalCommandTemplet;

		// Token: 0x0400300A RID: 12298
		private int m_TCLevel = 1;

		// Token: 0x0400300B RID: 12299
		private Color m_TimeWarningColor;

		// Token: 0x0400300C RID: 12300
		private Color m_TimeNormalColor;

		// Token: 0x0400300D RID: 12301
		private Color m_SkillLvOriginalColor;
	}
}
