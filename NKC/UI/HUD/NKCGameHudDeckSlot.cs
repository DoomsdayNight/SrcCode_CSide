using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.HUD
{
	// Token: 0x02000C3F RID: 3135
	public class NKCGameHudDeckSlot : MonoBehaviour
	{
		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x06009234 RID: 37428 RVA: 0x0031E2FF File Offset: 0x0031C4FF
		// (set) Token: 0x06009235 RID: 37429 RVA: 0x0031E307 File Offset: 0x0031C507
		public NKMUnitData m_UnitData { get; private set; }

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x06009236 RID: 37430 RVA: 0x0031E310 File Offset: 0x0031C510
		// (set) Token: 0x06009237 RID: 37431 RVA: 0x0031E318 File Offset: 0x0031C518
		public bool RespawnReady { get; set; }

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x06009238 RID: 37432 RVA: 0x0031E321 File Offset: 0x0031C521
		// (set) Token: 0x06009239 RID: 37433 RVA: 0x0031E329 File Offset: 0x0031C529
		public bool RetreatReady { get; set; }

		// Token: 0x0600923B RID: 37435 RVA: 0x0031E35C File Offset: 0x0031C55C
		public void InitUI(NKCGameHud cNKCGameHud, int index)
		{
			this.m_NKCGameHud = cNKCGameHud;
			this.m_Index = index;
			if (this.m_Index < 4 || this.m_Index == 5)
			{
				this.m_eventTrigger = base.GetComponentInChildren<EventTrigger>();
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.BeginDrag;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCSystemEvent.UI_HUD_DECK_DRAG_BEGIN(eventData);
				});
				this.m_eventTrigger.triggers.Add(entry);
				entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.Drag;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCSystemEvent.UI_HUD_DECK_DRAG(eventData);
				});
				this.m_eventTrigger.triggers.Add(entry);
				entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.EndDrag;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCSystemEvent.UI_HUD_DECK_DRAG_END(eventData);
				});
				this.m_eventTrigger.triggers.Add(entry);
				entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerDown;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCSystemEvent.UI_HUD_DECK_DOWN(this.m_Index);
				});
				this.m_eventTrigger.triggers.Add(entry);
				entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerUp;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCSystemEvent.UI_HUD_DECK_UP(this.m_Index);
				});
				this.m_eventTrigger.triggers.Add(entry);
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnRetreat, new UnityAction(this.UnitRetreat));
		}

		// Token: 0x0600923C RID: 37436 RVA: 0x0031E4F4 File Offset: 0x0031C6F4
		public void Init()
		{
			this.m_UnitData = null;
			this.m_UnitTemplet = null;
			this.m_imgUnitPanel.sprite = null;
			this.m_imgUnitGrayPanel.sprite = null;
			NKCUtil.SetImageSprite(this.m_imgUnitAddPanel, null, false);
			NKCUtil.SetGameobjectActive(this.m_imgUnitAddPanel, false);
			this.SetAuto(false);
			this.SetAssist(false);
			NKCUtil.SetGameobjectActive(this.m_objTouchBorder, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnRetreat, false);
			NKCUtil.SetGameobjectActive(this.m_objInBattle, false);
			this.RespawnReady = false;
			this.RetreatReady = false;
			this.m_fRespawnCostNow = 0f;
			this.m_bCanRespawn = true;
			this.m_bEventControl = false;
			this.m_TrackScale.SetNowValue(1f);
			this.m_TouchScale.SetNowValue(1f);
			this.m_TrackPosX.SetNowValue(this.m_rtSubRoot.anchoredPosition.x);
			if (this.m_SkillCoolTime != null)
			{
				this.m_SkillCoolTime.SetSkillCoolVisible(false);
				this.m_SkillCoolTime.SetHyperCoolVisible(false);
			}
			Color color = this.m_imgBG.color;
			color.r = 1f;
			color.g = 1f;
			color.b = 1f;
			this.m_imgBG.color = color;
		}

		// Token: 0x0600923D RID: 37437 RVA: 0x0031E636 File Offset: 0x0031C836
		public void SetEnemy(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objCardEnemy, bSet);
		}

		// Token: 0x0600923E RID: 37438 RVA: 0x0031E644 File Offset: 0x0031C844
		public void SetAuto(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objAuto, bSet);
		}

		// Token: 0x0600923F RID: 37439 RVA: 0x0031E652 File Offset: 0x0031C852
		public void SetAssist(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objAssist, bSet);
		}

		// Token: 0x06009240 RID: 37440 RVA: 0x0031E660 File Offset: 0x0031C860
		public void SetEmpty(bool bEmpty)
		{
			NKCUtil.SetGameobjectActive(this.m_imgBG, !bEmpty);
			NKCUtil.SetGameobjectActive(this.m_imgBGEmpty, bEmpty);
			NKCUtil.SetGameobjectActive(this.m_imgUnitPanel, !bEmpty);
			NKCUtil.SetGameobjectActive(this.m_imgUnitAddPanel, !bEmpty);
			NKCUtil.SetGameobjectActive(this.m_imgUnitGrayPanel, !bEmpty);
			this.SetAuto(false);
			this.SetAssist(false);
			NKCUtil.SetGameobjectActive(this.m_objSummonCool, !bEmpty);
			NKCUtil.SetGameobjectActive(this.m_objCostRoot, !bEmpty);
			NKCUtil.SetGameobjectActive(this.m_lbCost, !bEmpty);
			NKCUtil.SetGameobjectActive(this.m_objBorderCombo, false);
			if (bEmpty)
			{
				this.m_comUnitLevel.text = "";
				NKCUtil.SetGameobjectActive(this.m_imgUnitRole, false);
				NKCUtil.SetGameobjectActive(this.m_imgAttackType, false);
				this.m_imgUnitRole.sprite = null;
				this.m_imgAttackType.sprite = null;
				if (this.m_SkillCoolTime != null)
				{
					this.m_SkillCoolTime.SetSkillCoolVisible(false);
					this.m_SkillCoolTime.SetHyperCoolVisible(false);
				}
			}
		}

		// Token: 0x06009241 RID: 37441 RVA: 0x0031E768 File Offset: 0x0031C968
		public void SetDeckSprite(NKMUnitData unitData, bool bLeader, bool bAssist, bool bAutoRespawn, float fTime = 0.3f)
		{
			if (unitData == null)
			{
				this.SetEmpty(true);
				this.Init();
				return;
			}
			this.m_UnitTemplet = NKMUnitManager.GetUnitTemplet(unitData.m_UnitID);
			if (this.m_UnitTemplet == null)
			{
				this.SetEmpty(true);
				this.Init();
				return;
			}
			this.SetEmpty(false);
			NKCUtil.SetGameobjectActive(this.m_imgUnitAddPanel, false);
			this.SetAuto(bAutoRespawn);
			this.SetAssist(bAssist);
			NKCUtil.SetGameobjectActive(this.m_objCostBGGray, bAssist);
			this.m_UnitData = unitData;
			NKCAssetResourceData unitResource = NKCResourceUtility.GetUnitResource(NKCResourceUtility.eUnitResourceType.INVEN_ICON, this.m_UnitData);
			if (unitResource == null)
			{
				NKCGameHud nkcgameHud = this.m_NKCGameHud;
				if (nkcgameHud != null)
				{
					nkcgameHud.LoadUnitDeck(this.m_UnitData, false);
				}
				unitResource = NKCResourceUtility.GetUnitResource(NKCResourceUtility.eUnitResourceType.INVEN_ICON, this.m_UnitData);
			}
			if (unitResource != null)
			{
				this.m_imgUnitPanel.sprite = unitResource.GetAsset<Sprite>();
				this.m_imgUnitAddPanel.sprite = unitResource.GetAsset<Sprite>();
				this.m_imgUnitGrayPanel.sprite = unitResource.GetAsset<Sprite>();
			}
			if (this.m_NKCGameHud != null)
			{
				int respawnCost = this.m_NKCGameHud.GetRespawnCost(this.m_UnitTemplet.m_StatTemplet, bLeader);
				NKCUtil.SetLabelText(this.m_lbCost, respawnCost.ToString());
				if (bLeader)
				{
					if (this.m_NKCGameHud.IsBanUnit(this.m_UnitTemplet.m_UnitTempletBase.m_UnitID))
					{
						this.m_lbCost.color = new Color(1f, 0.3f, 0.3f);
					}
					else if (this.m_NKCGameHud.IsUpUnit(this.m_UnitTemplet.m_UnitTempletBase.m_UnitID))
					{
						this.m_lbCost.color = new Color(0f, 1f, 1f);
					}
					else
					{
						this.m_lbCost.color = new Color(1f, 0.8039f, 0.02745f);
					}
				}
				else if (this.m_NKCGameHud.IsBanUnit(this.m_UnitTemplet.m_UnitTempletBase.m_UnitID))
				{
					this.m_lbCost.color = new Color(1f, 0.3f, 0.3f);
				}
				else if (this.m_NKCGameHud.IsUpUnit(this.m_UnitTemplet.m_UnitTempletBase.m_UnitID))
				{
					this.m_lbCost.color = new Color(0f, 1f, 1f);
				}
				else
				{
					this.m_lbCost.color = new Color(1f, 1f, 1f);
				}
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbCost, "");
			}
			this.m_comUnitLevel.SetLevel(this.m_UnitData, 0, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, Array.Empty<Text>());
			NKCUtil.SetGameobjectActive(this.m_imgUnitRole, true);
			NKCUtil.SetGameobjectActive(this.m_imgAttackType, true);
			this.m_imgUnitRole.sprite = NKCResourceUtility.GetOrLoadUnitRoleIcon(this.m_UnitTemplet.m_UnitTempletBase, true);
			this.m_imgAttackType.sprite = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(this.m_UnitTemplet.m_UnitTempletBase, true);
			if (fTime > 0f)
			{
				this.m_TrackPosX.SetNowValue(-1000f);
				this.m_TrackPosX.SetTracking(0f, fTime, TRACKING_DATA_TYPE.TDT_SLOWER);
				Vector2 anchoredPosition = this.m_rtSubRoot.anchoredPosition;
				anchoredPosition.y = 0f;
				this.SetAnchoredPos(anchoredPosition);
				this.m_TrackScale.SetNowValue(0f);
				this.m_TrackScale.SetTracking(1f, fTime, TRACKING_DATA_TYPE.TDT_SLOWER);
			}
			if (this.m_SkillCoolTime != null)
			{
				this.m_SkillCoolTime.SetUnit(this.m_UnitTemplet, this.m_UnitData);
			}
		}

		// Token: 0x06009242 RID: 37442 RVA: 0x0031EAE2 File Offset: 0x0031CCE2
		public void SetDeckUnitLevel(NKMUnitData unitData, int buffUnitLevel)
		{
			this.m_comUnitLevel.SetLevel(this.m_UnitData, buffUnitLevel, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, Array.Empty<Text>());
		}

		// Token: 0x06009243 RID: 37443 RVA: 0x0031EB00 File Offset: 0x0031CD00
		public void SetDeckData(float fRespawnCostNow, bool bCanRespawn, bool bLeader, float fSkillCoolNow, float fSkillCoolMax, float fHyperSkillCoolNow, float fHyperSkillMax, NKMTacticalCombo cNKMTacticalComboGoal)
		{
			if (this.m_UnitTemplet == null)
			{
				return;
			}
			this.m_fRespawnCostNow = fRespawnCostNow;
			this.m_bCanRespawn = bCanRespawn;
			int num = 0;
			if (this.m_NKCGameHud != null)
			{
				if (bLeader)
				{
					num = this.m_NKCGameHud.GetRespawnCost(this.m_UnitTemplet.m_StatTemplet, true);
				}
				else
				{
					num = this.m_NKCGameHud.GetRespawnCost(this.m_UnitTemplet.m_StatTemplet, false);
				}
			}
			this.m_fRespawnCostRateNow = this.m_fRespawnCostNow / (float)num;
			if (this.m_fRespawnCostRateNow > 1f)
			{
				this.m_fRespawnCostRateNow = 1f;
			}
			this.m_imgSummonCool.fillAmount = this.m_fRespawnCostRateNow;
			if (!this.m_bCanRespawn || this.m_fRespawnCostRateNow < 1f)
			{
				NKCUtil.SetGameobjectActive(this.m_imgUnitPanel, false);
				NKCUtil.SetGameobjectActive(this.m_imgUnitGrayPanel, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_imgUnitPanel, true);
				NKCUtil.SetGameobjectActive(this.m_imgUnitGrayPanel, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objSummonCool, this.m_fRespawnCostRateNow < 1f);
			Color color = this.m_imgBG.color;
			if (this.CanRespawn())
			{
				if (color.r < 1f && this.m_animatorUnitCardRoot.gameObject.activeInHierarchy)
				{
					this.m_animatorUnitCardRoot.Play("READY", -1, 0f);
				}
				color.r = 1f;
				color.g = 1f;
				color.b = 1f;
			}
			else
			{
				color.r = 0.5f;
				color.g = 0.5f;
				color.b = 0.5f;
			}
			this.m_imgBG.color = color;
			if (!this.m_bCanRespawn)
			{
				NKCUtil.SetGameobjectActive(this.m_objInBattle, true);
				if (this.m_NKCGameHud.GetSelectUnitDeckIndex() == this.m_Index)
				{
					if (this.m_UnitData != null && this.m_NKCGameHud.GetGameClient().IsGameUnitAllInBattle(this.m_UnitData.m_UnitUID) == NKM_ERROR_CODE.NEC_OK)
					{
						NKCUtil.SetGameobjectActive(this.m_csbtnRetreat, true);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_csbtnRetreat, false);
					}
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objInBattle, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnRetreat, false);
				if (this.RetreatReady)
				{
					this.UseCompleteDeck(true);
				}
			}
			if (this.m_SkillCoolTime != null)
			{
				this.m_SkillCoolTime.SetSkillCooltime(fSkillCoolNow, fSkillCoolMax);
				this.m_SkillCoolTime.SetHyperCooltime(fHyperSkillCoolNow, fHyperSkillMax);
			}
			if (cNKMTacticalComboGoal == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objBorderCombo, false);
				return;
			}
			bool bValue = cNKMTacticalComboGoal.CheckCond(this.m_UnitTemplet.m_UnitTempletBase, num);
			NKCUtil.SetGameobjectActive(this.m_objBorderCombo, bValue);
		}

		// Token: 0x06009244 RID: 37444 RVA: 0x0031ED8B File Offset: 0x0031CF8B
		public bool CanRespawn()
		{
			return this.m_fRespawnCostRateNow >= 1f && this.m_bCanRespawn;
		}

		// Token: 0x06009245 RID: 37445 RVA: 0x0031EDA8 File Offset: 0x0031CFA8
		public void MoveDeck(float fPosX, float fPosY)
		{
			float width = NKCUIManager.Get_NUF_DRAG().GetWidth();
			float height = NKCUIManager.Get_NUF_DRAG().GetHeight();
			float num = width / (float)Screen.width;
			float num2 = height / (float)Screen.height;
			fPosX = fPosX * num - 0.5f * width;
			fPosY = fPosY * num2 - 0.5f * height;
			this.m_Vec2Temp.Set(fPosX, fPosY);
			this.SetPos(this.m_Vec2Temp);
			this.m_rtSubRoot.ForceUpdateRectTransforms();
			this.m_Vec2Temp = this.m_rtSubRoot.anchoredPosition;
			float num3 = Vector2.Distance(this.m_Vec2Temp, Vector2.zero);
			Vector3 localScale = this.m_rtSubRoot.localScale;
			float num4 = (200f - num3) / 200f;
			if (num4 < 0f)
			{
				num4 = 0f;
			}
			localScale.x = num4;
			localScale.y = num4;
			this.SetScale(localScale);
		}

		// Token: 0x06009246 RID: 37446 RVA: 0x0031EE88 File Offset: 0x0031D088
		public void ReturnDeck(bool bReturnDeckActive = true)
		{
			if (this.RespawnReady)
			{
				return;
			}
			if (this.m_UnitData != null && this.m_NKCGameHud.GetGameClient().GetMyTeamData().IsAssistUnit(this.m_UnitData.m_UnitUID) && this.m_NKCGameHud.GetGameClient().GetMyTeamData().m_DeckData.GetAutoRespawnIndexAssist() == -1)
			{
				return;
			}
			this.SetActive(bReturnDeckActive, false);
			this.SetAnchoredPos(Vector2.zero);
			this.SetScale(Vector2.one);
		}

		// Token: 0x06009247 RID: 37447 RVA: 0x0031EF04 File Offset: 0x0031D104
		public void UseDeck(bool bRetreat)
		{
			this.RespawnReady = true;
			if (bRetreat)
			{
				this.RetreatReady = true;
			}
			this.SetActive(false, false);
		}

		// Token: 0x06009248 RID: 37448 RVA: 0x0031EF1F File Offset: 0x0031D11F
		public void UseCompleteDeck(bool bReturnDeckActive = true)
		{
			this.RespawnReady = false;
			this.RetreatReady = false;
			this.ReturnDeck(bReturnDeckActive);
		}

		// Token: 0x06009249 RID: 37449 RVA: 0x0031EF38 File Offset: 0x0031D138
		public void UpdateDeck(float fDeltaTime)
		{
			bool flag = this.m_TrackPosX.IsTracking();
			this.m_TrackPosX.Update(fDeltaTime);
			if (this.m_TrackPosX.IsTracking() || flag)
			{
				Vector2 anchoredPosition = this.m_rtSubRoot.anchoredPosition;
				anchoredPosition.x = this.m_TrackPosX.GetNowValue();
				this.SetAnchoredPos(anchoredPosition);
			}
			flag = this.m_TrackScale.IsTracking();
			this.m_TrackScale.Update(fDeltaTime);
			this.m_TouchScale.Update(fDeltaTime);
			if (this.m_TrackScale.IsTracking() || this.m_TouchScale.IsTracking() || flag)
			{
				Vector3 localScale = this.m_rtSubRoot.localScale;
				localScale.x = this.m_TrackScale.GetNowValue() * this.m_TouchScale.GetNowValue();
				localScale.y = this.m_TrackScale.GetNowValue() * this.m_TouchScale.GetNowValue();
				localScale.z = 1f;
				this.SetScale(localScale);
			}
		}

		// Token: 0x0600924A RID: 37450 RVA: 0x0031F030 File Offset: 0x0031D230
		public void Enable()
		{
			NKCUtil.SetGameobjectActive(this.m_imgUnitGrayPanel, false);
			NKCUtil.SetGameobjectActive(this.m_objSummonCool, false);
			Color color = this.m_imgBG.color;
			color.r = 1f;
			color.g = 1f;
			color.b = 1f;
			this.m_imgBG.color = color;
			NKCUtil.SetGameobjectActive(this.m_imgUnitPanel, true);
		}

		// Token: 0x0600924B RID: 37451 RVA: 0x0031F09D File Offset: 0x0031D29D
		public void TouchDown()
		{
			this.m_TouchScale.SetNowValue(this.m_rtSubRoot.localScale.x);
			this.m_TouchScale.SetTracking(0.9f, 0.1f, TRACKING_DATA_TYPE.TDT_SLOWER);
		}

		// Token: 0x0600924C RID: 37452 RVA: 0x0031F0D0 File Offset: 0x0031D2D0
		public void TouchUp()
		{
			this.m_TouchScale.SetNowValue(this.m_rtSubRoot.localScale.x);
			this.m_TouchScale.SetTracking(1f, 0.1f, TRACKING_DATA_TYPE.TDT_SLOWER);
		}

		// Token: 0x0600924D RID: 37453 RVA: 0x0031F104 File Offset: 0x0031D304
		public void TouchSelectUnitDeck(bool bUseTouchScale)
		{
			if (bUseTouchScale)
			{
				this.m_TouchScale.SetNowValue(this.m_rtSubRoot.localScale.x);
				this.m_TouchScale.SetTracking(1.1f, 0.1f, TRACKING_DATA_TYPE.TDT_SLOWER);
			}
			NKCUtil.SetGameobjectActive(this.m_objTouchBorder, true);
			if (!this.m_bCanRespawn && this.m_UnitData != null && this.m_NKCGameHud.GetGameClient().IsGameUnitAllInBattle(this.m_UnitData.m_UnitUID) == NKM_ERROR_CODE.NEC_OK)
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnRetreat, true);
			}
		}

		// Token: 0x0600924E RID: 37454 RVA: 0x0031F18C File Offset: 0x0031D38C
		public void TouchUnSelectUnitDeck()
		{
			this.m_TouchScale.SetNowValue(this.m_rtSubRoot.localScale.x);
			this.m_TouchScale.SetTracking(1f, 0.1f, TRACKING_DATA_TYPE.TDT_SLOWER);
			NKCUtil.SetGameobjectActive(this.m_objTouchBorder, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnRetreat, false);
		}

		// Token: 0x0600924F RID: 37455 RVA: 0x0031F1E2 File Offset: 0x0031D3E2
		public void Drag(Vector2 pos, GameObject frontObject = null)
		{
			if (frontObject != null)
			{
				this.SetParent(frontObject);
			}
			this.SetPos(pos);
		}

		// Token: 0x06009250 RID: 37456 RVA: 0x0031F1FC File Offset: 0x0031D3FC
		public void DragEnd(float fTrackingTime = 0f)
		{
			if (this.m_rtSubRoot.parent != base.transform)
			{
				this.SetParent(null);
			}
			Vector2 anchoredPosition = this.m_rtSubRoot.anchoredPosition;
			anchoredPosition.Set(0f, 0f);
			this.SetAnchoredPos(anchoredPosition);
		}

		// Token: 0x06009251 RID: 37457 RVA: 0x0031F24C File Offset: 0x0031D44C
		private void SetPos(Vector2 pos)
		{
			this.m_rtSubRoot.position = pos;
		}

		// Token: 0x06009252 RID: 37458 RVA: 0x0031F25F File Offset: 0x0031D45F
		private void SetAnchoredPos(Vector2 pos)
		{
			this.m_rtSubRoot.anchoredPosition = pos;
		}

		// Token: 0x06009253 RID: 37459 RVA: 0x0031F26D File Offset: 0x0031D46D
		private void SetScale(Vector2 scale)
		{
			this.m_rtSubRoot.localScale = scale;
		}

		// Token: 0x06009254 RID: 37460 RVA: 0x0031F280 File Offset: 0x0031D480
		private void SetScale(Vector3 scale)
		{
			this.m_rtSubRoot.localScale = scale;
		}

		// Token: 0x06009255 RID: 37461 RVA: 0x0031F290 File Offset: 0x0031D490
		public void SetActive(bool bActive, bool bEventControl = false)
		{
			if (bActive && this.m_bEventControl && !bEventControl)
			{
				return;
			}
			this.m_bEventControl = bEventControl;
			if (!bActive)
			{
				NKCUtil.SetGameobjectActive(this.m_objTouchBorder, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnRetreat, false);
				NKCUtil.SetGameobjectActive(this.m_imgUnitAddPanel, false);
			}
			NKCUtil.SetGameobjectActive(this.m_rtSubRoot, bActive);
		}

		// Token: 0x06009256 RID: 37462 RVA: 0x0031F2E6 File Offset: 0x0031D4E6
		public void SetParent(GameObject go = null)
		{
			if (go == null)
			{
				this.m_rtSubRoot.SetParent(base.transform);
				return;
			}
			this.m_rtSubRoot.SetParent(go.transform);
		}

		// Token: 0x06009257 RID: 37463 RVA: 0x0031F314 File Offset: 0x0031D514
		public RectTransform GetRectATKMark()
		{
			if (this.m_imgAttackType == null)
			{
				return null;
			}
			return this.m_imgAttackType.gameObject.GetComponent<RectTransform>();
		}

		// Token: 0x06009258 RID: 37464 RVA: 0x0031F338 File Offset: 0x0031D538
		private void UnitRetreat()
		{
			if (this.m_UnitData == null)
			{
				return;
			}
			if (this.m_NKCGameHud.GetGameClient() == null)
			{
				return;
			}
			this.m_NKCGameHud.GetGameClient().Send_Packet_GAME_UNIT_RETREAT_REQ(this.m_UnitData.m_UnitUID);
			this.m_NKCGameHud.UseDeck(this.m_Index, true);
			this.m_NKCGameHud.GetGameClient().UI_HUD_DECK_UP(this.m_Index);
		}

		// Token: 0x04007F1D RID: 32541
		private NKCGameHud m_NKCGameHud;

		// Token: 0x04007F1E RID: 32542
		private int m_Index;

		// Token: 0x04007F20 RID: 32544
		private NKMUnitTemplet m_UnitTemplet;

		// Token: 0x04007F21 RID: 32545
		[Header("메인 오브젝트")]
		public RectTransform m_rtSubRoot;

		// Token: 0x04007F22 RID: 32546
		public EventTrigger m_eventTrigger;

		// Token: 0x04007F23 RID: 32547
		public Animator m_animatorUnitCardRoot;

		// Token: 0x04007F24 RID: 32548
		[Header("Unit Image")]
		public Image m_imgUnitPanel;

		// Token: 0x04007F25 RID: 32549
		public Image m_imgUnitAddPanel;

		// Token: 0x04007F26 RID: 32550
		public Image m_imgUnitGrayPanel;

		// Token: 0x04007F27 RID: 32551
		[Header("적 표식/퇴각")]
		public GameObject m_objCardEnemy;

		// Token: 0x04007F28 RID: 32552
		public GameObject m_objTouchBorder;

		// Token: 0x04007F29 RID: 32553
		public NKCUIComStateButton m_csbtnRetreat;

		// Token: 0x04007F2A RID: 32554
		[Header("코스트")]
		public Text m_lbCost;

		// Token: 0x04007F2B RID: 32555
		public GameObject m_objCostRoot;

		// Token: 0x04007F2C RID: 32556
		public GameObject m_objCostBGGray;

		// Token: 0x04007F2D RID: 32557
		[Header("배경")]
		public RectTransform m_rtBG;

		// Token: 0x04007F2E RID: 32558
		public Image m_imgBG;

		// Token: 0x04007F2F RID: 32559
		public Image m_imgBGEmpty;

		// Token: 0x04007F30 RID: 32560
		[Header("소환 코스트 쿨타임")]
		public GameObject m_objSummonCool;

		// Token: 0x04007F31 RID: 32561
		public Image m_imgSummonCool;

		// Token: 0x04007F32 RID: 32562
		[Header("추가 정보")]
		public GameObject m_objInBattle;

		// Token: 0x04007F33 RID: 32563
		public GameObject m_objAuto;

		// Token: 0x04007F34 RID: 32564
		public GameObject m_objAssist;

		// Token: 0x04007F35 RID: 32565
		public NKCUIGameUnitSkillCooltime m_SkillCoolTime;

		// Token: 0x04007F36 RID: 32566
		[Header("유닛 정보")]
		public NKCUIComTextUnitLevel m_comUnitLevel;

		// Token: 0x04007F37 RID: 32567
		public Image m_imgUnitRole;

		// Token: 0x04007F38 RID: 32568
		public Image m_imgAttackType;

		// Token: 0x04007F39 RID: 32569
		[Header("기타")]
		public GameObject m_objBorderCombo;

		// Token: 0x04007F3A RID: 32570
		private float m_fRespawnCostNow;

		// Token: 0x04007F3B RID: 32571
		private float m_fRespawnCostRateNow;

		// Token: 0x04007F3C RID: 32572
		private bool m_bCanRespawn;

		// Token: 0x04007F3F RID: 32575
		public NKMTrackingFloat m_TrackPosX = new NKMTrackingFloat();

		// Token: 0x04007F40 RID: 32576
		public NKMTrackingFloat m_TrackScale = new NKMTrackingFloat();

		// Token: 0x04007F41 RID: 32577
		public NKMTrackingFloat m_TouchScale = new NKMTrackingFloat();

		// Token: 0x04007F42 RID: 32578
		private bool m_bEventControl;

		// Token: 0x04007F43 RID: 32579
		private Vector2 m_Vec2Temp;
	}
}
