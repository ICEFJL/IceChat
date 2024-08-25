//导入request.js请求工具
import request from '@/utils/request.js'
import {ref} from "vue";


export const flashFriendService = (userId)=>{
    //借助于UrlSearchParams完成传递
    let querystring="?userId="+userId;
    return request.get('/friend/getFriendsList'+querystring);
}
export const getAllFriendsService = ()=>{
    return request.get('/friend/getAllFriends');
}

export const addFriendService = (userId1,userId2)=>{
    return request.get('/friend/addFriend?userId1='+userId1+"&userId2="+userId2);
}

export const removeFriendService = (userId1,userId2)=>{
    return request.get('/friend/removeFriend?userId1='+userId1+"&userId2="+userId2);
}

export const searchFriendService = (msg)=>{
    return request.get('/user/searchUser?msg='+msg);
}

export const disagreeFriendService = (userId1, userId2)=>{
    return request.get('/friend/disagreeFriend?userId1=' + userId1 + "&userId2=" + userId2);
}

export const getFriendApplyingService = (userId)=>{
    return request.get("/friend/getFriendApplying?userId="+userId);
}

export const agreeFriendService = (userId1, userId2) => {
    return request.get('/friend/agreeFriend?userId1=' + userId1 + "&userId2=" + userId2);
}

export const searchAllFriendService = (msg) => {
    return request.get('/friend/searchAllFriend?msg=' + msg);
}