using System;
using System.Collections.Generic;
using ClientPacket.Office;
using Cs.Logging;
using NKC.Templet;
using NKC.Templet.Office;
using NKC.UI.Office;
using NKC.Util;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using NKM.Templet.Office;
using UnityEngine;

namespace NKC.Office
{
	// Token: 0x0200083C RID: 2108
	public static class NKCOfficeManager
	{
		// Token: 0x060053E9 RID: 21481 RVA: 0x00199308 File Offset: 0x00197508
		public static void LoadTemplets()
		{
			if (!NKCOfficeManager.m_bTempletInitialized)
			{
				NKMTempletContainer<NKMOfficeRoomTemplet>.Load("AB_SCRIPT", "LUA_OFFICE_ROOM_TEMPLET", "m_OfficeRoom", new Func<NKMLua, NKMOfficeRoomTemplet>(NKMOfficeRoomTemplet.LoadFromLUA));
				NKMTempletContainer<NKMOfficeSectionTemplet>.Load("AB_SCRIPT", "LUA_OFFICE_SECTION_TEMPLET", "m_OfficeSection", new Func<NKMLua, NKMOfficeSectionTemplet>(NKMOfficeSectionTemplet.LoadFromLua));
				NKMTempletContainer<NKCOfficeCharacterTemplet>.Load("AB_SCRIPT", "LUA_OFFICE_CHARACTER_TEMPLET", "m_OfficeCharacter", new Func<NKMLua, NKCOfficeCharacterTemplet>(NKCOfficeCharacterTemplet.LoadFromLUA));
				NKMTempletContainer<NKCOfficePartyTemplet>.Load("AB_SCRIPT", "LUA_OFFICE_PARTY_TEMPLET", "OFFICE_PARTY_TEMPLET", new Func<NKMLua, NKCOfficePartyTemplet>(NKCOfficePartyTemplet.LoadFromLUA));
				NKMTempletContainer<NKMOfficeThemePresetTemplet>.Load("AB_SCRIPT", "LUA_OFFICE_THEMA_PRESET_TEMPLET", "OFFICE_THEMA_PRESET_TEMPLET", new Func<NKMLua, NKMOfficeThemePresetTemplet>(NKMOfficeThemePresetTemplet.LoadFromLUA), (NKMOfficeThemePresetTemplet e) => e.ThemaPresetStringID);
				if (!NKCAnimationEventManager.DataExist)
				{
					NKCAnimationEventManager.LoadFromLua();
				}
				NKMTempletContainer<NKMOfficeThemePresetTemplet>.Join();
				NKMTempletContainer<NKMOfficeSectionTemplet>.Join();
				NKMTempletContainer<NKMOfficeRoomTemplet>.Join();
				NKCOfficeManager.m_bTempletInitialized = true;
			}
		}

		// Token: 0x060053EA RID: 21482 RVA: 0x00199400 File Offset: 0x00197600
		public static void Drop()
		{
			NKCOfficeManager.m_bTempletInitialized = false;
			NKCOfficeManager.s_dicInteractionSkinGroup = null;
		}

		// Token: 0x060053EB RID: 21483 RVA: 0x00199410 File Offset: 0x00197610
		public static bool FunitureBoundaryCheck(int roomSizeX, int roomSizeY, NKCOfficeFunitureData funitureData)
		{
			return funitureData.Templet != null && funitureData.PosX >= 0 && funitureData.PosY >= 0 && funitureData.PosX + funitureData.SizeX - 1 < roomSizeX && funitureData.PosY + funitureData.SizeY - 1 < roomSizeY;
		}

