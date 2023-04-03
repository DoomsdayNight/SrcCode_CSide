using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using UnityEngine;

namespace NKC.UI.NPC
{
	// Token: 0x02000BFA RID: 3066
	public abstract class NKCUINPCBase : MonoBehaviour
	{
		// Token: 0x06008E16 RID: 36374
		public abstract void Init(bool bUseIdleAnimation = true);

		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x06008E17 RID: 36375
		protected abstract string LUA_ASSET_NAME { get; }

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x06008E18 RID: 36376
		protected abstract NPC_TYPE NPCType { get; }

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x06008E19 RID: 36377 RVA: 0x00306914 File Offset: 0x00304B14
		protected Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>> m_dicNPCTemplet
		{
			get
			{
				return NKCUINPCBase.GetNPCTempletDic(this.NPCType);
			}
		}

		// Token: 0x06008E1A RID: 36378 RVA: 0x00306924 File Offset: 0x00304B24
		public static Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>> GetNPCTempletDic(NPC_TYPE npcType)
		{
			switch (npcType)
			{
			case NPC_TYPE.STORE_SERINA:
				return NKCUINPCBase.m_dicStoreSerinaTemplet;
			case NPC_TYPE.MACHINE_GAP:
				return NKCUINPCBase.m_dicMachineGapTemplet;
			case NPC_TYPE.OPERATOR_LENA:
				return NKCUINPCBase.m_dicOeratorLenaTemplet;
			case NPC_TYPE.OPERATOR_CHLOE:
				return NKCUINPCBase.m_dicOeratorChloeTemplet;
			case NPC_TYPE.PROFESSOR_OLIVIA:
				return NKCUINPCBase.m_dicProfessorOliviaTemplet;
			case NPC_TYPE.FACTORY_ANASTASIA:
				return NKCUINPCBase.m_dicFactoryAnastasiaTemplet;
			case NPC_TYPE.MANAGER_KIMHANA:
				return NKCUINPCBase.m_dicManagerKimHaNaTemplet;
			case NPC_TYPE.HANGAR_NAHEERIN:
				return NKCUINPCBase.m_dicHangarNaHeeRinTemplet;
			case NPC_TYPE.ASSISTANT_LEEYOONJUNG:
				return NKCUINPCBase.m_dicAssistantLeeYoonJung;
			case NPC_TYPE.STORE_SIGMA:
				return NKCUINPCBase.m_dicStoreSigmaTemplet;
			default:
				return new Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>>();
			}
		}

		// Token: 0x06008E1B RID: 36379 RVA: 0x003069A4 File Offset: 0x00304BA4
		protected void LoadFromLua()
		{
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT_NPC", this.LUA_ASSET_NAME, true))
			{
				if (nkmlua.OpenTable("m_dicNPCTemplet"))
				{
					int num = 1;
					while (nkmlua.OpenTable(num))
					{
						NKCNPCTemplet nkcnpctemplet = new NKCNPCTemplet();
						if (nkcnpctemplet.LoadLUA(nkmlua))
						{
							if (!this.m_dicNPCTemplet.ContainsKey(nkcnpctemplet.m_ActionType))
							{
								this.m_dicNPCTemplet.Add(nkcnpctemplet.m_ActionType, new List<NKCNPCTemplet>());
							}
							this.m_dicNPCTemplet[nkcnpctemplet.m_ActionType].Add(nkcnpctemplet);
						}
						num++;
						nkmlua.CloseTable();
					}
					nkmlua.CloseTable();
				}
				nkmlua.LuaClose();
			}
		}

		// Token: 0x06008E1C RID: 36380 RVA: 0x00306A4F File Offset: 0x00304C4F
		public List<NKCNPCTemplet> GetNPCTempletList(NPC_ACTION_TYPE npcActionType)
		{
			return NKCUINPCBase.GetNPCTempletList(this.NPCType, npcActionType);
		}

