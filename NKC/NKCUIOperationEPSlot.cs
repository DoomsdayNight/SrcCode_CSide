using System;
using System.Collections.Generic;
using DG.Tweening;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009C2 RID: 2498
	public class NKCUIOperationEPSlot : MonoBehaviour
	{
		// Token: 0x06006A51 RID: 27217 RVA: 0x00228397 File Offset: 0x00226597
		public void ResetPos()
		{
			this.m_RTEPSlot.anchoredPosition = new Vector2(790f + (float)this.m_Index * 470f, 0f);
		}

		// Token: 0x06006A52 RID: 27218 RVA: 0x002283C1 File Offset: 0x002265C1
		public float GetHalfOfWidth()
		{
			return this.m_fOrgHalfWidth;
		}

		// Token: 0x06006A53 RID: 27219 RVA: 0x002283C9 File Offset: 0x002265C9
		private void OnDestroy()
		{
			if (this.m_imgBG != null)
			{
				this.m_imgBG.sprite = null;
			}
			NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceData);
		}

		// Token: 0x06006A54 RID: 27220 RVA: 0x002283F0 File Offset: 0x002265F0
		public static NKCUIOperationEPSlot GetNewInstance(int index, Transform parent, NKCUIOperationEPSlot.OnSelectedSlot selectedSlot = null, NKCUIOperationEPSlot.OnPointerUp onPointerUp = null, NKCUIOperationEPSlot.OnClickMedalRate onClickMedalRate = null)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_OPERATION", "NKM_UI_OPERATION_LIST", false, null);
			if (nkcassetInstanceData == null || nkcassetInstanceData.m_Instant == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIOperationEPSlot Prefab null!");
				return null;
			}
			NKCUIOperationEPSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIOperationEPSlot>();
			if (component == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIOperationEPSlot null!");
				return null;
			}
			component.m_NKCAssetInstanceData = nkcassetInstanceData;
			component.m_Index = index;
			component.transform.SetParent(parent, false);
			component.ResetPos();
			Vector3 localScale = new Vector3(0.9f, 0.9f, 1f);
			component.m_RTEPSlot.localScale = localScale;
			component.m_fOrgHalfWidth = component.m_RTButton.sizeDelta.x / 2f;
			component.ResetPos();
			component.SetOnSelectedSlot(selectedSlot);
			component.SetOnPointerUp(null);
			component.m_OnClickMedalRate = onClickMedalRate;
			component.m_btnMedal.PointerClick.RemoveAllListeners();
			component.m_btnMedal.PointerClick.AddListener(new UnityAction(component.OnClickMedal));
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06006A55 RID: 27221 RVA: 0x00228510 File Offset: 0x00226710
		public void SetSelectUI(bool bSet)
		{
			if (this.m_NKMEpisodeTemplet == null || this.m_NKMEpisodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_COUNTERCASE)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_LIST_SELECT, false);
				return;
			}
			bool activeSelf = this.m_NKM_UI_OPERATION_EPISODE_LIST_SELECT.activeSelf;
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_LIST_SELECT, bSet);
			if (bSet)
			{
				if (!activeSelf)
				{
					this.m_NKM_UI_OPERATION_EPISODE_LIST_SELECT_CG.alpha = 0f;
					this.m_NKM_UI_OPERATION_EPISODE_LIST_SELECT_CG.DOFade(1f, 0.7f);
					return;
				}
				this.m_NKM_UI_OPERATION_EPISODE_LIST_SELECT_CG.alpha = 1f;
			}
		}

		// Token: 0x06006A56 RID: 27222 RVA: 0x00228595 File Offset: 0x00226795
		public float GetCenterX()
		{
			return this.m_RTEPSlot.anchoredPosition.x + this.m_RTButton.sizeDelta.x / 2f;
		}

		// Token: 0x06006A57 RID: 27223 RVA: 0x002285BE File Offset: 0x002267BE
		public void OnClickMedal()
		{
			NKCUIOperationEPSlot.OnClickMedalRate onClickMedalRate = this.m_OnClickMedalRate;
			if (onClickMedalRate == null)
			{
				return;
			}
			onClickMedalRate(this.m_NKMEpisodeTemplet);
		}

		// Token: 0x06006A58 RID: 27224 RVA: 0x002285D6 File Offset: 0x002267D6
		public void SetOnSelectedSlot(NKCUIOperationEPSlot.OnSelectedSlot selectedSlot)
		{
			if (selectedSlot != null)
			{
				this.m_Button.PointerClick.RemoveAllListeners();
				this.m_OnSelectedSlot = selectedSlot;
				this.m_Button.PointerClick.AddListener(new UnityAction(this.OnSelectedSlotImpl));
			}
		}

		// Token: 0x06006A59 RID: 27225 RVA: 0x0022860E File Offset: 0x0022680E
		public void SetOnPointerUp(NKCUIOperationEPSlot.OnPointerUp onPointerUp)
		{
			if (onPointerUp != null)
			{
				this.m_Button.PointerUp.RemoveAllListeners();
				this.m_OnPointerUp = onPointerUp;
				this.m_Button.PointerUp.AddListener(new UnityAction(this.OnPointerUpImpl));
			}
		}

		// Token: 0x06006A5A RID: 27226 RVA: 0x00228646 File Offset: 0x00226846
		private void OnSelectedSlotImpl()
		{
			if (this.m_OnSelectedSlot != null)
			{
				this.m_OnSelectedSlot(this.m_NKMEpisodeTemplet);
			}
		}

		// Token: 0x06006A5B RID: 27227 RVA: 0x00228661 File Offset: 0x00226861
		private void OnPointerUpImpl()
		{
			if (this.m_OnPointerUp != null)
			{
				this.m_OnPointerUp();
			}
		}

		// Token: 0x06006A5C RID: 27228 RVA: 0x00228678 File Offset: 0x00226878
		public void SetData(NKMEpisodeTempletV2 cNKMEpisodeTemplet)
		{
			if (cNKMEpisodeTemplet == null)
			{
				this.m_NKMEpisodeTemplet = null;
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_LIST_EMPTY, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_LIST, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_LIST, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_LIST_EMPTY, false);
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			this.m_NKMEpisodeTemplet = cNKMEpisodeTemplet;
			this.m_imgBG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_EP_THUMBNAIL", string.Format("EP_THUMBNAIL_{0}", cNKMEpisodeTemplet.m_EPThumbnail), false);
			if (this.m_rtBG != null)
			{
				if (cNKMEpisodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_COUNTERCASE)
				{
					this.m_rtBG.sizeDelta = new Vector2(676f, 850f);
				}
				else
				{
					this.m_rtBG.sizeDelta = new Vector2(540f, 795f);
				}
			}
			this.m_lbEPTitle.text = "";
			this.m_lbEPTitleSmall.text = "";
			bool flag;
			switch (cNKMEpisodeTemplet.m_EPCategory)
			{
			case EPISODE_CATEGORY.EC_MAINSTREAM:
			case EPISODE_CATEGORY.EC_SIDESTORY:
			case EPISODE_CATEGORY.EC_FIELD:
			case EPISODE_CATEGORY.EC_EVENT:
			case EPISODE_CATEGORY.EC_CHALLENGE:
				flag = true;
				goto IL_115;
			}
			flag = false;
			IL_115:
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_PROGRESS, flag);
			NKCUtil.SetGameobjectActive(this.m_goBasicStyleTitle, cNKMEpisodeTemplet.m_EPCategory != EPISODE_CATEGORY.EC_COUNTERCASE);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_SUBINFO, cNKMEpisodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_DAILY);
			if (flag)
			{
				this.m_lbEPTitle.text = cNKMEpisodeTemplet.GetEpisodeTitle();
			}
			else
			{
				this.m_NKM_UI_OPERATION_EPISODE_SUBINFO_TEXT.text = this.m_NKMEpisodeTemplet.GetEpisodeTitle();
			}
			this.m_lbEPName.text = cNKMEpisodeTemplet.GetEpisodeName();
			this.m_bLock = !NKMEpisodeMgr.IsPossibleEpisode(myUserData, cNKMEpisodeTemplet);
			this.m_bOpenedDayOfWeek = this.m_NKMEpisodeTemplet.IsOpenedDayOfWeek();
			NKCUtil.SetGameobjectActive(this.m_goLock, this.m_bLock || !this.m_bOpenedDayOfWeek);
			if (this.m_bLock)
			{
				NKCUtil.SetLabelText(this.m_lbLockMessage, "");
				NKCAssetResourceData assetResource = NKCResourceUtility.GetAssetResource("AB_UI_NKM_UI_OPERATION_EP_THUMBNAIL", "EP_THUMBNAIL_BLACK_AND_WHITE");
				if (assetResource != null)
				{
					this.m_imgBG.material = assetResource.GetAsset<Material>();
				}
			}
			else
			{
				this.m_imgBG.material = null;
			}
			this.SetProgressData(myUserData, cNKMEpisodeTemplet);
			if (this.m_bLock)
			{
				UnlockInfo unlockInfo = cNKMEpisodeTemplet.GetUnlockInfo();
				if (!NKMContentUnlockManager.IsStarted(unlockInfo))
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_TIME, true);
					string @string = NKCStringTable.GetString("SI_DP_TIME_TO_OPEN", new object[]
					{
						NKCUtilString.GetRemainTimeString(NKMContentUnlockManager.GetConditionStartTime(unlockInfo), 2)
					});
					NKCUtil.SetLabelText(this.m_TIME_TEXT, @string);
				}
				else if (cNKMEpisodeTemplet.HasEventTimeLimit)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_TIME, true);
					NKCUtil.SetLabelText(this.m_TIME_TEXT, NKCUtilString.GetRemainTimeStringEx(cNKMEpisodeTemplet.EpisodeDateEndUtc));
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_TIME, false);
				}
			}
			else if (cNKMEpisodeTemplet.HasEventTimeLimit)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_TIME, true);
				NKCUtil.SetLabelText(this.m_TIME_TEXT, NKCUtilString.GetRemainTimeStringEx(cNKMEpisodeTemplet.EpisodeDateEndUtc));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_TIME, false);
			}
			NKCUtil.SetGameobjectActive(this.m_EventBadge, cNKMEpisodeTemplet.HaveEventDrop);
		}

		// Token: 0x06006A5D RID: 27229 RVA: 0x0022897C File Offset: 0x00226B7C
		public bool IsActive()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x06006A5E RID: 27230 RVA: 0x00228989 File Offset: 0x00226B89
		public void SetActive(bool bSet)
		{
			if (base.gameObject.activeSelf != bSet)
			{
				base.gameObject.SetActive(bSet);
			}
		}

		// Token: 0x06006A5F RID: 27231 RVA: 0x002289A8 File Offset: 0x00226BA8
		private void Update()
		{
			if (this.m_NKMEpisodeTemplet == null)
			{
				return;
			}
			this.m_time += Time.deltaTime;
			if (this.m_time < 1f)
			{
				return;
			}
			this.m_time = 0f;
			if (this.m_bLock)
			{
				if (NKMEpisodeMgr.IsPossibleEpisode(NKCScenManager.CurrentUserData(), this.m_NKMEpisodeTemplet))
				{
					this.m_bLock = false;
					NKCUtil.SetGameobjectActive(this.m_goLock, false);
					return;
				}
				UnlockInfo unlockInfo = this.m_NKMEpisodeTemplet.GetUnlockInfo();
				if (!NKMContentUnlockManager.IsStarted(unlockInfo))
				{
					string @string = NKCStringTable.GetString("SI_DP_TIME_TO_OPEN", new object[]
					{
						NKCUtilString.GetRemainTimeString(NKMContentUnlockManager.GetConditionStartTime(unlockInfo), 2)
					});
					NKCUtil.SetLabelText(this.m_TIME_TEXT, @string);
					return;
				}
			}
			else
			{
				if (!this.m_NKMEpisodeTemplet.HasEventTimeLimit)
				{
					return;
				}
				if (this.m_NKMEpisodeTemplet != null && !this.m_NKMEpisodeTemplet.IsOpen)
				{
					this.m_bLock = true;
					NKCUtil.SetGameobjectActive(this.m_goLock, true);
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EXCEPTION_EVENT_EXPIRED_POPUP, delegate()
					{
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
					}, "");
					return;
				}
				NKCUtil.SetLabelText(this.m_TIME_TEXT, NKCUtilString.GetRemainTimeStringEx(this.m_NKMEpisodeTemplet.EpisodeDateEndUtc));
				if (!this.m_NKMEpisodeTemplet.IsOpen || !this.m_bOpenedDayOfWeek)
				{
					this.m_goLock.SetActive(true);
				}
			}
		}

		// Token: 0x06006A60 RID: 27232 RVA: 0x00228B04 File Offset: 0x00226D04
		private void SetProgressData(NKMUserData userData, NKMEpisodeTempletV2 episodeTemplet)
		{
			bool flag = false;
			bool flag2 = false;
			float width = this.m_rtNKM_UI_OPERATION_EPISODE_LIST_PROGRESSBAR.GetWidth();
			for (int i = 0; i < this.m_EpisodeProgress.Length; i++)
			{
				if (i > 1)
				{
					Debug.LogError("$추가 난이도가 있는 경우, 추가 작업 해주세요.");
					break;
				}
				if (i == 0)
				{
					NKCUtil.SetGameobjectActive(this.m_EpisodeProgress[i].ObjProgress, true);
				}
				if (i == 1)
				{
					NKCUtil.SetGameobjectActive(this.m_EpisodeProgress[i].ObjProgress, NKMEpisodeTempletV2.Find(episodeTemplet.m_EpisodeID, EPISODE_DIFFICULTY.HARD) != null);
				}
				NKCUtil.SetGameobjectActive(this.m_objMedalLock, !episodeTemplet.HasCompletionReward);
				float epprogressPercent = NKMEpisodeMgr.GetEPProgressPercent(userData, episodeTemplet);
				this.m_EpisodeProgress[i].PROGRESS_BAR.value = epprogressPercent;
				NKCUtil.SetLabelText(this.m_EpisodeProgress[i].PROGRESS_TEXT, ((int)(epprogressPercent * 100f)).ToString() + "%");
				List<int> list = new List<int>();
				for (int j = 0; j < episodeTemplet.m_CompletionReward.Length; j++)
				{
					if (episodeTemplet.m_CompletionReward[j] != null)
					{
						list.Add(episodeTemplet.m_CompletionReward[j].m_CompleteRate);
					}
				}
				if (list.Count == 0)
				{
					NKCUtil.SetGameobjectActive(this.m_EpisodeProgress[i].ObjProgress, false);
				}
				else
				{
					for (int k = 0; k < this.m_EpisodeProgress[i].rtMETAL.Length; k++)
					{
						NKCUtil.SetGameobjectActive(this.m_EpisodeProgress[i].rtMETAL[k].gameObject, list.Count > k);
						if (list.Count >= k + 1)
						{
							float x = width * (float)list[k] * 0.01f;
							this.m_EpisodeProgress[i].rtMETAL[k].anchoredPosition = new Vector2(x, this.m_EpisodeProgress[i].rtMETAL[k].anchoredPosition.y);
						}
					}
					NKC_EP_ACHIEVE_RATE nkc_EP_ACHIEVE_RATE;
					if (list.Count > 2 && epprogressPercent >= (float)list[2] * 0.01f)
					{
						nkc_EP_ACHIEVE_RATE = NKC_EP_ACHIEVE_RATE.AR_CLEAR_STEP_3;
					}
					else if (list.Count > 1 && epprogressPercent >= (float)list[1] * 0.01f)
					{
						nkc_EP_ACHIEVE_RATE = NKC_EP_ACHIEVE_RATE.AR_CLEAR_STEP_2;
					}
					else if (list.Count > 0 && epprogressPercent >= (float)list[0] * 0.01f)
					{
						nkc_EP_ACHIEVE_RATE = NKC_EP_ACHIEVE_RATE.AR_CLEAR_STEP_1;
					}
					else
					{
						nkc_EP_ACHIEVE_RATE = NKC_EP_ACHIEVE_RATE.AR_NONE;
					}
					int num = 0;
					while (num < 3 && num < list.Count)
					{
						int completePercent = list[num];
						if (!flag)
						{
							flag = this.m_NKMEpisodeTemplet.CheckExistReward(completePercent);
						}
						if (num <= (int)nkc_EP_ACHIEVE_RATE)
						{
							if (i == 0)
							{
								NKCUtil.SetImageColor(this.m_EpisodeProgress[i].METAL[num], NKCUtil.GetColor("#00D8FF"));
							}
							else if (i == 1)
							{
								NKCUtil.SetImageColor(this.m_EpisodeProgress[i].METAL[num], NKCUtil.GetColor("#FFDE00"));
							}
							NKCUtil.SetGameobjectActive(this.m_EpisodeProgress[i].FX[num], true);
						}
						else
						{
							NKCUtil.SetImageColor(this.m_EpisodeProgress[i].METAL[num], NKCUtil.GetColor("#FFFFFF"));
							NKCUtil.SetGameobjectActive(this.m_EpisodeProgress[i].FX[num], false);
						}
						num++;
					}
					if (!flag2)
					{
						for (int l = 0; l < 3; l++)
						{
							if (NKMEpisodeMgr.CanGetEpisodeCompleteReward(userData, this.m_NKMEpisodeTemplet.m_EpisodeID, (EPISODE_DIFFICULTY)i, l) == NKM_ERROR_CODE.NEC_OK)
							{
								flag2 = true;
								break;
							}
						}
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_MEDAL_ICON_FX1, flag2);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_MEDAL_ICON_FX2, flag2);
			if (flag2)
			{
				this.m_NKM_UI_OPERATION_EPISODE_MEDAL_ICON.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_MEDAL_REWARD_BUTTON_ON", false);
			}
			else
			{
				this.m_NKM_UI_OPERATION_EPISODE_MEDAL_ICON.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_MEDAL_REWARD_BUTTON_OFF", false);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_MEDAL_COMPLETABLE, flag2);
		}

		// Token: 0x040055FF RID: 22015
		public GameObject m_NKM_UI_OPERATION_EPISODE_LIST;

		// Token: 0x04005600 RID: 22016
		public GameObject m_NKM_UI_OPERATION_EPISODE_LIST_EMPTY;

		// Token: 0x04005601 RID: 22017
		public GameObject m_NKM_UI_OPERATION_EPISODE_LIST_SELECT;

		// Token: 0x04005602 RID: 22018
		public CanvasGroup m_NKM_UI_OPERATION_EPISODE_LIST_SELECT_CG;

		// Token: 0x04005603 RID: 22019
		public RectTransform m_rtBG;

		// Token: 0x04005604 RID: 22020
		public Image m_imgBG;

		// Token: 0x04005605 RID: 22021
		public GameObject m_goBasicStyleTitle;

		// Token: 0x04005606 RID: 22022
		public Text m_lbEPTitle;

		// Token: 0x04005607 RID: 22023
		public Text m_lbEPTitleSmall;

		// Token: 0x04005608 RID: 22024
		public Text m_lbEPName;

		// Token: 0x04005609 RID: 22025
		public NKCUIComButton m_Button;

		// Token: 0x0400560A RID: 22026
		public GameObject m_goLock;

		// Token: 0x0400560B RID: 22027
		public Text m_lbLockMessage;

		// Token: 0x0400560C RID: 22028
		public RectTransform m_RTEPSlot;

		// Token: 0x0400560D RID: 22029
		public RectTransform m_RTButton;

		// Token: 0x0400560E RID: 22030
		public Slider m_SliderProgress;

		// Token: 0x0400560F RID: 22031
		public Text m_lbProgress;

		// Token: 0x04005610 RID: 22032
		public List<GameObject> m_AchieveRateMedalsGO;

		// Token: 0x04005611 RID: 22033
		public List<Image> m_AchieveRateMedalsIMG;

		// Token: 0x04005612 RID: 22034
		public List<GameObject> m_AchieveRateMedalsFX;

		// Token: 0x04005613 RID: 22035
		public Image m_NKM_UI_OPERATION_EPISODE_MEDAL_ICON;

		// Token: 0x04005614 RID: 22036
		public GameObject m_NKM_UI_OPERATION_EPISODE_MEDAL_ICON_FX1;

		// Token: 0x04005615 RID: 22037
		public GameObject m_NKM_UI_OPERATION_EPISODE_MEDAL_ICON_FX2;

		// Token: 0x04005616 RID: 22038
		public GameObject m_NKM_UI_OPERATION_EPISODE_MEDAL_COMPLETABLE;

		// Token: 0x04005617 RID: 22039
		public NKCUIComButton m_btnMedal;

		// Token: 0x04005618 RID: 22040
		public GameObject m_objMedalLock;

		// Token: 0x04005619 RID: 22041
		public GameObject m_NKM_UI_OPERATION_EPISODE_PROGRESS;

		// Token: 0x0400561A RID: 22042
		public GameObject m_NKM_UI_OPERATION_EPISODE_SUBINFO;

		// Token: 0x0400561B RID: 22043
		public Text m_NKM_UI_OPERATION_EPISODE_SUBINFO_TEXT;

		// Token: 0x0400561C RID: 22044
		public GameObject m_NKM_UI_OPERATION_EPISODE_TIME;

		// Token: 0x0400561D RID: 22045
		public Text m_TIME_TEXT;

		// Token: 0x0400561E RID: 22046
		public GameObject m_EventBadge;

		// Token: 0x0400561F RID: 22047
		private float m_fOrgHalfWidth;

		// Token: 0x04005620 RID: 22048
		public NKMEpisodeTempletV2 m_NKMEpisodeTemplet;

		// Token: 0x04005621 RID: 22049
		public const float SIZE_X = 540f;

		// Token: 0x04005622 RID: 22050
		public const float SIZE_OFFSET_X = 70f;

		// Token: 0x04005623 RID: 22051
		public const float POS_OFFSET_X = 790f;

		// Token: 0x04005624 RID: 22052
		private NKCUIOperationEPSlot.OnSelectedSlot m_OnSelectedSlot;

		// Token: 0x04005625 RID: 22053
		private NKCUIOperationEPSlot.OnPointerUp m_OnPointerUp;

		// Token: 0x04005626 RID: 22054
		private NKCUIOperationEPSlot.OnClickMedalRate m_OnClickMedalRate;

		// Token: 0x04005627 RID: 22055
		private int m_Index = -1;

		// Token: 0x04005628 RID: 22056
		private bool m_bLock;

		// Token: 0x04005629 RID: 22057
		private bool m_bOpenedDayOfWeek;

		// Token: 0x0400562A RID: 22058
		private NKCAssetInstanceData m_NKCAssetInstanceData;

		// Token: 0x0400562B RID: 22059
		private float m_time;

		// Token: 0x0400562C RID: 22060
		[Header("Progress Bar")]
		public NKCUIOperationEPSlot.EP_PROGRESS[] m_EpisodeProgress;

		// Token: 0x0400562D RID: 22061
		public RectTransform m_rtNKM_UI_OPERATION_EPISODE_LIST_PROGRESSBAR;

		// Token: 0x020016C2 RID: 5826
		// (Invoke) Token: 0x0600B135 RID: 45365
		public delegate void OnSelectedSlot(NKMEpisodeTempletV2 _NKMEpisodeTemplet);

		// Token: 0x020016C3 RID: 5827
		// (Invoke) Token: 0x0600B139 RID: 45369
		public delegate void OnPointerUp();

		// Token: 0x020016C4 RID: 5828
		// (Invoke) Token: 0x0600B13D RID: 45373
		public delegate void OnClickMedalRate(NKMEpisodeTempletV2 _NKMEpisodeTemplet);

		// Token: 0x020016C5 RID: 5829
		[Serializable]
		public struct EP_PROGRESS
		{
			// Token: 0x0400A52E RID: 42286
			public GameObject ObjProgress;

			// Token: 0x0400A52F RID: 42287
			public Slider PROGRESS_BAR;

			// Token: 0x0400A530 RID: 42288
			public Text PROGRESS_TEXT;

			// Token: 0x0400A531 RID: 42289
			public RectTransform[] rtMETAL;

			// Token: 0x0400A532 RID: 42290
			public Image[] METAL;

			// Token: 0x0400A533 RID: 42291
			public GameObject[] FX;
		}
	}
}
