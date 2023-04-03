using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Office;
using NKM;
using NKM.Templet;
using NKM.Templet.Office;

namespace NKC.Office
{
	// Token: 0x02000827 RID: 2087
	public class NKCOfficeData
	{
		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x0600521C RID: 21020 RVA: 0x0018F462 File Offset: 0x0018D662
		public IEnumerable<NKMOfficeRoom> Rooms
		{
			get
			{
				return this.m_lstRooms;
			}
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x0600521D RID: 21021 RVA: 0x0018F46A File Offset: 0x0018D66A
		// (set) Token: 0x0600521E RID: 21022 RVA: 0x0018F472 File Offset: 0x0018D672
		public NKMOfficePostState PostState { get; private set; }

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x0600521F RID: 21023 RVA: 0x0018F47B File Offset: 0x0018D67B
		public int BizcardCount
		{
			get
			{
				if (this.m_lstBizCard == null)
				{
					return 0;
				}
				return this.m_lstBizCard.Count;
			}
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06005220 RID: 21024 RVA: 0x0018F494 File Offset: 0x0018D694
		public int RecvCountLeft
		{
			get
			{
				if (NKCSynchronizedTime.IsFinished(NKCSynchronizedTime.ToUtcTime(this.PostState.nextResetDate)))
				{
					return NKMCommonConst.Office.NameCard.DailyLimit;
				}
				return NKMCommonConst.Office.NameCard.DailyLimit - this.PostState.recvCount;
			}
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06005221 RID: 21025 RVA: 0x0018F4E3 File Offset: 0x0018D6E3
		public bool CanReceiveBizcard
		{
			get
			{
				return NKCSynchronizedTime.IsFinished(NKCSynchronizedTime.ToUtcTime(this.PostState.nextResetDate)) || NKMCommonConst.Office.NameCard.DailyLimit > this.PostState.recvCount;
			}
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06005222 RID: 21026 RVA: 0x0018F51A File Offset: 0x0018D71A
		public bool CanSendBizcardBroadcast
		{
			get
			{
				return NKCSynchronizedTime.IsFinished(NKCSynchronizedTime.ToUtcTime(this.PostState.nextResetDate)) || !this.PostState.broadcastExecution;
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06005223 RID: 21027 RVA: 0x0018F544 File Offset: 0x0018D744
		// (remove) Token: 0x06005224 RID: 21028 RVA: 0x0018F57C File Offset: 0x0018D77C
		public event NKCOfficeData.OnInteriorInventoryUpdate dOnInteriorInventoryUpdate;

		// Token: 0x06005225 RID: 21029 RVA: 0x0018F5B1 File Offset: 0x0018D7B1
		public bool CanRefreshOfficePost()
		{
			return NKCSynchronizedTime.IsFinished(this.m_dtNextRefreshTime);
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x0018F5BE File Offset: 0x0018D7BE
		public void TryRefreshOfficePost(bool bForce)
		{
			if (!bForce && !this.CanRefreshOfficePost())
			{
				return;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.OFFICE, 0, 0))
			{
				return;
			}
			this.m_dtNextRefreshTime = NKCSynchronizedTime.GetServerUTCTime(5.0);
			NKCPacketSender.Send_NKMPacket_OFFICE_POST_LIST_REQ(0L);
		}

		// Token: 0x06005227 RID: 21031 RVA: 0x0018F5F3 File Offset: 0x0018D7F3
		public NKMOfficePost GetBizCard(int index)
		{
			if (index < 0)
			{
				return null;
			}
			if (index >= this.BizcardCount)
			{
				return null;
			}
			return this.m_lstBizCard[index];
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06005228 RID: 21032 RVA: 0x0018F612 File Offset: 0x0018D812
		public bool IsVisiting
		{
			get
			{
				return this.m_currentFriendState != null;
			}
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06005229 RID: 21033 RVA: 0x0018F61D File Offset: 0x0018D81D
		public long CurrentFriendUid
		{
			get
			{
				return this.m_lCurrentFriendUId;
			}
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x0018F65C File Offset: 0x0018D85C
		public void SetData(NKMMyOfficeState officeState)
		{
			if (officeState == null)
			{
				this.m_lstOpenedSectionIds = new List<int>
				{
					101
				};
				return;
			}
			this.m_lstOpenedSectionIds = officeState.openedSectionIds;
			this.m_lstRooms = officeState.rooms;
			this.m_dicInteriors.Clear();
			this.UpdateInteriorData(officeState.interiors);
			this.PostState = officeState.postState;
			this.m_lstOfficePreset = officeState.presets;
			this.m_dicRooms.Clear();
			if (this.m_lstRooms != null)
			{
				this.m_lstRooms.ForEach(delegate(NKMOfficeRoom e)
				{
					this.m_dicRooms.Add(e.id, e);
				});
			}
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x0018F6F1 File Offset: 0x0018D8F1
		public void SetFriendData(long userUId, NKMOfficeState officeState)
		{
			if (!this.m_dicFriendOfficeState.ContainsKey(userUId))
			{
				this.m_dicFriendOfficeState.Add(userUId, officeState);
			}
			else
			{
				this.m_dicFriendOfficeState[userUId] = officeState;
			}
			this.m_lCurrentFriendUId = userUId;
			this.m_currentFriendState = officeState;
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x0018F72C File Offset: 0x0018D92C
		public NKMOfficeRoom GetFriendRoom(long userUID, int roomID)
		{
			NKMOfficeState nkmofficeState;
			if (this.m_dicFriendOfficeState.TryGetValue(userUID, out nkmofficeState))
			{
				return nkmofficeState.rooms.Find((NKMOfficeRoom x) => x.id == roomID);
			}
			return null;
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x0018F770 File Offset: 0x0018D970
		public void UpdateRoomData(NKMOfficeRoom officeRoom)
		{
			if (officeRoom == null)
			{
				return;
			}
			if (this.m_lstRooms == null)
			{
				this.m_lstRooms = new List<NKMOfficeRoom>();
			}
			int num = this.m_lstRooms.FindIndex((NKMOfficeRoom e) => e.id == officeRoom.id);
			if (num >= 0 && num < this.m_lstRooms.Count)
			{
				this.m_lstRooms[num] = officeRoom;
			}
			else
			{
				this.m_lstRooms.Add(officeRoom);
			}
			if (this.m_dicRooms.ContainsKey(officeRoom.id))
			{
				this.m_dicRooms[officeRoom.id] = officeRoom;
				return;
			}
			this.m_dicRooms.Add(officeRoom.id, officeRoom);
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x0018F848 File Offset: 0x0018DA48
		public void UpdateSectionData(int sectionId, List<NKMOfficeRoom> officeRooms)
		{
			if (officeRooms == null)
			{
				return;
			}
			if (this.m_lstOpenedSectionIds == null)
			{
				this.m_lstOpenedSectionIds = new List<int>();
			}
			if (!this.m_lstOpenedSectionIds.Contains(sectionId))
			{
				this.m_lstOpenedSectionIds.Add(sectionId);
			}
			if (this.m_lstRooms == null)
			{
				this.m_lstRooms = new List<NKMOfficeRoom>();
			}
			int count = officeRooms.Count;
			int i;
			Predicate<NKMOfficeRoom> <>9__0;
			int j;
			for (i = 0; i < count; i = j)
			{
				List<NKMOfficeRoom> lstRooms = this.m_lstRooms;
				Predicate<NKMOfficeRoom> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((NKMOfficeRoom e) => e.id == officeRooms[i].id));
				}
				int num = lstRooms.FindIndex(match);
				if (num >= 0 && num < this.m_lstRooms.Count)
				{
					this.m_lstRooms[num] = officeRooms[i];
				}
				else
				{
					this.m_lstRooms.Add(officeRooms[i]);
				}
				if (this.m_dicRooms.ContainsKey(officeRooms[i].id))
				{
					this.m_dicRooms[officeRooms[i].id] = officeRooms[i];
				}
				else
				{
					this.m_dicRooms.Add(officeRooms[i].id, officeRooms[i]);
				}
				j = i + 1;
			}
		}

		// Token: 0x06005230 RID: 21040 RVA: 0x0018F9E4 File Offset: 0x0018DBE4
		public NKMOfficeRoom GetOfficeRoom(int roomId)
		{
			if (this.IsVisiting)
			{
				return this.m_currentFriendState.rooms.Find((NKMOfficeRoom e) => e.id == roomId);
			}
			NKMOfficeRoom result = null;
			this.m_dicRooms.TryGetValue(roomId, out result);
			return result;
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x0018FA3A File Offset: 0x0018DC3A
		public int GetOpenedDormsCount()
		{
			if (this.IsVisiting)
			{
				return this.GetOpenedDormCount(this.m_currentFriendState.rooms);
			}
			return this.GetOpenedDormCount(this.m_lstRooms);
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x0018FA64 File Offset: 0x0018DC64
		public int GetOpenedRoomCountInSection(int sectionId, ref int roomSequenceNumber, int roomId = 0)
		{
			int num = 0;
			if (this.m_lstRooms != null)
			{
				int count = this.m_lstRooms.Count;
				for (int i = 0; i < count; i++)
				{
					NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(this.m_lstRooms[i].id);
					if (nkmofficeRoomTemplet != null)
					{
						if (nkmofficeRoomTemplet.Type == NKMOfficeRoomTemplet.RoomType.Dorm)
						{
							num++;
						}
						if (nkmofficeRoomTemplet.ID == roomId)
						{
							roomSequenceNumber = num;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x0018FAC6 File Offset: 0x0018DCC6
		public bool IsOpenedSection(int sectionId)
		{
			if (this.IsVisiting)
			{
				return this.m_dicFriendOfficeState[this.m_lCurrentFriendUId].openedSectionIds.Contains(sectionId);
			}
			return this.m_lstOpenedSectionIds != null && this.m_lstOpenedSectionIds.Contains(sectionId);
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x0018FB04 File Offset: 0x0018DD04
		public bool IsOpenedRoom(int roomId)
		{
			if (this.IsVisiting)
			{
				return this.m_dicFriendOfficeState[this.m_lCurrentFriendUId].rooms.Find((NKMOfficeRoom e) => e.id == roomId) != null;
			}
			return this.m_dicRooms.ContainsKey(roomId);
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x0018FB64 File Offset: 0x0018DD64
		public int GetUnitAssignedNumber(long unitUid, int roomId)
		{
			if (!this.m_dicRooms.ContainsKey(roomId))
			{
				return 0;
			}
			if (this.m_dicRooms[roomId].unitUids == null)
			{
				return 0;
			}
			return this.m_dicRooms[roomId].unitUids.FindIndex((long e) => e == unitUid) + 1;
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x0018FBC8 File Offset: 0x0018DDC8
		public void UpdateInteriorData(IEnumerable<NKMInteriorData> lstData)
		{
			foreach (NKMInteriorData data in lstData)
			{
				this.UpdateInteriorData(data);
			}
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x0018FC10 File Offset: 0x0018DE10
		public void AddInteriorData(IEnumerable<NKMInteriorData> lstData)
		{
			foreach (NKMInteriorData data in lstData)
			{
				this.AddInteriorData(data);
			}
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x0018FC58 File Offset: 0x0018DE58
		public void AddInteriorData(NKMInteriorData data)
		{
			NKMInteriorData nkminteriorData;
			if (!this.m_dicInteriors.TryGetValue(data.itemId, out nkminteriorData))
			{
				NKCOfficeData.OnInteriorInventoryUpdate onInteriorInventoryUpdate = this.dOnInteriorInventoryUpdate;
				if (onInteriorInventoryUpdate != null)
				{
					onInteriorInventoryUpdate(data, true);
				}
				this.m_dicInteriors[data.itemId] = data;
				return;
			}
			nkminteriorData.count += data.count;
			NKCOfficeData.OnInteriorInventoryUpdate onInteriorInventoryUpdate2 = this.dOnInteriorInventoryUpdate;
			if (onInteriorInventoryUpdate2 == null)
			{
				return;
			}
			onInteriorInventoryUpdate2(data, false);
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x0018FCC8 File Offset: 0x0018DEC8
		public void UpdateInteriorData(NKMInteriorData data)
		{
			bool bAdded = !this.m_dicInteriors.ContainsKey(data.itemId);
			this.m_dicInteriors[data.itemId] = data;
			NKCOfficeData.OnInteriorInventoryUpdate onInteriorInventoryUpdate = this.dOnInteriorInventoryUpdate;
			if (onInteriorInventoryUpdate == null)
			{
				return;
			}
			onInteriorInventoryUpdate(data, bAdded);
		}

		// Token: 0x0600523A RID: 21050 RVA: 0x0018FD10 File Offset: 0x0018DF10
		public long GetInteriorCount(NKMOfficeInteriorTemplet templet)
		{
			NKMInteriorData nkminteriorData;
			if (this.m_dicInteriors.TryGetValue(templet.m_ItemMiscID, out nkminteriorData))
			{
				return nkminteriorData.count;
			}
			return 0L;
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x0018FD3C File Offset: 0x0018DF3C
		public long GetInteriorCount(int itemID)
		{
			long freeInteriorCount = this.GetFreeInteriorCount(itemID);
			int num = 0;
			Predicate<NKMOfficeFurniture> <>9__0;
			foreach (KeyValuePair<int, NKMOfficeRoom> keyValuePair in this.m_dicRooms)
			{
				List<NKMOfficeFurniture> furnitures = keyValuePair.Value.furnitures;
				Predicate<NKMOfficeFurniture> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((NKMOfficeFurniture x) => x.itemId == itemID));
				}
				List<NKMOfficeFurniture> list = furnitures.FindAll(match);
				num += list.Count;
			}
			return freeInteriorCount + (long)num;
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x0018FDE8 File Offset: 0x0018DFE8
		public IEnumerable<NKMInteriorData> GetAllInteriorData()
		{
			foreach (KeyValuePair<int, NKMInteriorData> keyValuePair in this.m_dicInteriors)
			{
				yield return keyValuePair.Value;
			}
			Dictionary<int, NKMInteriorData>.Enumerator enumerator = default(Dictionary<int, NKMInteriorData>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x0018FDF8 File Offset: 0x0018DFF8
		public long GetFreeInteriorCount(int itemID)
		{
			NKMInteriorData nkminteriorData;
			if (this.m_dicInteriors.TryGetValue(itemID, out nkminteriorData))
			{
				return nkminteriorData.count;
			}
			return 0L;
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x0018FE20 File Offset: 0x0018E020
		public int GetRoomInteriorScore(int roomID)
		{
			NKMOfficeRoom officeRoom = this.GetOfficeRoom(roomID);
			if (officeRoom == null)
			{
				return 0;
			}
			return officeRoom.interiorScore;
		}

		// Token: 0x0600523F RID: 21055 RVA: 0x0018FE40 File Offset: 0x0018E040
		public NKMOfficeUnitData GetFriendUnit(long userUID, long unitUId)
		{
			NKMOfficeState nkmofficeState;
			if (this.m_dicFriendOfficeState.TryGetValue(userUID, out nkmofficeState))
			{
				return nkmofficeState.units.Find((NKMOfficeUnitData e) => e.unitUid == unitUId);
			}
			return null;
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x0018FE84 File Offset: 0x0018E084
		public int GetFriendUnitId(long unitUId)
		{
			NKMOfficeUnitData nkmofficeUnitData = this.m_currentFriendState.units.Find((NKMOfficeUnitData e) => e.unitUid == unitUId);
			if (nkmofficeUnitData == null)
			{
				return 0;
			}
			return nkmofficeUnitData.unitId;
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x0018FEC6 File Offset: 0x0018E0C6
		public NKMCommonProfile GetFriendProfile()
		{
			NKMOfficeState currentFriendState = this.m_currentFriendState;
			if (currentFriendState == null)
			{
				return null;
			}
			return currentFriendState.commonProfile;
		}

		// Token: 0x06005242 RID: 21058 RVA: 0x0018FED9 File Offset: 0x0018E0D9
		public void ResetFriendUId()
		{
			this.m_dicFriendOfficeState.Clear();
			this.m_lCurrentFriendUId = 0L;
			this.m_currentFriendState = null;
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x0018FEF8 File Offset: 0x0018E0F8
		private int GetOpenedDormCount(List<NKMOfficeRoom> roomList)
		{
			if (roomList == null)
			{
				return 0;
			}
			int num = 0;
			int count = roomList.Count;
			for (int i = 0; i < count; i++)
			{
				NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(roomList[i].id);
				if (nkmofficeRoomTemplet != null && nkmofficeRoomTemplet.Type == NKMOfficeRoomTemplet.RoomType.Dorm)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06005244 RID: 21060 RVA: 0x0018FF41 File Offset: 0x0018E141
		public void UpdatePostState(NKMOfficePostState postState)
		{
			this.PostState = postState;
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x0018FF4A File Offset: 0x0018E14A
		public void UpdatePostList(List<NKMOfficePost> lstPost)
		{
			this.m_lstBizCard = lstPost;
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x0018FF53 File Offset: 0x0018E153
		public void UpdateRandomVisitor(List<NKMUserProfileData> lstVisitor)
		{
			this.m_lstRandomVisitor = lstVisitor;
			this.m_randomVisitorIndex = 0;
		}

		// Token: 0x06005247 RID: 21063 RVA: 0x0018FF64 File Offset: 0x0018E164
		public NKMUserProfileData GetRandomVisitor()
		{
			if (this.m_lstRandomVisitor == null || this.m_lstRandomVisitor.Count == 0)
			{
				return null;
			}
			NKMUserProfileData result = this.m_lstRandomVisitor[this.m_randomVisitorIndex % this.m_lstRandomVisitor.Count];
			this.m_randomVisitorIndex++;
			return result;
		}

		// Token: 0x06005248 RID: 21064 RVA: 0x0018FFB4 File Offset: 0x0018E1B4
		public List<NKMUserProfileData> GetRandomVisitor(int count)
		{
			if (this.m_lstRandomVisitor.Count < count)
			{
				return this.m_lstRandomVisitor;
			}
			List<NKMUserProfileData> list = new List<NKMUserProfileData>();
			while (list.Count < count)
			{
				list.Add(this.m_lstRandomVisitor[this.m_randomVisitorIndex % this.m_lstRandomVisitor.Count]);
				this.m_randomVisitorIndex++;
			}
			return list;
		}

		// Token: 0x06005249 RID: 21065 RVA: 0x0019001C File Offset: 0x0018E21C
		public List<int> GetOpenedRoomIdList()
		{
			List<int> roomIdList = new List<int>();
			if (this.IsVisiting)
			{
				NKMOfficeState currentFriendState = this.m_currentFriendState;
				if (currentFriendState != null)
				{
					currentFriendState.rooms.ForEach(delegate(NKMOfficeRoom e)
					{
						roomIdList.Add(e.id);
					});
				}
			}
			else
			{
				List<NKMOfficeRoom> lstRooms = this.m_lstRooms;
				if (lstRooms != null)
				{
					lstRooms.ForEach(delegate(NKMOfficeRoom e)
					{
						roomIdList.Add(e.id);
					});
				}
			}
			roomIdList.Sort();
			return roomIdList;
		}

		// Token: 0x0600524A RID: 21066 RVA: 0x00190094 File Offset: 0x0018E294
		public int GetPresetCount()
		{
			if (this.m_lstOfficePreset != null)
			{
				return this.m_lstOfficePreset.Count;
			}
			return 0;
		}

		// Token: 0x0600524B RID: 21067 RVA: 0x001900AC File Offset: 0x0018E2AC
		public void SetPresetCount(int newCount)
		{
			while (this.m_lstOfficePreset.Count < newCount)
			{
				NKMOfficePreset nkmofficePreset = new NKMOfficePreset();
				nkmofficePreset.presetId = this.m_lstOfficePreset.Count;
				this.m_lstOfficePreset.Add(nkmofficePreset);
			}
		}

		// Token: 0x0600524C RID: 21068 RVA: 0x001900EC File Offset: 0x0018E2EC
		public void SetPreset(NKMOfficePreset preset)
		{
			if (preset == null)
			{
				return;
			}
			if (preset.presetId < this.m_lstOfficePreset.Count)
			{
				this.m_lstOfficePreset[preset.presetId] = preset;
			}
		}

		// Token: 0x0600524D RID: 21069 RVA: 0x00190117 File Offset: 0x0018E317
		public NKMOfficePreset GetPreset(int index)
		{
			if (index < this.m_lstOfficePreset.Count)
			{
				return this.m_lstOfficePreset[index];
			}
			return null;
		}

		// Token: 0x0600524E RID: 21070 RVA: 0x00190135 File Offset: 0x0018E335
		public void ChangePresetName(int index, string name)
		{
			if (index < this.m_lstOfficePreset.Count)
			{
				this.m_lstOfficePreset[index].name = name;
			}
		}

		// Token: 0x0400424E RID: 16974
		private List<int> m_lstOpenedSectionIds;

		// Token: 0x0400424F RID: 16975
		private List<NKMOfficeRoom> m_lstRooms;

		// Token: 0x04004250 RID: 16976
		private Dictionary<int, NKMInteriorData> m_dicInteriors = new Dictionary<int, NKMInteriorData>();

		// Token: 0x04004251 RID: 16977
		private Dictionary<int, NKMOfficeRoom> m_dicRooms = new Dictionary<int, NKMOfficeRoom>();

		// Token: 0x04004253 RID: 16979
		private List<NKMOfficePost> m_lstBizCard = new List<NKMOfficePost>();

		// Token: 0x04004254 RID: 16980
		private List<NKMUserProfileData> m_lstRandomVisitor;

		// Token: 0x04004255 RID: 16981
		private int m_randomVisitorIndex;

		// Token: 0x04004256 RID: 16982
		private List<NKMOfficePreset> m_lstOfficePreset;

		// Token: 0x04004258 RID: 16984
		private const float REFRESH_INTERVAL = 5f;

		// Token: 0x04004259 RID: 16985
		private DateTime m_dtNextRefreshTime;

		// Token: 0x0400425A RID: 16986
		private Dictionary<long, NKMOfficeState> m_dicFriendOfficeState = new Dictionary<long, NKMOfficeState>();

		// Token: 0x0400425B RID: 16987
		private long m_lCurrentFriendUId;

		// Token: 0x0400425C RID: 16988
		private NKMOfficeState m_currentFriendState;

		// Token: 0x020014CD RID: 5325
		// (Invoke) Token: 0x0600A9E0 RID: 43488
		public delegate void OnInteriorInventoryUpdate(NKMInteriorData interiorData, bool bAdded);
	}
}
