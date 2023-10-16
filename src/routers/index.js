import { createRouter, createWebHistory } from 'vue-router'
import Login from '../views/Login.vue';
import Register from '../views/Register.vue';
import Chat from '../views/Chat.vue';


const router = createRouter({
    history: createWebHistory(),
    routes: [
      {
        path: '/',
        name: 'home',
        component: () => Chat
      },
      {
        path: '/login',
        name: 'login',
        component: () => Login
      },
      {
        path: '/register',
        name: 'register',
        component: () => Register,
        params: (route) => ({ time: route.query.time })
      },
    ]
  })
  
  export default router
  