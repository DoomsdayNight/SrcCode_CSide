using System;
using System.Collections;
using NKM;
using NKM.EventPass;
using NKM.Templet;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A49 RID: 2633
	public class NKCPopupEventPassUnlock : NKCUIBase
	{
		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x0600738A RID: 29578 RVA: 0x00266F60 File Offset: 0x00265160
		public static NKCPopupEventPassUnlock Instance
		{
			get
			{
				if (NKCPopupEventPassUnlock.m_Instance == null)
				{
					NKCPopupEventPassUnlock.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupEventPassUnlock>("AB_UI_NKM_UI_EVENT_PASS", "NKM_UI_POPUP_EVENT_PASS_SPECIAL_UNLOCK", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupEventPassUnlock.CleanupInstance)).GetInstance<NKCPopupEventPassUnlock>();
					NKCPopupEventPassUnlock instance = NKCPopupEventPassUnlock.m_Instance;
					if (instance != null)
					{
						instance.Init();
					}
				}
				return NKCPopupEventPassUnlock.m_Instance;
			}
		}

		// Token: 0x0600738B RID: 29579 RVA: 0x00266FB5 File Offset: 0x002651B5
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupEventPassUnlock.m_Instance != null && NKCPopupEventPassUnlock.m_Instance.IsOpen)
			{
				NKCPopupEventPassUnlock.m_Instance.Close();
			}
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x0600738C RID: 29580 RVA: 0x00266FDA File Offset: 0x002651DA
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupEventPassUnlock.m_Instance != null && NKCPopupEventPassUnlock.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600738D RID: 29581 RVA: 0x00266FF5 File Offset: 0x002651F5
		private static void CleanupInstance()
		{
			NKCPopupEventPassUnlock.m_Instance = null;
		}

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x0600738E RID: 29582 RVA: 0x00266FFD File Offset: 0x002651FD
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x0600738F RID: 29583 RVA: 0x00267000 File Offset: 0x00265200
		public override string MenuName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06007390 RID: 29584 RVA: 0x00267008 File Offset: 0x00265208
		private void Init()
		{
			base.gameObject.SetActive(false);
			if (this.m_eventTriggerBg != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCPopupEventPassUnlock.CheckInstanceAndClose();
				});
				this.m_eventTriggerBg.triggers.Add(entry);
			}
			NKCEventPassDataManager eventPassDataManager = NKCScenManager.GetScenManager().GetEventPassDataManager();
			if (eventPassDataManager == null)
			{
				return;
			}
			this.InitData(NKMEventPassTemplet.Find(eventPassDataManager.EventPassId));
		}

		// Token: 0x06007391 RID: 29585 RVA: 0x00267098 File Offset: 0x00265298
		public void InitData(NKMEventPassTemplet eventPassTemplet)
		{
			if (eventPassTemplet == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString(eventPassTemplet.EventPassTitleStrId, false));
			switch (eventPassTemplet.EventPassMainRewardType)
			{
			case NKM_REWARD_TYPE.RT_UNIT:
			case NKM_REWARD_TYPE.RT_OPERATOR:
			{
				this.ActivateImageCard(this.m_imgUnitCard);
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(eventPassTemplet.EventPassMainReward);
				if (unitTempletBase != null)
				{
					NKCUtil.SetImageSprite(this.m_imgUnitCard, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase), true);
					return;
				}
				break;
			}
			case NKM_REWARD_TYPE.RT_SHIP:
			{
				this.ActivateImageCard(this.m_imgShipCard);
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(eventPassTemplet.EventPassMainReward);
				if (unitTempletBase != null)
				{
					NKCUtil.SetImageSprite(this.m_imgShipCard, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase), true);
					return;
				}
				break;
			}
			case NKM_REWARD_TYPE.RT_MISC:
			{
				this.ActivateImageCard(this.m_imgEquipCard);
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(eventPassTemplet.EventPassMainReward);
				if (itemMiscTempletByID != null)
				{
					NKCUtil.SetImageSprite(this.m_imgEquipCard, NKCResourceUtility.GetOrLoadMiscItemIcon(itemMiscTempletByID), true);
				}
				break;
			}
			case NKM_REWARD_TYPE.RT_USER_EXP:
			case NKM_REWARD_TYPE.RT_BUFF:
			case NKM_REWARD_TYPE.RT_EMOTICON:
			case NKM_REWARD_TYPE.RT_MISSION_POINT:
			case NKM_REWARD_TYPE.RT_BINGO_TILE:
			case NKM_REWARD_TYPE.RT_PASS_EXP:
				break;
			case NKM_REWARD_TYPE.RT_EQUIP:
			{
				this.ActivateImageCard(this.m_imgEquipCard);
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(eventPassTemplet.EventPassMainReward);
				if (equipTemplet != null)
				{
					NKCUtil.SetImageSprite(this.m_imgEquipCard, NKCResourceUtility.GetOrLoadEquipIcon(equipTemplet), true);
					return;
				}
				break;
			}
			case NKM_REWARD_TYPE.RT_MOLD:
			{
				this.ActivateImageCard(this.m_imgEquipCard);
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(eventPassTemplet.EventPassMainReward);
				if (itemMoldTempletByID != null)
				{
					NKCUtil.SetImageSprite(this.m_imgEquipCard, NKCResourceUtility.GetOrLoadMoldIcon(itemMoldTempletByID), true);
					return;
				}
				break;
			}
			case NKM_REWARD_TYPE.RT_SKIN:
			{
				this.ActivateImageCard(this.m_imgUnitCard);
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(eventPassTemplet.EventPassMainReward);
				if (skinTemplet != null)
				{
					NKCUtil.SetImageSprite(this.m_imgUnitCard, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, skinTemplet), true);
					return;
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06007392 RID: 29586 RVA: 0x00267228 File Offset: 0x00265428
		public void Open()
		{
			base.gameObject.SetActive(true);
			base.StartCoroutine(this.ProcessTimer());
			base.UIOpened(true);
		}

		// Token: 0x06007393 RID: 29587 RVA: 0x0026724A File Offset: 0x0026544A
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007394 RID: 29588 RVA: 0x00267258 File Offset: 0x00265458
		private IEnumerator ProcessTimer()
		{
			while (this.m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
			float delayTimer = 0f;
			while (delayTimer < this.m_fCloseDelay)
			{
				delayTimer += Time.deltaTime;
				yield return null;
			}
			base.Close();
			yield break;
		}

		// Token: 0x06007395 RID: 29589 RVA: 0x00267267 File Offset: 0x00265467
		private void ActivateImageCard(Image imageCard)
		{
			NKCUtil.SetGameobjectActive(this.m_imgUnitCard, false);
			NKCUtil.SetGameobjectActive(this.m_imgShipCard, false);
			NKCUtil.SetGameobjectActive(this.m_imgEquipCard, false);
			NKCUtil.SetGameobjectActive(imageCard, true);
		}

		// Token: 0x04005F87 RID: 24455
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_EVENT_PASS";

		// Token: 0x04005F88 RID: 24456
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_EVENT_PASS_SPECIAL_UNLOCK";

		// Token: 0x04005F89 RID: 24457
		private static NKCPopupEventPassUnlock m_Instance;

		// Token: 0x04005F8A RID: 24458
		public EventTrigger m_eventTriggerBg;

		// Token: 0x04005F8B RID: 24459
		public Animator m_animator;

		// Token: 0x04005F8C RID: 24460
		public Text m_lbTitle;

		// Token: 0x04005F8D RID: 24461
		public Image m_imgUnitCard;

		// Token: 0x04005F8E RID: 24462
		public Image m_imgShipCard;

		// Token: 0x04005F8F RID: 24463
		public Image m_imgEquipCard;

		// Token: 0x04005F90 RID: 24464
		public float m_fCloseDelay;
	}
}