		// Token: 0x06008E1D RID: 36381 RVA: 0x00306A60 File Offset: 0x00304C60
		public static List<NKCNPCTemplet> GetNPCTempletList(NPC_TYPE npcType, List<NPC_ACTION_TYPE> lstNpcActionType)
		{
			Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>> npctempletDic = NKCUINPCBase.GetNPCTempletDic(npcType);
			List<NKCNPCTemplet> list = new List<NKCNPCTemplet>();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			for (int i = 0; i < lstNpcActionType.Count; i++)
			{
				if (npctempletDic != null && npctempletDic.ContainsKey(lstNpcActionType[i]))
				{
					List<NKCNPCTemplet> list2 = npctempletDic[lstNpcActionType[i]];
					for (int j = 0; j < list2.Count; j++)
					{
						NPC_CONDITION conditionType = list2[j].m_ConditionType;
						if (conditionType <= NPC_CONDITION.IDLE_TIME || conditionType != NPC_CONDITION.TOTAL_PAY)
						{
							list.Add(list2[j]);
						}
						else
						{
							double? num = (nkmuserData != null) ? new double?(nkmuserData.m_ShopData.GetTotalPayment()) : null;
							double num2 = (double)list2[j].m_ConditionValue;
							if (num.GetValueOrDefault() >= num2 & num != null)
							{
								list.Add(list2[j]);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06008E1E RID: 36382 RVA: 0x00306B60 File Offset: 0x00304D60
		public static List<NKCNPCTemplet> GetNPCTempletList(NPC_TYPE npcType, NPC_ACTION_TYPE npcActionType)
		{
			Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>> npctempletDic = NKCUINPCBase.GetNPCTempletDic(npcType);
			List<NKCNPCTemplet> list = new List<NKCNPCTemplet>();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (npctempletDic != null && npctempletDic.ContainsKey(npcActionType))
			{
				List<NKCNPCTemplet> list2 = npctempletDic[npcActionType];
				for (int i = 0; i < list2.Count; i++)
				{
					NPC_CONDITION conditionType = list2[i].m_ConditionType;
					if (conditionType <= NPC_CONDITION.IDLE_TIME || conditionType != NPC_CONDITION.TOTAL_PAY)
					{
						list.Add(list2[i]);
					}
					else
					{
						double? num = (nkmuserData != null) ? new double?(nkmuserData.m_ShopData.GetTotalPayment()) : null;
						double num2 = (double)list2[i].m_ConditionValue;
						if (num.GetValueOrDefault() >= num2 & num != null)
						{
							list.Add(list2[i]);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06008E1F RID: 36383 RVA: 0x00306C36 File Offset: 0x00304E36
		public NKCNPCTemplet GetNPCTemplet(NPC_ACTION_TYPE npcActionType)
		{
			return NKCUINPCBase.GetNPCTemplet(this.NPCType, npcActionType);
		}

		// Token: 0x06008E20 RID: 36384 RVA: 0x00306C44 File Offset: 0x00304E44
		public static NKCNPCTemplet GetNPCTemplet(NPC_TYPE npcType, List<NPC_ACTION_TYPE> lstNpcActionType)
		{
			List<NKCNPCTemplet> npctempletList = NKCUINPCBase.GetNPCTempletList(npcType, lstNpcActionType);
			if (npctempletList.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, npctempletList.Count);
				return npctempletList[index];
			}
			return null;
		}

		// Token: 0x06008E21 RID: 36385 RVA: 0x00306C78 File Offset: 0x00304E78
		public static NKCNPCTemplet GetNPCTemplet(NPC_TYPE npcType, NPC_ACTION_TYPE npcActionType)
		{
			List<NKCNPCTemplet> npctempletList = NKCUINPCBase.GetNPCTempletList(npcType, npcActionType);
			if (npctempletList.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, npctempletList.Count);
				return npctempletList[index];
			}
			return null;
		}

		// Token: 0x06008E22 RID: 36386 RVA: 0x00306CAC File Offset: 0x00304EAC
		private static string GetVoiceBundleName(NPC_TYPE npcType)
		{
			switch (npcType)
			{
			case NPC_TYPE.STORE_SERINA:
				return "AB_NPC_VOICE_STORE_SERINA";
			case NPC_TYPE.MACHINE_GAP:
				return "AB_NPC_VOICE_MACHINE_GAP";
			case NPC_TYPE.OPERATOR_LENA:
				return "AB_NPC_VOICE_OPERATOR_LENA";
			case NPC_TYPE.OPERATOR_CHLOE:
				return "AB_NPC_VOICE_OPERATOR_CHLOE";
			case NPC_TYPE.PROFESSOR_OLIVIA:
				return "AB_NPC_VOICE_PROFESSOR_OLIVIA";
			case NPC_TYPE.FACTORY_ANASTASIA:
				return "AB_NPC_VOICE_BLACKSMITH_ANASTASIA";
			case NPC_TYPE.MANAGER_KIMHANA:
				return "AB_NPC_VOICE_MANAGER_KIMHANA";
			case NPC_TYPE.HANGAR_NAHEERIN:
				return "AB_NPC_VOICE_HANGAR_NAHEERIN";
			case NPC_TYPE.ASSISTANT_LEEYOONJUNG:
				return "AB_NPC_VOICE_ASSISTANT_LEE_YOON_JUNG";
			default:
				return "";
			}
		}

		// Token: 0x06008E23 RID: 36387 RVA: 0x00306D20 File Offset: 0x00304F20
		public static NKMAssetName PlayVoice(NPC_TYPE npcType, NKCNPCTemplet npcTemplet, bool bStopCurrentSound = true, bool bIgnoreCoolTime = false, bool bShowCaption = false)
		{
			if (string.IsNullOrEmpty(npcTemplet.m_VoiceFileName))
			{
				return null;
			}
			if (!NKCUINPCBase.VoiceCoolTimeElapsed(npcType, npcTemplet, bIgnoreCoolTime))
			{
				return null;
			}
			if (!bStopCurrentSound && NKCSoundManager.IsPlayingVoice(-1))
			{
				return null;
			}
			NKCSoundManager.StopSound(NKCUINPCBase.m_CurrentSoundUID);
			Log.Debug("[Voice] play " + npcTemplet.m_VoiceFileName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Components/NKCUINPCBase.cs", 433);
			NKCUINPCBase.m_CurrentSoundUID = NKCSoundManager.PlayVoice(NKCUINPCBase.GetVoiceBundleName(npcType), npcTemplet.m_VoiceFileName, 0, true, false, (float)npcTemplet.m_Volume, 0f, 0f, false, 0f, bShowCaption, 0f, 0f);
			if (NKCUINPCBase.m_CurrentSoundUID <= 0)
			{
				return null;
			}
			return new NKMAssetName(NKCAssetResourceManager.GetBundleName(npcTemplet.m_VoiceFileName), npcTemplet.m_VoiceFileName);
		}

		// Token: 0x06008E24 RID: 36388 RVA: 0x00306DDC File Offset: 0x00304FDC
		public static void PlayVoice(NPC_TYPE npcType, string voiceFileName, bool bStopCurrentSound = true, bool bShowCaption = false)
		{
			if (!bStopCurrentSound && NKCSoundManager.IsPlayingVoice(-1))
			{
				return;
			}
			NKCSoundManager.StopSound(NKCUINPCBase.m_CurrentSoundUID);
			Log.Debug("[Voice] play " + voiceFileName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Components/NKCUINPCBase.cs", 455);
			NKCUINPCBase.m_CurrentSoundUID = NKCSoundManager.PlayVoice(NKCUINPCBase.GetVoiceBundleName(npcType), voiceFileName, 0, true, false, 1f, 0f, 0f, false, 0f, bShowCaption, 0f, 0f);
		}

		// Token: 0x06008E25 RID: 36389 RVA: 0x00306E4D File Offset: 0x0030504D
		public void StopVoice()
		{
			if (NKCSoundManager.IsPlayingVoice(-1))
			{
				NKCSoundManager.StopSound(NKCUINPCBase.m_CurrentSoundUID);
			}
		}

		// Token: 0x06008E26 RID: 36390 RVA: 0x00306E64 File Offset: 0x00305064
		private static bool VoiceCoolTimeElapsed(NPC_TYPE npcType, NKCNPCTemplet npcTemplet, bool bIgnoreCoolTime)
		{
			if (NKCUINPCBase.prevNPCType != npcType)
			{
				NKCUINPCBase.m_dicCoolTimeInfo.Clear();
			}
			NKCUINPCBase.prevNPCType = npcType;
			float value = Time.time + npcTemplet.m_ActionCoolTime;
			float num;
			if (NKCUINPCBase.m_dicCoolTimeInfo.TryGetValue(npcTemplet.m_ActionType, out num))
			{
				if (Time.time < num)
				{
					return bIgnoreCoolTime;
				}
				NKCUINPCBase.m_dicCoolTimeInfo[npcTemplet.m_ActionType] = value;
			}
			else
			{
				NKCUINPCBase.m_dicCoolTimeInfo.Add(npcTemplet.m_ActionType, value);
			}
			return true;
		}

		// Token: 0x06008E27 RID: 36391 RVA: 0x00306EDE File Offset: 0x003050DE
		public bool HasAction(NPC_ACTION_TYPE actionType)
		{
			return this.GetNPCTemplet(actionType) != null;
		}

		// Token: 0x06008E28 RID: 36392
		public abstract void PlayAni(NPC_ACTION_TYPE actionType, bool bMute = false);

		// Token: 0x06008E29 RID: 36393
		public abstract void PlayAni(eEmotion emotion);

		// Token: 0x06008E2A RID: 36394
		public abstract void DragEvent();

		// Token: 0x06008E2B RID: 36395
		public abstract void OpenTalk(bool bLeft, NKC_UI_TALK_BOX_DIR dir, string talk, float fadeTime = 0f);

		// Token: 0x06008E2C RID: 36396
		public abstract void CloseTalk();

		// Token: 0x04007B88 RID: 31624
		private const string LUA_ASSET_BUNDLE_NAME = "AB_SCRIPT_NPC";

		// Token: 0x04007B89 RID: 31625
		protected static Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>> m_dicStoreSerinaTemplet = new Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>>();

		// Token: 0x04007B8A RID: 31626
		protected static Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>> m_dicMachineGapTemplet = new Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>>();

		// Token: 0x04007B8B RID: 31627
		protected static Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>> m_dicOeratorLenaTemplet = new Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>>();

		// Token: 0x04007B8C RID: 31628
		protected static Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>> m_dicOeratorChloeTemplet = new Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>>();

		// Token: 0x04007B8D RID: 31629
		protected static Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>> m_dicProfessorOliviaTemplet = new Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>>();

		// Token: 0x04007B8E RID: 31630
		protected static Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>> m_dicFactoryAnastasiaTemplet = new Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>>();

		// Token: 0x04007B8F RID: 31631
		protected static Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>> m_dicManagerKimHaNaTemplet = new Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>>();

		// Token: 0x04007B90 RID: 31632
		protected static Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>> m_dicHangarNaHeeRinTemplet = new Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>>();

		// Token: 0x04007B91 RID: 31633
		protected static Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>> m_dicAssistantLeeYoonJung = new Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>>();

		// Token: 0x04007B92 RID: 31634
		protected static Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>> m_dicStoreSigmaTemplet = new Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>>();

		// Token: 0x04007B93 RID: 31635
		protected static int m_CurrentSoundUID;

		// Token: 0x04007B94 RID: 31636
		protected static NPC_TYPE prevNPCType;

		// Token: 0x04007B95 RID: 31637
		protected static Dictionary<NPC_ACTION_TYPE, float> m_dicCoolTimeInfo = new Dictionary<NPC_ACTION_TYPE, float>();
	}
}
