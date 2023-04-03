using System;
using System.Collections;
using System.Collections.Generic;
using ClientPacket.Common;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Result
{
	// Token: 0x02000B99 RID: 2969
	public class NKCUIPopupModuleResult : NKCUIBase
	{
		// Token: 0x0600890C RID: 35084 RVA: 0x002E58A0 File Offset: 0x002E3AA0
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupModuleResult.m_loadedUIData != null)
			{
				NKCUIPopupModuleResult.m_loadedUIData.CloseInstance();
				NKCUIPopupModuleResult.m_loadedUIData = null;
			}
		}

		// Token: 0x0600890D RID: 35085 RVA: 0x002E58BC File Offset: 0x002E3ABC
		public static NKCUIPopupModuleResult MakeInstance(string bundleName, string assetName)
		{
			if (NKCUIPopupModuleResult.m_loadedUIData == null)
			{
				NKCUIPopupModuleResult.m_loadedUIData = NKCUIManager.OpenNewInstance<NKCUIPopupModuleResult>(bundleName, assetName, NKCUIManager.eUIBaseRect.UIOverlay, null);
			}
			if (NKCUIPopupModuleResult.m_loadedUIData == null)
			{
				return null;
			}
			NKCUIPopupModuleResult instance = NKCUIPopupModuleResult.m_loadedUIData.GetInstance<NKCUIPopupModuleResult>();
			if (null == instance)
			{
				return null;
			}
			instance.Init();
			return instance;
		}

		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x0600890E RID: 35086 RVA: 0x002E5904 File Offset: 0x002E3B04
		public override string MenuName
		{
			get
			{
				return "NKCUIPopupModuleResult ���â";
			}
		}

		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x0600890F RID: 35087 RVA: 0x002E590B File Offset: 0x002E3B0B
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x06008910 RID: 35088 RVA: 0x002E590E File Offset: 0x002E3B0E
		public override void CloseInternal()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06008911 RID: 35089 RVA: 0x002E592C File Offset: 0x002E3B2C
		public void Init()
		{
			foreach (NKCUISlot nkcuislot in this.m_lstResult)
			{
				nkcuislot.Init();
			}
			NKCUIComStateButton[] csbtnBack = this.m_csbtnBack;
			for (int i = 0; i < csbtnBack.Length; i++)
			{
				NKCUtil.SetBindFunction(csbtnBack[i], new UnityAction(this.OnClose));
			}
		}

		// Token: 0x06008912 RID: 35090 RVA: 0x002E59A8 File Offset: 0x002E3BA8
		public override void OnBackButton()
		{
			this.OnClose();
		}

		// Token: 0x06008913 RID: 35091 RVA: 0x002E59B0 File Offset: 0x002E3BB0
		public void Open(NKMRewardData reward, UnityAction closeCallBack)
		{
			this.dClose = closeCallBack;
			NKCUtil.SetGameobjectActive(this.m_objMiscReward, false);
			if (reward.MiscItemDataList != null && reward.MiscItemDataList.Count > 0)
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(reward.MiscItemDataList[0].ItemID);
				if (itemMiscTempletByID != null)
				{
					string msg = string.Format(NKCUtilString.GET_STRING_MODULE_CONTRACT_MILEAGE_POINT_DESC_02, itemMiscTempletByID.GetItemName(), reward.MiscItemDataList[0].TotalCount);
					NKCUtil.SetLabelText(this.m_lbMiscRewardDesc, msg);
					Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(itemMiscTempletByID);
					NKCUtil.SetImageSprite(this.m_imgMiscRewardIcon, orLoadMiscItemSmallIcon, false);
					NKCUtil.SetGameobjectActive(this.m_objMiscReward, true);
				}
			}
			List<NKMUnitData> unitDataList = reward.UnitDataList;
			unitDataList.Sort(delegate(NKMUnitData x, NKMUnitData y)
			{
				if (x.GetUnitGrade() < y.GetUnitGrade())
				{
					return 1;
				}
				if (x.GetUnitGrade() > y.GetUnitGrade())
				{
					return -1;
				}
				return 0;
			});
			for (int i = 0; i < this.m_lstResult.Count; i++)
			{
				if (!(this.m_lstResult[i] == null))
				{
					if (i >= unitDataList.Count)
					{
						NKCUtil.SetGameobjectActive(this.m_lstResult[i], false);
					}
					else
					{
						NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeUnitData(unitDataList[i]);
						if (slotData != null)
						{
							this.m_lstResult[i].SetData(slotData, true, null);
						}
						NKCUtil.SetGameobjectActive(this.m_lstResult[i], true);
					}
				}
			}
			this.OnPlayAni("INTRO");
			base.UIOpened(true);
		}

		// Token: 0x06008914 RID: 35092 RVA: 0x002E5B20 File Offset: 0x002E3D20
		public void Open(NKMRewardData rewardData, NKMAdditionalReward additionalReward, UnityAction closeCallBack)
		{
			this.dClose = closeCallBack;
			List<NKCUISlot.SlotData> list = new List<NKCUISlot.SlotData>();
			if (rewardData != null)
			{
				list.AddRange(NKCUISlot.MakeSlotDataListFromReward(rewardData, false, false));
			}
			if (additionalReward != null)
			{
				list.AddRange(NKCUISlot.MakeSlotDataListFromReward(additionalReward));
			}
			for (int i = 0; i < this.m_lstResult.Count; i++)
			{
				if (!(this.m_lstResult[i] == null))
				{
					if (i >= list.Count)
					{
						NKCUtil.SetGameobjectActive(this.m_lstResult[i], false);
					}
					else
					{
						if (list[i] != null)
						{
							this.m_lstResult[i].SetData(list[i], true, null);
						}
						NKCUtil.SetGameobjectActive(this.m_lstResult[i], true);
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objMiscReward, false);
			this.OnPlayAni("INTRO");
			base.UIOpened(true);
		}

		// Token: 0x06008915 RID: 35093 RVA: 0x002E5BF4 File Offset: 0x002E3DF4
		private void OnClose()
		{
			this.OnPlayAni("OUTRO");
		}

		// Token: 0x06008916 RID: 35094 RVA: 0x002E5C01 File Offset: 0x002E3E01
		private void OnPlayAni(string trigger)
		{
			this.m_Ani.SetTrigger(trigger);
			if (string.Equals(trigger, "OUTRO"))
			{
				base.StartCoroutine(this.PlayCoroutine());
			}
		}

		// Token: 0x06008917 RID: 35095 RVA: 0x002E5C2C File Offset: 0x002E3E2C
		private bool AnimatorIsPlaying()
		{
			return 1f > this.m_Ani.GetCurrentAnimatorStateInfo(0).normalizedTime;
		}

		// Token: 0x06008918 RID: 35096 RVA: 0x002E5C54 File Offset: 0x002E3E54
		private IEnumerator PlayCoroutine()
		{
			yield return new WaitForSeconds(0.3f);
			if (this.AnimatorIsPlaying())
			{
				yield return null;
			}
			this.OnExit();
			yield break;
		}

		// Token: 0x06008919 RID: 35097 RVA: 0x002E5C63 File Offset: 0x002E3E63
		private void OnExit()
		{
			UnityAction unityAction = this.dClose;
			if (unityAction != null)
			{
				unityAction();
			}
			base.Close();
			base.StopAllCoroutines();
		}

		// Token: 0x0600891A RID: 35098 RVA: 0x002E5C82 File Offset: 0x002E3E82
		public override void OnHotkeyHold(HotkeyEventType hotkey)
		{
			if (hotkey == HotkeyEventType.Skip)
			{
				if (!NKCUIManager.IsTopmostUI(this))
				{
					return;
				}
				this.m_Ani.SetTrigger("SKIP");
			}
		}

		// Token: 0x04007585 RID: 30085
		private static NKCUIManager.LoadedUIData m_loadedUIData;

		// Token: 0x04007586 RID: 30086
		public List<NKCUISlot> m_lstResult = new List<NKCUISlot>();

		// Token: 0x04007587 RID: 30087
		public NKCUIComStateButton[] m_csbtnBack;

		// Token: 0x04007588 RID: 30088
		public Animator m_Ani;

		// Token: 0x04007589 RID: 30089
		[Header("Misc ����")]
		public GameObject m_objMiscReward;

		// Token: 0x0400758A RID: 30090
		public Image m_imgMiscRewardIcon;

		// Token: 0x0400758B RID: 30091
		public Text m_lbMiscRewardDesc;

		// Token: 0x0400758C RID: 30092
		private UnityAction dClose;

		// Token: 0x0400758D RID: 30093
		private const string TRIGGER_INTRO = "INTRO";

		// Token: 0x0400758E RID: 30094
		private const string TRIGGER_OUTRO = "OUTRO";
	}
}
