<script setup>
import {
Select,CloseBold
} from '@element-plus/icons-vue'
import { ref ,onMounted } from 'vue'
import {useUserInfoStore }  from '@/store/userInfo.js'
import {useTargetInfoStore} from '@/store/targetInfo.js'
const userInfoStore = useUserInfoStore();

const targetInfoStore = useTargetInfoStore();

const targetInfo=targetInfoStore.info
const userInfo = userInfoStore.info
let createGroupFlag=ref(false)

const friends = ref([

])

const groups = ref([

])

//调用接口,添加表单
import { ElMessage } from 'element-plus'
//删除分类
import {ElMessageBox} from 'element-plus'

import router from "@/router/index.js";
import {disagreeFriendService, getFriendApplyingService,agreeFriendService} from "@/api/friend.js";
import {getGroupApplyingService,agreeAddGroupService,disagreeAddGroupService} from "@/api/group.js";
const showDeleteFriendDialog = (row) => {
  //提示用户  确认框
  ElMessageBox.confirm(
      '要拒绝该好友申请吗?',
      '温馨提示',
      {
        confirmButtonText: '确认',
        cancelButtonText: '取消',
        type: 'warning',
      }
  )
      .then(async () => {
        //调用接口
        console.log("test")
        let result = await disagreeFriend(userInfo.userId,row.userId);
      })
      .catch(() => {
        ElMessage({
          type: 'info',
          message: '用户取消了拒绝',
        })
      })
}

const agreeFriend = async (row) => {
  console.log(row)
  let result = await agreeFriendService(userInfo.userId,row.userId);
  if(result.code===0){
    ElMessage.success( '同意成功')
  }
  else ElMessage.error(result.message ? result.message : '意外错误')
  console.log(result.data)
  await flashFriend()
}

const disagreeFriend = async (userId1,userId2) => {
  let result = await disagreeFriendService(userId1,userId2);
  if(result.code===0){
    ElMessage.success( '拒绝成功')
  }
  else ElMessage.error(result.message ? result.message : '意外错误')
  console.log(result.data)
  await flashFriend()
}

const showDeleteGroupDialog = (row) => {
  //提示用户  确认框
  ElMessageBox.confirm(
      '要拒绝该入群申请吗?',
      '温馨提示',
      {
        confirmButtonText: '确认',
        cancelButtonText: '取消',
        type: 'warning',
      }
  )
      .then(async () => {
        //调用接口
        console.log("test")
        let result = await disagreeGroup(row.userId,row.groupId,userInfo.userId);
      })
      .catch(() => {
        ElMessage({
          type: 'info',
          message: '用户取消了拒绝',
        })
      })
}
const agreeGroup = async (row) => {
  let result = await agreeAddGroupService(row.userId,row.groupId,userInfo.userId);
  if(result.code===0){
    ElMessage.success( '同意成功')
  }
  else ElMessage.error(result.message ? result.message : '意外错误')
  console.log(result.data)
  await flashGroup()
}

const disagreeGroup = async (userId, groupId, groupOwner) => {
  let result = await disagreeAddGroupService(userId, groupId, groupOwner);
  if(result.code===0){
    ElMessage.success( '拒绝成功')
  }
  else ElMessage.error(result.message ? result.message : '意外错误')
  console.log(result.data)
  await flashGroup()
}
let timer = setInterval(() => {
  flashFriend();
  flashGroup();
}, 3000);

onMounted(() => {
  flashFriend()
  flashGroup()
});

const flashFriend=async () => {
    if(router.currentRoute.value.fullPath!=='/apply'){
      clearInterval(timer)
    }
  let result = await getFriendApplyingService(userInfo.userId);
  console.log(result.data)
  friends.value=result.data;
}
const flashGroup=async () => {
  let result = await getGroupApplyingService(userInfo.userId);
  console.log(result.data)
  groups.value=result.data;
}


</script>
<template style="overflow: hidden">
  <div class="flex-container">
    <el-container style="width: auto;height: 100%">
      <el-card class="page-container">
        <template #header>
          <div class="header">
            <span>好友申请列表</span>
            <div class="extra">
              <el-button type="primary" @click="flashFriend();">刷新</el-button>
            </div>
          </div>
        </template>
        <el-table class="groupList" :data="friends" style="width: 100%">
          <el-table-column label="FriendName"  prop="userName" class="friendNameStyle"> </el-table-column>
          <el-table-column label="NickName" prop="nickName" class="nickNameStyle"></el-table-column>
          <el-table-column label="FriendId" prop="userId" class="friendIdStyle"></el-table-column>
          <el-table-column label="操作" width="100">
            <template #default="{ row }">
              <el-button :icon="Select" circle plain @click="agreeFriend(row)"></el-button>
              <el-button :icon="CloseBold" circle plain type="danger" @click="showDeleteFriendDialog
(row)"></el-button>
            </template>
          </el-table-column>
          <template #empty>
            <el-empty description="没有数据" />
          </template>
        </el-table>
      </el-card>
    </el-container>
    <el-container style="width: auto;height: 100%">
    <el-card class="page-container">
      <template #header>
        <div class="header">
          <span>群组申请列表</span>
          <div class="extra">
            <el-button type="primary" @click="flashGroup();">刷新</el-button>
          </div>
        </div>
      </template>
      <el-table class="groupList" :data="groups" style="width: 100%" @row-dblclick="toChat">
        <el-table-column label="GroupId"  prop="groupId"> </el-table-column>
        <el-table-column label="GroupName" prop="groupName"></el-table-column>
        <el-table-column label="UserId" prop="userId"></el-table-column>
        <el-table-column label="UserName" prop="userName"></el-table-column>
        <el-table-column label="操作">
          <template #default="{ row }">
            <el-button :icon="Select" circle plain @click="agreeGroup(row)"></el-button>
              <el-button :icon="CloseBold" circle plain type="danger" @click="showDeleteGroupDialog
(row)"></el-button>
            </template>
        </el-table-column>
        <template #empty>
          <el-empty description="没有数据" />
        </template>
      </el-table>
    </el-card>
  </el-container>
  </div>
</template>

<style lang="scss" scoped>

.friendIdStyle{
  width: 25%;
}
.friendNameStyle{
  width: 35%;
}
.nickNameStyle{
  width: 30%;
}


.page-container {
  background-color: whitesmoke;
  min-height: 100%;
  box-sizing: border-box;
  width: 100%;
  .header {
    display: flex;
    align-items: center;
    justify-content: space-between;
  }
}

.groupList{
  background-color: whitesmoke;
}
.flex-container {
  display: flex; /* 使用Flexbox布局 */
  justify-content: space-between; /* 使两个容器水平排列，并且空间平均分布 */
  overflow: hidden;
  height: 100%
}

.el-row{
  display:flex;

}

.searchPageHeader {
  display: flex; /* 使用Flexbox布局 */
  justify-content: space-between; /* 元素之间平均分布空间 */
  align-items: center; /* 垂直居中对齐 */
}


</style>