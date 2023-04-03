using System;
using NKM;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.HUD
{
	// Token: 0x02000C4A RID: 3146
	public class NKCUIHudShipSkillDeck : MonoBehaviour
	{
		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x0600929F RID: 37535 RVA: 0x00320C19 File Offset: 0x0031EE19
		// (set) Token: 0x060092A0 RID: 37536 RVA: 0x00320C21 File Offset: 0x0031EE21
		public short m_GameUnitUID { get; private set; }

		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x060092A1 RID: 37537 RVA: 0x00320C2A File Offset: 0x0031EE2A
		// (set) Token: 0x060092A2 RID: 37538 RVA: 0x00320C32 File Offset: 0x0031EE32
		public NKMShipSkillTemplet m_NKMShipSkillTemplet { get; private set; }

		// Token: 0x060092A3 RID: 37539 RVA: 0x00320C3C File Offset: 0x0031EE3C
		public void InitUI(int index)
		{
			this.m_Index = index;
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.BeginDrag;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				NKCSystemEvent.UI_HUD_SHIP_SKILL_DECK_DRAG_BEGIN(eventData);
			});
			this.m_EventTrigger.triggers.Add(entry);
			entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.Drag;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				NKCSystemEvent.UI_HUD_SHIP_SKILL_DECK_DRAG(eventData);
			});
			this.m_EventTrigger.triggers.Add(entry);
			entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.EndDrag;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				NKCSystemEvent.UI_HUD_SHIP_SKILL_DECK_DRAG_END(eventData);
			});
			this.m_EventTrigger.triggers.Add(entry);
			entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerDown;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				NKCSystemEvent.UI_HUD_SHIP_SKILL_DECK_DOWN(this.m_Index, eventData);
			});
			this.m_EventTrigger.triggers.Add(entry);
			entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerUp;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				NKCSystemEvent.UI_HUD_SHIP_SKILL_DECK_UP(this.m_Index);
			});
			this.m_EventTrigger.triggers.Add(entry);
		}

		// Token: 0x060092A4 RID: 37540 RVA: 0x00320D94 File Offset: 0x0031EF94
		public void Init()
		{
			this.m_UnitUID = 0L;
			this.m_UnitID = 0;
			this.m_UnitTemplet = null;
			this.m_NKMShipSkillTemplet = null;
			NKCAssetResourceData assetResource = NKCResourceUtility.GetAssetResource("AB_UI_SHIP_SKILL_ICON", "SS_NO_SKILL_ICON");
			NKCUtil.SetGameobjectActive(this.m_imgSkill, true);
			NKCUtil.SetGameobjectActive(this.m_imgGray, true);
			if (assetResource != null)
			{
				this.m_imgSkill.sprite = assetResource.GetAsset<Sprite>();
				this.m_imgGray.sprite = assetResource.GetAsset<Sprite>();
				this.m_imgAddPanel.sprite = assetResource.GetAsset<Sprite>();
			}
			else
			{
				this.m_imgSkill.sprite = null;
				this.m_imgGray.sprite = null;
				this.m_imgAddPanel.sprite = null;
			}
			NKCUtil.SetGameobjectActive(this.m_objCooltime, false);
			this.m_TouchScale.SetNowValue(1f);
			this.m_fStateCoolTime = 0f;
			this.m_bEventControl = false;
		}

		// Token: 0x060092A5 RID: 37541 RVA: 0x00320E74 File Offset: 0x0031F074
		public void SetDeckSprite(NKMUnitData unitData, NKMShipSkillTemplet cNKMShipSkillTemplet)
		{
			if (unitData == null)
			{
				this.Init();
				return;
			}
			this.m_UnitTemplet = NKMUnitManager.GetUnitTemplet(unitData.m_UnitID);
			if (this.m_UnitTemplet == null)
			{
				this.Init();
				return;
			}
			this.m_UnitUID = unitData.m_UnitUID;
			this.m_UnitID = unitData.m_UnitID;
			if (unitData.m_listGameUnitUID.Count > 0)
			{
				this.m_GameUnitUID = unitData.m_listGameUnitUID[0];
			}
			this.SetActive(true, false);
			this.m_NKMShipSkillTemplet = cNKMShipSkillTemplet;
			if (cNKMShipSkillTemplet == null)
			{
				this.Init();
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_imgAddPanel, false);
			NKCAssetResourceData assetResource = NKCResourceUtility.GetAssetResource("AB_UI_SHIP_SKILL_ICON", cNKMShipSkillTemplet.m_ShipSkillIcon);
			if (assetResource != null)
			{
				this.m_imgSkill.sprite = assetResource.GetAsset<Sprite>();
				this.m_imgAddPanel.sprite = assetResource.GetAsset<Sprite>();
				this.m_imgGray.sprite = assetResource.GetAsset<Sprite>();
			}
			else
			{
				this.m_imgSkill.sprite = null;
				this.m_imgAddPanel.sprite = null;
				this.m_imgGray.sprite = null;
			}
			NKCUtil.SetGameobjectActive(this.m_imgSkill, true);
			NKCUtil.SetGameobjectActive(this.m_imgGray, true);
		}

		// Token: 0x060092A6 RID: 37542 RVA: 0x00320F90 File Offset: 0x0031F190
		public void UpdateShipSkillDeck(float fDeltaTime)
		{
			this.m_TouchScale.Update(fDeltaTime);
			float num = this.m_fMoveScale * this.m_TouchScale.GetNowValue();
			if (this.m_fScaleBefore != num)
			{
				this.m_fScaleBefore = num;
				this.m_Vec2Temp.Set(num, num);
				this.m_rtSubRoot.localScale = this.m_Vec2Temp;
			}
		}

		// Token: 0x060092A7 RID: 37543 RVA: 0x00320FF0 File Offset: 0x0031F1F0
		public void SetDeckData(float fStateCoolTime)
		{
			this.m_fStateCoolTime = fStateCoolTime;
			float num = 1f;
			if (this.m_NKMShipSkillTemplet != null)
			{
				if (this.m_NKMShipSkillTemplet.m_UnitStateName.Length > 1)
				{
					float num2 = 0f;
					NKCUnitClient nkcunitClient = null;
					if (NKCScenManager.GetScenManager() != null && NKCScenManager.GetScenManager().GetGameClient() != null)
					{
						nkcunitClient = (NKCUnitClient)NKCScenManager.GetScenManager().GetGameClient().GetUnit(this.m_GameUnitUID, true, true);
					}
					if (nkcunitClient != null)
					{
						num2 = nkcunitClient.GetStateMaxCoolTime(this.m_NKMShipSkillTemplet.m_UnitStateName);
					}
					else
					{
						NKMUnitState unitState = this.m_UnitTemplet.GetUnitState(this.m_NKMShipSkillTemplet.m_UnitStateName, true);
						if (unitState != null)
						{
							num2 = unitState.m_StateCoolTime.m_Max;
						}
					}
					num = 1f - fStateCoolTime / num2;
					if (num > 1f)
					{
						num = 1f;
					}
				}
				if (this.m_NKMShipSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_PASSIVE)
				{
					num = 1f;
				}
			}
			this.m_imgCooltime.fillAmount = num;
			if (fStateCoolTime > 0f || num < 1f)
			{
				NKCUtil.SetGameobjectActive(this.m_imgSkill, false);
				NKCUtil.SetGameobjectActive(this.m_imgGray, true);
			}
			else
			{
				if (!this.m_imgSkill.gameObject.activeSelf)
				{
					this.m_imgSkill.gameObject.SetActive(true);
					if (this.m_AnimatorSkillReady.gameObject.activeInHierarchy)
					{
						this.m_AnimatorSkillReady.Play("NKM_UI_GAME_SHIP_SKILL_DECK_CARD_READY", -1, 0f);
					}
				}
				NKCUtil.SetGameobjectActive(this.m_imgGray, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objCooltime, num < 1f);
		}

		// Token: 0x060092A8 RID: 37544 RVA: 0x00321173 File Offset: 0x0031F373
		public bool CanUse()
		{
			return this.m_imgCooltime.fillAmount >= 1f && this.m_fStateCoolTime <= 0f;
		}

		// Token: 0x060092A9 RID: 37545 RVA: 0x00321198 File Offset: 0x0031F398
		public void MoveShipSkillDeck(float fPosX, float fPosY)
		{
			float width = NKCUIManager.Get_NUF_DRAG().GetWidth();
			float height = NKCUIManager.Get_NUF_DRAG().GetHeight();
			float num = width / (float)Screen.width;
			float num2 = height / (float)Screen.height;
			fPosX = fPosX * num - 0.5f * width;
			fPosY = fPosY * num2 - 0.5f * height;
			this.m_Vec2Temp.Set(fPosX, fPosY);
			this.m_rtSubRoot.position = this.m_Vec2Temp;
			this.m_rtSubRoot.ForceUpdateRectTransforms();
			this.m_Vec2Temp = this.m_rtSubRoot.anchoredPosition;
			float num3 = Vector2.Distance(this.m_Vec2Temp, Vector2.zero);
			this.m_fMoveScale = (200f - num3) / 200f;
			if (this.m_fMoveScale < 0f)
			{
				this.m_fMoveScale = 0f;
			}
		}

		// Token: 0x060092AA RID: 37546 RVA: 0x00321264 File Offset: 0x0031F464
		public void ReturnShipSkillDeck()
		{
			this.SetActive(true, false);
			this.m_rtSubRoot.anchoredPosition = Vector2.zero;
			this.m_fMoveScale = 1f;
		}

		// Token: 0x060092AB RID: 37547 RVA: 0x00321289 File Offset: 0x0031F489
		public void TouchDown()
		{
			this.m_TouchScale.SetTracking(0.9f, 0.1f, TRACKING_DATA_TYPE.TDT_SLOWER);
		}

		// Token: 0x060092AC RID: 37548 RVA: 0x003212A1 File Offset: 0x0031F4A1
		public void TouchUp()
		{
			this.m_TouchScale.SetTracking(1f, 0.1f, TRACKING_DATA_TYPE.TDT_SLOWER);
		}

		// Token: 0x060092AD RID: 37549 RVA: 0x003212B9 File Offset: 0x0031F4B9
		public void TouchSelectShipSkillDeck()
		{
			this.m_TouchScale.SetTracking(1.1f, 0.1f, TRACKING_DATA_TYPE.TDT_SLOWER);
			if (this.m_NKMShipSkillTemplet != null && this.m_NKMShipSkillTemplet.m_NKM_SKILL_TYPE != NKM_SKILL_TYPE.NST_PASSIVE)
			{
				NKCUtil.SetGameobjectActive(this.m_objSelectBorder, true);
			}
		}

		// Token: 0x060092AE RID: 37550 RVA: 0x003212F3 File Offset: 0x0031F4F3
		public void TouchUnSelectShipSkillDeck()
		{
			this.m_TouchScale.SetTracking(1f, 0.1f, TRACKING_DATA_TYPE.TDT_SLOWER);
			NKCUtil.SetGameobjectActive(this.m_objSelectBorder, false);
		}

		// Token: 0x060092AF RID: 37551 RVA: 0x00321317 File Offset: 0x0031F517
		public void SetActive(bool bActive, bool bEventControl = false)
		{
			if (bActive && this.m_bEventControl && !bEventControl)
			{
				return;
			}
			this.m_bEventControl = bEventControl;
			NKCUtil.SetGameobjectActive(this.m_rtSubRoot, bActive);
		}

		// Token: 0x04007F9A RID: 32666
		private int m_Index;

		// Token: 0x04007F9B RID: 32667
		private long m_UnitUID;

		// Token: 0x04007F9C RID: 32668
		private int m_UnitID;

		// Token: 0x04007F9E RID: 32670
		private NKMUnitTemplet m_UnitTemplet;

		// Token: 0x04007FA0 RID: 32672
		[Header("����")]
		public RectTransform m_rtSubRoot;

		// Token: 0x04007FA1 RID: 32673
		public EventTrigger m_EventTrigger;

		// Token: 0x04007FA2 RID: 32674
		[Header("������")]
		public Image m_imgSkill;

		// Token: 0x04007FA3 RID: 32675
		public Image m_imgGray;

		// Token: 0x04007FA4 RID: 32676
		public Image m_imgAddPanel;

		// Token: 0x04007FA5 RID: 32677
		public Animator m_AnimatorSkillReady;

		// Token: 0x04007FA6 RID: 32678
		[Header("��Ÿ��")]
		public GameObject m_objCooltime;

		// Token: 0x04007FA7 RID: 32679
		public Image m_imgCooltime;

		// Token: 0x04007FA8 RID: 32680
		[Header("��Ÿ")]
		public GameObject m_objSelectBorder;

		// Token: 0x04007FA9 RID: 32681
		private NKMTrackingFloat m_TouchScale = new NKMTrackingFloat();

		// Token: 0x04007FAA RID: 32682
		private float m_fStateCoolTime;

		// Token: 0x04007FAB RID: 32683
		private bool m_bEventControl;

		// Token: 0x04007FAC RID: 32684
		private float m_fMoveScale;

		// Token: 0x04007FAD RID: 32685
		private float m_fScaleBefore;

		// Token: 0x04007FAE RID: 32686
		private Vector2 m_Vec2Temp;
	}
}
