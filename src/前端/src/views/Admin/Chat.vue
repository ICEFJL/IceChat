<script setup>
import {
  Management,
  Promotion,
  UserFilled,
  User,
  Crop,
  EditPen,
  SwitchButton,
  CaretBottom, Document
} from '@element-plus/icons-vue'
import { ref ,onMounted } from 'vue'
import {useUserInfoStore }  from '@/store/userInfo.js'
import {useTargetInfoStore} from '@/store/targetInfo.js'
import {ElMessage} from "element-plus";
import {
  getGroupsMessageService,
  getPrivateMessageService,
  searchPrivateMessageService,
  searchGroupsMessageService,
    removePrivateMessageService,
    removeGroupsMessageService
} from "@/api/chat.js";
const userInfoStore = useUserInfoStore();
const targetInfoStore = useTargetInfoStore();
const targetInfo=targetInfoStore.info
const userInfo = userInfoStore.info
const newMessageList=ref([
])
const messageList=ref([
  {
    "messageId":-1,
    "senderId":99999,
    "nickName":"icechat助手",
    "messageText":"欢迎使用Icechat，您还未开始聊天",
    "messageTime":'2024-06-25T07:15:31.440+00:00',
  }
])

const searchMsgs=ref([

])
const getMessage=()=>{
  console.log(messageList.value)
  console.log(newMessageList.value)
  if(targetInfo.targetType==='friend'){
    return getFriendMessage();
  }
  else if(targetInfo.targetType==='group'){
    return getGroupMessage();
  }else{
    return
  }
}

const getGroupMessage=async()=>{
  let result=await getGroupsMessageService(userInfo.userId,targetInfo.groupId)
  if(result.code===0){
    messageList.value=result.data
    return result;
  }
  else {
    ElMessage.error(result.message ? result.message : '意外错误')
  }
}

const  getFriendMessage=async()=>{
  let result=await getPrivateMessageService(targetInfo.senderId,targetInfo.friendId)
  if(result.code===0){
    messageList.value=result.data
    return result;
  }
  else {
    ElMessage.error(result.message ? result.message : '意外错误')
  }
}

const flashMessage=()=>{
  getMessage();
}

const message=ref('')


const searchStr=ref("")
const searchMsg= async()=>{
 if(searchStr.value===''||searchStr.value===null){
    let result = await getMessage()
    searchMsgs.value = result.data
   return
 }
  if(targetInfo.targetType==='friend'){
    await SearchPrivateMessage();
  }
  else if(targetInfo.targetType==='group'){
    await SearchGroupMessage();
  }else{
    ElMessage.error("您还未选取一名好友或者群组")
  }
  flashMessage();
}

const SearchPrivateMessage=async()=>{
  let result=await searchPrivateMessageService(targetInfo.senderId,targetInfo.friendId,searchStr.value)
  if(result.code===0){
    searchMsgs.value=result.data
    console.log(searchMsgs.value)
    return;
  }
  else {
    ElMessage.error(result.message ? result.message : '意外错误')
  }
}

const SearchGroupMessage=async()=>{
  let result=await searchGroupsMessageService(targetInfo.groupId,searchStr.value)
  if(result.code===0){
    searchMsgs.value=result.data
    return;
  }
  else {
    ElMessage.error(result.message ? result.message : '意外错误')
  }
}

const removeMessage=async(row)=>{
  if(targetInfo.targetType==='friend'){
    await removePrivateMessage(row);
  }
  else if(targetInfo.targetType==='group'){
    await removeGroupMessage(row);
  }else{
    ElMessage.error("您还未选取一名好友或者群组")
  }
  flashMessage();
  searchMsg(); 
}

const removePrivateMessage=async(row)=>{
  let result=await removePrivateMessageService(row.messageId)
  if(result.code===0){
    ElMessage.success( '删除成功')
    return;
  }
  else {
    ElMessage.error(result.message ? result.message : '意外错误')
  }
}

const removeGroupMessage=async(row)=>{
  let result=await removeGroupsMessageService(row.messageId)
  if(result.code===0){
    ElMessage.success( '删除成功')
    return;
  }
  else {
    ElMessage.error(result.message ? result.message : '意外错误')
  }
}

const formatMessageTime=(messageTime)=> {
  // 解析 ISO 8601 时间字符串
  const date = new Date(messageTime);
  // 获取年月日时分秒
  const year = date.getFullYear();
  const month = (date.getMonth() + 1).toString().padStart(2, '0'); // 月份是从 0 开始的，所以需要+1
  const day = date.getDate().toString().padStart(2, '0');
  const hours = date.getHours().toString().padStart(2, '0');
  const minutes = date.getMinutes().toString().padStart(2, '0');
  // 组合成月日时分的格式
  return `${month}-${day} ${hours}:${minutes}`;
}

onMounted(async ()=>{
  getMessage();
  searchMsg();
})

</script>

