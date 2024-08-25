<script setup>
import {
  Edit,
  Delete,
  Search, Share, Upload, ChatLineSquare,
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
//控制添加分类弹窗
const searchFriends=ref([

])

//调用接口,添加表单
import { ElMessage } from 'element-plus'
//删除分类
import {ElMessageBox} from 'element-plus'

import router from "@/router/index.js";
import { getAllFriendsService,  searchAllFriendService} from "@/api/friend.js";


const toChat = (row) => {
  //跳转到聊天页面
  targetInfoStore.setTargetName(row.userName1+"-"+row.userName2)
  targetInfoStore.setTargetType('friend')
  targetInfoStore.setGroupId(0)
  targetInfoStore.setFriendId(row.userId2)
  targetInfoStore.setSenderId(row.userId1)
  console.log(targetInfoStore.info)

  router.push('/admin/chat')
}

const flashFreind=async () => {
  let result = await getAllFriendsService();
  console.log(result.data)
  friends.value=result.data;
}
onMounted(() => {
  flashFreind()
});
const searchStr=ref("")

const searchFriend =async () => {
  console.log(searchStr.value)
  let result = await searchAllFriendService(searchStr.value);
  console.log(result.data)
  searchFriends.value=result.data;
}


</script>
<template style="overflow: hidden">
  <div class="flex-container">
    <el-container style="width: auto;height: 100%">
      <el-card class="page-container">
        <template #header>
          <div class="header">
            <span>好友聊天列表</span>
            <div class="extra">
              <el-button type="primary" @click="flashFreind();">刷新</el-button>
            </div>
          </div>
        </template>
        <el-table class="groupList" :data="friends" style="width: 100%">
            <el-table-column label="UserId1" prop="userId1" class="friendIdStyle"></el-table-column>
          <el-table-column label="UserName1"  prop="userName1" class="friendNameStyle"> </el-table-column>
              <el-table-column label="UserId2" prop="userId2" class="friendIdStyle"></el-table-column>
          <el-table-column label="UserName2"  prop="userName2" class="friendNameStyle"> </el-table-column>
         
          <el-table-column label="操作" width="100">
            <template #default="{ row }">
              <el-button :icon="ChatLineSquare" circle plain @click="toChat(row)"></el-button>
            </template>
          </el-table-column>
          <template #empty>
            <el-empty description="没有数据" />
          </template>
        </el-table>
      </el-card>
    </el-container>
    <el-container class="searchGroupPage" style="width: 40%;height: 100%">
      <el-header class="searchPageHeader" style="width: 100%">
        <el-col :span="14"><div class="grid-content ep-bg-purple" />
          <el-input v-model="searchStr" placeholder="请输入昵称或用户Id" style="width: 100%" @keyup.enter.native="searchFriend()"/>
        </el-col>
        <el-col :span="5" style="margin-left: 10px">
          <el-button :icon="Search" @click="searchFriend()">
            搜索
          </el-button>
        </el-col>
      </el-header>
      <el-main>
        <el-table :data="searchFriends" class="searchGroupTable" height="100%" style="width: 100%;height: 100%">
         <el-table-column label="UserId1" prop="userId1" class="friendIdStyle"></el-table-column>
          <el-table-column label="UserName1"  prop="userName1" class="friendNameStyle"> </el-table-column>
              <el-table-column label="UserId2" prop="userId2" class="friendIdStyle"></el-table-column>
          <el-table-column label="UserName2"  prop="userName2" class="friendNameStyle"> </el-table-column>
         
          <el-table-column label="操作" style="width: 10%">
            <template #default="{ row }">
              <el-button :icon="ChatLineSquare" circle plain @click="toChat(row)"></el-button>
            </template>
          </el-table-column>
        </el-table>
      </el-main>
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