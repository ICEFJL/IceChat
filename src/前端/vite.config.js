import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import VueDevTools from 'vite-plugin-vue-devtools'
import electron from 'vite-plugin-electron'

// https://vitejs.dev/config/
export default defineConfig({

  plugins: [
    vue(),
    VueDevTools(),
    electron({
        // 主进程入口文件
        entry: 'background.js'
    })
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  }
  ,
  server:{
    port:5173,
    proxy:{
      '/api':{//获取路径中包含了/api的请求
        target:'http://localhost:5054',//后台服务所在的源

        // target:'https://9057838ddse9.vicp.fun',//后台服务所在的源
        changeOrigin:true,//修改源
        rewrite:(path)=>path.replace(/^\/api/,'')///api替换为''
      }
    },
    host:'0.0.0.0'
  },
  build:{
    chunkSizeWarningLimit:1500,
  }
})
