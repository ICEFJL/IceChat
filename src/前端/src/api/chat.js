//导入request.js请求工具
import request from '@/utils/request.js'


export const getPrivateMessageService = (userId,sendUserId)=>{
    return request.get('/chat/getPrivateMessage?userId1='+userId+"&userId2="+sendUserId);
}

export const getGroupsMessageService = (userId,groupId)=>{
    return request.get('/chat/getGroupsMessage?userId='+userId+"&groupId="+groupId);
}

export const sendPrivateMessageService = (userId,toUserId,msg)=>{
    return request.get('/chat/sendPrivateMessage?userId='+userId+"&toUserId="+toUserId+"&msg="+msg);
}

export const sendGroupsMessageService = (userId,toGroupId,msg)=>{
    return request.get('/chat/sendGroupsMessage?userId='+userId+"&toGroupId="+toGroupId+"&msg="+msg);
}


export const searchPrivateMessageService = (userId, toUserId, msg) => {
    return request.get('/chat/searchPrivateMessage?userId=' + userId + "&toUserId=" + toUserId + "&msg=" + msg);
}

export const searchGroupsMessageService = (groupId, msg) => {
    return request.get('/chat/searchGroupsMessage?groupId=' + groupId + "&msg=" + msg);
}

export const removePrivateMessageService = (messageId) => {
    return request.get('/chat/removePrivateMessage?messageId=' + messageId);
}

export const removeGroupsMessageService = (messageId) => {
    return request.get('/chat/removeGroupsMessage?messageId=' + messageId);
}

