using System;
using System.Collections;
using NKM;
using NKM.Templet;
using NKM.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Result
{
	// Token: 0x02000BAC RID: 2988
	public class NKCUIResultUnitSlot : MonoBehaviour
	{
		// Token: 0x060089FE RID: 35326 RVA: 0x002EC558 File Offset: 0x002EA758
		public void SetData(NKCUIResultSubUIUnitExp.UnitLevelupUIData unitLevelupData, Sprite spSlotBGN, Sprite spSlotBGR, Sprite spSlotBGSR, Sprite spSlotBGSSR)
		{
			base.StopAllCoroutines();
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitLevelupData.m_UnitData.m_UnitID);
			this.m_UnitID = unitLevelupData.m_UnitData.m_UnitID;
			Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitLevelupData.m_UnitData);
			if (sprite != null)
			{
				NKCUtil.SetGameobjectActive(this.m_imgUnitFace, true);
				this.m_imgUnitFace.sprite = sprite;
			}
			else
			{
				Debug.LogError("UnitFace Not Found! unitID : " + unitLevelupData.m_UnitData.m_UnitID.ToString());
				NKCUtil.SetGameobjectActive(this.m_imgUnitFace, false);
			}
			Sprite orLoadUnitAttackTypeIcon;
			if (unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc == NKM_FIND_TARGET_TYPE.NFTT_INVALID)
			{
				orLoadUnitAttackTypeIcon = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(unitTempletBase.m_NKM_FIND_TARGET_TYPE, true);
			}
			else
			{
				orLoadUnitAttackTypeIcon = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc, true);
			}
			if (orLoadUnitAttackTypeIcon != null)
			{
				NKCUtil.SetGameobjectActive(this.m_imgBattleType, true);
				this.m_imgBattleType.sprite = orLoadUnitAttackTypeIcon;
			}
			else
			{
				Debug.LogError("AttackTypeIcon Not Found! unitID : " + unitTempletBase.m_UnitID.ToString());
				NKCUtil.SetGameobjectActive(this.m_imgBattleType, false);
			}
			Sprite orLoadUnitRoleIcon = NKCResourceUtility.GetOrLoadUnitRoleIcon(unitTempletBase, true);
			if (orLoadUnitRoleIcon != null)
			{
				NKCUtil.SetGameobjectActive(this.m_imgClass, true);
				this.m_imgClass.sprite = orLoadUnitRoleIcon;
			}
			else
			{
				Debug.LogError("RoleIcon Not Found! unitID : " + unitTempletBase.m_UnitID.ToString());
				NKCUtil.SetGameobjectActive(this.m_imgClass, false);
			}
			switch (unitTempletBase.m_NKM_UNIT_GRADE)
			{
			case NKM_UNIT_GRADE.NUG_N:
				this.m_imgBG.sprite = spSlotBGN;
				break;
			case NKM_UNIT_GRADE.NUG_R:
				this.m_imgBG.sprite = spSlotBGR;
				break;
			case NKM_UNIT_GRADE.NUG_SR:
				this.m_imgBG.sprite = spSlotBGSR;
				break;
			case NKM_UNIT_GRADE.NUG_SSR:
				this.m_imgBG.sprite = spSlotBGSSR;
				break;
			default:
				Debug.LogError("Unexpected unit Grade Type");
				this.m_imgBG.sprite = spSlotBGN;
				break;
			}
			NKCUIComTextUnitLevel lbLevel = this.m_lbLevel;
			if (lbLevel != null)
			{
				lbLevel.SetText(unitLevelupData.m_iLevelOld.ToString(), unitLevelupData.m_UnitData, Array.Empty<Text>());
			}
			NKCUtil.SetGameobjectActive(this.m_objLeader, false);
			NKCUtil.SetGameobjectActive(this.m_objPermanantContract, unitLevelupData.m_UnitData.IsPermanentContract);
			NKCUtil.SetGameobjectActive(this.m_objLevelUp, false);
			NKMUnitExpTemplet nkmunitExpTemplet = NKMUnitExpTemplet.FindByUnitId(unitLevelupData.m_UnitData.m_UnitID, unitLevelupData.m_iLevelOld);
			if (nkmunitExpTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objExpMax, false);
				this.m_sliderExpOld.value = 0f;
				this.m_sliderExpNew.value = 0f;
			}
			else if (nkmunitExpTemplet.m_iExpRequired == 0)
			{
				this.m_sliderExpOld.value = 1f;
				this.m_sliderExpNew.value = 1f;
				NKCUtil.SetGameobjectActive(this.m_objExpMax, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objExpMax, false);
				float value = (float)unitLevelupData.m_iExpOld / (float)nkmunitExpTemplet.m_iExpRequired;
				this.m_sliderExpOld.value = value;
				this.m_sliderExpNew.value = value;
			}
			this.m_iLastLevel = unitLevelupData.m_iLevelNew;
			this.m_iLastExp = unitLevelupData.m_iExpNew;
			this.m_bLevelUp = (unitLevelupData.m_iLevelOld != unitLevelupData.m_iLevelNew);
			this.m_lbExpGain.text = string.Format(NKCUtilString.GET_STRING_PLUS_EXP_ONE_PARAM, unitLevelupData.m_iTotalExpGain);
			NKCUtil.SetGameobjectActive(this.m_objLoyalty, false);
			this.m_eLoyalty = unitLevelupData.m_loyalty;
		}

		// Token: 0x060089FF RID: 35327 RVA: 0x002EC88E File Offset: 0x002EAA8E
		public void SetLeader(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objLeader, bValue);
		}

		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x06008A00 RID: 35328 RVA: 0x002EC89C File Offset: 0x002EAA9C
		// (set) Token: 0x06008A01 RID: 35329 RVA: 0x002EC8A4 File Offset: 0x002EAAA4
		public bool ProgressFinished { get; private set; }

		// Token: 0x06008A02 RID: 35330 RVA: 0x002EC8B0 File Offset: 0x002EAAB0
		public void StartExpProcess(int unitID, int oldLevel, int oldExp, int newLevel, int newExp, int expGain)
		{
			this.m_UnitID = unitID;
			this.ProgressFinished = false;
			this.m_ExpFxOrgPosition = this.m_objExpFX.transform.localPosition;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			NKCUtil.SetAwakenFX(this.m_animAwakenFX, unitTempletBase);
			base.StartCoroutine(this.ExpProcess(unitID, oldLevel, oldExp, newLevel, newExp, expGain));
		}

		// Token: 0x06008A03 RID: 35331 RVA: 0x002EC90A File Offset: 0x002EAB0A
		private IEnumerator ExpProcess(int unitID, int oldLevel, int oldExp, int newLevel, int newExp, int expGain)
		{
			this.m_iLastLevel = newLevel;
			this.m_iLastExp = newExp;
			this.m_bLevelUp = (oldLevel != newLevel);
			this.ProgressFinished = false;
			NKMUnitExpTemplet nkmunitExpTemplet = NKMUnitExpTemplet.FindByUnitId(unitID, oldLevel);
			if (nkmunitExpTemplet.m_iExpRequired == 0)
			{
				this.Finish();
				yield break;
			}
			float beginValue = (float)oldExp / (float)nkmunitExpTemplet.m_iExpRequired;
			this.m_sliderExpOld.value = beginValue;
			this.m_sliderExpNew.value = beginValue;
			this.m_objExpFX.transform.localPosition = new Vector3(this.m_sliderExpNew.transform.localPosition.x + this.m_sliderExpNew.fillRect.GetWidth() * this.m_sliderExpNew.value, this.m_ExpFxOrgPosition.y, this.m_ExpFxOrgPosition.z);
			NKCUtil.SetGameobjectActive(this.m_objLevelUp, false);
			NKMUnitExpTemplet nkmunitExpTemplet2 = NKMUnitExpTemplet.FindByUnitId(unitID, newLevel);
			bool flag = nkmunitExpTemplet2.m_iExpRequired == 0;
			float endValue = (float)(newLevel - oldLevel);
			if (!flag)
			{
				endValue += (float)newExp / (float)nkmunitExpTemplet2.m_iExpRequired;
			}
			float totalTime = (endValue - beginValue) * 0.6f;
			float deltaTime = 0f;
			int currentLevelUpCount = 0;
			while (deltaTime < totalTime)
			{
				float num = NKCUtil.TrackValue(TRACKING_DATA_TYPE.TDT_SLOWER, beginValue, endValue, deltaTime, totalTime);
				int num2 = (int)num;
				if (currentLevelUpCount != num2)
				{
					currentLevelUpCount = num2;
					this.m_sliderExpOld.value = 0f;
					this.m_objLevelUp.SetActive(false);
					this.m_objLevelUp.SetActive(true);
					int num3 = oldLevel + currentLevelUpCount;
					this.m_lbLevel.text = num3.ToString();
				}
				this.m_sliderExpNew.value = num % 1f;
				this.m_objExpFX.transform.localPosition = new Vector3(this.m_sliderExpNew.transform.localPosition.x + this.m_sliderExpNew.fillRect.GetWidth() * this.m_sliderExpNew.value, this.m_ExpFxOrgPosition.y, this.m_ExpFxOrgPosition.z);
				deltaTime += Time.deltaTime;
				yield return null;
			}
			NKCUtil.SetGameobjectActive(this.m_objLoyalty, this.m_eLoyalty > NKCUIResultSubUIUnitExp.UNIT_LOYALTY.None);
			if (this.m_eLoyalty == NKCUIResultSubUIUnitExp.UNIT_LOYALTY.Up)
			{
				this.m_aniLoyalty.Play("AB_UI_NKM_UI_RESULT_LOYALTY_UP_BASE");
			}
			else if (this.m_eLoyalty == NKCUIResultSubUIUnitExp.UNIT_LOYALTY.Down)
			{
				this.m_aniLoyalty.Play("AB_UI_NKM_UI_RESULT_LOYALTY_DOWN_BASE");
			}
			yield return null;
			this.Finish();
			yield break;
		}

		// Token: 0x06008A04 RID: 35332 RVA: 0x002EC940 File Offset: 0x002EAB40
		public void Finish()
		{
			this.ProgressFinished = true;
			if (!base.gameObject.activeSelf)
			{
				return;
			}
			base.StopAllCoroutines();
			this.m_lbLevel.text = this.m_iLastLevel.ToString();
			if (this.m_bLevelUp)
			{
				this.m_sliderExpOld.value = 0f;
				NKCUtil.SetGameobjectActive(this.m_objLevelUp, true);
			}
			NKMUnitExpTemplet nkmunitExpTemplet = NKMUnitExpTemplet.FindByUnitId(this.m_UnitID, this.m_iLastLevel);
			if (nkmunitExpTemplet == null || nkmunitExpTemplet.m_iExpRequired == 0)
			{
				this.m_sliderExpNew.value = 1f;
				NKCUtil.SetGameobjectActive(this.m_objExpMax, true);
			}
			else
			{
				float value = (float)this.m_iLastExp / (float)nkmunitExpTemplet.m_iExpRequired;
				this.m_sliderExpNew.value = value;
				NKCUtil.SetGameobjectActive(this.m_objExpMax, false);
			}
			this.m_objExpFX.transform.localPosition = new Vector3(this.m_sliderExpNew.transform.localPosition.x + this.m_sliderExpNew.fillRect.GetWidth() * this.m_sliderExpNew.value, this.m_ExpFxOrgPosition.y, this.m_ExpFxOrgPosition.z);
			NKCUtil.SetGameobjectActive(this.m_objLoyalty, this.m_eLoyalty > NKCUIResultSubUIUnitExp.UNIT_LOYALTY.None);
			if (this.m_eLoyalty == NKCUIResultSubUIUnitExp.UNIT_LOYALTY.Up)
			{
				if (!this.m_aniLoyalty.GetCurrentAnimatorStateInfo(0).IsName("AB_UI_NKM_UI_RESULT_LOYALTY_UP_BASE"))
				{
					this.m_aniLoyalty.Play("AB_UI_NKM_UI_RESULT_LOYALTY_UP_BASE");
					return;
				}
			}
			else if (this.m_eLoyalty == NKCUIResultSubUIUnitExp.UNIT_LOYALTY.Down && !this.m_aniLoyalty.GetCurrentAnimatorStateInfo(0).IsName("AB_UI_NKM_UI_RESULT_LOYALTY_DOWN_BASE"))
			{
				this.m_aniLoyalty.Play("AB_UI_NKM_UI_RESULT_LOYALTY_DOWN_BASE");
			}
		}

		// Token: 0x0400767D RID: 30333
		[Header("Common")]
		public Image m_imgBG;

		// Token: 0x0400767E RID: 30334
		public Image m_imgUnitFace;

		// Token: 0x0400767F RID: 30335
		public NKCUIComTextUnitLevel m_lbLevel;

		// Token: 0x04007680 RID: 30336
		public Image m_imgBattleType;

		// Token: 0x04007681 RID: 30337
		public Image m_imgClass;

		// Token: 0x04007682 RID: 30338
		public GameObject m_objLeader;

		// Token: 0x04007683 RID: 30339
		public GameObject m_objPermanantContract;

		// Token: 0x04007684 RID: 30340
		[Header("Exp")]
		public Slider m_sliderExpOld;

		// Token: 0x04007685 RID: 30341
		public Slider m_sliderExpNew;

		// Token: 0x04007686 RID: 30342
		public Text m_lbExpGain;

		// Token: 0x04007687 RID: 30343
		public GameObject m_objLevelUp;

		// Token: 0x04007688 RID: 30344
		public GameObject m_objExpFX;

		// Token: 0x04007689 RID: 30345
		public GameObject m_objExpMax;

		// Token: 0x0400768A RID: 30346
		[Header("애사심 증감")]
		public GameObject m_objLoyalty;

		// Token: 0x0400768B RID: 30347
		public Animator m_aniLoyalty;

		// Token: 0x0400768C RID: 30348
		[Header("각성 Fx")]
		public Animator m_animAwakenFX;

		// Token: 0x0400768D RID: 30349
		private int m_iLastLevel;

		// Token: 0x0400768E RID: 30350
		private int m_iLastExp;

		// Token: 0x0400768F RID: 30351
		private bool m_bLevelUp;

		// Token: 0x04007690 RID: 30352
		private Vector3 m_ExpFxOrgPosition;

		// Token: 0x04007691 RID: 30353
		private NKCUIResultSubUIUnitExp.UNIT_LOYALTY m_eLoyalty;

		// Token: 0x04007692 RID: 30354
		private const string LOYALTY_UP_ANI_NAME = "AB_UI_NKM_UI_RESULT_LOYALTY_UP_BASE";

		// Token: 0x04007693 RID: 30355
		private const string LOYALTY_DOWN_ANI_NAME = "AB_UI_NKM_UI_RESULT_LOYALTY_DOWN_BASE";

		// Token: 0x04007695 RID: 30357
		private int m_UnitID;
	}
}
