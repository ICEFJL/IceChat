import { createRouter, createWebHistory, createWebHashHistory } from 'vue-router'

//导入组件
import LoginVue from '@/views/Login.vue'

import MainVue from "@/views/Main.vue";
import LayoutVue from "@/views/Layout.vue";
import FriendsVue from "@/views/Friends.vue";
import GroupsVue from "@/views/Groups.vue"
import ChatVue from "@/views/Chat.vue"
import UserInfoVue from "@/views/UserInfo.vue"
import UserResetPasswordVue from "@/views/UserResetPassword.vue"
import ApplyVue from "@/views/Apply.vue"
import MainAdminVue from "@/views/Admin/MainAdmin.vue"
import UsersVue from "@/views/Admin/Users.vue"
import UserchatsVue from "@/views/Admin/Userchats.vue"
import GroupchatsVue from "@/views/Admin/Groupchats.vue"
import ChatAdminVue from "@/views/Admin/Chat.vue"


//定义路由关系

const routes = [
    { path: '/login', component: LoginVue },
    {
        path: '/', component: MainVue, redirect: '/login', children: [
            { path: '/layout', component: LayoutVue },
            { path: '/friends', component: FriendsVue },
            { path: '/groups', component: GroupsVue },
            { path: '/chat', component: ChatVue },
            { path: '/userinfo', component: UserInfoVue },
            { path: '/userResetPassword', component: UserResetPasswordVue },
            { path: '/apply', component: ApplyVue },
        ]
    },
    {
        path: '/admin', component: MainAdminVue, redirect: '/login', children: [
            { path: '/admin/layout', component: LayoutVue },
            { path: '/admin/userinfo', component: UserInfoVue },
            { path: '/admin/userResetPassword', component: UserResetPasswordVue },
            { path: '/admin/users', component: UsersVue },
            { path: '/admin/userchats', component: UserchatsVue },
            { path: '/admin/groupchats', component: GroupchatsVue },
            { path: '/admin/chat', component: ChatAdminVue }
        ]
    }
]

//创建路由器
const router = createRouter({
    history: createWebHashHistory(),
    routes: routes
})

//导出路由
export default router