<template>
  <div class="flex-container">
    <el-container class="mainPage" style="width: auto;height: 100%">
      <el-header style="height: 25px;margin-top: 10px;">
      {{ targetInfo.targetName }}
      <el-button type="primary" @click="searchMsg()">刷新</el-button>
      </el-header>
      <el-main class="chatPage">

        <div class="message-list">
          <div v-for="message in messageList" :key="message.messageTime" style="width: 100%;height: auto;">
            <div v-if="message.senderId===targetInfo.senderId" class="my-message">
                <div style="display: flex;flex-direction: column;  align-items: end; /* 昵称在右侧 */;">
                  <span class="message-time">{{ message.nickName }}</span>
                  <span class="message-time">{{formatMessageTime(message.messageTime) }}</span>
                </div>
                <div class="message-bubble">
                  <span class="message-text">{{ message.messageText }}</span>
                </div>
            </div>
            <div v-else class="other-message">
              <div style="display: flex;flex-direction: column; /* 昵称在右侧 */">
                <span class="message-time">{{ message.nickName }}</span>
                <span class="message-time">{{ formatMessageTime(message.messageTime) }}</span>
              </div>
              <div class="message-bubble">
                <span class="message-text">{{ message.messageText }}</span>
              </div>
            </div>
          </div>
        </div>
      </el-main>
    </el-container>
    <el-container class="searchGroupPage" style="width: 30%;height: 100%">
      <el-header class="searchPageHeader" style="width: 100%">
        <el-col :span="14"><div class="grid-content ep-bg-purple" />
          <el-input v-model="searchStr" placeholder="请输入消息内容" style="width: 100%" @keyup.enter.native="searchMsg()"/>
        </el-col>
        <el-col :span="5" style="margin-left: 10px">
          <el-button :icon="Search" @click="searchMsg()">
            搜索
          </el-button>
        </el-col>
      </el-header>
      <el-main>
        <el-table :data="searchMsgs" class="searchGroupTable" height="100%" style="width: 100%;height: 100%">
         
            <el-table-column label="消息Id" prop="messageId" class="messageIdStyle"></el-table-column>
            <el-table-column label="发送时间" prop="messageTime" class="messageTimeStyle">
                <template #default="{ row }">
                    {{ formatMessageTime(row.messageTime) }}
                </template>
            </el-table-column>
          <el-table-column label="发送者昵称" prop="nickName" class="nickNameStyle"></el-table-column>
          <el-table-column label="发送内容" prop="messageText" class="messageTextStyle"></el-table-column>
          <el-table-column label="操作" style="width: 10%">
            <template #default="{ row }">
              <el-button @click="removeMessage(row)">
                删除信息
              </el-button>
            </template>
          </el-table-column>
        </el-table>
      </el-main>
    </el-container>
  </div>
</template>

<style scoped>
.el-textarea__inner,.el-input__inner {
  background: transparent !important;
}

.contentInput0{

  height: 100%;
}
.message-text{
  width: auto;
}

.inputContainer{
  height: 30%;
  border-radius: 3px;
  width: 100%;
  padding: 10px;
}

.contentInput {
 height: 100%;
}


.sendPage{
  height: 100%;
  position: relative;
}
.send{
  display: flex;
  flex-direction: column;
  align-items: end;
  position: absolute;
  top: 95px;
  right: 20px;
}


.flex-container {
  display: flex; /* 使用Flexbox布局 */
  justify-content: space-between; /* 使两个容器水平排列，并且空间平均分布 */
  overflow: hidden;
  height: 100%
}

.mainPage{
  height: 100%;
}
.midSlice{
background-color: #2c3e50;
  width: 10%;
}

.chatPage {
  height: 40%;
  overflow-y: hidden; /* 添加滚动条 */
  display: flex;
  flex-direction: column;
  padding: 10px;
}

.message-list {
  flex: 1;
  display: flex;
  flex-direction: column-reverse;
  overflow: auto;
  width: auto;
  height: 100%;
  background-color: #f1daeb;
  border: 0px solid #ebeef5;
  border-radius: 5px;
  padding: 15px;
}


.message-bubble {
  border-radius: 18px;
  padding: 10px;
  max-width: 70%;
}
.my-message .message-bubble {
  background-color: rgba(46, 17, 65, 0.67); /* 绿色示例，您可以自定义颜色 */
  color: white;
}
.other-message .message-bubble {
  background-color: rgba(252, 251, 251, 0.75); /* 通常消息背景色 */
  color: #333;
}


.my-message {
  height: auto;
  display: flex;
  flex-direction: column;
  align-items: end;
  width: 100%;
  overflow: hidden;
}
/* 其他人的消息 */
.other-message {
  height: auto;
  display: flex;
  flex-direction: column;
  align-items: start;
  width: 100%;
  overflow: hidden;
}


:deep(.el-textarea__inner){
  background-color: rgba(152, 227, 238, 0.3);

}

</style>