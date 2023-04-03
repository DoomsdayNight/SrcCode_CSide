using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ClientPacket.Common;
using ClientPacket.Game;
using Cs.Core.Util;
using Cs.Logging;
using Cs.Protocol;
using NKM.Templet;
using NKM.Templet.Office;
using NKM.Unit;

namespace NKM
{
	// Token: 0x02000496 RID: 1174
	[DataContract]
	public sealed class NKMUnitData : Cs.Protocol.ISerializable
	{
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x000A651E File Offset: 0x000A471E
		public bool IsPermanentContract
		{
			get
			{
				return this.isPermanentContract;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060020A5 RID: 8357 RVA: 0x000A6526 File Offset: 0x000A4726
		public IReadOnlyList<long> EquipItemUids
		{
			get
			{
				return this.m_EquipItemList;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060020A6 RID: 8358 RVA: 0x000A652E File Offset: 0x000A472E
		public bool IsSeized
		{
			get
			{
				return this.isSeized;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x000A6536 File Offset: 0x000A4736
		public bool FromContract
		{
			get
			{
				return this.fromContract;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x000A653E File Offset: 0x000A473E
		public int OfficeRoomId
		{
			get
			{
				return this.officeRoomId;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x000A6546 File Offset: 0x000A4746
		public OfficeGrade OfficeGrade
		{
			get
			{
				return this.officeGrade;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x000A654E File Offset: 0x000A474E
		public DateTime OfficeGaugeStartTime
		{
			get
			{
				return this.officeGaugeStartTime;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x000A6558 File Offset: 0x000A4758
		public bool IsActiveUnit
		{
			get
			{
				if (!this.m_bLock && this.m_UnitLevel <= 1 && this.m_SkinID == 0 && this.tacticLevel <= 0 && this.m_LimitBreakLevel <= 0 && !this.isPermanentContract)
				{
					if (!this.m_aUnitSkillLevel.Any((int x) => x > 1))
					{
						return this.EquipItemUids.Any((long x) => x != 0L);
					}
				}
				return true;
			}
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x000A65F4 File Offset: 0x000A47F4
		public NKMUnitData()
		{
			for (int i = 0; i <= 5; i++)
			{
				this.m_listStatEXP.Add(0);
			}
			this.Init();
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x000A6698 File Offset: 0x000A4898
		public NKMUnitData(long userUid, int unitId, long unitUid, short limitBreakLv, int loyalty, bool isPermanentContract, bool isLock)
		{
			for (int i = 0; i <= 5; i++)
			{
				this.m_listStatEXP.Add(0);
			}
			this.Init();
			this.m_UserUID = userUid;
			this.m_UnitID = unitId;
			this.m_UnitUID = unitUid;
			this.m_LimitBreakLevel = limitBreakLv;
			this.loyalty = loyalty;
			this.isPermanentContract = isPermanentContract;
			this.m_bLock = isLock;
			this.FillSkillLevelByUnitID(this.m_UnitID);
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x000A677C File Offset: 0x000A497C
		public NKMUnitData(int unit_id, long unit_uid, bool islock, bool isPermanentContract, bool isSeized, bool fromContract)
		{
			for (int i = 0; i <= 5; i++)
			{
				this.m_listStatEXP.Add(0);
			}
			this.Init();
			this.m_UnitID = unit_id;
			this.m_UnitUID = unit_uid;
			this.m_bLock = islock;
			this.isPermanentContract = isPermanentContract;
			this.isSeized = isSeized;
			this.fromContract = fromContract;
			this.FillSkillLevelByUnitID(this.m_UnitID);
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000A6858 File Offset: 0x000A4A58
		public NKMUnitData(int unit_id, long unit_uid, bool islock, bool isPermanentContract, bool isSeized, bool fromContract, bool isFavorite)
		{
			for (int i = 0; i <= 5; i++)
			{
				this.m_listStatEXP.Add(0);
			}
			this.Init();
			this.m_UnitID = unit_id;
			this.m_UnitUID = unit_uid;
			this.m_bLock = islock;
			this.isPermanentContract = isPermanentContract;
			this.isSeized = isSeized;
			this.fromContract = fromContract;
			this.isFavorite = isFavorite;
			this.FillSkillLevelByUnitID(this.m_UnitID);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000A693C File Offset: 0x000A4B3C
		public NKMUnitData(NKMDummyUnitData dummyUnit)
		{
			for (int i = 0; i <= 5; i++)
			{
				this.m_listStatEXP.Add(0);
			}
			this.Init();
			this.m_UnitID = dummyUnit.UnitId;
			this.m_UnitLevel = dummyUnit.UnitLevel;
			this.m_LimitBreakLevel = dummyUnit.LimitBreakLevel;
			this.m_SkinID = dummyUnit.SkinId;
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x000A6A10 File Offset: 0x000A4C10
		public void Init()
		{
			this.m_UnitUID = 0L;
			this.m_UserUID = 0L;
			this.m_UnitID = 0;
			this.m_UnitLevel = 1;
			this.m_iUnitLevelEXP = 0;
			this.m_fInjury = 0f;
			this.m_LimitBreakLevel = 0;
			this.m_bLock = false;
			for (int i = 0; i <= 5; i++)
			{
				this.m_listStatEXP[i] = 0;
			}
			for (int j = 0; j < 4; j++)
			{
				this.m_EquipItemList[j] = 0L;
			}
			this.m_listGameUnitUID.Clear();
			this.m_listGameUnitUIDChange.Clear();
			this.m_listNearTargetRange.Clear();
			this.m_DungeonRespawnUnitTempletUID = 0L;
			this.m_DungeonRespawnUnitTemplet = null;
			this.m_fLastRespawnTime = -1f;
			this.m_fLastDieTime = -1f;
			this.m_bSummonUnit = false;
			for (int k = 0; k < 5; k++)
			{
				this.m_aUnitSkillLevel[k] = 0;
			}
			this.m_regDate = ServiceTime.UtcNow;
			this.ShipCommandModule = new List<NKMShipCmdModule>();
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x000A6B04 File Offset: 0x000A4D04
		public void Rearm(int newUnitId, NKMUnitTempletBase fromUnitTemplet, NKMUnitTempletBase toUnitTemplet)
		{
			this.m_UnitID = newUnitId;
			this.m_UnitLevel = 1;
			this.m_iUnitLevelEXP = 0;
			this.m_bLock = true;
			for (int i = fromUnitTemplet.GetSkillCount(); i < toUnitTemplet.GetSkillCount(); i++)
			{
				this.m_aUnitSkillLevel[i] = 1;
			}
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x000A6B4C File Offset: 0x000A4D4C
		public void SetDungeonRespawnUnitTemplet(NKMDungeonRespawnUnitTemplet cNKMDungeonRespawnUnitTemplet)
		{
			this.m_DungeonRespawnUnitTemplet = cNKMDungeonRespawnUnitTemplet;
			if (this.m_DungeonRespawnUnitTemplet != null)
			{
				this.m_DungeonRespawnUnitTempletUID = this.m_DungeonRespawnUnitTemplet.m_TempletUID;
				return;
			}
			this.m_DungeonRespawnUnitTempletUID = 0L;
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000A6B77 File Offset: 0x000A4D77
		public void SetDungeonRespawnUnitTemplet()
		{
			if (this.m_DungeonRespawnUnitTempletUID != 0L)
			{
				this.m_DungeonRespawnUnitTemplet = NKMDungeonManager.GetNKMDungeonRespawnUnitTemplet(this.m_DungeonRespawnUnitTempletUID);
				return;
			}
			this.m_DungeonRespawnUnitTemplet = null;
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000A6B9A File Offset: 0x000A4D9A
		public NKMUnitExpTemplet GetExpTemplet()
		{
			return NKMUnitExpTemplet.FindByUnitId(this.m_UnitID, this.m_UnitLevel);
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x000A6BB0 File Offset: 0x000A4DB0
		public void FillSkillLevelByUnitID(int unitID)
		{
			if (unitID <= 0)
			{
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			if (unitTempletBase == null)
			{
				Log.Error(string.Format("Can not found Unit ID. {0}", unitID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitData.cs", 265);
				return;
			}
			if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
			{
				int skillCount = NKMUnitManager.GetUnitTempletBase(unitID).GetSkillCount();
				for (int i = 0; i < 5; i++)
				{
					if (i < skillCount)
					{
						this.m_aUnitSkillLevel[i] = 1;
					}
					else
					{
						this.m_aUnitSkillLevel[i] = 0;
					}
				}
			}
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x000A6C28 File Offset: 0x000A4E28
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_UnitUID);
			stream.PutOrGet(ref this.m_UserUID);
			stream.PutOrGet(ref this.m_UnitID);
			stream.PutOrGet(ref this.m_UnitLevel);
			stream.PutOrGet(ref this.m_iUnitLevelEXP);
			stream.PutOrGet(ref this.m_SkinID);
			stream.PutOrGet(ref this.m_fInjury);
			stream.PutOrGet(ref this.m_LimitBreakLevel);
			stream.PutOrGet(ref this.m_bLock);
			stream.PutOrGet(ref this.m_listStatEXP);
			stream.PutOrGet(ref this.m_listGameUnitUID);
			stream.PutOrGet(ref this.m_listGameUnitUIDChange);
			stream.PutOrGet(ref this.m_listNearTargetRange);
			stream.PutOrGet(ref this.m_aUnitSkillLevel);
			stream.PutOrGet(ref this.m_EquipItemList);
			stream.PutOrGet(ref this.loyalty);
			stream.PutOrGet(ref this.isPermanentContract);
			stream.PutOrGet(ref this.isSeized);
			stream.PutOrGet(ref this.fromContract);
			stream.PutOrGet(ref this.officeRoomId);
			stream.PutOrGet(ref this.m_regDate);
			stream.PutOrGetEnum<OfficeGrade>(ref this.officeGrade);
			stream.PutOrGet(ref this.officeGaugeStartTime);
			stream.PutOrGet(ref this.m_DungeonRespawnUnitTempletUID);
			stream.PutOrGet(ref this.isFavorite);
			stream.PutOrGet<NKMShipCmdModule>(ref this.ShipCommandModule);
			stream.PutOrGet(ref this.tacticLevel);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x000A6D79 File Offset: 0x000A4F79
		public NKMUnitTemplet GetUnitTemplet()
		{
			return NKMUnitManager.GetUnitTemplet(this.m_UnitID);
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x000A6D86 File Offset: 0x000A4F86
		public int GetShipGroupId()
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitID);
			if (unitTempletBase == null)
			{
				return -1;
			}
			return unitTempletBase.m_ShipGroupID;
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x000A6D9E File Offset: 0x000A4F9E
		public void SetOfficeRoomId(int roomId, OfficeGrade officeGrade, DateTime startTime)
		{
			this.officeRoomId = roomId;
			this.officeGrade = officeGrade;
			this.officeGaugeStartTime = startTime;
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x000A6DB8 File Offset: 0x000A4FB8
		public float GetOfficeRoomHeartGauge()
		{
			if (this.officeRoomId <= 0)
			{
				return 0f;
			}
			NKMOfficeGradeTemplet nkmofficeGradeTemplet = NKMOfficeGradeTemplet.Find(this.OfficeGrade);
			return Math.Min(1f, (float)((ServiceTime.Recent - this.officeGaugeStartTime).TotalSeconds / nkmofficeGradeTemplet.ChargingTime.TotalSeconds));
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x000A6E14 File Offset: 0x000A5014
		public bool CheckOfficeRoomHeartFull()
		{
			if (this.officeRoomId <= 0)
			{
				return false;
			}
			if (this.IsPermanentContract)
			{
				return false;
			}
			if (this.loyalty >= 10000)
			{
				return false;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitID);
			if (unitTempletBase != null && unitTempletBase.IsTrophy)
			{
				return false;
			}
			NKMOfficeGradeTemplet nkmofficeGradeTemplet = NKMOfficeGradeTemplet.Find(this.OfficeGrade);
			return ServiceTime.Recent - this.officeGaugeStartTime >= nkmofficeGradeTemplet.ChargingTime;
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x000A6E88 File Offset: 0x000A5088
		public int GetShipSkillIndex(int skillID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitID);
			if (unitTempletBase == null)
			{
				return -1;
			}
			return unitTempletBase.GetShipSkillIndex(skillID);
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x000A6EB0 File Offset: 0x000A50B0
		public int GetShipSkillLevel(int skillID)
		{
			int shipSkillIndex = this.GetShipSkillIndex(skillID);
			if (shipSkillIndex < 0)
			{
				return 0;
			}
			return this.GetSkillLevelByIndex(shipSkillIndex);
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x000A6ED2 File Offset: 0x000A50D2
		public NKMShipSkillTemplet GetShipSkillTempletByIndex(int index)
		{
			return NKMShipSkillManager.GetShipSkillTempletByStrID(NKMUnitManager.GetUnitTempletBase(this.m_UnitID).GetSkillStrID(index));
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x000A6EEC File Offset: 0x000A50EC
		public int GetUnitSkillIndex(int skillID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitID);
			if (unitTempletBase == null)
			{
				return -1;
			}
			return unitTempletBase.GetSkillIndex(skillID);
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x000A6F14 File Offset: 0x000A5114
		public int GetUnitSkillLevel(int skillID)
		{
			int unitSkillIndex = this.GetUnitSkillIndex(skillID);
			if (unitSkillIndex < 0)
			{
				return 0;
			}
			return this.GetSkillLevelByIndex(unitSkillIndex);
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x000A6F36 File Offset: 0x000A5136
		public NKMUnitSkillTemplet GetUnitSkillTempletByIndex(int index)
		{
			return NKMUnitSkillManager.GetSkillTemplet(NKMUnitManager.GetUnitTempletBase(this.m_UnitID).GetSkillStrID(index), this.GetSkillLevelByIndex(index));
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000A6F58 File Offset: 0x000A5158
		public NKMUnitSkillTemplet GetUnitSkillTempletByType(NKM_SKILL_TYPE eSkillType)
		{
			if (eSkillType == NKM_SKILL_TYPE.NST_INVALID)
			{
				return null;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitID);
			if (unitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				return null;
			}
			for (int i = 0; i < unitTempletBase.GetSkillCount(); i++)
			{
				string skillStrID = unitTempletBase.GetSkillStrID(i);
				int skillLevelByIndex = this.GetSkillLevelByIndex(i);
				NKMUnitSkillTemplet skillTemplet = NKMUnitSkillManager.GetSkillTemplet(skillStrID, skillLevelByIndex);
				if (skillTemplet == null)
				{
					Log.Error(string.Format("SkillType {0} of unitID {1} (SkillID {2}, Lv {3}) not found", new object[]
					{
						eSkillType,
						this.m_UnitID,
						skillStrID,
						skillLevelByIndex
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitData.cs", 451);
				}
				else if (skillTemplet.m_NKM_SKILL_TYPE == eSkillType)
				{
					return skillTemplet;
				}
			}
			if (eSkillType != NKM_SKILL_TYPE.NST_ATTACK)
			{
				Log.Error(string.Format("unitID {0} do not have skillType {1}", this.m_UnitID, eSkillType), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitData.cs", 462);
			}
			return null;
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000A7030 File Offset: 0x000A5230
		public int GetSkillIndex(string skillStrID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitID);
			if (unitTempletBase == null)
			{
				return -1;
			}
			return unitTempletBase.GetSkillIndex(skillStrID);
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000A7058 File Offset: 0x000A5258
		public int GetSkillLevel(string skillStrID)
		{
			int skillIndex = this.GetSkillIndex(skillStrID);
			if (skillIndex < 0)
			{
				return 0;
			}
			return this.GetSkillLevelByIndex(skillIndex);
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x000A707A File Offset: 0x000A527A
		public int GetSkillLevelByIndex(int index)
		{
			if (index < 0)
			{
				return 0;
			}
			if (index >= this.m_aUnitSkillLevel.Length)
			{
				return 0;
			}
			if (this.m_aUnitSkillLevel[index] != 0)
			{
				return this.m_aUnitSkillLevel[index];
			}
			return 1;
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x000A70A4 File Offset: 0x000A52A4
		public bool IsUnitSkillUnlockedByType(NKM_SKILL_TYPE eSkillType)
		{
			if (eSkillType == NKM_SKILL_TYPE.NST_INVALID)
			{
				return true;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitID);
			if (unitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				return true;
			}
			for (int i = 0; i < unitTempletBase.GetSkillCount(); i++)
			{
				string skillStrID = unitTempletBase.GetSkillStrID(i);
				int skillLevelByIndex = this.GetSkillLevelByIndex(i);
				NKMUnitSkillTemplet skillTemplet = NKMUnitSkillManager.GetSkillTemplet(skillStrID, skillLevelByIndex);
				if (skillTemplet == null)
				{
					Log.Error(string.Format("SkillType {0} of unitID {1} (SkillID {2}, Lv {3}) not found", new object[]
					{
						eSkillType,
						this.m_UnitID,
						skillStrID,
						skillLevelByIndex
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitData.cs", 523);
				}
				else if (skillTemplet.m_NKM_SKILL_TYPE == eSkillType)
				{
					return !NKMUnitSkillManager.IsLockedSkill(skillTemplet.m_ID, (int)this.m_LimitBreakLevel);
				}
			}
			if (eSkillType != NKM_SKILL_TYPE.NST_ATTACK)
			{
				Log.Error(string.Format("unitID {0} do not have skillType {1}", this.m_UnitID, eSkillType), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitData.cs", 534);
			}
			return true;
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000A7195 File Offset: 0x000A5395
		public int GetUnitSkillCount()
		{
			return NKMUnitManager.GetUnitTempletBase(this.m_UnitID).GetSkillCount();
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x000A71A8 File Offset: 0x000A53A8
		public int GetStarGrade(NKMUnitTempletBase templetBase)
		{
			NKM_UNIT_TYPE nkm_UNIT_TYPE = templetBase.m_NKM_UNIT_TYPE;
			if (nkm_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL || nkm_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
			{
				return templetBase.m_StarGradeMax - 3 + (int)Math.Min(this.m_LimitBreakLevel, 3);
			}
			return templetBase.m_StarGradeMax;
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x000A71E0 File Offset: 0x000A53E0
		public int GetStarGrade()
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitID);
			NKM_UNIT_TYPE nkm_UNIT_TYPE = unitTempletBase.m_NKM_UNIT_TYPE;
			if (nkm_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL || nkm_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
			{
				return unitTempletBase.m_StarGradeMax - 3 + (int)Math.Min(this.m_LimitBreakLevel, 3);
			}
			return unitTempletBase.m_StarGradeMax;
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000A7224 File Offset: 0x000A5424
		public NKM_UNIT_GRADE GetUnitGrade()
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitID);
			if (unitTempletBase == null)
			{
				return NKM_UNIT_GRADE.NUG_N;
			}
			return unitTempletBase.m_NKM_UNIT_GRADE;
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x000A7248 File Offset: 0x000A5448
		public int CalculateOperationPower(NKMInventoryData inventoryData, int checkLv = 0, Dictionary<int, NKMBanShipData> dicNKMBanShipData = null, NKMOperator operatorData = null)
		{
			switch (NKMUnitManager.GetUnitTempletBase(this.m_UnitID).m_NKM_UNIT_TYPE)
			{
			case NKM_UNIT_TYPE.NUT_NORMAL:
			{
				NKMEquipmentSet equipmentSet = this.GetEquipmentSet(inventoryData);
				return this.CalculateUnitOperationPower(equipmentSet);
			}
			case NKM_UNIT_TYPE.NUT_SHIP:
				return this.CalculateShipOperationPower(checkLv, dicNKMBanShipData);
			case NKM_UNIT_TYPE.NUT_OPERATOR:
				return operatorData.CalculateOperatorOperationPower();
			default:
				return 0;
			}
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000A729E File Offset: 0x000A549E
		public void InitEquipItemFromDb(long weaponUid, long defenceUid, long accessoryUid, long accessroy2Uid)
		{
			this.m_EquipItemList[0] = weaponUid;
			this.m_EquipItemList[1] = defenceUid;
			this.m_EquipItemList[2] = accessoryUid;
			this.m_EquipItemList[3] = accessroy2Uid;
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x000A72C8 File Offset: 0x000A54C8
		public bool EquipItem(NKMInventoryData InventoryData, long equip_item_uid, out long unequip_item_uid, ITEM_EQUIP_POSITION equipPosition)
		{
			unequip_item_uid = 0L;
			if (equipPosition == ITEM_EQUIP_POSITION.IEP_NONE)
			{
				return false;
			}
			if (equip_item_uid == 0L)
			{
				return false;
			}
			unequip_item_uid = this.m_EquipItemList[(int)equipPosition];
			if (unequip_item_uid != 0L && !this.UnEquipItem(InventoryData, unequip_item_uid, equipPosition))
			{
				return false;
			}
			NKMEquipItemData itemEquip = InventoryData.GetItemEquip(equip_item_uid);
			if (itemEquip == null)
			{
				return false;
			}
			itemEquip.m_OwnerUnitUID = this.m_UnitUID;
			this.m_EquipItemList[(int)equipPosition] = equip_item_uid;
			return true;
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x000A7328 File Offset: 0x000A5528
		public bool UnEquipItem(NKMInventoryData InventoryData, long equipUid)
		{
			int num = -1;
			for (int i = 0; i < this.EquipItemUids.Count; i++)
			{
				if (this.m_EquipItemList[i] == equipUid)
				{
					num = i;
					break;
				}
			}
			if (num < 0)
			{
				return false;
			}
			NKMEquipItemData itemEquip = InventoryData.GetItemEquip(equipUid);
			if (itemEquip == null)
			{
				return false;
			}
			itemEquip.m_OwnerUnitUID = -1L;
			this.m_EquipItemList[num] = 0L;
			return true;
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x000A7384 File Offset: 0x000A5584
		public bool UnEquipItem(NKMInventoryData InventoryData, long unequip_item_uid, ITEM_EQUIP_POSITION equipPisition)
		{
			if (equipPisition == ITEM_EQUIP_POSITION.IEP_NONE)
			{
				return false;
			}
			if (this.m_EquipItemList[(int)equipPisition] == 0L)
			{
				return false;
			}
			NKMEquipItemData itemEquip = InventoryData.GetItemEquip(unequip_item_uid);
			if (itemEquip == null)
			{
				return false;
			}
			itemEquip.m_OwnerUnitUID = -1L;
			this.m_EquipItemList[(int)equipPisition] = 0L;
			return true;
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x000A73C3 File Offset: 0x000A55C3
		public long GetEquipItemWeaponUid()
		{
			return this.m_EquipItemList[0];
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000A73CD File Offset: 0x000A55CD
		public long GetEquipItemDefenceUid()
		{
			return this.m_EquipItemList[1];
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x000A73D7 File Offset: 0x000A55D7
		public long GetEquipItemAccessoryUid()
		{
			return this.m_EquipItemList[2];
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x000A73E1 File Offset: 0x000A55E1
		public long GetEquipItemAccessory2Uid()
		{
			return this.m_EquipItemList[3];
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x000A73EB File Offset: 0x000A55EB
		public long GetEquipUid(ITEM_EQUIP_POSITION eITEM_EQUIP_POSITION)
		{
			return this.m_EquipItemList[(int)eITEM_EQUIP_POSITION];
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000A73F8 File Offset: 0x000A55F8
		public void ResetEquipment()
		{
			for (int i = 0; i < this.m_EquipItemList.Length; i++)
			{
				this.m_EquipItemList[i] = 0L;
			}
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000A7422 File Offset: 0x000A5622
		public IEnumerable<long> GetValidEquipUids()
		{
			int num;
			for (int i = 0; i < this.m_EquipItemList.Length; i = num)
			{
				if (this.m_EquipItemList[i] > 0L)
				{
					yield return this.m_EquipItemList[i];
				}
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000A7432 File Offset: 0x000A5632
		public void FillDataFromDummy(NKMDummyUnitData cNKMDummyUnitData)
		{
			this.m_UnitID = cNKMDummyUnitData.UnitId;
			this.m_UnitLevel = cNKMDummyUnitData.UnitLevel;
			this.m_SkinID = cNKMDummyUnitData.SkinId;
			this.m_LimitBreakLevel = cNKMDummyUnitData.LimitBreakLevel;
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000A7464 File Offset: 0x000A5664
		public void FillDataFromAsyncUnitData(NKMAsyncUnitData cNKMAsyncleUnitData)
		{
			this.m_UnitID = cNKMAsyncleUnitData.unitId;
			this.m_UnitLevel = cNKMAsyncleUnitData.unitLevel;
			this.m_SkinID = cNKMAsyncleUnitData.skinId;
			this.m_LimitBreakLevel = (short)cNKMAsyncleUnitData.limitBreakLevel;
			this.ShipCommandModule = cNKMAsyncleUnitData.shipModules;
			this.tacticLevel = cNKMAsyncleUnitData.tacticLevel;
			for (int i = 0; i < this.m_aUnitSkillLevel.Length; i++)
			{
				if (cNKMAsyncleUnitData.skillLevel.Count > i)
				{
					this.m_aUnitSkillLevel[i] = cNKMAsyncleUnitData.skillLevel[i];
				}
			}
			for (int j = 0; j < this.m_EquipItemList.Length; j++)
			{
				if (cNKMAsyncleUnitData.equipUids.Count > j)
				{
					this.m_EquipItemList[j] = cNKMAsyncleUnitData.equipUids[j];
				}
			}
			for (int k = 0; k < this.m_listStatEXP.Count; k++)
			{
				if (cNKMAsyncleUnitData.statExp.Count > k)
				{
					this.m_listStatEXP[k] = cNKMAsyncleUnitData.statExp[k];
				}
			}
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x000A7560 File Offset: 0x000A5760
		public void FillDataFromRespawnOrigin(NKMUnitData cRespawnOriginUnitData, bool bSimple)
		{
			this.m_UnitLevel = cRespawnOriginUnitData.m_UnitLevel;
			this.m_LimitBreakLevel = cRespawnOriginUnitData.m_LimitBreakLevel;
			this.tacticLevel = cRespawnOriginUnitData.tacticLevel;
			if (!bSimple)
			{
				for (int i = 0; i < this.m_aUnitSkillLevel.Length; i++)
				{
					this.m_aUnitSkillLevel[i] = cRespawnOriginUnitData.m_aUnitSkillLevel[i];
				}
				for (int j = 0; j < this.m_listStatEXP.Count; j++)
				{
					this.m_listStatEXP[j] = cRespawnOriginUnitData.m_listStatEXP[j];
				}
				for (int k = 0; k < this.m_EquipItemList.Length; k++)
				{
					this.m_EquipItemList[k] = cRespawnOriginUnitData.m_EquipItemList[k];
				}
				this.loyalty = cRespawnOriginUnitData.loyalty;
			}
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x000A7617 File Offset: 0x000A5817
		public bool IsDungeonUnit()
		{
			return this.m_DungeonRespawnUnitTemplet != null;
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x000A7622 File Offset: 0x000A5822
		public float GetMultiplierByPermanentContract()
		{
			if (this.isPermanentContract || this.loyalty >= 10000)
			{
				return 0.02f;
			}
			return 0f;
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x000A7644 File Offset: 0x000A5844
		public void SetPermanentContract()
		{
			this.isPermanentContract = true;
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x000A764D File Offset: 0x000A584D
		public bool IsPermanentContractEnable()
		{
			return this.loyalty >= 10000 && !this.isPermanentContract;
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x000A7668 File Offset: 0x000A5868
		public NKMEquipmentSet GetEquipmentSet(NKMInventoryData inventoryData)
		{
			if (inventoryData == null)
			{
				return null;
			}
			NKMEquipItemData itemEquip = inventoryData.GetItemEquip(this.GetEquipItemWeaponUid());
			NKMEquipItemData itemEquip2 = inventoryData.GetItemEquip(this.GetEquipItemDefenceUid());
			NKMEquipItemData itemEquip3 = inventoryData.GetItemEquip(this.GetEquipItemAccessoryUid());
			NKMEquipItemData itemEquip4 = inventoryData.GetItemEquip(this.GetEquipItemAccessory2Uid());
			return new NKMEquipmentSet(itemEquip, itemEquip2, itemEquip3, itemEquip4);
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x000A76B5 File Offset: 0x000A58B5
		public NKMDummyUnitData ToDummyUnitData()
		{
			return new NKMDummyUnitData
			{
				UnitId = this.m_UnitID,
				UnitLevel = this.m_UnitLevel,
				LimitBreakLevel = this.m_LimitBreakLevel,
				SkinId = this.m_SkinID
			};
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x000A76EC File Offset: 0x000A58EC
		public int CalculateUnitOperationPower(NKMEquipmentSet equipmentSet)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitID);
			if (unitTempletBase == null)
			{
				Log.Error(string.Format("Can not found UnitTempletBase. UnitId:{0}", this.m_UnitID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitData.cs", 856);
				return 0;
			}
			if (unitTempletBase.StatTemplet == null)
			{
				Log.Error(string.Format("Can not found StatTemplet. UnitId:{0}", this.m_UnitID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitData.cs", 863);
				return 0;
			}
			float num = (float)(NKMConst.OperationPowerFactor.GetClassValue(unitTempletBase.m_NKM_UNIT_ROLE_TYPE) + NKMConst.OperationPowerFactor.GetGradeValue(unitTempletBase.m_NKM_UNIT_GRADE, unitTempletBase.m_bAwaken, unitTempletBase.IsRearmUnit)) * ((float)this.m_UnitLevel / 100f) * (1f + 0.1f * (float)this.m_LimitBreakLevel);
			int num2 = unitTempletBase.IsRearmUnit ? 50 : 100;
			int num3 = this.m_aUnitSkillLevel.Sum() * num2;
			int num4 = this.tacticLevel * NKMConst.OperationPowerFactor.GetTacticValue(unitTempletBase.m_NKM_UNIT_GRADE, unitTempletBase.m_bAwaken, unitTempletBase.IsRearmUnit);
			float num5 = (num + (float)num3 + (float)num4) * 0.6f;
			int num6 = 0;
			if (equipmentSet != null)
			{
				HashSet<long> setItemActivatedMark = NKMItemManager.GetSetItemActivatedMark(equipmentSet);
				num6 = this.CalculateEquipmentOperationPower(equipmentSet.Weapon, setItemActivatedMark.Contains(equipmentSet.WeaponUid));
				num6 += this.CalculateEquipmentOperationPower(equipmentSet.Defence, setItemActivatedMark.Contains(equipmentSet.DefenceUid));
				num6 += this.CalculateEquipmentOperationPower(equipmentSet.Accessory, setItemActivatedMark.Contains(equipmentSet.AccessoryUid));
				num6 += this.CalculateEquipmentOperationPower(equipmentSet.Accessory2, setItemActivatedMark.Contains(equipmentSet.Accessory2Uid));
			}
			float num7 = (float)num6 * 0.5f;
			return (int)(num5 + num7);
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000A787C File Offset: 0x000A5A7C
		public UnitLoyaltyUpdateData ToUnitLoyaltyUpdateData()
		{
			return new UnitLoyaltyUpdateData
			{
				unitUid = this.m_UnitUID,
				loyalty = this.loyalty,
				officeRoomId = this.OfficeRoomId,
				officeGrade = this.OfficeGrade,
				heartGaugeStartTime = this.OfficeGaugeStartTime
			};
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x000A78CC File Offset: 0x000A5ACC
		private int CalculateEquipmentOperationPower(NKMEquipItemData equipItemData, bool bSetOptionEnabled)
		{
			if (equipItemData == null)
			{
				return 0;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipItemData.m_ItemEquipID);
			if (equipTemplet == null)
			{
				Log.Error(string.Format("Can not found EquipTemplet. EquipId:{0}", equipItemData.m_ItemEquipID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitData.cs", 918);
				return 0;
			}
			int nkm_ITEM_TIER = equipTemplet.m_NKM_ITEM_TIER;
			float num = 100f * ((float)nkm_ITEM_TIER + 1.2f);
			float num2 = (float)((int)(equipTemplet.m_NKM_ITEM_GRADE + 1 - NKM_ITEM_GRADE.NIG_R) * (150 + 15 * (nkm_ITEM_TIER - 1)));
			float num3 = (num + num2) * (1f + 0.1f * (float)equipItemData.m_EnchantLevel);
			float precisionFactor = this.GetPrecisionFactor(nkm_ITEM_TIER, equipItemData.m_Precision, equipTemplet.m_bRelic);
			float num4 = 0f;
			if (equipItemData.m_Precision2 > 0)
			{
				num4 = this.GetPrecisionFactor(nkm_ITEM_TIER, equipItemData.m_Precision2, false);
			}
			float num5 = 0f;
			if (equipTemplet.m_bRelic && equipItemData.potentialOption != null)
			{
				float num6 = equipItemData.CalculatePotentialPercent();
				num5 = (10f + (float)nkm_ITEM_TIER * 12.5f) * (float)equipItemData.potentialOption.OpenedSocketCount * (1f + num6 - 0.5f);
			}
			float num7 = 1f;
			if (bSetOptionEnabled)
			{
				NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(equipItemData.m_SetOptionId);
				if (equipSetOptionTemplet != null)
				{
					num7 = 1f + (float)equipSetOptionTemplet.m_EquipSetPart * 0.05f;
				}
			}
			return (int)((num3 + precisionFactor + num4 + num5) * num7);
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x000A7A14 File Offset: 0x000A5C14
		private float GetPrecisionFactor(int equipTier, int precision, bool bRelic)
		{
			int num = bRelic ? 2 : 8;
			return (float)((4 + equipTier * 5) * num) * ((50f + (float)precision / 2f) / 100f);
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000A7A48 File Offset: 0x000A5C48
		private int CalculateShipOperationPower(int checkLv = 0, Dictionary<int, NKMBanShipData> dicNKMBanShipData = null)
		{
			int num = (int)(NKMUnitManager.GetUnitTempletBase(this.m_UnitID).m_NKM_UNIT_GRADE + 1);
			int level = (checkLv != 0) ? checkLv : this.m_UnitLevel;
			int shipFactor = this.GetShipFactor1(num, level);
			int num2 = (NKMUnitManager.GetUnitTempletBase(this.m_UnitID).m_StarGradeMax - 1) * 250 * (num + 2);
			int shipFactor2 = this.GetShipFactor3();
			int num3 = shipFactor + num2 + shipFactor2;
			NKMBanShipData nkmbanShipData;
			if (dicNKMBanShipData != null && dicNKMBanShipData.TryGetValue(this.GetShipGroupId(), out nkmbanShipData))
			{
				int nerfPercentByShipBanLevel = NKMUnitStatManager.GetNerfPercentByShipBanLevel((int)nkmbanShipData.m_BanLevel);
				num3 = (int)((float)num3 * ((float)(100 - nerfPercentByShipBanLevel) * 0.01f));
			}
			return num3;
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000A7ADD File Offset: 0x000A5CDD
		private int GetShipFactor1(int shipGrade, int level)
		{
			return level * (120 + shipGrade * 3);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000A7AE8 File Offset: 0x000A5CE8
		private int GetShipFactor3()
		{
			int num = (int)(this.m_LimitBreakLevel * 300);
			int num2 = 0;
			if (this.ShipCommandModule != null)
			{
				for (int i = 0; i < this.ShipCommandModule.Count; i++)
				{
					if (this.ShipCommandModule[i] != null && this.ShipCommandModule[i].slots != null)
					{
						for (int j = 0; j < this.ShipCommandModule[i].slots.Length; j++)
						{
							NKMShipCmdSlot nkmshipCmdSlot = this.ShipCommandModule[i].slots[j];
							if (nkmshipCmdSlot != null && nkmshipCmdSlot.statType != NKM_STAT_TYPE.NST_RANDOM)
							{
								num2++;
								break;
							}
						}
					}
				}
			}
			return num + num2 * 500;
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000A7B95 File Offset: 0x000A5D95
		public bool IsUnlockAccessory2()
		{
			return this.m_LimitBreakLevel >= 3;
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000A7BA4 File Offset: 0x000A5DA4
		public bool IsSameBaseUnit(int unitID)
		{
			if (unitID == this.m_UnitID)
			{
				return true;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitID);
			return unitTempletBase != null && unitTempletBase.IsSameBaseUnit(unitID);
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x000A7BD4 File Offset: 0x000A5DD4
		public void SetSeized()
		{
			this.isSeized = true;
		}

		// Token: 0x0400214E RID: 8526
		private bool isPermanentContract;

		// Token: 0x0400214F RID: 8527
		private bool isSeized;

		// Token: 0x04002150 RID: 8528
		private bool fromContract;

		// Token: 0x04002151 RID: 8529
		private int officeRoomId;

		// Token: 0x04002152 RID: 8530
		private OfficeGrade officeGrade;

		// Token: 0x04002153 RID: 8531
		private DateTime officeGaugeStartTime;

		// Token: 0x04002154 RID: 8532
		public long m_UnitUID;

		// Token: 0x04002155 RID: 8533
		public long m_UserUID;

		// Token: 0x04002156 RID: 8534
		[DataMember]
		public int m_UnitID;

		// Token: 0x04002157 RID: 8535
		public int m_UnitLevel;

		// Token: 0x04002158 RID: 8536
		public int m_iUnitLevelEXP;

		// Token: 0x04002159 RID: 8537
		public int m_SkinID;

		// Token: 0x0400215A RID: 8538
		public float m_fInjury;

		// Token: 0x0400215B RID: 8539
		public int[] m_aUnitSkillLevel = new int[5];

		// Token: 0x0400215C RID: 8540
		public short m_LimitBreakLevel;

		// Token: 0x0400215D RID: 8541
		public bool m_bLock;

		// Token: 0x0400215E RID: 8542
		public List<int> m_listStatEXP = new List<int>();

		// Token: 0x0400215F RID: 8543
		public List<short> m_listGameUnitUID = new List<short>();

		// Token: 0x04002160 RID: 8544
		public List<short> m_listGameUnitUIDChange = new List<short>();

		// Token: 0x04002161 RID: 8545
		public List<float> m_listNearTargetRange = new List<float>();

		// Token: 0x04002162 RID: 8546
		public long m_DungeonRespawnUnitTempletUID;

		// Token: 0x04002163 RID: 8547
		public NKMDungeonRespawnUnitTemplet m_DungeonRespawnUnitTemplet;

		// Token: 0x04002164 RID: 8548
		public float m_fLastRespawnTime = -1f;

		// Token: 0x04002165 RID: 8549
		public float m_fLastDieTime = -1f;

		// Token: 0x04002166 RID: 8550
		public bool m_bSummonUnit;

		// Token: 0x04002167 RID: 8551
		public DateTime m_regDate = DateTime.MinValue;

		// Token: 0x04002168 RID: 8552
		public int loyalty;

		// Token: 0x04002169 RID: 8553
		public bool isFavorite;

		// Token: 0x0400216A RID: 8554
		public int unitPower;

		// Token: 0x0400216B RID: 8555
		public int tacticLevel;

		// Token: 0x0400216C RID: 8556
		private long[] m_EquipItemList = new long[4];

		// Token: 0x0400216D RID: 8557
		public List<NKMShipCmdModule> ShipCommandModule = new List<NKMShipCmdModule>();

		// Token: 0x0400216E RID: 8558
		public const int ACC_2_UNLOCK_LEVEL = 3;
	}
}
