//导入request.js请求工具
import request from '@/utils/request.js'

//提供调用注册接口的函数
export const userRegisterService = (registerData)=>{
    console.log(registerData)
    return request.post('/user/registerByMail',registerData);
}

//提供调用登录接口的函数
export const userLoginService = (loginData)=>{
    console.log(loginData)
    return request.post('/user/login', loginData)
}

export const sendCodeService = (email)=>{
    console.log('/user/codeByMail?mail='+email)
    return request.get('/user/codeByMail?mail='+email)
}


//获取用户详细信息
export const userInfoService = (userId)=>{
    return request.get('/user/userInfo?userId='+userId)
}

//修改个人信息
export const userInfoUpdateService = (userInfoData)=>{
    console.log(userInfoData)
    return request.post('/user/update',userInfoData)
}

//修改头像
export const userAvatarUpdateService = (avatarUrl)=>{
    const params = new URLSearchParams();
    params.append('avatarUrl',avatarUrl)
    return request.patch('/user/updateAvatar',params)
}

export const resetPasswordService= (updatePasswordData)=>{
    return request.post('/user/ResetPassword',updatePasswordData)
}

export const getUserApplyingService = () => {
    return request.get('/user/getUserApplying')
}

export const agreeAddUserService = (userId) => {
    return request.get('/user/agreeAddUser?userId=' + userId)
}

export const disagreeAddUserService = (userId) => {
    return request.get('/user/disagreeAddUser?userId=' + userId)
}

export const getUserService = ()=>{
    return request.get('/user/getUser')
}

export const blockUserService = (userId) => {
    return request.get('/user/blockUser?userId=' + userId)
}

export const unblockUserService = (userId) => {
    return request.get('/user/unblockUser?userId=' + userId)
}
