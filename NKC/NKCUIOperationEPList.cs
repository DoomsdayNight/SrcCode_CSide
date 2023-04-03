using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009C1 RID: 2497
	public class NKCUIOperationEPList : MonoBehaviour
	{
		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06006A37 RID: 27191 RVA: 0x002273C2 File Offset: 0x002255C2
		public bool IsOpen
		{
			get
			{
				return this.m_bOpen;
			}
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06006A38 RID: 27192 RVA: 0x002273CC File Offset: 0x002255CC
		private NKCPopupAchieveRateReward NKCPopupAchieveRateReward
		{
			get
			{
				if (this.m_NKCPopupAchieveRateReward == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupAchieveRateReward>("AB_UI_NKM_UI_OPERATION", "NKM_UI_OPERATION_POPUP_MEDAL", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null);
					this.m_NKCPopupAchieveRateReward = loadedUIData.GetInstance<NKCPopupAchieveRateReward>();
					NKCPopupAchieveRateReward nkcpopupAchieveRateReward = this.m_NKCPopupAchieveRateReward;
					if (nkcpopupAchieveRateReward != null)
					{
						nkcpopupAchieveRateReward.InitUI();
					}
				}
				return this.m_NKCPopupAchieveRateReward;
			}
		}

		// Token: 0x06006A39 RID: 27193 RVA: 0x00227421 File Offset: 0x00225621
		public static NKCUIOperationEPList GetInstance()
		{
			return NKCUIOperationEPList.m_scNKCUIOperationEPList;
		}

		// Token: 0x06006A3A RID: 27194 RVA: 0x00227428 File Offset: 0x00225628
		public static NKCUIOperationEPList InitUI(GameObject _goNKM_OPERATION_Panel)
		{
			NKCUIOperationEPList.m_scNKCUIOperationEPList = _goNKM_OPERATION_Panel.transform.Find("NKM_UI_OPERATION_EP_LIST").gameObject.GetComponent<NKCUIOperationEPList>();
			for (int i = 0; i < 10; i++)
			{
				NKCUIOperationEPSlot newInstance = NKCUIOperationEPSlot.GetNewInstance(i, NKCUIOperationEPList.m_scNKCUIOperationEPList.m_rectListContent, new NKCUIOperationEPSlot.OnSelectedSlot(NKCUIOperationEPList.m_scNKCUIOperationEPList.OnSlotSelected), new NKCUIOperationEPSlot.OnPointerUp(NKCUIOperationEPList.m_scNKCUIOperationEPList.OnSlotPointerUp), new NKCUIOperationEPSlot.OnClickMedalRate(NKCUIOperationEPList.m_scNKCUIOperationEPList.OnClickMedal));
				NKCUIOperationEPList.m_scNKCUIOperationEPList.m_listNKCUIOperationEPSlot.Add(newInstance);
			}
			NKCUIOperationEPList.m_scNKCUIOperationEPList.m_ViewPortCenterX = NKCUIOperationEPList.m_scNKCUIOperationEPList.m_rectViewPort.anchoredPosition.x + NKCUIOperationEPList.m_scNKCUIOperationEPList.m_rectViewPort.sizeDelta.x / 2f;
			NKCUIOperationEPList.m_scNKCUIOperationEPList.m_fRectListContentOrgX = NKCUIOperationEPList.m_scNKCUIOperationEPList.m_rectListContent.anchoredPosition.x;
			if (NKCUIOperationEPList.m_scNKCUIOperationEPList.gameObject.activeSelf)
			{
				NKCUIOperationEPList.m_scNKCUIOperationEPList.gameObject.SetActive(false);
			}
			return NKCUIOperationEPList.m_scNKCUIOperationEPList;
		}

		// Token: 0x06006A3B RID: 27195 RVA: 0x00227530 File Offset: 0x00225730
		public void SetFirstOpen()
		{
			for (int i = 0; i < 12; i++)
			{
				this.m_arbFirstOpen[i] = true;
				this.m_arbFirstOpenSound[i] = true;
			}
			NKCUIOperationEPList.m_arLastXPos = new float[12];
		}

		// Token: 0x06006A3C RID: 27196 RVA: 0x00227568 File Offset: 0x00225768
		private void ScrollForCenter(NKCUIOperationEPSlot cNKCUIOperationEPSlot, float fTime = 0.6f)
		{
			if (cNKCUIOperationEPSlot == null)
			{
				return;
			}
			this.m_NKMTrackingFloat.SetNowValue(this.m_rectListContent.anchoredPosition.x);
			this.m_NKMTrackingFloat.SetTracking(cNKCUIOperationEPSlot.GetHalfOfWidth() + this.m_rectListContent.anchoredPosition.x + (this.m_ViewPortCenterX - (this.m_rectListContent.anchoredPosition.x + cNKCUIOperationEPSlot.GetCenterX())), fTime, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.SetSelectUI(cNKCUIOperationEPSlot);
		}

		// Token: 0x06006A3D RID: 27197 RVA: 0x002275E4 File Offset: 0x002257E4
		private void SnapToGrid()
		{
			float num = float.MaxValue;
			NKCUIOperationEPSlot nkcuioperationEPSlot = null;
			for (int i = 0; i < this.m_listNKCUIOperationEPSlot.Count; i++)
			{
				NKCUIOperationEPSlot nkcuioperationEPSlot2 = this.m_listNKCUIOperationEPSlot[i];
				if (nkcuioperationEPSlot2.IsActive() && nkcuioperationEPSlot2.m_NKMEpisodeTemplet != null)
				{
					float num2 = Mathf.Abs(this.m_ViewPortCenterX - (nkcuioperationEPSlot2.GetCenterX() - nkcuioperationEPSlot2.GetHalfOfWidth() + (this.m_rectListContent.anchoredPosition.x - this.m_fRectListContentOrgX)));
					if (num2 < num)
					{
						num = num2;
						nkcuioperationEPSlot = nkcuioperationEPSlot2;
					}
				}
			}
			if (nkcuioperationEPSlot != null)
			{
				this.ScrollForCenter(nkcuioperationEPSlot, 0.6f);
			}
		}

		// Token: 0x06006A3E RID: 27198 RVA: 0x0022767E File Offset: 0x0022587E
		public void OnSlotPointerUp()
		{
			this.m_bReserveSnapToGrid = true;
			this.m_fElapsedTimeReserveSnapToGrid = 0f;
			this.m_bDragging = false;
		}

		// Token: 0x06006A3F RID: 27199 RVA: 0x00227699 File Offset: 0x00225899
		public void OnSlotDragStart()
		{
			this.m_NKMTrackingFloat.StopTracking();
			this.m_SREPList.inertia = true;
			this.m_bDragging = true;
		}

		// Token: 0x06006A40 RID: 27200 RVA: 0x002276BC File Offset: 0x002258BC
		private void OnSlotSelected(NKMEpisodeTempletV2 _NKMEpisodeTemplet)
		{
			if (_NKMEpisodeTemplet == null)
			{
				return;
			}
			if (this.m_CandidateSlotForCenter != null && this.m_CandidateSlotForCenter.m_NKMEpisodeTemplet == _NKMEpisodeTemplet)
			{
				if (NKMEpisodeMgr.IsPossibleEpisode(NKCScenManager.GetScenManager().GetMyUserData(), _NKMEpisodeTemplet))
				{
					if (!_NKMEpisodeTemplet.IsOpen)
					{
						NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_MENU_EXCEPTION_EVENT_EXPIRED_SIMPLE", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
						return;
					}
					if (!_NKMEpisodeTemplet.IsOpenedDayOfWeek())
					{
						NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_DAILY_CHECK_DAY, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
						return;
					}
					NKCScenManager.GetScenManager().Get_SCEN_EPISODE().SetEpisodeID(_NKMEpisodeTemplet.m_EpisodeID);
					NKCScenManager.GetScenManager().Get_SCEN_EPISODE().SetFirstOpen();
					NKCScenManager.GetScenManager().Get_SCEN_EPISODE().SetReservedActID(-1);
					int firstStageID = NKCContentManager.GetFirstStageID(_NKMEpisodeTemplet, 1, EPISODE_DIFFICULTY.NORMAL);
					NKCContentManager.RemoveUnlockedContent(ContentsType.EPISODE, firstStageID, true);
				}
				else
				{
					bool flag = true;
					NKMStageTempletV2 nkmstageTempletV = null;
					if (_NKMEpisodeTemplet.GetFirstStage(1) == null)
					{
						flag = false;
					}
					if (flag)
					{
						if (NKCContentManager.IsUnlockableContents(ContentsType.EPISODE, nkmstageTempletV.Key))
						{
							NKCContentManager.ShowLockedMessagePopup(ContentsType.EPISODE, nkmstageTempletV.Key);
						}
						else
						{
							NKCPopupMessageManager.AddPopupMessage(NKCContentManager.MakeUnlockConditionString(nkmstageTempletV.m_UnlockInfo, false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
						}
					}
				}
			}
			for (int i = 0; i < this.m_listNKCUIOperationEPSlot.Count; i++)
			{
				NKCUIOperationEPSlot nkcuioperationEPSlot = this.m_listNKCUIOperationEPSlot[i];
				if (!(nkcuioperationEPSlot == this.m_CandidateSlotForCenter) && nkcuioperationEPSlot.IsActive() && nkcuioperationEPSlot.m_NKMEpisodeTemplet == _NKMEpisodeTemplet)
				{
					this.ScrollForCenter(nkcuioperationEPSlot, 0.6f);
					return;
				}
			}
		}

		// Token: 0x06006A41 RID: 27201 RVA: 0x00227830 File Offset: 0x00225A30
		private void SetSelectUI(NKCUIOperationEPSlot cNKCUIOperationEPSlotToSelect)
		{
			for (int i = 0; i < this.m_listNKCUIOperationEPSlot.Count; i++)
			{
				NKCUIOperationEPSlot nkcuioperationEPSlot = this.m_listNKCUIOperationEPSlot[i];
				if (nkcuioperationEPSlot.IsActive())
				{
					if (nkcuioperationEPSlot == cNKCUIOperationEPSlotToSelect)
					{
						nkcuioperationEPSlot.SetSelectUI(true);
					}
					else
					{
						nkcuioperationEPSlot.SetSelectUI(false);
					}
				}
			}
		}

		// Token: 0x06006A42 RID: 27202 RVA: 0x00227884 File Offset: 0x00225A84
		private void Update()
		{
			if (this.m_bOpen)
			{
				if (this.m_bDragging)
				{
					Vector2 anchoredPosition = this.m_rectListContent.anchoredPosition;
					if (anchoredPosition.x >= this.MIN_POS_X)
					{
						anchoredPosition.x = this.MIN_POS_X;
						this.m_rectListContent.anchoredPosition = anchoredPosition;
					}
					else if (anchoredPosition.x <= this.MIN_POS_X + this.ADD_MAX_POS_X_PER_COUNT * (float)(this.m_EPCount - 1))
					{
						anchoredPosition.x = this.MIN_POS_X + this.ADD_MAX_POS_X_PER_COUNT * (float)(this.m_EPCount - 1);
						this.m_rectListContent.anchoredPosition = anchoredPosition;
					}
				}
				if (this.m_bReserveSnapToGrid)
				{
					this.m_fElapsedTimeReserveSnapToGrid += Time.deltaTime;
					if (this.m_fElapsedTimeReserveSnapToGrid > 0.1f)
					{
						if (Mathf.Abs(this.m_SREPList.velocity.x) <= this.MIN_VELOCITY_TO_SNAP_TO_GRID)
						{
							this.m_SREPList.velocity = Vector2.zero;
							this.SnapToGrid();
							this.m_bReserveSnapToGrid = false;
						}
						else if (this.m_CandidateSlotForCenter != null && this.m_CandidateSlotForCenter.m_NKMEpisodeTemplet == null)
						{
							this.m_SREPList.velocity = Vector2.zero;
							this.SnapToGrid();
							this.m_bReserveSnapToGrid = false;
							this.m_SREPList.inertia = false;
						}
					}
				}
				this.m_NKMTrackingFloat.Update(Time.deltaTime);
				if (this.m_NKMTrackingFloat.IsTracking())
				{
					this.m_rectListContent.anchoredPosition = new Vector2(this.m_NKMTrackingFloat.GetNowValue(), this.m_rectListContent.anchoredPosition.y);
				}
				NKCUIOperationEPList.m_arLastXPos[(int)this.m_EPISODE_CATEGORY] = this.m_rectListContent.anchoredPosition.x;
				float num = float.MaxValue;
				NKCUIOperationEPSlot nkcuioperationEPSlot = null;
				for (int i = 0; i < this.m_listNKCUIOperationEPSlot.Count; i++)
				{
					NKCUIOperationEPSlot nkcuioperationEPSlot2 = this.m_listNKCUIOperationEPSlot[i];
					if (nkcuioperationEPSlot2.IsActive())
					{
						float num2 = Mathf.Abs(this.m_ViewPortCenterX - (nkcuioperationEPSlot2.GetCenterX() - nkcuioperationEPSlot2.GetHalfOfWidth() + (this.m_rectListContent.anchoredPosition.x - this.m_fRectListContentOrgX)));
						if (num2 < num)
						{
							num = num2;
							nkcuioperationEPSlot = nkcuioperationEPSlot2;
						}
						if (num2 > nkcuioperationEPSlot2.GetHalfOfWidth() * 2f)
						{
							nkcuioperationEPSlot2.m_RTEPSlot.localScale = new Vector3(Mathf.Max(0f, 0.9f - 0.79f * ((num2 - nkcuioperationEPSlot2.GetHalfOfWidth() * 2f) / num2)), Mathf.Max(0f, 0.9f - 0.79f * ((num2 - nkcuioperationEPSlot2.GetHalfOfWidth() * 2f) / num2)), 1f);
						}
						else if (num2 > nkcuioperationEPSlot2.GetHalfOfWidth())
						{
							nkcuioperationEPSlot2.m_RTEPSlot.localScale = new Vector3(0.9f, 0.9f, 1f);
						}
						else
						{
							nkcuioperationEPSlot2.m_RTEPSlot.localScale = new Vector3(0.9f + 0.1f * ((nkcuioperationEPSlot2.GetHalfOfWidth() - num2) / nkcuioperationEPSlot2.GetHalfOfWidth()), 0.9f + 0.1f * ((nkcuioperationEPSlot2.GetHalfOfWidth() - num2) / nkcuioperationEPSlot2.GetHalfOfWidth()), 1f);
						}
					}
				}
				if (nkcuioperationEPSlot != null && this.m_CandidateSlotForCenter != nkcuioperationEPSlot)
				{
					nkcuioperationEPSlot.transform.SetSiblingIndex(this.m_listNKCUIOperationEPSlot.Count - 1);
					nkcuioperationEPSlot.ResetPos();
					this.m_CandidateSlotForCenter = nkcuioperationEPSlot;
					if (!this.m_arbFirstOpenSound[(int)this.m_EPISODE_CATEGORY])
					{
						NKCSoundManager.PlaySound("FX_PAGE_TURN", 1f, 0f, 0f, false, 0f, false, 0f);
					}
					this.m_arbFirstOpenSound[(int)this.m_EPISODE_CATEGORY] = false;
				}
				if (Input.mouseScrollDelta.y > 0f)
				{
					this.OnSlotSelected(this.FindEpisodeFromCenter(-1));
					return;
				}
				if (Input.mouseScrollDelta.y < 0f)
				{
					this.OnSlotSelected(this.FindEpisodeFromCenter(1));
				}
			}
		}

		// Token: 0x06006A43 RID: 27203 RVA: 0x00227C68 File Offset: 0x00225E68
		public void UpdateSlots()
		{
			this.ClearEffectData();
			List<NKMEpisodeTempletV2> listNKMEpisodeTempletByCategory = NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(this.m_EPISODE_CATEGORY, true, EPISODE_DIFFICULTY.NORMAL);
			if (this.m_listNKCUIOperationEPSlot.Count < listNKMEpisodeTempletByCategory.Count + 2)
			{
				int count = this.m_listNKCUIOperationEPSlot.Count;
				for (int i = 0; i < listNKMEpisodeTempletByCategory.Count + 2 - count; i++)
				{
					NKCUIOperationEPSlot newInstance = NKCUIOperationEPSlot.GetNewInstance(count + i, NKCUIOperationEPList.m_scNKCUIOperationEPList.m_rectListContent, new NKCUIOperationEPSlot.OnSelectedSlot(NKCUIOperationEPList.m_scNKCUIOperationEPList.OnSlotSelected), new NKCUIOperationEPSlot.OnPointerUp(NKCUIOperationEPList.m_scNKCUIOperationEPList.OnSlotPointerUp), new NKCUIOperationEPSlot.OnClickMedalRate(NKCUIOperationEPList.m_scNKCUIOperationEPList.OnClickMedal));
					NKCUIOperationEPList.m_scNKCUIOperationEPList.m_listNKCUIOperationEPSlot.Add(newInstance);
				}
			}
			this.m_listNKCUIOperationEPSlot[0].SetData(null);
			this.m_listNKCUIOperationEPSlot[0].SetActive(true);
			for (int j = 0; j < this.m_listNKCUIOperationEPSlot.Count; j++)
			{
				if (j < listNKMEpisodeTempletByCategory.Count)
				{
					this.m_listNKCUIOperationEPSlot[j + 1].SetData(listNKMEpisodeTempletByCategory[j]);
					this.m_listNKCUIOperationEPSlot[j + 1].SetActive(true);
					if (listNKMEpisodeTempletByCategory[j].m_EPCategory == EPISODE_CATEGORY.EC_MAINSTREAM || listNKMEpisodeTempletByCategory[j].m_EPCategory == EPISODE_CATEGORY.EC_SIDESTORY)
					{
						int firstStageID = NKCContentManager.GetFirstStageID(listNKMEpisodeTempletByCategory[j], 1, EPISODE_DIFFICULTY.NORMAL);
						if (NKCContentManager.UnlockEffectRequired(ContentsType.EPISODE, firstStageID))
						{
							GameObject gameObject = NKCContentManager.AddUnlockedEffect(this.m_listNKCUIOperationEPSlot[j + 1].transform);
							gameObject.GetComponent<RectTransform>().offsetMax = Vector2.zero;
							gameObject.GetComponent<RectTransform>().offsetMin = Vector2.zero;
							gameObject.GetComponent<RectTransform>().GetComponentInChildren<Image>().GetComponent<RectTransform>().offsetMin += new Vector2(0f, -10f);
							this.m_lstUnlockEffect.Add(gameObject);
						}
					}
				}
				else if (j == listNKMEpisodeTempletByCategory.Count)
				{
					this.m_listNKCUIOperationEPSlot[j + 1].SetData(null);
					this.m_listNKCUIOperationEPSlot[j + 1].SetActive(true);
				}
				else if (j + 1 < this.m_listNKCUIOperationEPSlot.Count)
				{
					this.m_listNKCUIOperationEPSlot[j + 1].SetActive(false);
				}
			}
		}

		// Token: 0x06006A44 RID: 27204 RVA: 0x00227EAC File Offset: 0x002260AC
		public void Open(EPISODE_CATEGORY _EPISODE_CATEGORY = EPISODE_CATEGORY.EC_MAINSTREAM)
		{
			this.m_EPISODE_CATEGORY = _EPISODE_CATEGORY;
			List<NKMEpisodeTempletV2> listNKMEpisodeTempletByCategory = NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(this.m_EPISODE_CATEGORY, true, EPISODE_DIFFICULTY.NORMAL);
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			this.m_bOpen = true;
			this.m_bDragging = false;
			this.m_SREPList.scrollSensitivity = 0f;
			this.UpdateSlots();
			this.m_EPCount = listNKMEpisodeTempletByCategory.Count;
			Vector2 sizeDelta = this.m_rectListContent.sizeDelta;
			sizeDelta.x = (float)(listNKMEpisodeTempletByCategory.Count + 2) * 470f + 1130f;
			this.m_rectListContent.sizeDelta = sizeDelta;
			if (this.m_NKMTrackingFloat != null)
			{
				this.m_NKMTrackingFloat.StopTracking();
			}
			this.m_SREPList.velocity = new Vector2(0f, 0f);
			if (this.m_arbFirstOpen[(int)this.m_EPISODE_CATEGORY])
			{
				this.m_arbFirstOpen[(int)this.m_EPISODE_CATEGORY] = false;
				this.m_rectListContent.anchoredPosition = new Vector2(0f, this.m_rectListContent.anchoredPosition.y);
				this.ScrollForCenter(this.GetEPSlot(this.GetLatestEpisodeTemplet()), 0.01f);
				return;
			}
			NKMTrackingFloat nkmtrackingFloat = this.m_NKMTrackingFloat;
			if (nkmtrackingFloat != null)
			{
				nkmtrackingFloat.SetNowValue(NKCUIOperationEPList.m_arLastXPos[(int)this.m_EPISODE_CATEGORY]);
			}
			this.m_rectListContent.anchoredPosition = new Vector2(NKCUIOperationEPList.m_arLastXPos[(int)this.m_EPISODE_CATEGORY], this.m_rectListContent.anchoredPosition.y);
			this.SnapToGrid();
		}

		// Token: 0x06006A45 RID: 27205 RVA: 0x00228024 File Offset: 0x00226224
		private NKCUIOperationEPSlot GetEPSlot(NKMEpisodeTempletV2 cNKMEpisodeTemplet)
		{
			if (cNKMEpisodeTemplet == null)
			{
				return null;
			}
			for (int i = 0; i < this.m_listNKCUIOperationEPSlot.Count; i++)
			{
				NKCUIOperationEPSlot nkcuioperationEPSlot = this.m_listNKCUIOperationEPSlot[i];
				if (nkcuioperationEPSlot != null && nkcuioperationEPSlot.IsActive() && nkcuioperationEPSlot.m_NKMEpisodeTemplet == cNKMEpisodeTemplet)
				{
					return nkcuioperationEPSlot;
				}
			}
			return null;
		}

		// Token: 0x06006A46 RID: 27206 RVA: 0x00228078 File Offset: 0x00226278
		private NKMEpisodeTempletV2 GetLatestEpisodeTemplet()
		{
			List<NKMEpisodeTempletV2> listNKMEpisodeTempletByCategory = NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(this.m_EPISODE_CATEGORY, true, EPISODE_DIFFICULTY.HARD);
			if (listNKMEpisodeTempletByCategory.Count > 0)
			{
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				for (int i = listNKMEpisodeTempletByCategory.Count - 1; i >= 0; i--)
				{
					NKMEpisodeTempletV2 nkmepisodeTempletV = listNKMEpisodeTempletByCategory[i];
					NKMStageTempletV2 firstStage = nkmepisodeTempletV.GetFirstStage(1);
					if (NKMEpisodeMgr.CheckEpisodeMission(myUserData, firstStage))
					{
						return nkmepisodeTempletV;
					}
				}
			}
			return null;
		}

		// Token: 0x06006A47 RID: 27207 RVA: 0x002280D8 File Offset: 0x002262D8
		private void ClearEffectData()
		{
			for (int i = 0; i < this.m_lstUnlockEffect.Count; i++)
			{
				UnityEngine.Object.Destroy(this.m_lstUnlockEffect[i]);
			}
			this.m_lstUnlockEffect = new List<GameObject>();
		}

		// Token: 0x06006A48 RID: 27208 RVA: 0x00228118 File Offset: 0x00226318
		public void PreLoad()
		{
			IReadOnlyList<NKMEpisodeTempletV2> episodeTemplets = NKMEpisodeMgr.EpisodeTemplets;
			for (int i = 0; i < episodeTemplets.Count; i++)
			{
				NKMEpisodeTempletV2 nkmepisodeTempletV = episodeTemplets[i];
				if (nkmepisodeTempletV != null && nkmepisodeTempletV.m_EPCategory != EPISODE_CATEGORY.EC_COUNTERCASE && nkmepisodeTempletV.IsOpen)
				{
					NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_UI_NKM_UI_OPERATION_EP_THUMBNAIL", string.Format("EP_THUMBNAIL_{0}", nkmepisodeTempletV.m_EPThumbnail), true);
				}
			}
			NKCResourceUtility.LoadAssetResourceTemp<Material>("AB_UI_NKM_UI_OPERATION_EP_THUMBNAIL", "EP_THUMBNAIL_BLACK_AND_WHITE", true);
		}

		// Token: 0x06006A49 RID: 27209 RVA: 0x00228183 File Offset: 0x00226383
		public void Close()
		{
			this.ClearEffectData();
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			this.m_bOpen = false;
		}

		// Token: 0x06006A4A RID: 27210 RVA: 0x002281AC File Offset: 0x002263AC
		public NKCUIOperationEPSlot SetEPSlotToCenter(EPISODE_CATEGORY category, int episodeID)
		{
			List<NKMEpisodeTempletV2> listNKMEpisodeTempletByCategory = NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(category, false, EPISODE_DIFFICULTY.NORMAL);
			if (listNKMEpisodeTempletByCategory == null)
			{
				return null;
			}
			NKMEpisodeTempletV2 cNKMEpisodeTemplet = listNKMEpisodeTempletByCategory.Find((NKMEpisodeTempletV2 x) => x.m_EpisodeID == episodeID);
			NKCUIOperationEPSlot epslot = this.GetEPSlot(cNKMEpisodeTemplet);
			this.ScrollForCenter(epslot, 0.01f);
			return epslot;
		}

		// Token: 0x06006A4B RID: 27211 RVA: 0x002281FC File Offset: 0x002263FC
		private void OnClickMedal(NKMEpisodeTempletV2 episodeTemplet)
		{
			NKCPopupAchieveRateReward nkcpopupAchieveRateReward = this.NKCPopupAchieveRateReward;
			if (nkcpopupAchieveRateReward == null)
			{
				return;
			}
			nkcpopupAchieveRateReward.Open(episodeTemplet);
		}

		// Token: 0x06006A4C RID: 27212 RVA: 0x0022820F File Offset: 0x0022640F
		public bool IsLockCategory(EPISODE_CATEGORY category)
		{
			return NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(category, true, EPISODE_DIFFICULTY.NORMAL).Count == 0;
		}

		// Token: 0x06006A4D RID: 27213 RVA: 0x00228224 File Offset: 0x00226424
		public bool OnHotkey(HotkeyEventType hotkey)
		{
			if (hotkey <= HotkeyEventType.Right)
			{
				if (hotkey == HotkeyEventType.Left)
				{
					this.OnSlotSelected(this.FindEpisodeFromCenter(-1));
					return true;
				}
				if (hotkey == HotkeyEventType.Right)
				{
					this.OnSlotSelected(this.FindEpisodeFromCenter(1));
					return true;
				}
			}
			else if (hotkey != HotkeyEventType.Confirm)
			{
				if (hotkey == HotkeyEventType.ShowHotkey)
				{
					NKCUIComHotkeyDisplay.OpenInstance(this.m_SREPList.transform, new HotkeyEventType[]
					{
						HotkeyEventType.Left,
						HotkeyEventType.Right,
						HotkeyEventType.Confirm
					});
					return false;
				}
			}
			else
			{
				if (this.m_CandidateSlotForCenter != null && this.m_CandidateSlotForCenter.m_NKMEpisodeTemplet != null)
				{
					this.OnSlotSelected(this.m_CandidateSlotForCenter.m_NKMEpisodeTemplet);
					return true;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06006A4E RID: 27214 RVA: 0x002282BC File Offset: 0x002264BC
		private NKMEpisodeTempletV2 FindEpisodeFromCenter(int move)
		{
			if (this.m_CandidateSlotForCenter == null)
			{
				return null;
			}
			int num = this.m_listNKCUIOperationEPSlot.IndexOf(this.m_CandidateSlotForCenter) + move;
			if (num >= 0 && num < this.m_listNKCUIOperationEPSlot.Count)
			{
				return this.m_listNKCUIOperationEPSlot[num].m_NKMEpisodeTemplet;
			}
			return null;
		}

		// Token: 0x040055E6 RID: 21990
		private const int EXTRA_EP_COUNT = 2;

		// Token: 0x040055E7 RID: 21991
		private static NKCUIOperationEPList m_scNKCUIOperationEPList = null;

		// Token: 0x040055E8 RID: 21992
		public RectTransform m_rectListContent;

		// Token: 0x040055E9 RID: 21993
		public RectTransform m_rectViewPort;

		// Token: 0x040055EA RID: 21994
		public ScrollRect m_SREPList;

		// Token: 0x040055EB RID: 21995
		private float m_ViewPortCenterX;

		// Token: 0x040055EC RID: 21996
		private float m_fRectListContentOrgX;

		// Token: 0x040055ED RID: 21997
		private bool[] m_arbFirstOpen = new bool[12];

		// Token: 0x040055EE RID: 21998
		private bool[] m_arbFirstOpenSound = new bool[12];

		// Token: 0x040055EF RID: 21999
		private bool m_bOpen;

		// Token: 0x040055F0 RID: 22000
		private bool m_bReserveSnapToGrid;

		// Token: 0x040055F1 RID: 22001
		private float m_fElapsedTimeReserveSnapToGrid;

		// Token: 0x040055F2 RID: 22002
		private float MIN_VELOCITY_TO_SNAP_TO_GRID = 170f;

		// Token: 0x040055F3 RID: 22003
		private float MIN_POS_X = -473f;

		// Token: 0x040055F4 RID: 22004
		private float ADD_MAX_POS_X_PER_COUNT = -470f;

		// Token: 0x040055F5 RID: 22005
		private bool m_bDragging;

		// Token: 0x040055F6 RID: 22006
		private int m_EPCount;

		// Token: 0x040055F7 RID: 22007
		private List<NKCUIOperationEPSlot> m_listNKCUIOperationEPSlot = new List<NKCUIOperationEPSlot>();

		// Token: 0x040055F8 RID: 22008
		private List<GameObject> m_lstUnlockEffect = new List<GameObject>();

		// Token: 0x040055F9 RID: 22009
		private const int DEFAULT_SLOT_COUNT = 10;

		// Token: 0x040055FA RID: 22010
		private NKCUIOperationEPSlot m_CandidateSlotForCenter;

		// Token: 0x040055FB RID: 22011
		private NKMTrackingFloat m_NKMTrackingFloat = new NKMTrackingFloat();

		// Token: 0x040055FC RID: 22012
		private EPISODE_CATEGORY m_EPISODE_CATEGORY;

		// Token: 0x040055FD RID: 22013
		private static float[] m_arLastXPos = new float[12];

		// Token: 0x040055FE RID: 22014
		private NKCPopupAchieveRateReward m_NKCPopupAchieveRateReward;
	}
}
