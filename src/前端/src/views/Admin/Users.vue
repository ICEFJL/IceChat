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

const ApplyingUsers = ref([

])

const Users = ref([

])

//调用接口,添加表单
import { ElMessage } from 'element-plus'
//删除分类
import {ElMessageBox} from 'element-plus'
import {disagreeAddUserService, getUserApplyingService,agreeAddUserService,
    getUserService,blockUserService,unblockUserService} from "@/api/user.js";
    
const showDeleteApplyingUserDialog = (row) => {
  //提示用户  确认框
  ElMessageBox.confirm(
      '要拒绝该用户申请吗?',
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
        await disagreeUser(row.userId);
      })
      .catch(() => {
        ElMessage({
          type: 'info',
          message: '管理员取消了拒绝',
        })
      })
}

const agreeUser = async (row) => {
  console.log(row)
  let result = await agreeAddUserService(row.userId);
  if(result.code===0){
    ElMessage.success( '同意成功')
  }
  else ElMessage.error(result.message ? result.message : '意外错误')
  console.log(result.data)
  await flashApplyingUser()
  await flashUser()
}

const disagreeUser = async (userId) => {
  let result = await disagreeAddUserService(userId);
  if(result.code===0){
    ElMessage.success( '拒绝成功')
  }
  else ElMessage.error(result.message ? result.message : '意外错误')
  console.log(result.data)
  await flashApplyingUser()
  await flashUser()
}

const flashApplyingUser=async () => {
  let result = await getUserApplyingService();
  console.log(result.data)
  ApplyingUsers.value=result.data;
}

const showBlockUserDialog = (row) => {
  //提示用户  确认框
  ElMessageBox.confirm(
      '要封禁该用户吗?',
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
        let result = await blockUser(row.userId);
      })
      .catch(() => {
        ElMessage({
          type: 'info',
          message: '管理员取消了封禁',
        })
      })
}

const blockUser = async (userId) => {
  let result = await blockUserService(userId);
  if(result.code===0){
    ElMessage.success( '封禁成功')
  }
  else ElMessage.error(result.message ? result.message : '意外错误')
  console.log(result.data)
  await flashUser()
}

const unblockUser = async (row) => {
  let result = await unblockUserService(row.userId);
  if(result.code===0){
    ElMessage.success( '解封成功')
  }
  else ElMessage.error(result.message ? result.message : '意外错误')
  console.log(result.data)
  await flashUser()
}

const flashUser = async () => {
  let result = await getUserService();
  console.log(result.data)
  Users.value=result.data;
}

onMounted(() => {
  flashUser()
  flashApplyingUser()
});
</script>

<template style="overflow: hidden">
  <div class="flex-container">
    <el-container style="width: auto;height: 100%">
      <el-card class="page-container">
        <template #header>
          <div class="header">
            <span>用户申请列表</span>
            <div class="extra">
              <el-button type="primary" @click="flashApplyingUser();">刷新</el-button>
            </div>
          </div>
        </template>
        <el-table class="groupList" :data="ApplyingUsers" style="width: 100%">
          <el-table-column label="UserId" prop="userId" class="UserIdStyle"></el-table-column>
          <el-table-column label="UserName"  prop="userName" class="UserNameStyle"> </el-table-column>
          <el-table-column label="NickName" prop="nickName" class="nickNameStyle"></el-table-column>

          <el-table-column label="操作" width="100">
            <template #default="{ row }">
              <el-button :icon="Select" circle plain @click="agreeUser(row)"></el-button>
              <el-button :icon="CloseBold" circle plain type="danger" @click="showDeleteApplyingUserDialog
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
          <span>用户管理列表</span>
          <div class="extra">
            <el-button type="primary" @click="flashUser();">刷新</el-button>
          </div>
        </div>
      </template>
      <el-table class="groupList" :data="Users" style="width: 100%">
         <el-table-column label="UserId" prop="userId" class="UserIdStyle"></el-table-column>
          <el-table-column label="UserName"  prop="userName" class="UserNameStyle"> </el-table-column>
          <el-table-column label="NickName" prop="nickName" class="nickNameStyle"></el-table-column>
           <el-table-column label="Status" prop="status" class="nickNameStyle"></el-table-column>
        <el-table-column label="操作">
          <template #default="{ row }">
            <el-button v-if="row.status === 'BLOCKED'"  @click="unblockUser(row)">解禁</el-button>
            <el-button v-else-if="row.status === 'ACTIVE'" type="danger" @click="showBlockUserDialog(row)">封禁</el-button>
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

.UserIdStyle{
  width: 25%;
}
.UserNameStyle{
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