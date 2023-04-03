using System;
using System.Collections.Generic;
using ClientPacket.Game;
using NKC.UI;
using NKM;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;

namespace NKC
{
	// Token: 0x02000627 RID: 1575
	public class NKCGameHudEmoticon : MonoBehaviour
	{
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x000F0A44 File Offset: 0x000EEC44
		// (set) Token: 0x0600309D RID: 12445 RVA: 0x000F0AD5 File Offset: 0x000EECD5
		public NKCPopupInGameEmoticon NKCPopupInGameEmoticon
		{
			get
			{
				if (this.m_NKCPopupInGameEmoticon == null)
				{
					this.m_cNKCAssetInstanceDataPopup = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_EMOTICON", "NKM_UI_EMOTICON", false, null);
					if (this.m_cNKCAssetInstanceDataPopup != null)
					{
						this.m_NKCPopupInGameEmoticon = this.m_cNKCAssetInstanceDataPopup.m_Instant.GetComponent<NKCPopupInGameEmoticon>();
						if (this.m_NKCPopupInGameEmoticon == null)
						{
							Debug.LogError("NKCPopupInGameEmoticon load fail!");
							return null;
						}
						this.m_NKCPopupInGameEmoticon.transform.SetParent(NKCUIManager.rectFrontCanvas, false);
						this.m_NKCPopupInGameEmoticon.Init();
					}
				}
				return this.m_NKCPopupInGameEmoticon;
			}
			private set
			{
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x000F0AD7 File Offset: 0x000EECD7
		public bool IsNKCPopupInGameEmoticonOpen
		{
			get
			{
				return this.m_NKCPopupInGameEmoticon != null && this.m_NKCPopupInGameEmoticon.IsOpen;
			}
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x000F0AF4 File Offset: 0x000EECF4
		public void Awake()
		{
			this.m_csbtnEmoticonSelectPopupOpen.PointerClick.RemoveAllListeners();
			this.m_csbtnEmoticonSelectPopupOpen.PointerClick.AddListener(new UnityAction(this.OnClickOpenEmoticonSelectPopup));
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000F0B22 File Offset: 0x000EED22
		public void Start()
		{
			this.m_NKCGameHudEmoticonCommentLeft.SetEnableBtn(false);
			this.m_NKCGameHudEmoticonCommentRight.SetEnableBtn(false);
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000F0B3C File Offset: 0x000EED3C
		private void PreLoadEmoticonAni(int emoticonID, bool bLeft)
		{
			NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(emoticonID);
			if (nkmemoticonTemplet != null)
			{
				if (nkmemoticonTemplet.m_EmoticonType != NKM_EMOTICON_TYPE.NET_ANI)
				{
					return;
				}
				if (bLeft)
				{
					if (this.m_dic_sg_EMOTICON_LEFT.ContainsKey(nkmemoticonTemplet.m_EmoticonAssetName))
					{
						return;
					}
				}
				else if (this.m_dic_sg_EMOTICON_RIGHT.ContainsKey(nkmemoticonTemplet.m_EmoticonAssetName))
				{
					return;
				}
				NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_" + nkmemoticonTemplet.m_EmoticonAssetName, nkmemoticonTemplet.m_EmoticonAssetName, false, null);
				SkeletonGraphic componentInChildren = nkcassetInstanceData.m_Instant.GetComponentInChildren<SkeletonGraphic>();
				if (componentInChildren == null || componentInChildren.AnimationState == null)
				{
					Debug.LogError("emoticon prefab Can't find Skeleton graphic, AssetName : " + nkmemoticonTemplet.m_EmoticonAssetName);
					return;
				}
				componentInChildren.AnimationState.SetAnimation(0, "BASE_END", false);
				if (bLeft)
				{
					this.m_dic_sg_EMOTICON_LEFT.Add(nkmemoticonTemplet.m_EmoticonAssetName, componentInChildren);
					nkcassetInstanceData.m_Instant.transform.SetParent(this.m_obj_EMOTICON_LEFT.transform, false);
				}
				else
				{
					this.m_dic_sg_EMOTICON_RIGHT.Add(nkmemoticonTemplet.m_EmoticonAssetName, componentInChildren);
					nkcassetInstanceData.m_Instant.transform.SetParent(this.m_obj_EMOTICON_RIGHT.transform, false);
				}
				nkcassetInstanceData.m_Instant.transform.localPosition = new Vector3(nkcassetInstanceData.m_Instant.transform.localPosition.x, nkcassetInstanceData.m_Instant.transform.localPosition.y, 0f);
				this.m_lstNKCAssetInstanceDataAni.Add(nkcassetInstanceData);
			}
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000F0CA0 File Offset: 0x000EEEA0
		private void PreLoadEmoticonAni(List<int> lstEmoticonMy, List<int> lstEmoticonEnemy)
		{
			if (lstEmoticonMy != null)
			{
				for (int i = 0; i < lstEmoticonMy.Count; i++)
				{
					this.PreLoadEmoticonAni(lstEmoticonMy[i], true);
				}
			}
			if (lstEmoticonEnemy != null)
			{
				for (int j = 0; j < lstEmoticonEnemy.Count; j++)
				{
					this.PreLoadEmoticonAni(lstEmoticonEnemy[j], false);
				}
			}
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000F0CF4 File Offset: 0x000EEEF4
		public void Clear()
		{
			if (this.m_cNKCAssetInstanceDataPopup != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_cNKCAssetInstanceDataPopup);
			}
			this.m_cNKCAssetInstanceDataPopup = null;
			for (int i = 0; i < this.m_lstNKCAssetInstanceDataAni.Count; i++)
			{
				NKCAssetResourceManager.CloseInstance(this.m_lstNKCAssetInstanceDataAni[i]);
			}
			this.m_lstNKCAssetInstanceDataAni.Clear();
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000F0D50 File Offset: 0x000EEF50
		public void SetUI(NKMGameData cNKMGameData)
		{
			if (cNKMGameData == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.SetBlockUI();
			NKMGameTeamData teamData = cNKMGameData.GetTeamData(NKCScenManager.CurrentUserData().m_UserUID);
			if (teamData == null)
			{
				return;
			}
			NKMGameTeamData enemyTeamData = cNKMGameData.GetEnemyTeamData(teamData.m_eNKM_TEAM_TYPE);
			if (enemyTeamData == null)
			{
				return;
			}
			this.PreLoadEmoticonAni(teamData.m_emoticonPreset.animationList, enemyTeamData.m_emoticonPreset.animationList);
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000F0DB8 File Offset: 0x000EEFB8
		public void SetBlockUI()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null)
			{
				this.SetBlockUI(gameOptionData.UseEmoticonBlock);
			}
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000F0DE0 File Offset: 0x000EEFE0
		private void OnClickOpenEmoticonSelectPopup()
		{
			if (NKCReplayMgr.IsPlayingReplay())
			{
				return;
			}
			NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
			if (gameClient == null)
			{
				return;
			}
			NKMGameData gameData = gameClient.GetGameData();
			if (gameData == null)
			{
				return;
			}
			NKMGameTeamData teamData = gameData.GetTeamData(NKCScenManager.CurrentUserData().m_UserUID);
			if (teamData == null)
			{
				return;
			}
			if (teamData.m_emoticonPreset == null)
			{
				return;
			}
			List<int> lstEmoticonID_SD = new List<int>();
			lstEmoticonID_SD = teamData.m_emoticonPreset.animationList;
			List<int> lstEmoticonIDComment = new List<int>();
			lstEmoticonIDComment = teamData.m_emoticonPreset.textList;
			this.NKCPopupInGameEmoticon.Open(lstEmoticonID_SD, lstEmoticonIDComment);
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000F0E61 File Offset: 0x000EF061
		private void SetBlockUI(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objEmoticonBlockOn, bSet);
			NKCUtil.SetGameobjectActive(this.m_objEmoticonBlockOff, !bSet);
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000F0E80 File Offset: 0x000EF080
		private void TurnOffEmoticoAni(bool bLeft)
		{
			Dictionary<string, SkeletonGraphic> dictionary;
			if (bLeft)
			{
				dictionary = this.m_dic_sg_EMOTICON_LEFT;
			}
			else
			{
				dictionary = this.m_dic_sg_EMOTICON_RIGHT;
			}
			foreach (KeyValuePair<string, SkeletonGraphic> keyValuePair in dictionary)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.AnimationState.SetAnimation(0, "BASE_END", false);
				}
			}
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000F0F04 File Offset: 0x000EF104
		private void TurnOnEmoticoAni(bool bLeft, NKMEmoticonTemplet cNKMEmoticonTemplet)
		{
			if (cNKMEmoticonTemplet == null || cNKMEmoticonTemplet.m_EmoticonType != NKM_EMOTICON_TYPE.NET_ANI)
			{
				return;
			}
			Dictionary<string, SkeletonGraphic> dictionary;
			if (bLeft)
			{
				dictionary = this.m_dic_sg_EMOTICON_LEFT;
			}
			else
			{
				dictionary = this.m_dic_sg_EMOTICON_RIGHT;
			}
			if (!dictionary.ContainsKey(cNKMEmoticonTemplet.m_EmoticonAssetName))
			{
				return;
			}
			foreach (KeyValuePair<string, SkeletonGraphic> keyValuePair in dictionary)
			{
				if (keyValuePair.Key == cNKMEmoticonTemplet.m_EmoticonAssetName)
				{
					keyValuePair.Value.AnimationState.SetAnimation(0, "BASE", false);
					keyValuePair.Value.AnimationState.AddAnimation(0, "BASE_END", false, 0f);
				}
				else
				{
					keyValuePair.Value.AnimationState.SetAnimation(0, "BASE_END", false);
				}
			}
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000F0FE4 File Offset: 0x000EF1E4
		public void OnRecv(NKMPacket_GAME_EMOTICON_NOT cNKMPacket_GAME_EMOTICON_NOT)
		{
			NKMGameData gameData = NKCScenManager.GetScenManager().GetGameClient().GetGameData();
			if (gameData == null)
			{
				return;
			}
			NKMGameTeamData teamData = gameData.GetTeamData(cNKMPacket_GAME_EMOTICON_NOT.senderUserUID);
			if (teamData == null || teamData.m_emoticonPreset == null)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < teamData.m_emoticonPreset.textList.Count; i++)
			{
				if (teamData.m_emoticonPreset.textList[i] == cNKMPacket_GAME_EMOTICON_NOT.emoticonID)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				for (int j = 0; j < teamData.m_emoticonPreset.animationList.Count; j++)
				{
					if (teamData.m_emoticonPreset.animationList[j] == cNKMPacket_GAME_EMOTICON_NOT.emoticonID)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			bool flag2 = false;
			if (NKCScenManager.CurrentUserData().m_UserUID == cNKMPacket_GAME_EMOTICON_NOT.senderUserUID)
			{
				flag2 = true;
			}
			NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(cNKMPacket_GAME_EMOTICON_NOT.emoticonID);
			if (nkmemoticonTemplet == null)
			{
				return;
			}
			if (flag2)
			{
				if (this.m_PrevLeftSoundID != 0)
				{
					NKCSoundManager.StopSound(this.m_PrevLeftSoundID);
					this.m_PrevLeftSoundID = 0;
				}
			}
			else if (this.m_PrevRightSoundID != 0)
			{
				NKCSoundManager.StopSound(this.m_PrevRightSoundID);
				this.m_PrevRightSoundID = 0;
			}
			if (!string.IsNullOrWhiteSpace(nkmemoticonTemplet.m_EmoticonSound))
			{
				int num = NKCSoundManager.PlaySound("AB_FX_UI_EMOTICON_" + nkmemoticonTemplet.m_EmoticonSound, 1f, 0f, 0f, false, 0f, false, 0f);
				if (flag2)
				{
					this.m_PrevLeftSoundID = num;
				}
				else
				{
					this.m_PrevRightSoundID = num;
				}
			}
			if (nkmemoticonTemplet.m_EmoticonType != NKM_EMOTICON_TYPE.NET_TEXT)
			{
				if (nkmemoticonTemplet.m_EmoticonType == NKM_EMOTICON_TYPE.NET_ANI)
				{
					if (flag2)
					{
						this.m_NKCGameHudEmoticonCommentLeft.Stop();
						this.TurnOnEmoticoAni(true, nkmemoticonTemplet);
						return;
					}
					this.m_NKCGameHudEmoticonCommentRight.Stop();
					this.TurnOnEmoticoAni(false, nkmemoticonTemplet);
				}
				return;
			}
			if (flag2)
			{
				this.m_NKCGameHudEmoticonCommentLeft.Play(nkmemoticonTemplet.m_EmoticonID);
				this.TurnOffEmoticoAni(true);
				return;
			}
			this.m_NKCGameHudEmoticonCommentRight.Play(nkmemoticonTemplet.m_EmoticonID);
			this.TurnOffEmoticoAni(false);
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000F11D0 File Offset: 0x000EF3D0
		private void Update()
		{
			if (NKCScenManager.GetScenManager().GetNKCPowerSaveMode().GetEnable())
			{
				return;
			}
			if (NKCInputManager.CheckHotKeyEvent(InGamehotkeyEventType.Emoticon))
			{
				if (this.IsNKCPopupInGameEmoticonOpen)
				{
					this.NKCPopupInGameEmoticon.Close();
				}
				else
				{
					this.OnClickOpenEmoticonSelectPopup();
				}
				NKCInputManager.ConsumeHotKeyEvent(InGamehotkeyEventType.Emoticon);
			}
			if (NKCInputManager.CheckHotKeyEvent(HotkeyEventType.ShowHotkey))
			{
				NKCUIComStateButton csbtnEmoticonSelectPopupOpen = this.m_csbtnEmoticonSelectPopupOpen;
				NKCUIComHotkeyDisplay.OpenInstance((csbtnEmoticonSelectPopupOpen != null) ? csbtnEmoticonSelectPopupOpen.transform : null, InGamehotkeyEventType.Emoticon);
			}
		}

		// Token: 0x04003014 RID: 12308
		public NKCUIComStateButton m_csbtnEmoticonSelectPopupOpen;

		// Token: 0x04003015 RID: 12309
		public GameObject m_objEmoticonBlockOff;

		// Token: 0x04003016 RID: 12310
		public GameObject m_objEmoticonBlockOn;

		// Token: 0x04003017 RID: 12311
		public NKCGameHudEmoticonComment m_NKCGameHudEmoticonCommentLeft;

		// Token: 0x04003018 RID: 12312
		public NKCGameHudEmoticonComment m_NKCGameHudEmoticonCommentRight;

		// Token: 0x04003019 RID: 12313
		public GameObject m_obj_EMOTICON_LEFT;

		// Token: 0x0400301A RID: 12314
		private Dictionary<string, SkeletonGraphic> m_dic_sg_EMOTICON_LEFT = new Dictionary<string, SkeletonGraphic>();

		// Token: 0x0400301B RID: 12315
		public GameObject m_obj_EMOTICON_RIGHT;

		// Token: 0x0400301C RID: 12316
		private Dictionary<string, SkeletonGraphic> m_dic_sg_EMOTICON_RIGHT = new Dictionary<string, SkeletonGraphic>();

		// Token: 0x0400301D RID: 12317
		private int m_PrevLeftSoundID;

		// Token: 0x0400301E RID: 12318
		private int m_PrevRightSoundID;

		// Token: 0x0400301F RID: 12319
		private NKCAssetInstanceData m_cNKCAssetInstanceDataPopup;

		// Token: 0x04003020 RID: 12320
		private List<NKCAssetInstanceData> m_lstNKCAssetInstanceDataAni = new List<NKCAssetInstanceData>();

		// Token: 0x04003021 RID: 12321
		private NKCPopupInGameEmoticon m_NKCPopupInGameEmoticon;
	}
}
