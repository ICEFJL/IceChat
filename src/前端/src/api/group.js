//导入request.js请求工具
import request from '@/utils/request.js'
import {ref} from "vue";


export const reflashGroupService = (userId)=>{
    //借助于UrlSearchParams完成传递
    let querystring="?userId="+userId;
    return request.get('/groups/getGroupsList'+querystring);
}

export const addGroupService = (userId,groupId)=>{
    return request.get('/groups/addGroups?userId='+userId+"&groupId="+groupId);
}

export const deleteGroupService = (groupId,userId)=>{
    return request.get('/groups/deleteGroups?userId='+userId+"&groupId="+groupId);
}

export const createGroupService = (groupName,userId)=>{
    return request.get('/groups/createGroups?groupOwner='+userId+"&groupName="+groupName);
}

export const getGroupService = (msg)=>{
    let querystring="?msg="+msg;
    console.log(querystring)
    return request.get('/groups/searchGroupsByNameOrId'+querystring);
}

export const agreeAddGroupService = (userId, groupId, groupOwner)=>{
    return request.get('/groups/agreeAddGroups?userId='+userId+"&groupId="+groupId+"&groupOwner="+groupOwner);
}

export const disagreeAddGroupService = (userId, groupId, groupOwner)=>{
    return request.get('/groups/disagreeAddGroups?userId='+userId+"&groupId="+groupId+"&groupOwner="+groupOwner);
}

export const getGroupApplyingService = (userId)=>{
    return request.get("/groups/getGroupsApplyingList?userId=" + userId);
}

export const reflashAllGroupService = ()=>{
    return request.get("/groups/getAllGroups");
}