		// Token: 0x060053EC RID: 21484 RVA: 0x00199464 File Offset: 0x00197664
		public static bool IsFunitureOverlaps(NKCOfficeFunitureData lhs, NKCOfficeFunitureData rhs)
		{
			return lhs.PosX + lhs.SizeX - 1 >= rhs.PosX && rhs.PosX + rhs.SizeX - 1 >= lhs.PosX && lhs.PosY + lhs.SizeY - 1 >= rhs.PosY && rhs.PosY + rhs.SizeY - 1 >= lhs.PosY;
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x001994D8 File Offset: 0x001976D8
		public static ValueTuple<int, int> GetSize(this NKMOfficeRoomTemplet templet, BuildingFloor target)
		{
			if (target <= BuildingFloor.Tile)
			{
				return new ValueTuple<int, int>(templet.FloorX, templet.FloorY);
			}
			if (target == BuildingFloor.LeftWall)
			{
				return new ValueTuple<int, int>(templet.LeftWallX, templet.LeftWallY);
			}
			if (target != BuildingFloor.RightWall)
			{
				return new ValueTuple<int, int>(0, 0);
			}
			return new ValueTuple<int, int>(templet.RightWallX, templet.RightWallY);
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x00199534 File Offset: 0x00197734
		public static void TryPlayReactionInteraction(NKCOfficeFuniture furniture, IEnumerable<NKCOfficeCharacter> lstCharacter)
		{
			if (furniture == null || furniture.Templet == null || lstCharacter == null)
			{
				return;
			}
			List<NKCOfficeFurnitureInteractionTemplet> interactionTempletList = NKCOfficeFurnitureInteractionTemplet.GetInteractionTempletList(furniture.Templet, NKCOfficeFurnitureInteractionTemplet.ActType.Reaction);
			if (interactionTempletList.Count == 0)
			{
				return;
			}
			Debug.Log("Try playing reaction : " + furniture.Templet.InteractionGroupID);
			using (IEnumerator<NKCOfficeCharacter> enumerator = lstCharacter.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NKCOfficeCharacter character = enumerator.Current;
					if (!(character == null))
					{
						List<NKCOfficeFurnitureInteractionTemplet> list = interactionTempletList.FindAll((NKCOfficeFurnitureInteractionTemplet x) => x.CheckUnitInteractionCondition(character) && NKCAnimationEventManager.CanPlayAnimEvent(character, x.UnitAni));
						if (list != null && list.Count != 0)
						{
							NKCOfficeFurnitureInteractionTemplet templet = NKCTempletUtility.PickRatio<NKCOfficeFurnitureInteractionTemplet>(list, (NKCOfficeFurnitureInteractionTemplet x) => x.ActProbability);
							character.RegisterInteraction(furniture, templet);
						}
					}
				}
			}
		}

		// Token: 0x060053EF RID: 21487 RVA: 0x00199628 File Offset: 0x00197828
		public static bool PlayInteraction(NKCOfficeCharacter character, NKCOfficeFuniture furniture)
		{
			if (character == null || furniture == null)
			{
				return false;
			}
			if (furniture.Templet == null)
			{
				return false;
			}
			List<NKCOfficeFurnitureInteractionTemplet> possibleTemplets = NKCOfficeManager.GetPossibleTemplets(character, furniture, NKCOfficeFurnitureInteractionTemplet.ActType.Common);
			if (possibleTemplets == null || possibleTemplets.Count == 0)
			{
				return false;
			}
			NKCOfficeFurnitureInteractionTemplet templet = NKCTempletUtility.PickRatio<NKCOfficeFurnitureInteractionTemplet>(possibleTemplets, (NKCOfficeFurnitureInteractionTemplet x) => x.ActProbability);
			character.RegisterInteraction(furniture, templet);
			return true;
		}

		// Token: 0x060053F0 RID: 21488 RVA: 0x0019969C File Offset: 0x0019789C
		public static bool PlayInteraction(NKCOfficeCharacter actor, NKCOfficeCharacter target, bool bIgnoreRange = false, bool bForceAlign = false)
		{
			if (actor == null || target == null)
			{
				return false;
			}
			List<NKCOfficeUnitInteractionTemplet> possibleTemplets = NKCOfficeManager.GetPossibleTemplets(actor, target, bIgnoreRange);
			if (possibleTemplets == null || possibleTemplets.Count == 0)
			{
				return false;
			}
			NKCOfficeUnitInteractionTemplet nkcofficeUnitInteractionTemplet = possibleTemplets[UnityEngine.Random.Range(0, possibleTemplets.Count)];
			if (bForceAlign || nkcofficeUnitInteractionTemplet.AlignUnit)
			{
				Vector3 actionPosition;
				Vector3 actionPosition2;
				if (!actor.OfficeBuilding.CalcInteractionPos(actor, target, out actionPosition, out actionPosition2))
				{
					return false;
				}
				actor.RegisterInteraction(nkcofficeUnitInteractionTemplet, target, true, actionPosition);
				target.RegisterInteraction(nkcofficeUnitInteractionTemplet, actor, false, actionPosition2);
			}
			else
			{
				actor.RegisterInteraction(nkcofficeUnitInteractionTemplet, target, true, actor.transform.localPosition);
				target.RegisterInteraction(nkcofficeUnitInteractionTemplet, actor, false, target.transform.localPosition);
			}
			return true;
		}

		// Token: 0x060053F1 RID: 21489 RVA: 0x0019974C File Offset: 0x0019794C
		public static bool CanPlayInteraction(NKCOfficeCharacter character, NKCOfficeFuniture furniture)
		{
			if (character == null || furniture == null)
			{
				return false;
			}
			if (NKCUIOffice.IsInstanceOpen && !NKCUIOffice.GetInstance().CanPlayInteraction())
			{
				return false;
			}
			if (furniture.Templet == null)
			{
				return false;
			}
			if (furniture.HasInteractionTarget())
			{
				return false;
			}
			if (character.HasInteractionTarget())
			{
				return false;
			}
			if (NKCOfficeManager.IsFunitureInteractionPointBlocked(furniture, character.OfficeBuilding))
			{
				return false;
			}
			List<NKCOfficeFurnitureInteractionTemplet> possibleTemplets = NKCOfficeManager.GetPossibleTemplets(character, furniture, NKCOfficeFurnitureInteractionTemplet.ActType.Common);
			return possibleTemplets != null && possibleTemplets.Count != 0;
		}

		// Token: 0x060053F2 RID: 21490 RVA: 0x001997C8 File Offset: 0x001979C8
		public static bool CanPlayInteraction(NKCOfficeCharacter actor, NKCOfficeCharacter target, bool bIgnoreRange = false)
		{
			if (actor == null || target == null)
			{
				return false;
			}
			if (actor == target)
			{
				return false;
			}
			if (NKCUIOffice.IsInstanceOpen && !NKCUIOffice.GetInstance().CanPlayInteraction())
			{
				return false;
			}
			if (!target.IsUnitInteractTargetable())
			{
				return false;
			}
			if (!NKCOfficeManager.IsUnitInSameInteractionSkinGroup(actor, target))
			{
				return false;
			}
			List<NKCOfficeUnitInteractionTemplet> possibleTemplets = NKCOfficeManager.GetPossibleTemplets(actor, target, bIgnoreRange);
			return possibleTemplets != null && possibleTemplets.Count != 0;
		}

		// Token: 0x060053F3 RID: 21491 RVA: 0x00199838 File Offset: 0x00197A38
		private static List<NKCOfficeFurnitureInteractionTemplet> GetPossibleTemplets(NKCOfficeCharacter character, NKCOfficeFuniture furniture, NKCOfficeFurnitureInteractionTemplet.ActType type)
		{
			List<NKCOfficeFurnitureInteractionTemplet> interactionTempletList = NKCOfficeFurnitureInteractionTemplet.GetInteractionTempletList(furniture.Templet, type);
			if (interactionTempletList == null || interactionTempletList.Count == 0)
			{
				return null;
			}
			List<NKCOfficeFurnitureInteractionTemplet> list = new List<NKCOfficeFurnitureInteractionTemplet>();
			foreach (NKCOfficeFurnitureInteractionTemplet nkcofficeFurnitureInteractionTemplet in interactionTempletList)
			{
				if (nkcofficeFurnitureInteractionTemplet.CheckUnitInteractionCondition(character) && NKCOfficeManager.CheckInteractionPlay(nkcofficeFurnitureInteractionTemplet, character, furniture))
				{
					list.Add(nkcofficeFurnitureInteractionTemplet);
				}
			}
			return list;
		}

		// Token: 0x060053F4 RID: 21492 RVA: 0x001998BC File Offset: 0x00197ABC
		private static string GetSkinInteractionGroup(int skinID)
		{
			if (skinID == 0)
			{
				return string.Empty;
			}
			if (NKCOfficeManager.s_dicInteractionSkinGroup == null)
			{
				NKCOfficeUnitInteractionTemplet.LoadFromLua();
			}
			string result;
			if (NKCOfficeManager.s_dicInteractionSkinGroup.TryGetValue(skinID, out result))
			{
				return result;
			}
			return string.Empty;
		}

		// Token: 0x060053F5 RID: 21493 RVA: 0x001998F4 File Offset: 0x00197AF4
		private static bool IsUnitInSameInteractionSkinGroup(NKCOfficeCharacter actor, NKCOfficeCharacter target)
		{
			string skinInteractionGroup = NKCOfficeManager.GetSkinInteractionGroup(actor.SkinID);
			string skinInteractionGroup2 = NKCOfficeManager.GetSkinInteractionGroup(target.SkinID);
			return skinInteractionGroup.Equals(skinInteractionGroup2);
		}

		// Token: 0x060053F6 RID: 21494 RVA: 0x00199920 File Offset: 0x00197B20
		private static List<NKCOfficeUnitInteractionTemplet> GetPossibleTemplets(NKCOfficeCharacter actor, NKCOfficeCharacter target, bool bIgnoreRange = false)
		{
			List<NKCOfficeUnitInteractionTemplet> unitInteractionCache = actor.UnitInteractionCache;
			if (unitInteractionCache == null || unitInteractionCache.Count == 0)
			{
				return null;
			}
			float magnitude = (target.transform.localPosition - actor.transform.localPosition).magnitude;
			List<NKCOfficeUnitInteractionTemplet> list = new List<NKCOfficeUnitInteractionTemplet>();
			foreach (NKCOfficeUnitInteractionTemplet nkcofficeUnitInteractionTemplet in unitInteractionCache)
			{
				if ((bIgnoreRange || nkcofficeUnitInteractionTemplet.ActRange >= magnitude) && nkcofficeUnitInteractionTemplet.CheckUnitInteractionCondition(target, true) && NKCAnimationEventManager.CanPlayAnimEvent(target, nkcofficeUnitInteractionTemplet.TargetAni))
				{
					list.Add(nkcofficeUnitInteractionTemplet);
				}
			}
			return list;
		}

		// Token: 0x060053F7 RID: 21495 RVA: 0x001999D8 File Offset: 0x00197BD8
		public static bool IsFunitureInteractionPointBlocked(NKCOfficeFuniture furniture, NKCOfficeBuildingBase building)
		{
			GameObject interactionPoint = furniture.GetInteractionPoint();
			if (interactionPoint == null)
			{
				return false;
			}
			Vector3 localPos = building.m_Floor.Rect.InverseTransformPoint(interactionPoint.transform.position);
			OfficeFloorPosition officeFloorPosition = building.CalculateFloorPosition(localPos, 1, 1, false);
			if (!building.m_Floor.IsInBound(officeFloorPosition))
			{
				return true;
			}
			long num = building.FloorMap[officeFloorPosition.x, officeFloorPosition.y];
			return num != 0L && num != furniture.UID;
		}

		// Token: 0x060053F8 RID: 21496 RVA: 0x00199A57 File Offset: 0x00197C57
		private static bool CheckInteractionPlay(NKCOfficeFurnitureInteractionTemplet templet, NKCOfficeCharacter character, NKCOfficeFuniture furniture)
		{
			return NKCAnimationEventManager.CanPlayAnimEvent(character, templet.UnitAni) && (string.IsNullOrEmpty(templet.InteriorAni) || NKCAnimationEventManager.CanPlayAnimEvent(furniture, templet.InteriorAni));
		}

		// Token: 0x060053F9 RID: 21497 RVA: 0x00199A87 File Offset: 0x00197C87
		public static bool IsActTarget(NKCOfficeCharacter character, ActTargetType eActTargetType, HashSet<string> hsActTargetGroupID)
		{
			return !(character == null) && NKCOfficeManager.IsActTarget(character.UnitID, character.SkinID, eActTargetType, hsActTargetGroupID);
		}

		// Token: 0x060053FA RID: 21498 RVA: 0x00199AA8 File Offset: 0x00197CA8
		public static bool IsActTarget(int unitID, int skinID, ActTargetType eActTargetType, HashSet<string> hsActTargetGroupID)
		{
			if (hsActTargetGroupID == null)
			{
				return false;
			}
			switch (eActTargetType)
			{
			case ActTargetType.Group:
				return NKCOfficeManager.IsActGroup(unitID, hsActTargetGroupID);
			case ActTargetType.Skin:
				return hsActTargetGroupID.Contains(skinID.ToString());
			}
			if (hsActTargetGroupID.Contains(unitID.ToString()))
			{
				return true;
			}
			NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(unitID);
			return nkmunitTempletBase != null && nkmunitTempletBase.IsRearmUnit && nkmunitTempletBase.BaseUnit != null && hsActTargetGroupID.Contains(nkmunitTempletBase.BaseUnit.m_UnitID.ToString());
		}

		// Token: 0x060053FB RID: 21499 RVA: 0x00199B2C File Offset: 0x00197D2C
		public static bool IsActGroup(int unitID, IEnumerable<string> lstGroup)
		{
			return NKCOfficeManager.IsActGroup(NKMUnitManager.GetUnitTempletBase(unitID), lstGroup);
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x00199B3C File Offset: 0x00197D3C
		public static bool IsActGroup(NKMUnitTempletBase unitTemplet, IEnumerable<string> lstGroup)
		{
			if (unitTemplet == null)
			{
				return false;
			}
			if (lstGroup == null)
			{
				return false;
			}
			foreach (string text in lstGroup)
			{
				if (text != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 1125130083U)
					{
						if (num <= 632940583U)
						{
							if (num != 80939203U)
							{
								if (num != 341218208U)
								{
									if (num == 632940583U)
									{
										if (text == "STRIKER")
										{
											if (unitTemplet.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_STRIKER)
											{
												return true;
											}
											continue;
										}
									}
								}
								else if (text == "RANGER")
								{
									if (unitTemplet.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_RANGER)
									{
										return true;
									}
									continue;
								}
							}
							else if (text == "COUNTER")
							{
								if (unitTemplet.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_COUNTER))
								{
									return true;
								}
								continue;
							}
						}
						else if (num != 849393365U)
						{
							if (num != 936700674U)
							{
								if (num == 1125130083U)
								{
									if (text == "REPLACER")
									{
										if (unitTemplet.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_REPLACER))
										{
											return true;
										}
										continue;
									}
								}
							}
							else if (text == "TOWER")
							{
								if (unitTemplet.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_TOWER)
								{
									return true;
								}
								continue;
							}
						}
						else if (text == "SOLDIER")
						{
							if (unitTemplet.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_SOLDIER))
							{
								return true;
							}
							continue;
						}
					}
					else if (num <= 1579972867U)
					{
						if (num != 1417416822U)
						{
							if (num != 1432457764U)
							{
								if (num == 1579972867U)
								{
									if (text == "CORRUPTED")
									{
										if (unitTemplet.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_CORRUPTED))
										{
											return true;
										}
										continue;
									}
								}
							}
							else if (text == "ALL")
							{
								return true;
							}
						}
						else if (text == "SNIPER")
						{
							if (unitTemplet.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_SNIPER)
							{
								return true;
							}
							continue;
						}
					}
					else if (num <= 2584858751U)
					{
						if (num != 2506046220U)
						{
							if (num == 2584858751U)
							{
								if (text == "SUPPORTER")
								{
									if (unitTemplet.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER)
									{
										return true;
									}
									continue;
								}
							}
						}
						else if (text == "SIEGE")
						{
							if (unitTemplet.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_SIEGE)
							{
								return true;
							}
							continue;
						}
					}
					else if (num != 3528418063U)
					{
						if (num == 3702208890U)
						{
							if (text == "DEFENDER")
							{
								if (unitTemplet.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_DEFENDER)
								{
									return true;
								}
								continue;
							}
						}
					}
					else if (text == "MECHANIC")
					{
						if (unitTemplet.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_MECHANIC))
						{
							return true;
						}
						continue;
					}
				}
				if (unitTemplet.m_hsActGroup != null && unitTemplet.m_hsActGroup.Contains(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x00199E58 File Offset: 0x00198058
		public static bool IsEmpryRoom(NKMOfficeRoom room)
		{
			return room == null || ((room.furnitures == null || room.furnitures.Count <= 0) && (room.floorInteriorId == 0 || room.floorInteriorId == NKMCommonConst.Office.DefaultFloorItem.Id) && (room.wallInteriorId == 0 || room.wallInteriorId == NKMCommonConst.Office.DefaultWallItem.Id) && (room.backgroundId == 0 || room.backgroundId == NKMCommonConst.Office.DefaultBackgroundItem.Id));
		}

		// Token: 0x060053FE RID: 21502 RVA: 0x00199EE8 File Offset: 0x001980E8
		public static bool IsEmpryPreset(NKMOfficePreset preset)
		{
			return preset == null || ((preset.furnitures == null || preset.furnitures.Count <= 0) && (preset.floorInteriorId == 0 || preset.floorInteriorId == NKMCommonConst.Office.DefaultFloorItem.Id) && (preset.wallInteriorId == 0 || preset.wallInteriorId == NKMCommonConst.Office.DefaultWallItem.Id) && (preset.backgroundId == 0 || preset.backgroundId == NKMCommonConst.Office.DefaultBackgroundItem.Id));
		}

		// Token: 0x060053FF RID: 21503 RVA: 0x00199F78 File Offset: 0x00198178
		public static Dictionary<int, long> MakeRequiredFurnitureHaveCountDic(int roomID, NKMOfficePreset preset)
		{
			NKCOfficeData officeData = NKCScenManager.CurrentUserData().OfficeData;
			Dictionary<int, long> dictionary = new Dictionary<int, long>();
			foreach (NKMOfficeFurniture nkmofficeFurniture in preset.furnitures)
			{
				int targetID = nkmofficeFurniture.itemId;
				if (!dictionary.ContainsKey(targetID))
				{
					long freeInteriorCount = officeData.GetFreeInteriorCount(targetID);
					long num = (long)officeData.GetOfficeRoom(roomID).furnitures.FindAll((NKMOfficeFurniture x) => x.itemId == targetID).Count;
					dictionary.Add(targetID, freeInteriorCount + num);
				}
			}
			return dictionary;
		}

		// Token: 0x06005400 RID: 21504 RVA: 0x0019A040 File Offset: 0x00198240
		public static bool CheckFurnitureHaveCount(int roomID, NKMOfficePreset preset)
		{
			Dictionary<int, long> dictionary = NKCOfficeManager.MakeRequiredFurnitureHaveCountDic(roomID, preset);
			foreach (NKMOfficeFurniture nkmofficeFurniture in preset.furnitures)
			{
				int itemId = nkmofficeFurniture.itemId;
				long num;
				if (!dictionary.TryGetValue(itemId, out num))
				{
					num = 0L;
				}
				if (num <= 0L)
				{
					return false;
				}
				dictionary[itemId] = num - 1L;
			}
			return true;
		}

		// Token: 0x06005401 RID: 21505 RVA: 0x0019A0C0 File Offset: 0x001982C0
		public static bool IsAllFurniturePlaced(NKMOfficePreset preset, NKMOfficeRoom room)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
			dictionary.Add(preset.floorInteriorId, 1);
			dictionary.Add(preset.wallInteriorId, 1);
			dictionary.Add(preset.backgroundId, 1);
			foreach (NKMOfficeFurniture nkmofficeFurniture in preset.furnitures)
			{
				if (dictionary.ContainsKey(nkmofficeFurniture.itemId))
				{
					Dictionary<int, int> dictionary3 = dictionary;
					int key = nkmofficeFurniture.itemId;
					dictionary3[key]++;
				}
				else
				{
					dictionary[nkmofficeFurniture.itemId] = 1;
				}
			}
			dictionary2.Add(room.floorInteriorId, 1);
			dictionary2.Add(room.wallInteriorId, 1);
			dictionary2.Add(room.backgroundId, 1);
			foreach (NKMOfficeFurniture nkmofficeFurniture2 in room.furnitures)
			{
				if (dictionary2.ContainsKey(nkmofficeFurniture2.itemId))
				{
					Dictionary<int, int> dictionary3 = dictionary2;
					int key = nkmofficeFurniture2.itemId;
					dictionary3[key]++;
				}
				else
				{
					dictionary2[nkmofficeFurniture2.itemId] = 1;
				}
			}
			foreach (KeyValuePair<int, int> keyValuePair in dictionary)
			{
				if (!dictionary2.ContainsKey(keyValuePair.Key))
				{
					return false;
				}
				Dictionary<int, int> dictionary3 = dictionary2;
				int key = keyValuePair.Key;
				dictionary3[key] -= keyValuePair.Value;
			}
			foreach (KeyValuePair<int, int> keyValuePair2 in dictionary2)
			{
				if (keyValuePair2.Value > 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005402 RID: 21506 RVA: 0x0019A2DC File Offset: 0x001984DC
		public static string GetMyPresetName(int index)
		{
			NKMOfficePreset preset = NKCScenManager.CurrentUserData().OfficeData.GetPreset(index);
			if (preset == null || string.IsNullOrEmpty(preset.name))
			{
				return NKCOfficeManager.GetDefaultPresetName(index);
			}
			return preset.name;
		}

		// Token: 0x06005403 RID: 21507 RVA: 0x0019A317 File Offset: 0x00198517
		public static string GetDefaultPresetName(int index)
		{
			return NKCStringTable.GetString("SI_PF_OFFICE_DECO_MODE_PRESET_NAME", new object[]
			{
				index + 1
			});
		}

		// Token: 0x06005404 RID: 21508 RVA: 0x0019A334 File Offset: 0x00198534
		public static void BuildSkinInteractionGroup()
		{
			NKCOfficeManager.s_dicInteractionSkinGroup = new Dictionary<int, string>();
			foreach (NKCOfficeUnitInteractionTemplet nkcofficeUnitInteractionTemplet in NKMTempletContainer<NKCOfficeUnitInteractionTemplet>.Values)
			{
				if (nkcofficeUnitInteractionTemplet != null && !string.IsNullOrEmpty(nkcofficeUnitInteractionTemplet.InteractionSkinGroup))
				{
					if (nkcofficeUnitInteractionTemplet.ActorType != ActTargetType.Skin || nkcofficeUnitInteractionTemplet.TargetType != ActTargetType.Skin)
					{
						Log.Error(string.Format("[NKCOfficeUnitInteractionTemplet:{0}] InteractionSkinGroup이 존재하나 양쪽 타입이 Skin이 아님", nkcofficeUnitInteractionTemplet.Key), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Office/NKCOfficeManager.cs", 1111);
					}
					else
					{
						NKCOfficeManager.AddToSkinInteractionGroup(nkcofficeUnitInteractionTemplet.Key, nkcofficeUnitInteractionTemplet.InteractionSkinGroup, nkcofficeUnitInteractionTemplet.hsActorGroup);
						NKCOfficeManager.AddToSkinInteractionGroup(nkcofficeUnitInteractionTemplet.Key, nkcofficeUnitInteractionTemplet.InteractionSkinGroup, nkcofficeUnitInteractionTemplet.hsTargetGroup);
					}
				}
			}
		}

		// Token: 0x06005405 RID: 21509 RVA: 0x0019A3FC File Offset: 0x001985FC
		private static void AddToSkinInteractionGroup(int key, string groupID, HashSet<string> hsGroup)
		{
			using (HashSet<string>.Enumerator enumerator = hsGroup.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num;
					string text;
					if (!int.TryParse(enumerator.Current, out num))
					{
						Log.Error(string.Format("[NKCOfficeUnitInteractionTemplet:{0}] Skin ID parse error", key), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Office/NKCOfficeManager.cs", 1126);
					}
					else if (NKCOfficeManager.s_dicInteractionSkinGroup.TryGetValue(num, out text))
					{
						if (!groupID.Equals(text))
						{
							Log.Error(string.Format("[NKCOfficeUnitInteractionTemplet:{0}] 스킨 {1}가 2개 이상의 그룹에 속함 : {2}, {3}", new object[]
							{
								key,
								num,
								groupID,
								text
							}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Office/NKCOfficeManager.cs", 1134);
						}
					}
					else
					{
						NKCOfficeManager.s_dicInteractionSkinGroup.Add(num, groupID);
					}
				}
			}
		}

		// Token: 0x04004322 RID: 17186
		private static bool m_bTempletInitialized;

		// Token: 0x04004323 RID: 17187
		public static Dictionary<int, string> s_dicInteractionSkinGroup;
	}
}
